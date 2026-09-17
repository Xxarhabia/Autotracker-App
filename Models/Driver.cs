namespace AutotrackerApp.Models
{
    public class Driver
    {
        public int Id { get; set; }
        public string Document {  get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? VehiclePlate { get; set; }
    }
}
