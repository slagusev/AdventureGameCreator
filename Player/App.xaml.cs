using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Player
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            if (e.Args.Count() > 0)
            {
                App.Current.Resources["FileName"] = e.Args[0];
            }
            else
            {
                App.Current.Resources["FileName"] = "";
            }
            //e.Args is the string[] of command line argruments
            base.OnStartup(e);
        }
    }
}
