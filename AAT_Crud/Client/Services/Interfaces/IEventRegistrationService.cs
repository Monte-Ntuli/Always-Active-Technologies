using SharedClasses.DTOs;

namespace Client.Services.Interfaces
{
    public interface IEventRegistrationService
    {
        Task CreateEventRegistration(CreateEventRegDTO createEventReg);
        Task<HttpResponseMessage> DeleteEventRegistration(Guid EventId);
        Task<IEnumerable<EventRegistrationDTO>> GetAllUserEventRegistrations(Guid UserId);
    }
}
