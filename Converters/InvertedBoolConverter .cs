using System.Globalization;

namespace AutotrackerApp.Converters
{
    // Invierte un bool: útil para casos como "deshabilitar el botón MIENTRAS
    // IsBusy es true" (IsEnabled necesita lo contrario de IsBusy).
    public class InvertedBoolConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
            => !(value is bool b && b);

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => !(value is bool b && b);
    }
}
