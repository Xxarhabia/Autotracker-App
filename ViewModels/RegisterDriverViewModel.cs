using AutotrackerApp.Models;
using AutotrackerApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AutotrackerApp.ViewModels
{
    public partial class RegisterDriverViewModel : ObservableObject
    {
        private readonly IDriverApiService _apiService;

        [ObservableProperty] private string name = string.Empty;
        [ObservableProperty] private string document = string.Empty;
        [ObservableProperty] private string phone = string.Empty;
        [ObservableProperty] private bool isBusy;

        public RegisterDriverViewModel(IDriverApiService apiService)
        {
            _apiService = apiService;
        }

        [RelayCommand]
        public async Task Guardar()
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Document))
            {
                await Shell.Current.DisplayAlert("Datos incompletos", "Nombre y documento son obligatorios", "OK");
                return;
            }

            try
            {
                IsBusy = true;

                var request = new CreateDriverRequest
                {
                    Name = name,
                    Document = document,
                    Phone = phone
                };

                var exito = await _apiService.RegisterAsync(request);

                if (!exito)
                {
                    await Shell.Current.DisplayAlert("Error", "No se pudo registrar el conductor (verifica que el documento no exista ya)", "OK");
                    return;
                }

                await Shell.Current.GoToAsync("..");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
