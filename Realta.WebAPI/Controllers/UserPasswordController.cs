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
    public class UserPasswordController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;

        public UserPasswordController(IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }

        // GET: api/<UserPasswordController>
        [HttpGet]
        public IActionResult Get()
        {
            var uspa = _repositoryManager.UserPasswordRepository.FindAllUserPassword().ToList();

            var uspaDto = uspa.Select(u => new UserPasswordDto
            {
                uspa_user_id = u.uspa_user_id,
                uspa_passwordHash = u.uspa_passwordHash,
                uspa_passwordSalt = u.uspa_passwordSalt,
            });

            return Ok(uspaDto);
         
        }

        // GET api/<UserPasswordController>/5
        [HttpGet("{id}", Name = "GetUspa")]
        public IActionResult FindUserPasswordById(int id)
        {
            var uspa = _repositoryManager.UserPasswordRepository.FindUserPasswordById(id);
            if (uspa == null) 
            {
                _logger.LogError("Uspa object sent from client is null");
                return BadRequest("Uspa object is null");
            }
            var uspaDto = new UserPasswordDto
            {
                uspa_user_id = uspa.uspa_user_id,
                uspa_passwordHash = uspa.uspa_passwordHash,
                uspa_passwordSalt = uspa.uspa_passwordSalt,
            };

            return Ok(uspaDto);
           
        }

        // POST api/<UserPasswordController>
        [HttpPost]
        public IActionResult CreateUspa([FromBody] UserPasswordDto uspaDto)
        {
            if (uspaDto == null)
            {
                _logger.LogError("UspaDto object sent from client is null");
                return BadRequest("UspaDto object is null");
            }

            var uspa = new UserPassword()
            {
                uspa_user_id = uspaDto.uspa_user_id,
                uspa_passwordHash = uspaDto.uspa_passwordHash,
                uspa_passwordSalt = uspaDto.uspa_passwordSalt
            };
           
            _repositoryManager.UserPasswordRepository.Insert(uspa);
            return CreatedAtRoute("GetUspa", new { id = uspaDto.uspa_user_id }, uspaDto);

        }

        // PUT api/<UserPasswordController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateUspa(int id, [FromBody] UserPasswordDto uspaDto)
        {
            if (uspaDto == null)
            {
                _logger.LogError("UspaDto object sent from client is null");
                return BadRequest("UspaDto object is null");
            }

            var uspa = new UserPassword()
            {
                uspa_user_id = id,
                uspa_passwordHash = uspaDto.uspa_passwordHash,
                uspa_passwordSalt = uspaDto.uspa_passwordSalt
            };
          
            _repositoryManager.UserPasswordRepository.Edit(uspa);
            return CreatedAtRoute("GetUspa", new { id = uspaDto.uspa_user_id }, new UserPasswordDto
            {
                uspa_user_id = id,
                uspa_passwordHash = uspa.uspa_passwordHash,
                uspa_passwordSalt = uspa.uspa_passwordSalt
            });

        }

        // DELETE api/<UserPasswordController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                _logger.LogError("Id object sent from client is null");
                return BadRequest("Id object is null");
            }

            //find id first
            var uspa = _repositoryManager.UserPasswordRepository.FindUserPasswordById(id.Value);
           
            if (uspa == null)
            {
                _logger.LogError($"User Password with id {id} not found");
                return NotFound();
            }

            _repositoryManager.UserPasswordRepository.Remove(uspa);
            return Ok("Data has been remove.");
        }
    }
}
