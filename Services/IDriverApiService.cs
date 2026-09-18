using AutotrackerApp.Models;

namespace AutotrackerApp.Services
{
    public interface IDriverApiService
    {
        Task<List<Driver>> GetAllAsync();
        Task<Driver?> GetByDocumentAsync(string document);
        Task<bool> RegisterAsync(CreateDriverRequest request);
        Task<bool> AssignVehicleAsync(string document, string plate);
        Task<bool> UnassignVehicleAsync(string document);
    }
}
