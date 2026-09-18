using AutotrackerApp.Models;

namespace AutotrackerApp.Services
{
    public interface IDriverSessionService
    {
        Driver? CurrentDriver { get; }
        Vehicle? CurrentVehicle { get; }
        bool IsLoggedIn { get; }

        void IniciarSesion(Driver driver, Vehicle vehicle);
        void CerrarSesion();
    }
}
