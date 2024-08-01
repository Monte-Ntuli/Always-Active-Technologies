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
    public class EventController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<EventController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public EventController(IUnitOfWork unitOfWork, ILogger<EventController> logger, IMapper mapper, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _config = config;
        }

        #region Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateEventDTO Event)
        {
            var create = await _unitOfWork.Events.AddAsync(_mapper.Map<EventsEntity>(Event));
            if (create != null) { return Accepted(create); } else return BadRequest(create);
        }
        #endregion

        #region Delete
        [HttpDelete("Delete/{EventId}")]
        public async Task<IActionResult> Delete(Guid EventId)
        {
            var Delete = await _unitOfWork.Events.DeleteAsync(EventId);
            if (Delete != null) { return Accepted(Delete); } else return BadRequest(Delete);
        }
        #endregion

        #region Get all
        [HttpGet("Get-All-Events")]
        public async Task<IActionResult> GetAll()
        {
            var events = await _unitOfWork.Events.GetAllEvents();
            if (events != null) { return Accepted(events); } else return BadRequest(events);
        }
        #endregion

        #region Get all by user id
        [HttpGet("Get-All-Events-By-UserId/{UserId}")]
        public async Task<IActionResult> GetAllEventsByUserId(string UserId)
        {
            var events = await _unitOfWork.Events.GetAllEventsByUser(UserId);
            if (events != null) { return Accepted(events); } else return BadRequest(events);
        }
        #endregion

        #region update
        [HttpGet("Update")]
        public async Task<IActionResult> UpdateEvents([FromBody] UpdateEventDTO updateEvent)
        {
            var events = await _unitOfWork.Events.UpdateEvent(_mapper.Map<EventsEntity>(updateEvent));
            if (events != null) { return Accepted(events); } else return BadRequest(events);
        }
        #endregion


    }
}
