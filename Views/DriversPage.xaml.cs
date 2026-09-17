using AutotrackerApp.ViewModels;

namespace AutotrackerApp.Views;

public partial class DriversPage : ContentPage
{
	private readonly DriverListViewModel _viewModel;

	public DriversPage(DriverListViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = viewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		_viewModel.CargarConductoresCommand.Execute(null);
    }

}