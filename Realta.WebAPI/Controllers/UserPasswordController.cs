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
                UspaUserId = u.UspaUserId,
                UspaPasswordHash = u.UspaPasswordHash,
                UspaPasswordSalt = u.UspaPasswordSalt,
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
                UspaUserId = uspa.UspaUserId,
                UspaPasswordHash = uspa.UspaPasswordHash,
                UspaPasswordSalt = uspa.UspaPasswordSalt,
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
                UspaUserId = uspaDto.UspaUserId,
                UspaPasswordHash = uspaDto.UspaPasswordHash,
                UspaPasswordSalt = uspaDto.UspaPasswordSalt
            };
           
            _repositoryManager.UserPasswordRepository.Insert(uspa);
            return CreatedAtRoute("GetUspa", new { id = uspaDto.UspaUserId }, uspaDto);

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
                UspaUserId = id,
                UspaPasswordHash = uspaDto.UspaPasswordHash,
                UspaPasswordSalt = uspaDto.UspaPasswordSalt
            };
          
            _repositoryManager.UserPasswordRepository.Edit(uspa);
            return CreatedAtRoute("GetUspa", new { id = uspaDto.UspaUserId }, new UserPasswordDto
            {
                UspaUserId = id,
                UspaPasswordHash = uspa.UspaPasswordHash,
                UspaPasswordSalt = uspa.UspaPasswordSalt
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
