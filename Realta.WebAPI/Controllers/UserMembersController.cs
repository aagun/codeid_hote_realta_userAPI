using Microsoft.AspNetCore.Mvc;
using Realta.Contract.Models;
using Realta.Domain.Base;
using Realta.Services.Abstraction;

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
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserMembersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserMembersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
