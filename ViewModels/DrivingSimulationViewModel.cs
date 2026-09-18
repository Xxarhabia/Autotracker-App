using AutotrackerApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AutotrackerApp.ViewModels
{
    public partial class DrivingSimulationViewModel : ObservableObject
    {
        private readonly IDriverApiService _driverApiService;
        private readonly IVehicleApiService _vehicleApiService;
        private readonly IDriverSessionService _session;

        [ObservableProperty] private string documentInput = string.Empty;
        [ObservableProperty] private string plateInput = string.Empty;

        [ObservableProperty] private bool isLoggedIn;
        [ObservableProperty] private string driverName = string.Empty;
        [ObservableProperty] private string vehiclePlate = string.Empty;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(EngineStatusText))]
        private bool engineOn;

        [ObservableProperty] private string lastLocationText = "Sin enviar";
        [ObservableProperty] private bool isBusy;

        public string EngineStatusText => EngineOn ? "Motor encendido" : "Motor apagado";

        public DrivingSimulationViewModel(
            IDriverApiService driverApiService,
            IVehicleApiService vehicleApiService,
            IDriverSessionService session)
        {
            _driverApiService = driverApiService;
            _vehicleApiService = vehicleApiService;
            _session = session;
        }

        [RelayCommand]
        private async Task Ingresar()
        {
            if (string.IsNullOrWhiteSpace(documentInput) || string.IsNullOrWhiteSpace(PlateInput))
            {
                await Shell.Current.DisplayAlert("Datos incompletos", "Ingresa tu documento y la placa asignada", "OK");
                return;
            }

            try
            {
                isBusy = true;

                var driver = await _driverApiService.GetByDocumentAsync(DocumentInput.Trim());

                if (driver is null)
                {
                    await Shell.Current.DisplayAlert("Error", "No existe un conductor con ese documento", "OK");
                    return;
                }

                if (string.IsNullOrWhiteSpace(driver.VehiclePlate) ||
                    !string.Equals(driver.VehiclePlate, PlateInput.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    await Shell.Current.DisplayAlert("Error", "La placa no coincide con el vehiculo asignado a este conductor", "OK");
                    return;
                }

                var vehicle = await _vehicleApiService.GetByPlateAsync(driver.VehiclePlate);

                if (vehicle is null)
                {
                    await Shell.Current.DisplayAlert("Error", "El vehiculo asinado ya no existe", "OK");
                    return;
                }

                _session.IniciarSesion(driver, vehicle);

                DriverName = driver.FullName;
                VehiclePlate = vehicle.Plate;
                EngineOn = vehicle.EngineOn;
                IsLoggedIn = true;

                DocumentInput = string.Empty;
                PlateInput = string.Empty;
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
        public Task Encender() => CambiarEstadoMotor(encender: true);

        [RelayCommand]
        public Task Apagar() => CambiarEstadoMotor(encender: false);

        private async Task CambiarEstadoMotor(bool encender)
        {
            if (isBusy) return;

            try
            {
                IsBusy = true;

                bool exito = encender
                    ? await _vehicleApiService.StarAsync(VehiclePlate)
                    : await _vehicleApiService.StopAsync(VehiclePlate);

                if (!exito)
                {
                    await Shell.Current.DisplayAlert("Error", "No se pudo cambiar el estado del vehiculo", "OK");
                    return;
                }

                EngineOn = encender;
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
        private async Task EnviarUbicaion()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;

                var permiso = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

                if (permiso != PermissionStatus.Granted)
                {
                    await Shell.Current.DisplayAlert("Permiso requerido", "Neceistas conceder el permiso de ubicacion", "OK");
                    return;
                }

                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                Location? gpsLocation = await Geolocation.Default.GetLocationAsync(request);

                if (gpsLocation is null)
                {
                    await Shell.Current.DisplayAlert("Error", "No se pudo obtener la ubicacion del dispositivo", "OK");
                    return;
                }

                var location = new Models.Location
                {
                    Latitude = gpsLocation.Latitude,
                    Longitude = gpsLocation.Longitude,
                    Timestamp = DateTime.Now
                };

                bool exito = await _vehicleApiService.UpdateLocationAsync(VehiclePlate, location);

                LastLocationText = exito
                    ? $"{location.Latitude:F5}, {location.Longitude:F5} ({location.Timestamp:HH:mm:ss})"
                    : "No se pudo enviar";

                if (!exito)
                    await Shell.Current.DisplayAlert("Error", "El vehiculo debe estar encendido para enviar la ubicacion", "OK");
            }
            catch (FeatureNotEnabledException)
            {
                await Shell.Current.DisplayAlert("Error", "El GPS esta desactivado en el dispositivo", "OK");
            }
            catch (PermissionException)
            {
                await Shell.Current.DisplayAlert("Error", "No se concedio el permiso de ubicacion", "OK");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"No se pudo obtener la ubicacion: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private void CerrarSesion()
        {
            _session.CerrarSesion();

            IsLoggedIn = false;
            DriverName = string.Empty;
            VehiclePlate = string.Empty;
            EngineOn = false;
            LastLocationText = "Sin enviar";
        }

    }
}
