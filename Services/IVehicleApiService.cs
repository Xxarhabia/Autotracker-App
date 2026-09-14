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
    }
}
