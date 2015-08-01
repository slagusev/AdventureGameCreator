using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Editor.Editors
{
    /// <summary>
    /// Interaction logic for ImageFinder.xaml
    /// </summary>
    public partial class ImageFinder : UserControl
    {
        public static readonly DependencyProperty PathProperty = DependencyProperty.Register("Path", typeof(string), typeof(ImageFinder), new PropertyMetadata("", PropertiesChanged));
        public ImageFinder()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.InitialDirectory = MainViewModel.MainViewModelStatic.Location;
            ofd.Filter = "Image files (*.jpeg, *.png, *.jpg, *.gif)|*.jpeg;*.png;*.jpg;*.gif";

            if (ofd.ShowDialog().Value)
            {
                var path = ofd.FileName;
                var relativePath = MainViewModel.GetRelativePath(MainViewModel.MainViewModelStatic.Location, path);
                SetValue(PathProperty, relativePath);
            }
        }
        public string Path { get { return (string)GetValue(PathProperty); } set { SetValue(PathProperty, value); } }
        //public string Path { get; set; }
        private void RefreshImagePreview()
        {
            if (Path != null && Path != "")
            {
                var path = MainViewModel.AbsolutePath(MainViewModel.MainViewModelStatic.Location, Path);
                if (File.Exists(path))
                {
                    imgImage.Source = new BitmapImage(new Uri(path));
                }
                else
                {
                    Path = "";
                }
            }
            textImageLink.Text = Path;
        }
        private static void PropertiesChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            (source as ImageFinder).RefreshImagePreview();
        }
    }
}
