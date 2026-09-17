using AutotrackerApp.ViewModels;

namespace AutotrackerApp.Views;

public partial class RegisterDriverPage : ContentPage
{
	public RegisterDriverPage(RegisterDriverViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}