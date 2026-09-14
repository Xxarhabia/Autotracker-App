using AutotrackerApp.ViewModels;

namespace AutotrackerApp.Views;

public partial class RegisterVehiclePage : ContentPage
{
	public RegisterVehiclePage(RegisterVehicleViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}