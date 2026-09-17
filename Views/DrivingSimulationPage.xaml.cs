using AutotrackerApp.ViewModels;

namespace AutotrackerApp.Views;

public partial class DrivingSimulationPage : ContentPage
{
	public DrivingSimulationPage(DrivingSimulationViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}