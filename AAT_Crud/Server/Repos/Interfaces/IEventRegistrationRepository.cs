using AAT_Crud.Entities;

namespace AAT_Crud.Repos.Interfaces
{
    public interface IEventRegistrationRepository
    {
        Task<EventRegistrationEntity> AddAsync(EventRegistrationEntity entity);
        Task<EventRegistrationEntity> DeleteAsync(Guid Id);
        Task<List<EventRegistrationEntity>> GetAllUserRegisteredEvents(Guid Id);
    }
}
