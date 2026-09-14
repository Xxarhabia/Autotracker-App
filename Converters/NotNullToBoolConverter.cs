using System.Globalization;

namespace AutotrackerApp.Converters
{
    // Traduce "el objeto no es null" a un bool, para IsVisible. Útil cuando un
    // dato opcional (como CurrentLocation) no debe mostrarse si aún no existe.
    public class NotNullToBoolConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
            => value is not null;

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
