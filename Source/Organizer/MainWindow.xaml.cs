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
            try
            {
                _vmodel = XmlWriter.Load<MainViewModel>("stickers.xml");
                _vmodel.Load();
            }
            catch (Exception)
            {
                _vmodel = new MainViewModel();
                //_vmodel.AddBoard.Execute(null);
            }

            this.DataContext = _vmodel;
        }

        private void MainWindow_OnClosed(object sender, EventArgs e)
        {
            _vmodel.Save();
            XmlWriter.Save(_vmodel, "stickers.xml");

        }
    }
}
