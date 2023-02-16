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
                ubpo_id = u.ubpo_id,
                ubpo_user_id = u.ubpo_user_id,
                ubpo_total_points = u.ubpo_total_points,
                ubpo_bonus_type = u.ubpo_bonus_type,
                ubpo_created_on = u.ubpo_created_on,
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
                ubpo_id = ubpo.ubpo_id,
                ubpo_user_id = ubpo.ubpo_user_id,
                ubpo_total_points = ubpo.ubpo_total_points,
                ubpo_bonus_type = ubpo.ubpo_bonus_type,
                ubpo_created_on = ubpo.ubpo_created_on,
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
                ubpo_user_id = ubpoDto.ubpo_user_id,
                ubpo_total_points = ubpoDto.ubpo_total_points,
                ubpo_bonus_type = ubpoDto.ubpo_bonus_type,
                ubpo_created_on = ubpoDto.ubpo_created_on
            };
           
            _repositoryManager.UserBonusPointsRepository.Insert(ubpo);

            var result = _repositoryManager.UserBonusPointsRepository.FindUserBonusPointsById(ubpo.ubpo_id);
            return CreatedAtRoute("GetUbpo", new { id = ubpo.ubpo_id }, result);

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
                ubpo_id = id,
                ubpo_user_id = ubpoDto.ubpo_user_id,
                ubpo_total_points = ubpoDto.ubpo_total_points,
                ubpo_bonus_type = ubpoDto.ubpo_bonus_type,
                ubpo_created_on = ubpoDto.ubpo_created_on
            };

            _repositoryManager.UserBonusPointsRepository.Edit(ubpo);
            return CreatedAtRoute("GetUbpo", new { id = ubpoDto.ubpo_id }, new UserBonusPointsDto
            {
                ubpo_id = id,
                ubpo_user_id = ubpo.ubpo_user_id,
                ubpo_total_points = ubpo.ubpo_total_points,
                ubpo_bonus_type = ubpo.ubpo_bonus_type,
                ubpo_created_on = ubpo.ubpo_created_on
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
