using AAT_Crud.Entities;
using AAT_Crud.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AAT_Crud.Repos
{
    public class EventRegistrationRepository : Repository<EventRegistrationEntity>, IEventRegistrationRepository
    {
        private EventsDBContext _dbContext => (EventsDBContext)_context;

        public EventRegistrationRepository(EventsDBContext context) : base(context)
        {

        }

        public async override Task<EventRegistrationEntity> AddAsync(EventRegistrationEntity entity)
        {
            
            var Event = await _dbContext.EventRegistration.FirstOrDefaultAsync(e => e.EventId == entity.EventId && e.UserId == entity.UserId);

            if (Event == null) 
            {
                var UserCount = await _dbContext.EventRegistration.Where(y => y.EventId == entity.EventId).ToListAsync();
                var SeatCount = await _dbContext.Events.FirstOrDefaultAsync(x => x.Id == entity.EventId);

                if(UserCount.Count < SeatCount.Seats)
                {
                    entity.Id = Guid.NewGuid();
                    entity.DateModified = DateTime.UtcNow;
                    await _dbContext.AddAsync(entity);
                    await _dbContext.SaveChangesAsync();
                    return entity;
                }

                return null;
            }

            return null;
        }

        public async Task<EventRegistrationEntity> DeleteAsync(Guid Id)
        {
            var EventReg = await _dbContext.EventRegistration.FirstOrDefaultAsync(x => x.Id == Id);

            if(EventReg == null)
            {
                return null;
            }

            _dbContext.EventRegistration.Remove(EventReg);
            await _dbContext.SaveChangesAsync();
            return EventReg;
        }

        public async Task<List<EventRegistrationEntity>> GetAllUserRegisteredEvents(Guid Id)
        {
            return await _dbContext.EventRegistration.Where(x => x.UserId == Id).ToListAsync();
        }
    }
}
