using AutotrackerApp.Models;
using AutotrackerApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace AutotrackerApp.ViewModels
{
    public partial class DriverListViewModel : ObservableObject
    {
        private readonly IDriverApiService _apiService;

        public ObservableCollection<Driver> Drivers { get; } = new();

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private string searchText = string.Empty;

        public DriverListViewModel(IDriverApiService apiService)
        {
            _apiService = apiService;
        }

        [RelayCommand]
        public async Task CargarConductores()
        {
            if (isBusy) return;

            try
            {
                IsBusy = true;
                var conductores = await _apiService.GetAllAsync();

                Drivers.Clear();
                foreach (var d in conductores)
                    Drivers.Add(d);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"No se pudo conectar con la API: {ex.Message}", "OK");
            }
            finally
            {
                isBusy = false;
            }
        }

        [RelayCommand]
        private async Task Buscar()
        {
            if (isBusy) return;

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                await CargarConductores();
                return;
            }

            try
            {
                IsBusy = true;
                var conductor = await _apiService.GetByDocumentAsync(SearchText.Trim());

                if (conductor is null)
                {
                    await Shell.Current.DisplayAlert("No encontrado", $"No existe un conductor con el documento {SearchText}", "OK");
                    return;
                }

                Drivers.Clear();
                Drivers.Add(conductor);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"No se pudo conectar con la API: {ex.Message}", "OK");
            }
            finally
            {
                isBusy = false;
            }
        }

        [RelayCommand]
        private async Task IrARegistrar()
        {
            await Shell.Current.GoToAsync(nameof(Views.RegisterVehiclePage));
        }

        [RelayCommand]
        private async Task AsignarVehiculo(Driver driver)
        {
            if (driver is null) return;

            string plate = await Shell.Current.DisplayPromptAsync(
                "Asignar vehiculo",
                $"Placa del vehiculo para {driver.FullName}",
                accept: "Asignar",
                cancel: "Cancelar",
                placeholder: "Ej. ABC123");

            if (string.IsNullOrWhiteSpace(plate)) return;

            try
            {
                //IsBusy = true;
                bool exito = await _apiService.AssignVehicleAsync(driver.Document, plate.Trim());

                if (!exito)
                {
                    await Shell.Current.DisplayAlert("Error", "No se pudo asignar el vehículo (verifica que la placa exista y no esté asignada a otro conductor)", "OK");
                    return;
                }

                await CargarConductores();
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task QuitarVehiculo(Driver driver)
        {
            if (driver is null) return;

            bool confirmar = await Shell.Current.DisplayAlert(
                "Quitar vehiculo",
                $"Retirar el vehiculo asignado a {driver.FullName}",
                "Si", "No");

            if (!confirmar) return;

            try
            {
                //IsBusy = true;
                bool exito = await _apiService.UnassignVehicleAsync(driver.Document);

                if (!exito)
                {
                    await Shell.Current.DisplayAlert("Error", "No se pudo quitar el vehículo", "OK");
                    return;
                }

                await CargarConductores();
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
