using AutotrackerApp.Services;
using AutotrackerApp.ViewModels;
using Microsoft.Extensions.Logging;

namespace AutotrackerApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		// La URL cambia segun donde corre la app: el emulador de android usa
		// 10.0.2.2 para referirse a la PC que lo hospeda, mientras que en windows
		// (o emulador de iOS) "localhost" funciona normal.
		string apiBaseUrl = DeviceInfo.Platform == DevicePlatform.Android
			? "http://10.0.2.2:5099/"
			: "http://localhost:5099/";

		var builder = MauiApp.CreateBuilder();

		builder.Services.AddHttpClient<IVehicleApiService, VehicleApiService>(client =>
		{
			client.BaseAddress = new Uri(apiBaseUrl);
		});

		builder.Services.AddTransient<VehicleListViewModel>();
		builder.Services.AddTransient<MainPage>();

		builder.Services.AddTransient<RegisterVehicleViewModel>();
		builder.Services.AddTransient<Views.RegisterVehiclePage>();

		builder.Services.AddTransient<VehicleDetailViewModel>();
		builder.Services.AddTransient<Views.VehicleDetailPage>();

		builder.Services.AddTransient<DriverListViewModel>();
		builder.Services.AddTransient<Views.DriversPage>();

		builder.Services.AddTransient<RegisterDriverViewModel>();
		builder.Services.AddTransient<Views.RegisterDriverPage>();

		builder.Services.AddTransient<DrivingSimulationViewModel>();
		builder.Services.AddTransient<Views.DrivingSimulationPage>();

		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
