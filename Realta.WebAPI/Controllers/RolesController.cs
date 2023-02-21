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
    public class RolesController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;

        public RolesController(IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }

        // GET: api/<RolesController>
        [HttpGet]
        public IActionResult Get()
        {
            var roles = _repositoryManager.RolesRepository.FindAllRoles().ToList();

            var rolesDto = roles.Select(u => new RolesDto
            {
                role_id = u.role_id,
                role_name = u.role_name,
            });

            return Ok(rolesDto);
          
        }

        // GET api/<RolesController>/5
        [HttpGet("{id}", Name = "GetRoles")]
        public IActionResult CreateRoles(int id)
        {
            var roles = _repositoryManager.RolesRepository.FindRolesById(id);

            if (roles == null)
            {
                _logger.LogError("Roles object sent from client is null");
                return BadRequest("Roles object is null");
            }
            var rolesDto = new RolesDto
            {
                role_id = roles.role_id,
                role_name = roles.role_name
            };

            return Ok(rolesDto);
        }

        // POST api/<RolesController>
        [HttpPost]
        public IActionResult CreateRoles([FromBody] RolesDto rolesDto)
        {
            if (rolesDto == null)
            {
                _logger.LogError("RolesDto object sent from client is null");
                return BadRequest("RolesDto object is null");
            }

            var roles = new Roles()
            {
                role_id = rolesDto.role_id,
                role_name = rolesDto.role_name
            };
           
            _repositoryManager.RolesRepository.Insert(roles);
            return CreatedAtRoute("GetRoles", new { id = rolesDto.role_id }, rolesDto);

        }

        // PUT api/<RolesController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateRoles(int id, [FromBody] RolesDto rolesDto)
        {
            if (rolesDto == null)
            {
                _logger.LogError("RolesDto object sent from client is null");
                return BadRequest("RolesDto object is null");
            }

            var roles = new Roles()
            {
                role_id = id,
                role_name = rolesDto.role_name
            };
           
            _repositoryManager.RolesRepository.Edit(roles);
            return CreatedAtRoute("GetRoles", new { id = rolesDto.role_id }, new RolesDto
            {
                role_id = id,
                role_name = roles.role_name
            });

        }

        // DELETE api/<RolesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                _logger.LogError("Id object sent from client is null");
                return BadRequest("Id object is null");
            }

            //find id first
            var roles = _repositoryManager.RolesRepository.FindRolesById(id.Value);

            if (roles == null)
            {
                _logger.LogError($"Roles with id {id} not found");
                return NotFound();
            }

            _repositoryManager.RolesRepository.Remove(roles);
            return Ok("Data has been remove.");
        }
    }
}
