using AAT_Crud.Entities;

namespace AAT_Crud.Repos.Interfaces
{
    public interface IEventsRepository
    {
        Task<EventsEntity> AddAsync(EventsEntity entity);
        Task<EventsEntity> DeleteAsync(Guid Id);
        Task<List<EventsEntity>> GetAllEvents();
        Task<List<EventsEntity>> GetAllEventsByUser(Guid Id);
        Task<EventsEntity> UpdateEvent(EventsEntity entity);
    }
}
