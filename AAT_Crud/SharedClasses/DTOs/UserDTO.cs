using Microsoft.AspNetCore.Identity;
namespace SharedClasses.DTOs
{
    public class UserDTO
    {
        public UserDTO()
        {
        }

        public UserDTO(string firstName, string lastName, string email, string userName, string phoneNum,  DateTime dateCreated)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            UserName = userName;
            PhoneNum = phoneNum;
            DateCreated = dateCreated;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNum { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
