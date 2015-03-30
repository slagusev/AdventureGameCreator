using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Player
{
    class DenormalizerConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            
            if (value != null)
            {
                double normalized;
                double convertTo;
                if (Double.TryParse(value.ToString(), out normalized) && Double.TryParse(parameter.ToString(), out convertTo))
                {
                    if (targetType == typeof(Thickness))
                    {
                        return new Thickness(normalized * 2 * convertTo, 0, 0, 0);
                    }
                    else if (targetType == typeof(Visibility))
                    {
                        return normalized > 0 ? Visibility.Visible : Visibility.Collapsed;
                    }
                    else
                    {
                        return normalized * convertTo;
                    }
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
