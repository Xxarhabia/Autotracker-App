using AutotrackerApp.Models;

namespace AutotrackerApp.Services
{

    // Contrato del servicio: el ViewModel depende de esta interfaz, no de la 
    // Implementacion concreta. Esto facilita testear o cambiar de backend despues
    public interface IVehicleApiService
    {
        Task<List<Vehicle>> GetAllAsync();
        Task<Vehicle?> GetByPlateAsync(string plate);
        Task<Vehicle?> RegisterAsync(CreateVehicleRequest request);
        Task<bool> StarAsync(string plate);
        Task<bool> StopAsync(string plate);
        Task<bool> LockAsync(string plate);
        Task<bool> UnlockAsync(string plate);
        Task<bool> UpdateLocationAsync(string plate, Models.Location location);
    }
}
