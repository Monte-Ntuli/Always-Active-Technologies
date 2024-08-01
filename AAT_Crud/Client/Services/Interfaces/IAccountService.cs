using SharedClasses.DTOs;

namespace Client.Services.Interfaces
{
    public interface IAccountService
    {
        Task Register(UserDTO appUser);
        Task ChangePassword(LoginDTO loginDTO);
        Task Login(LoginDTO loginDTO);
        Task FindEmail(string email);
        Task ForgotPassword(LoginDTO loginDTO);
        Task<UserDTO> GetUserByEmailTest(string email);

    }
}
