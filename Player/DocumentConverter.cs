using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;

namespace Player
{
    class DocumentConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var vals = value as ObservableCollection<object>;
            if (vals != null)
            {
                FlowDocument doc = new FlowDocument();
                foreach (var line in vals)
                {
                    if (line.GetType() == typeof(string))
                    {
                        string text = (string)line;
                        var p = new Paragraph(new Run(text));
                        
                        doc.Blocks.Add(p);
                    }
                }
                return doc;
            }
            else
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
