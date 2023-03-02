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
    public class UserBonusPointsController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;

        public UserBonusPointsController(IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }

        // GET: api/<UserBonusPointsController>
        [HttpGet]
        public IActionResult Get()
        {
            var ubpo = _repositoryManager.UserBonusPointsRepository.FindAllUserBonusPoints().ToList();

            var ubpoDto = ubpo.Select(u => new UserBonusPointsDto
            {
                UbpoId = u.UbpoId,
                UbpoUserId = u.UbpoUserId,
                UbpoTotalPoints = u.UbpoTotalPoints,
                UbpoBonusType = u.UbpoBonusType,
                UbpoCreatedOn = u.UbpoCreatedOn,
            });

            return Ok(ubpoDto);
        }

        // GET api/<UserBonusPointsController>/5
        [HttpGet("{id}", Name = "GetUbpo")]
        public IActionResult FindUserBonusPointsById(int id)
        {
            var ubpo = _repositoryManager.UserBonusPointsRepository.FindUserBonusPointsById(id);
            if (ubpo == null)
            {
                _logger.LogError("Ubpo object sent from client is null");
                return BadRequest("Ubpo object is null");
            }
            var ubpoDto = new UserBonusPointsDto
            {
                UbpoId = ubpo.UbpoId,
                UbpoUserId = ubpo.UbpoUserId,
                UbpoTotalPoints = ubpo.UbpoTotalPoints,
                UbpoBonusType = ubpo.UbpoBonusType,
                UbpoCreatedOn = ubpo.UbpoCreatedOn,
            };

            return Ok(ubpoDto);
          
        }

        // POST api/<UserBonusPointsController>
        [HttpPost]
        public IActionResult CreateUbpo([FromBody] UserBonusPointsDto ubpoDto)
        {
            if (ubpoDto == null)
            {
                _logger.LogError("UbpoDto object sent from client is null");
                return BadRequest("UbpoDto object is null");
            }

            var ubpo = new UserBonusPoints()
            {
                UbpoUserId = ubpoDto.UbpoUserId,
                UbpoTotalPoints = ubpoDto.UbpoTotalPoints,
                UbpoBonusType = ubpoDto.UbpoBonusType,
                UbpoCreatedOn = ubpoDto.UbpoCreatedOn
            };
           
            _repositoryManager.UserBonusPointsRepository.Insert(ubpo);

            var result = _repositoryManager.UserBonusPointsRepository.FindUserBonusPointsById(ubpo.UbpoId);
            return CreatedAtRoute("GetUbpo", new { id = ubpo.UbpoId }, result);

        }

        // PUT api/<UserBonusPointsController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateUbpo(int id, [FromBody] UserBonusPointsDto ubpoDto)
        {
            if (ubpoDto == null)
            {
                _logger.LogError("UbpoDto object sent from client is null");
                return BadRequest("UbpoDto object is null");
            }

            var ubpo = new UserBonusPoints()
            {
                UbpoId = id,
                UbpoUserId = ubpoDto.UbpoUserId,
                UbpoTotalPoints = ubpoDto.UbpoTotalPoints,
                UbpoBonusType = ubpoDto.UbpoBonusType,
                UbpoCreatedOn = ubpoDto.UbpoCreatedOn
            };

            _repositoryManager.UserBonusPointsRepository.Edit(ubpo);
            return CreatedAtRoute("GetUbpo", new { id = ubpoDto.UbpoId }, new UserBonusPointsDto
            {
                UbpoId = id,
                UbpoUserId = ubpo.UbpoUserId,
                UbpoTotalPoints = ubpo.UbpoTotalPoints,
                UbpoBonusType = ubpo.UbpoBonusType,
                UbpoCreatedOn = ubpo.UbpoCreatedOn
            });

        }

        // DELETE api/<UserBonusPointsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                _logger.LogError("Id object sent from client is null");
                return BadRequest("Id object is null");
            }

            //find id first
            var ubpo = _repositoryManager.UserBonusPointsRepository.FindUserBonusPointsById(id.Value);
            
            if (ubpo == null)
            {
                _logger.LogError($"Ubpo with id {id} not found");
                return NotFound();
            }

            _repositoryManager.UserBonusPointsRepository.Remove(ubpo);
            return Ok("Data has been remove.");
        }
    }
}
