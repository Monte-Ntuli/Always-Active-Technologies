namespace SharedClasses.DTOs
{
    public class CreateEventDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Seats { get; set; }
        public string CreatedBy { get; set; }
        public DateTime EventDate { get; set; }
    }
}
