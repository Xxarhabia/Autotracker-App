using AutotrackerApp.ViewModels;

namespace AutotrackerApp.Views;

public partial class VehicleDetailPage : ContentPage
{
	public VehicleDetailPage(VehicleDetailViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}