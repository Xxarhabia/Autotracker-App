namespace AutotrackerApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        // RegisterVehiclePage no aparece en el menú/tabs del Shell, así que
        // necesita registrarse como ruta para poder navegar hacia ella por string.
        Routing.RegisterRoute(nameof(Views.RegisterVehiclePage), typeof(Views.RegisterVehiclePage));
		Routing.RegisterRoute(nameof(Views.VehicleDetailPage), typeof(Views.VehicleDetailPage));
		Routing.RegisterRoute(nameof(Views.RegisterDriverPage), typeof(Views.RegisterDriverPage));
		Routing.RegisterRoute(nameof(Views.DriversPage), typeof(Views.DriversPage));
	}
}
