using AAT_Crud.Entities;
using AAT_Crud.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace AAT_Crud.Repos
{
    public class EventsRepository : Repository<EventsEntity>, IEventsRepository
    {
        private EventsDBContext _dbContext => (EventsDBContext)_context;

        public EventsRepository(EventsDBContext context) : base(context)
        {

        }

        public async override Task<EventsEntity> AddAsync(EventsEntity entity)
        {
            entity.Id = Guid.NewGuid();
            entity.DateModified = DateTime.UtcNow;
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<EventsEntity> DeleteAsync(Guid Id)
        {
            var Event = await _dbContext.Events.FirstOrDefaultAsync(x => x.Id == Id);

            if (Event == null)
            {
                return null;
            }

            var EventReg = await _dbContext.EventRegistration.Where(y => y.EventId == Id).ToListAsync();

            _dbContext.Events.Remove(Event);
            _dbContext.EventRegistration.RemoveRange(EventReg);
            await _dbContext.SaveChangesAsync();
            return Event;
        }

        public async Task<List<EventsEntity>> GetAllEvents()
        {
            return await _dbContext.Events.ToListAsync();
        }

        public async Task<List<EventsEntity>> GetAllEventsByUser(Guid Id)
        {
            var Events = await _dbContext.Events.Where(x => x.CreatedBy == Id).ToListAsync();

            if(Events == null)
            {
                return null;
            }

            return Events;
        }

        public async Task<EventsEntity> UpdateEvent(EventsEntity entity)
        {
            var Event = await _dbContext.Events.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if(Event == null)
            {
                return null;
            }

            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
            return Event;
        }

    }
}
