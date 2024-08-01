using Microsoft.AspNetCore.Components;
using MudBlazor;
using SharedClasses.DTOs;

namespace Client.Services.Interfaces
{
    public interface IEventsService
    {
        Task CreateEvent(CreateEventDTO createEvent);
        Task<IEnumerable<EventsDTO>> GetAllEvents();
        Task<IEnumerable<EventsDTO>> GetAllUserEvents(Guid UserId);
        Task<HttpResponseMessage> DeleteEvent(Guid EventId);
        Task Update(UpdateEventDTO updateEvent);
    }
}
