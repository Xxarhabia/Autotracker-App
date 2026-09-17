using AutotrackerApp.Models;
using System.Net.Http.Json;

namespace AutotrackerApp.Services
{
    public class DriverApiService : IDriverApiService
    {
        private readonly HttpClient _httpClient;

        public DriverApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Driver>> GetAllAsync()
        {
            var resposne = await _httpClient.GetAsync("api/drivers");

            if (!resposne.IsSuccessStatusCode)
                return new List<Driver>();

            var drivers = await resposne.Content.ReadFromJsonAsync<List<Driver>>();
            return drivers ?? new List<Driver>();
        }

        public async Task<Driver?> GetByDocumentAsync(string document)
        {
            var response = await _httpClient.GetAsync($"api/drivers/{document}");

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<Driver>();
        }

        public async Task<bool> RegisterAsync(CreateDriverRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/drivers", request);
            return response.IsSuccessStatusCode;
        }
    }
}
