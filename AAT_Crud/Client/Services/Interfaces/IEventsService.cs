using Microsoft.AspNetCore.Components;
using MudBlazor;
using SharedClasses.DTOs;

namespace Client.Services.Interfaces
{
    public interface IEventsService
    {
        Task CreateEvent(CreateEventDTO createEvent);
        Task<List<EventsDTO>> GetAllEvents();
        Task<List<EventsDTO>> GetAllUserEvents(string UserId);
        Task<EventsDTO> DeleteEvent(Guid EventId);
        Task Update(UpdateEventDTO updateEvent);
        Task<EventsDTO> GetEventById(Guid EventId);
    }
}
