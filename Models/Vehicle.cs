
namespace AutotrackerApp.Models
{
    public class Vehicle
    {
        public string Plate { set; get; } = string.Empty;
        public string Brand { set; get; } = string.Empty;
        public string Model { set; get; } = string.Empty;
        public string Year { set; get; } = string.Empty;
        public bool EngineOn { set; get; }
        public bool Locked { set; get; }
        public bool Inmovilized { set; get; }
        public Location? CurrentLocation { set; get; }
    }
}
