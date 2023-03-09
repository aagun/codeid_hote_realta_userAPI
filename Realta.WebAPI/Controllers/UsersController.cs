using Microsoft.AspNetCore.Mvc;
using Realta.Contract.AuthenticationWebAPI;
using Realta.Contract.Models;
using Realta.Domain.Base;
using Realta.Domain.Entities;
using Realta.Domain.RequestFeatures;
using Realta.Services.Abstraction;
using Realta.WebAPI.Authentication;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Realta.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;
        private readonly IAuthenticationManager _authenticationManager;

        public UsersController(IRepositoryManager repositoryManager, ILoggerManager logger, IAuthenticationManager authenticationManager)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
            _authenticationManager = authenticationManager;
        }

        //GET : api/signin
        [HttpPost("signin")]
        public async Task<IActionResult> Signin([FromBody] UserForAuthenticationDto userForAuthenticationDto)
        {
            if (!await _authenticationManager.ValidateUser(userForAuthenticationDto))
            {
                _logger.LogWarning($"{nameof(Authentication)} : Authentication Failed. Wrong email of password");
                return Unauthorized();
            }

            return Ok(new { Token = await _authenticationManager.CreateToken() });
        }

        // GET: api/<UsersController>
        [HttpGet]
        public IActionResult Get()
        {
            var users = _repositoryManager.UsersRepository.FindAllUsers().ToList();
            

            //use dto
            var usersDto = users.Select(u => new UsersDto
            {
                UserId = u.UserId,
                UserFullName = u.UserFullName,
                UserType = u.UserType,
                UserCompanyName = u.UserCompanyName,
                UserEmail = u.UserEmail,
                UserPhoneNumber = u.UserPhoneNumber,
                UserModifiedDate = u.UserModifiedDate,
             
            });

            return Ok(usersDto);
        }

        // Get Users Paging
        [HttpGet("paging")]
        public async Task<IActionResult> GetUsersPaging([FromQuery] UsersParameters usersParameters)
        {
            var users = await _repositoryManager.UsersRepository.GetUsersPaging(usersParameters);
            return Ok(users);
        }

        //GET api/User-Uspro
        [HttpGet("profile/{id}")]
        public IActionResult GetUsproById(int id) 
        {
            var userUspro = _repositoryManager.UsersRepository.GetUsersUspro(id);
            return Ok(userUspro);
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}", Name = "GetUsers")]
        public IActionResult FindUsersById(int id)
        {
            var users = _repositoryManager.UsersRepository.FindUsersById(id);
            if (users == null)
            {
                _logger.LogError("User object sent from client is null");
                return BadRequest("User object is null");
            }

            var usersDto = new UsersDto
            {
                UserId = users.UserId,
                UserFullName = users.UserFullName,
                UserType = users.UserType,
                UserCompanyName = users.UserCompanyName,
                UserEmail = users.UserEmail,
                UserPhoneNumber = users.UserPhoneNumber,
                UserModifiedDate = users.UserModifiedDate,

            };

            return Ok(usersDto);
        }

        // POST api/<UsersController>
        [HttpPost]
        public IActionResult CreateUser([FromBody] UsersDto usersDto)
        {
            if (usersDto == null)
            {
                _logger.LogError("UserDto object sent from client is null");
                return BadRequest("UserDto object is null");
            }

            var users = new Users()
            {
              
                UserFullName = usersDto.UserFullName,
                UserType = usersDto.UserType,
                UserCompanyName = usersDto.UserCompanyName,
                UserEmail = usersDto.UserEmail,
                UserPhoneNumber = usersDto.UserPhoneNumber,
                UserModifiedDate = usersDto.UserModifiedDate
               
            };

            _repositoryManager.UsersRepository.Insert(users);

            var result = _repositoryManager.UsersRepository.FindUsersById(users.UserId);

            return CreatedAtRoute("GetUsers", new { id = users.UserId }, result);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UsersDto usersDto)
        {
            //prevent regiondto from null
            if (usersDto == null)
            {
                _logger.LogError("UsersDto object sent from client is null");
                return BadRequest("UsersDto object is null");
            }

            var users = new Users()
            {
                UserId = id,
                UserFullName = usersDto.UserFullName,
                UserType = usersDto.UserType,
                UserCompanyName = usersDto.UserCompanyName,
                UserEmail = usersDto.UserEmail,
                UserPhoneNumber = usersDto.UserPhoneNumber,
                UserModifiedDate = usersDto.UserModifiedDate
            };

            _repositoryManager.UsersRepository.Edit(users);

            return CreatedAtRoute("GetUsers", new { id = usersDto.UserId }, new UsersDto 
            { 
                UserId = id,
                UserFullName = users.UserFullName,
                UserType = users.UserType,
                UserCompanyName = users.UserCompanyName,
                UserEmail = users.UserEmail,
                UserPhoneNumber = users.UserPhoneNumber,
                UserModifiedDate = users.UserModifiedDate
               
            });
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            //prevent regiondto from null
            if (id == null)
            {
                _logger.LogError("Id object sent from client is null");
                return BadRequest("Id object is null");
            }

            //find id first
            var users = _repositoryManager.UsersRepository.FindUsersById(id.Value);
            if (users == null)
            {
                _logger.LogError($"User with id {id} not found");
                return NotFound();
            }

            _repositoryManager.UsersRepository.Remove(users);
            return Ok("Data has been remove.");
        }
    }
}
