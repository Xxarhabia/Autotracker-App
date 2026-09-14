using AutotrackerApp.ViewModels;

namespace AutotrackerApp;

public partial class MainPage : ContentPage
{
	private readonly VehicleListViewModel _viewModel;

	public MainPage(VehicleListViewModel viewModel)
	{
        InitializeComponent();
        _viewModel = viewModel;
		BindingContext = viewModel;
	}

    // OnAppearing se ejecuta cada vez que la página se muestra (incluyendo al
    // volver de RegisterVehiclePage), así que es el lugar correcto para
    // refrescar la lista automáticamente sin acción del usuario.
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.CargarVehiculosCommand.Execute(null);
    }
}
