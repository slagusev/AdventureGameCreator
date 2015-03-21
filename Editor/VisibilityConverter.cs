using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Editor
{
    public class VisibilityConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && value.GetType() == typeof(bool))
            {
                if (parameter != null && parameter.ToString() == "InvertBoolean")
                {
                    return !(bool)value;
                }
                if (parameter != null && parameter.ToString() == "Invert")
                    return ((bool)value) ? Visibility.Collapsed : Visibility.Visible;
                return ((bool)value) ? Visibility.Visible : Visibility.Collapsed;
            }
            else if (value == null) return Visibility.Collapsed;
            else return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
