namespace AutotrackerApp.Models
{
    public class CreateVehicleRequest
    {
        public string Plate { set; get; } = string.Empty;
        public string Brand { set; get; } = string.Empty;
        public string Model { set; get; } = string.Empty;
        public string Year { set; get; } = string.Empty;
    }
}
