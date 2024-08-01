using AAT_Crud.Repos.Interfaces;

namespace AAT_Crud.Services.Interfaces
{
    public interface IUnitOfWork
    {
        IEventsRepository Events {  get; }
        IEventRegistrationRepository EventRegistration {  get; }
    }
}
