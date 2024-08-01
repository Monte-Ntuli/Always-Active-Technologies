using Microsoft.AspNetCore.Identity;

namespace SharedClasses.DTOs
{
    public class CreateEventRegDTO
    {
        public string UserId { get; set; }
        public Guid EventId { get; set; }
    }
}
