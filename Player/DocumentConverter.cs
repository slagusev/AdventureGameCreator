using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media.Imaging;

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
                        var p = new Paragraph();
                        p.Inlines.Add(new Run(text));
                        
                        doc.Blocks.Add(p);
                    }
                    else if (line.GetType() == typeof(ImageRef))
                    {
                        System.Windows.Controls.Image img = new System.Windows.Controls.Image();
                        var src = new BitmapImage(new Uri(((ImageRef)line).Path));
                        img.Source = src;
                        img.Width = src.Width;
                        img.Height = src.Height;
                        Paragraph p = new Paragraph();
                        p.Inlines.Add(img);
                        doc.Blocks.Add(p);
                    }
                    else if (line.GetType() == typeof(ObservableCollection<object>))
                    {
                        var p = new Paragraph();
                        foreach (var a in (ObservableCollection<object>)line)
                        {
                            if (a.GetType() == typeof(string))
                            {
                                
                                p.Inlines.Add(new Run(a.ToString()));
                            }
                            if (a.GetType() == typeof(ImageRef))
                            {
                                System.Windows.Controls.Image img = new System.Windows.Controls.Image();
                                var imgrf = (ImageRef)a;
                                var src = new BitmapImage(new Uri(imgrf.Path));
                                img.Source = src;
                                img.Width = imgrf.Width ?? src.Width;
                                img.Height = imgrf.Height ?? src.Height;
                                p.Inlines.Add(img);
                                
                            }
                        }
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
