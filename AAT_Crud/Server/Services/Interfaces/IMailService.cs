using SharedClasses.DTOs;

namespace AAT_Crud.Services.Interfaces
{
    public interface IMailService
    {
        Task SendResetEmailAsync(MailDTO mailRequestModel);
    }
}
