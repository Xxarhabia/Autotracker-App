using System.Globalization;

namespace AutotrackerApp.Converters
{
    // Traduce un bool (EngineOn) a un color visual. Los converters son la forma
    // correcta de transformar datos "para mostrar" sin ensuciar el ViewModel
    // con propiedades que solo existen por razones visuales.
    public class BoolToStatusColorConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            bool isOn = value is bool b && b;
            return isOn
                ? Application.Current!.Resources["StatusOn"]
                : Application.Current!.Resources["StatusOff"];
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}