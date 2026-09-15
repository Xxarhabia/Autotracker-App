using AutotrackerApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AutotrackerApp.ViewModels
{
    // [QueryProperty] recibe el vehículo que viajó como parámetro de navegación
    // desde VerDetalle en la lista. No hace falta llamar de nuevo a la API.
    [QueryProperty(nameof(Vehicle), "vehiculo")]
    public partial class VehicleDetailViewModel : ObservableObject
    {
        [ObservableProperty]
        private Vehicle vehicle = new();
    }
}
