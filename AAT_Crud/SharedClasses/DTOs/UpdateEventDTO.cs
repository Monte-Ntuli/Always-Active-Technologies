namespace SharedClasses.DTOs
{
    public class UpdateEventDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Seats { get; set; }
    }
}
