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
    public class UserRolesController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;

        public UserRolesController(IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }

        // GET: api/<UserRolesController>
        [HttpGet]
        public IActionResult Get()
        {
            var usro = _repositoryManager.UserRolesRepository.FindAllUserRoles().ToList();
            var usroDto = usro.Select(u => new UserRolesDto
            {
                usro_user_id = u.usro_user_id,
                usro_role_id = u.usro_role_id,
            });

            return Ok(usroDto);

        }

        // GET api/<UserRolesController>/5
        [HttpGet("{id}", Name = "GetUsro")]
        public IActionResult CreateUsro(int id)
        {

            var usro = _repositoryManager.UserRolesRepository.FindUserRolesById(id);

            if (usro == null)
            {
                _logger.LogError("Usro object sent from client is null");
                return BadRequest("Usro object is null");
            }
            var usroDto = new UserRolesDto
            {
                usro_user_id = usro.usro_user_id,
                usro_role_id = usro.usro_role_id,
            };

            return Ok(usroDto);
        }

        // POST api/<UserRolesController>
        [HttpPost]
        public IActionResult CreateUsro([FromBody] UserRolesDto usroDto)
        {
            if (usroDto == null)
            {
                _logger.LogError("UsroDto object sent from client is null");
                return BadRequest("UsroDto object is null");
            }

            var usro = new UserRoles()
            {
                usro_user_id = usroDto.usro_user_id,
                usro_role_id = usroDto.usro_role_id
            };
           
            _repositoryManager.UserRolesRepository.Insert(usro);
            return CreatedAtRoute("GetUsro", new { id = usroDto.usro_user_id }, usroDto);
     
        }

        // PUT api/<UserRolesController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateUsro(int id, [FromBody] UserRolesDto usroDto)
        {
            if (usroDto == null)
            {
                _logger.LogError("UsroDto object sent from client is null");
                return BadRequest("UsroDto object is null");
            }

            var usro = new UserRoles()
            {
                usro_user_id = id,
                usro_role_id = usroDto.usro_role_id
            };

            _repositoryManager.UserRolesRepository.Edit(usro);
            return CreatedAtRoute("GetUsro", new { id = usroDto.usro_user_id }, new UserRolesDto
            {
                usro_user_id = id,
                usro_role_id = usro.usro_role_id
            });
           
        }

        // DELETE api/<UserRolesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                _logger.LogError("Id object sent from client is null");
                return BadRequest("Id object is null");
            }

            //find id first
            var usro = _repositoryManager.UserRolesRepository.FindUserRolesById(id.Value);

            if (usro == null)
            {
                _logger.LogError($"User Password with id {id} not found");
                return NotFound();
            }

            _repositoryManager.UserRolesRepository.Remove(usro);
            return Ok("Data has been remove.");
        }
    }
}
