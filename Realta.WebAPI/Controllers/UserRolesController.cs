using Microsoft.AspNetCore.Mvc;
using Realta.Contract.Models;
using Realta.Domain.Base;
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
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserRolesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserRolesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserRolesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
