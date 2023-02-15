using Microsoft.AspNetCore.Mvc;
using Realta.Contract.Models;
using Realta.Domain.Base;
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
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserBonusPointsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserBonusPointsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
