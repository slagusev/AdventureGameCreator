﻿using Microsoft.Win32;
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
using System.Xml.Linq;

namespace Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "XML Files (*.xml)|*.xml";
            if (sfd.ShowDialog().Value)
            {
                //MainViewModel.MainViewModelStatic = mvm;
                var mvm = (MainViewModel)Editor.App.Current.Resources["MainViewModelStatic"] ;
                MainViewModel.MainViewModelStatic.Arrays = new System.Collections.ObjectModel.ObservableCollection<ObjectTypes.VarArray>();
                var xml = MainViewModel.MainViewModelStatic.ToXML();
                FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(xml.ToString());
                sw.Close();
                MainViewModel.MainViewModelStatic.Location = sfd.FileName;
                MainViewModel.MainViewModelStatic.ItemClasses.Add(Editor.ObjectTypes.ItemClass.GetBaseItemClass());
                
                //mvm.Zones = new System.Collections.ObjectModel.ObservableCollection<ObjectTypes.Zone>();
                new MainEditor().Show();
                this.Close();
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XML Files (*.xml)|*.xml";
            if (ofd.ShowDialog().Value)
            {
                FileStream fs = new FileStream(ofd.FileName, FileMode.Open);
                var sr = new StreamReader(fs);
                var xml = XElement.Parse(sr.ReadToEnd());
                sr.Close();
                
                var mvm = MainViewModel.FromXML(xml);
                mvm.Location = ofd.FileName;
                //mvm.Zones = new System.Collections.ObjectModel.ObservableCollection<ObjectTypes.Zone>();
                if (MainViewModel.MainViewModelStatic.ItemClasses.Count == 0)
                    mvm.ItemClasses.Add(Editor.ObjectTypes.ItemClass.GetBaseItemClass());
                Editor.App.Current.Resources["MainViewModelStatic"] = mvm;
                
                new MainEditor().Show();
                //MainViewModel.MainViewModelStatic.OpenWindows.Add(new WindowView()
                //{
                //    TabName = "Test Item",
                //    Content = new Editor.Editors.ItemEditor()
                //});
                this.Close();
            }
        }
    }
}
