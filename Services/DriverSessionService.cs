using AutotrackerApp.Models;

namespace AutotrackerApp.Services
{
    public class DriverSessionService : IDriverSessionService
    {
        public Driver? CurrentDriver { get; set; }
        public Vehicle? CurrentVehicle { get; set; }
        public bool IsLoggedIn => CurrentDriver != null;

        public void IniciarSesion(Driver driver, Vehicle vehicle)
        {
            CurrentDriver = driver;
            CurrentVehicle = vehicle;
        }

        public void CerrarSesion()
        {
            CurrentDriver = null;
            CurrentVehicle = null;
        }
    }
}
