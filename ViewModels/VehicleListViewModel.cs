using AutotrackerApp.Models;
using AutotrackerApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace AutotrackerApp.ViewModels
{
    // Maneja el estado de la pantalla principal: la lista de vechiculos y la
    // navegacion hacia el formulario de registro
    public partial class VehicleListViewModel : ObservableObject
    {
        private readonly IVehicleApiService _apiService;

        public ObservableCollection<Vehicle> Vehicles { get; } = new();

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private string searchText = string.Empty;

        public VehicleListViewModel(IVehicleApiService apiService)
        {
            _apiService = apiService;
        }

        // Carga (o recarga) la lista desde la API. Usamos IsBusy para mostrar
        // un indicador de carga y evitar que el usuario dispare la carga dos veces
        [RelayCommand]
        private async Task CargarVehiculos()
        {
            if (isBusy) return;

            try
            {
                IsBusy = true;
                var vehiculos = await _apiService.GetAllAsync();

                Vehicles.Clear();
                foreach (var v in vehiculos)
                    Vehicles.Add(v);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"No se pudo conectar con la API: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task Buscar()
        {
            if (isBusy) return;

            if (string.IsNullOrWhiteSpace(SearchText))
            {
                await CargarVehiculos();
                return;
            }

            try
            {
                IsBusy = true;
                var vehiculo = await _apiService.GetByPlateAsync(SearchText.Trim());

                if (vehiculo is null)
                {
                    await Shell.Current.DisplayAlert("No encontrado", $"No existe el vehiculo con la placa {SearchText}", "OK");
                    return;
                }

                Vehicles.Clear();
                Vehicles.Add(vehiculo);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"No se pudo conectar con la API: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        } 

        [RelayCommand]
        private async Task IrARegistrar()
        {
            await Shell.Current.GoToAsync(nameof(Views.RegisterVehiclePage));
        }

        [RelayCommand]
        private async Task VerDetalle(Vehicle vehicle)
        {
            if (vehicle is null) return;

            var parametros = new Dictionary<string, object> { { "vehiculo", vehicle } };
            await Shell.Current.GoToAsync(nameof(Views.VehicleDetailPage), parametros);
        }
    }
}
