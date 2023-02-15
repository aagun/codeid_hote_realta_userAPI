using Microsoft.AspNetCore.Mvc;
using Realta.Contract.Models;
using Realta.Domain.Base;
using Realta.Domain.Entities;
using Realta.Services.Abstraction;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Realta.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;

        public UsersController(IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public IActionResult Get()
        {
            var users = _repositoryManager.UsersRepository.FindAllUsers().ToList();
            

            //use dto
            var usersDto = users.Select(u => new UsersDto
            {
                user_id = u.user_id,
                user_full_name = u.user_full_name,
                user_type = u.user_type,
                user_company_name = u.user_company_name,
                user_email = u.user_email,
                user_phone_number = u.user_phone_number,
                user_modified_date = u.user_modified_date,
            });

            return Ok(usersDto);
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}", Name = "GetUsers")]
        public IActionResult FindUsersById(int id)
        {
            var users = _repositoryManager.UsersRepository.FindUsersById(id);
            if(users == null)
            {
                _logger.LogError("User object sent from client is null");
                return BadRequest("User object is null");
            }

            var usersDto = new UsersDto
            {
                user_id = users.user_id,
                user_full_name = users.user_full_name,
                user_type = users.user_type,
                user_company_name = users.user_company_name,
                user_email = users.user_email,
                user_phone_number = users.user_phone_number,
                user_modified_date = users.user_modified_date
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
              
                user_full_name = usersDto.user_full_name,
                user_type = usersDto.user_type,
                user_company_name = usersDto.user_company_name,
                user_email = usersDto.user_email,
                user_phone_number = usersDto.user_phone_number,
                user_modified_date = usersDto.user_modified_date
            };

            _repositoryManager.UsersRepository.Insert(users);

            var result = _repositoryManager.UsersRepository.FindUsersById(users.user_id);

            return CreatedAtRoute("GetUsers", new { id = users.user_id }, result);
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
                user_id = id,
                user_full_name = usersDto.user_full_name,
                user_type = usersDto.user_type,
                user_company_name = usersDto.user_company_name,
                user_email = usersDto.user_email,
                user_phone_number = usersDto.user_phone_number,

            };

            _repositoryManager.UsersRepository.Edit(users);

            return CreatedAtRoute("GetUsers", new { id = usersDto.user_id }, new UsersDto { user_id = id, user_full_name = users.user_full_name, user_type = users.user_type, user_company_name = users.user_company_name, user_email = users.user_email, user_phone_number = users.user_phone_number, user_modified_date = users.user_modified_date });
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
