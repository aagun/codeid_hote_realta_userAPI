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
                UsmeUserId = u.UsmeUserId,
                UsmeMembName = u.UsmeMembName,
                UsmePromoteDate = u.UsmePromoteDate,
                UsmePoints = u.UsmePoints,
                UsmeType = u.UsmeType,
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
                UsmeUserId = usme.UsmeUserId,
                UsmeMembName = usme.UsmeMembName,
                UsmePromoteDate = usme.UsmePromoteDate,
                UsmePoints = usme.UsmePoints,
                UsmeType = usme.UsmeType,
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
                UsmeUserId = usmeDto.UsmeUserId,
                UsmeMembName = usmeDto.UsmeMembName,
                UsmePromoteDate = usmeDto.UsmePromoteDate,
                UsmePoints = usmeDto.UsmePoints,
                UsmeType = usmeDto.UsmeType
            };

            _repositoryManager.UserMembersRepository.Insert(usme);

            return CreatedAtRoute("GetUsme", new { id = usmeDto.UsmeUserId }, usmeDto);

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
                UsmeUserId = id,
                UsmeMembName = usmeDto.UsmeMembName,
                UsmePromoteDate = usmeDto.UsmePromoteDate,
                UsmePoints = usmeDto.UsmePoints,
                UsmeType = usmeDto.UsmeType
            };
           
            _repositoryManager.UserMembersRepository.Edit(usme);

            return CreatedAtRoute("GetUsme", new { id = usmeDto.UsmeUserId }, new UserMembersDto
            {
                UsmeUserId = id,
                UsmeMembName = usme.UsmeMembName,
                UsmePromoteDate = usme.UsmePromoteDate,
                UsmePoints = usme.UsmePoints,
                UsmeType = usme.UsmeType
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
