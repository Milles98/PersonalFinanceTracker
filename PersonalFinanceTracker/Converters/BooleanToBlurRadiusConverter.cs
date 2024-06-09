using System.Globalization;
using System.Windows.Data;

namespace PersonalFinanceTracker.Converters;

public class BooleanToBlurRadiusConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (bool)value ? 5 : 0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}