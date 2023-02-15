using Microsoft.AspNetCore.Mvc;
using Realta.Contract.Models;
using Realta.Domain.Base;
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
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserPasswordController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserPasswordController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
