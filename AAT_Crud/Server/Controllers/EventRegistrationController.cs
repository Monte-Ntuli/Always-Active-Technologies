using SharedClasses.DTOs;
using AAT_Crud.Entities;
using AAT_Crud.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AAT_Crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventRegistrationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<EventRegistrationController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public EventRegistrationController(IUnitOfWork unitOfWork, ILogger<EventRegistrationController> logger, IMapper mapper, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _config = config;
        }

        #region Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateEventRegDTO EventReg)
        {
            var create = await _unitOfWork.EventRegistration.AddAsync(_mapper.Map<EventRegistrationEntity>(EventReg));
            if (create != null) { return Accepted(create); } else return BadRequest(create);
        }
        #endregion

        #region Delete
        [HttpDelete("Delete/{RegistrationId}")]
        public async Task<IActionResult> Create(Guid RegistrationId)
        {
            var Delete = await _unitOfWork.EventRegistration.DeleteAsync(RegistrationId);
            if (Delete != null) { return Accepted(Delete); } else return BadRequest(Delete);
        }
        #endregion

        #region Get Registered Events
        [HttpGet("Get-Registered/{UserId}")]
        public async Task<IActionResult> GetRegistered(string UserId)
        {
            var GetRegistered = await _unitOfWork.EventRegistration.GetAllUserRegisteredEvents(UserId);
            if (GetRegistered != null) { return Accepted(GetRegistered); } else return BadRequest(GetRegistered);
        }
        #endregion
    }
}
