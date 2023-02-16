using Microsoft.AspNetCore.Mvc;
using Realta.Contract.Models;
using Realta.Domain.Base;
using Realta.Domain.Entities;
using Realta.Persistence.RepositoryContext;
using Realta.Services.Abstraction;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Realta.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMembersController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;

        public UserMembersController(IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }

        // GET: api/<UserMembersController>
        [HttpGet]
        public IActionResult Get()
        {
            var usme = _repositoryManager.UserMembersRepository.FindAllUserMembers().ToList();

            //use dto
            var usmeDto = usme.Select(u => new UserMembersDto
            {
                usme_user_id = u.usme_user_id,
                usme_memb_name = u.usme_memb_name,
                usme_promote_date = u.usme_promote_date,
                usme_points = u.usme_points,
                usme_type = u.usme_type,
            });

            return Ok(usmeDto);
        }

        // GET api/<UserMembersController>/5
        [HttpGet("{id}", Name = "GetUsme")]
        public IActionResult FindUserMembersById(int id)
        {
            var usme = _repositoryManager.UserMembersRepository.FindUserMembersById(id);
            if (usme == null)
            {
                _logger.LogError("Usme object sent from client is null");
                return BadRequest("Usme object is null");
            }
            var usmeDto = new UserMembersDto
            {
                usme_user_id = usme.usme_user_id,
                usme_memb_name = usme.usme_memb_name,
                usme_promote_date = usme.usme_promote_date,
                usme_points = usme.usme_points,
                usme_type = usme.usme_type,
            };

            return Ok(usmeDto);
        }

        // POST api/<UserMembersController>
        [HttpPost]
        public IActionResult CreateUsme([FromBody] UserMembersDto usmeDto)
        {
            if (usmeDto == null)
            {
                _logger.LogError("UsmeDto object sent from client is null");
                return BadRequest("UsmeDto object is null");
            }

            var usme = new UserMembers()
            {
                usme_user_id = usmeDto.usme_user_id,
                usme_memb_name = usmeDto.usme_memb_name,
                usme_promote_date = usmeDto.usme_promote_date,
                usme_points = usmeDto.usme_points,
                usme_type = usmeDto.usme_type
            };

            _repositoryManager.UserMembersRepository.Insert(usme);

            return CreatedAtRoute("GetUsme", new { id = usmeDto.usme_user_id }, usmeDto);

        }

        // PUT api/<UserMembersController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateUsme(int id, [FromBody] UserMembersDto usmeDto)
        {
            if (usmeDto == null)
            {
                _logger.LogError("UsmeDto object sent from client is null");
                return BadRequest("UsmeDto object is null");
            }

            var usme = new UserMembers()
            {
                usme_user_id = id,
                usme_memb_name = usmeDto.usme_memb_name,
                usme_promote_date = usmeDto.usme_promote_date,
                usme_points = usmeDto.usme_points,
                usme_type = usmeDto.usme_type
            };
           
            _repositoryManager.UserMembersRepository.Edit(usme);

            return CreatedAtRoute("GetUsme", new { id = usmeDto.usme_user_id }, new UserMembersDto
            {
                usme_user_id = id,
                usme_memb_name = usme.usme_memb_name,
                usme_promote_date = usme.usme_promote_date,
                usme_points = usme.usme_points,
                usme_type = usme.usme_type
            });
          
        }

        // DELETE api/<UserMembersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                _logger.LogError("Id object sent from client is null");
                return BadRequest("Id object is null");
            }

            //find id first
            var usme = _repositoryManager.UserMembersRepository.FindUserMembersById(id.Value);
            
            if (usme == null)
            {
                _logger.LogError($"User Members with id {id} not found");
                return NotFound();
            }

            _repositoryManager.UserMembersRepository.Remove(usme);
            return Ok("Data has been remove.");
        }
    }
}
