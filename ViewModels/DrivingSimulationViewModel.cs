using CommunityToolkit.Mvvm.ComponentModel;

namespace AutotrackerApp.ViewModels
{
    public partial class DrivingSimulationViewModel : ObservableObject
    {
        [ObservableProperty]
        private string mensaje = "Aqui ira el login del conductor y la simulacion de conduccion";
    }
}
