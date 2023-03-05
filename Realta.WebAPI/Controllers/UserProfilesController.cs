using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Realta.Contract.Models;
using Realta.Domain.Base;
using Realta.Domain.Entities;
using Realta.Domain.RequestFeatures;
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
                UsproId = u.UsproId,
                UsproNationalId = u.UsproNationalId,
                UsproBirthDate = u.UsproBirthDate,
                UsproJobTitle = u.UsproJobTitle,
                UsproMaritalStatus = u.UsproMaritalStatus,
                UsproGender = u.UsproGender,
                UsproAddrId = u.UsproAddrId,
                UsproUserId = u.UsproUserId,
            });

            return Ok(usproDto);
        }

        // Get Uspro Pagelist
        [HttpGet("pageList")]
        public async Task<IActionResult> GetUsersPageList([FromQuery] UsproParameters usproParameters)
        {
            var uspro = await _repositoryManager.UserProfilesRepository.GetUserProfilesPageList(usproParameters);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(uspro.MetaData));

            return Ok(uspro);
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
                UsproId = uspro.UsproId,
                UsproNationalId = uspro.UsproNationalId,
                UsproBirthDate = uspro.UsproBirthDate,
                UsproJobTitle = uspro.UsproJobTitle,
                UsproMaritalStatus = uspro.UsproMaritalStatus,
                UsproGender = uspro.UsproGender,
                UsproUserId = uspro.UsproUserId,
                UsproAddrId = uspro.UsproAddrId
            };

            return Ok(usproDto);
        }

        // POST api/<UserProfilesController>
        [HttpPost]
        public IActionResult CreateUspro([FromBody] UserProfilesDto usproDto)
        {
            if (usproDto == null)
            {
                _logger.LogError("UsproDto object sent from client is null");
                return BadRequest("UsproDto object is null");
            }

            var uspro = new UserProfiles()
            {
                UsproNationalId = usproDto.UsproNationalId,
                UsproBirthDate = usproDto.UsproBirthDate,
                UsproJobTitle = usproDto.UsproJobTitle,
                UsproMaritalStatus = usproDto.UsproMaritalStatus,
                UsproGender = usproDto.UsproGender,
                UsproAddrId = usproDto.UsproAddrId,
                UsproUserId = usproDto.UsproUserId
            };
           
            _repositoryManager.UserProfilesRepository.Insert(uspro);

            var result = _repositoryManager.UserProfilesRepository.FindUserProfilesById(uspro.UsproId);

            return CreatedAtRoute("GetUspro", new { id = uspro.UsproId }, result);
        }

        // PUT api/<UserProfilesController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateUspro(int id, [FromBody] UserProfilesDto usproDto)
        {
            
            if (usproDto == null)
            {
                _logger.LogError("UsproDto object sent from client is null");
                return BadRequest("UsproDto object is null");
            }

            var uspro = new UserProfiles()
            {
                UsproId = id,
                UsproNationalId = usproDto.UsproNationalId,
                UsproBirthDate = usproDto.UsproBirthDate,
                UsproJobTitle = usproDto.UsproJobTitle,
                UsproMaritalStatus = usproDto.UsproMaritalStatus,
                UsproGender = usproDto.UsproGender,
                UsproAddrId = usproDto.UsproAddrId,
                UsproUserId = usproDto.UsproUserId
            };
         
            _repositoryManager.UserProfilesRepository.Edit(uspro);

            return CreatedAtRoute("GetUspro", new { id = usproDto.UsproId }, new UserProfilesDto
            {
                UsproId = id,
                UsproNationalId = uspro.UsproNationalId,
                UsproBirthDate = uspro.UsproBirthDate,
                UsproJobTitle = uspro.UsproJobTitle,
                UsproMaritalStatus = uspro.UsproMaritalStatus,
                UsproGender = uspro.UsproGender,
                UsproAddrId = uspro.UsproAddrId,
                UsproUserId = uspro.UsproUserId
            });

        }

        // DELETE api/<UserProfilesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            //prevent regiondto from null
            if (id == null)
            {
                _logger.LogError("Id object sent from client is null");
                return BadRequest("Id object is null");
            }

            //find id first
            var uspro = _repositoryManager.UserProfilesRepository.FindUserProfilesById(id.Value);
            if (uspro == null)
            {
                _logger.LogError($"User profiles with id {id} not found");
                return NotFound();
            }

            _repositoryManager.UserProfilesRepository.Remove(uspro);
            return Ok("Data has been remove.");
        }
    }
}
