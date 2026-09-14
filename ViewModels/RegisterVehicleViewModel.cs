
using AutotrackerApp.Models;
using AutotrackerApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AutotrackerApp.ViewModels
{
    public partial class RegisterVehicleViewModel : ObservableObject
    {
        private readonly IVehicleApiService _apiService;

        [ObservableProperty] private string plate = string.Empty;
        [ObservableProperty] private string brand = string.Empty;
        [ObservableProperty] private string model = string.Empty;
        [ObservableProperty] private string year = string.Empty;
        [ObservableProperty] private bool isBusy;

        public RegisterVehicleViewModel(IVehicleApiService apiService)
        {
            _apiService = apiService;
        }

        [RelayCommand]
        public async Task Guardar()
        {
            if (string.IsNullOrEmpty(plate) || string.IsNullOrWhiteSpace(Brand))
            {
                await Shell.Current.DisplayAlert("Datos incompletos", "Placa y marca son obligatorias", "OK");
                return;
            }

            try
            {
                IsBusy = true;

                var request = new CreateVehicleRequest
                {
                    Plate = Plate,
                    Brand = Brand,
                    Model = Model,
                    Year = Year
                };

                var resultado = await _apiService.RegisterAsync(request);

                if (resultado is null)
                {
                    await Shell.Current.DisplayAlert("Error", "No se pudo registrar el vehiculo", "OK");
                    return;
                }

                // Regresa a la lista tras registrar exitosamente
                await Shell.Current.GoToAsync("..");
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
