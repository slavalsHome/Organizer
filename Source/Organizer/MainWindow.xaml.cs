using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Common;
using Organizer.ViewModel;

namespace Organizer
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

        private MainViewModel _vmodel;

        private void OnFormLoaded(object sender, RoutedEventArgs e)
        {
            _vmodel = new MainViewModel();
            try
            {
                var plugin = XmlWriter.Load<StickerPlugin.ViewModel.MainViewModel>("stickers.xml");
                _vmodel.Plugins.Add(plugin);
            }
            catch (Exception)
            {
                _vmodel = new MainViewModel();
                var plugin = new StickerPlugin.ViewModel.MainViewModel();
                _vmodel.Plugins.Add(plugin);
            }

            this.DataContext = _vmodel.Plugins[0];
        }

        private void MainWindow_OnClosed(object sender, EventArgs e)
        {
            XmlWriter.Save(_vmodel.Plugins[0], "stickers.xml");
        }
    }
}
