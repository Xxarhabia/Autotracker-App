using AutotrackerApp.Models;
using System.Net.Http.Json;

namespace AutotrackerApp.Services
{
    // Implementacion concreta: encapsula todas las llamadas Http al API de Autotracker
    // Usa HttpClient inyectado (registrado en MauiProgram) en vez de crear uno manualmente
    // lo que evita problemas de agotamiento de sockets si la app crece
    internal class VehicleApiService : IVehicleApiService
    {
        private readonly HttpClient _httpClient;

        public VehicleApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Vehicle>> GetAllAsync()
        {
            var vehicles = await _httpClient.GetFromJsonAsync<List<Vehicle>>("api/vehicles");
            return vehicles ?? new List<Vehicle>();
        }

        public async Task<Vehicle?> GetByPlateAsync(string plate)
        {
            var response = await _httpClient.GetAsync($"api/vehicles/{plate}");

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<Vehicle>();
        }

        public async Task<Vehicle?> RegisterAsync(CreateVehicleRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/vehicles", request);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<Vehicle>();
        }
    }
}
