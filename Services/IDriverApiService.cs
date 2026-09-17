using AutotrackerApp.Models;

namespace AutotrackerApp.Services
{
    public interface IDriverApiService
    {
        Task<List<Driver>> GetAllAsync();
        Task<Driver?> GetByDocumentAsync(string document);
        Task<bool> RegisterAsync(CreateDriverRequest request);
    }
}
