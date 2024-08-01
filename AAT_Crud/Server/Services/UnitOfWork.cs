using AAT_Crud.Repos;
using AAT_Crud.Repos.Interfaces;
using AAT_Crud.Services.Interfaces;

namespace AAT_Crud.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly EventsDBContext _context;

        private readonly IConfiguration _config;

        public UnitOfWork(EventsDBContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public IEventRegistrationRepository _eventRegistration;

        public IEventRegistrationRepository EventRegistration
        {
            get
            {
                if (_eventRegistration == null)
                    _eventRegistration = new EventRegistrationRepository(_context);

                return _eventRegistration;
            }
        }

        public IEventsRepository _events;

        public IEventsRepository Events
        {
            get
            {
                if (_events == null)
                    _events = new EventsRepository(_context);

                return _events;
            }
        }
    }
}
