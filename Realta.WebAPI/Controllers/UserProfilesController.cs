using Microsoft.AspNetCore.Mvc;
using Realta.Contract.Models;
using Realta.Domain.Base;
using Realta.Services.Abstraction;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Realta.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfilesController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;

        public UserProfilesController(IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }

        // GET: api/<UserProfilesController>
        [HttpGet]
        public IActionResult Get()
        {
            var uspro = _repositoryManager.UserProfilesRepository.FindAllUserProfiles().ToList();

            //use dto
            var usproDto = uspro.Select(u => new UserProfilesDto
            {
                uspro_id = u.uspro_id,
                uspro_national_id = u.uspro_national_id,
                uspro_birth_date = u.uspro_birth_date,
                uspro_job_title = u.uspro_job_title,
                uspro_marital_status = u.uspro_marital_status,
                uspro_gender = u.uspro_gender,
                uspro_addr_id = u.uspro_addr_id,
                uspro_user_id = u.uspro_user_id,
            });

            return Ok(usproDto);
        }

        // GET api/<UserProfilesController>/5
        [HttpGet("{id}", Name = "GetUspro")]
        public IActionResult FindUserProfilesById(int id)
        {
            var uspro = _repositoryManager.UserProfilesRepository.FindUserProfilesById(id);
            if (uspro == null)
            {
                _logger.LogError("User object sent from client is null");
                return BadRequest("Uspro object is null");
            }

            var usproDto = new UserProfilesDto
            {
                uspro_id = uspro.uspro_id,
                uspro_national_id = uspro.uspro_national_id,
                uspro_birth_date = uspro.uspro_birth_date,
                uspro_job_title = uspro.uspro_job_title,
                uspro_marital_status = uspro.uspro_marital_status,
                uspro_gender = uspro.uspro_gender,
                uspro_user_id = uspro.uspro_user_id,
                uspro_addr_id = uspro.uspro_addr_id
            };

            return Ok(usproDto);
        }

        // POST api/<UserProfilesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserProfilesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserProfilesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
