using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;
using Common;
using Common.MvvmBase.Dialogs;
using Organizer.View;
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
            ServiceLocator.RegisterSingleton<IDialogService, DialogService>();
            ServiceLocator.RegisterSingleton<IWindowViewModelMappings, WindowViewModelMappings>();

            InitializeComponent();

            try
            {
                _config = XmlWriter.Load<Config>("config.xml");

                this.Height = _config.WindowSettings.Heigth;
                this.Width = _config.WindowSettings.Width;
                this.Left = _config.WindowSettings.Left;
                this.Top = _config.WindowSettings.Top;
                this.WindowState = _config.WindowSettings.WindowState;

                leftSide.Width = new GridLength(_config.LeftSideWidth, GridUnitType.Pixel);
            }
            catch (Exception)
            {
                _config = new Config();
                leftSide.Width = new GridLength(180, GridUnitType.Pixel);
            }

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1, 0), DispatcherPriority.Normal, OnSaveTimer, this.Dispatcher);
        }




        private void OnSaveTimer(object sender, EventArgs e)
        {
            SaveStickers(false);
        }

        private DispatcherTimer _timer;

        private MainViewModel _vmodel;

        private Config _config;

        private void OnFormLoaded(object sender, RoutedEventArgs e)
        {
            _vmodel = new MainViewModel();
            try
            {
                var state = XmlWriter.CheckBackup("stickers.xml");

                if (state == XmlWriter.BackupState.FileNotFoundAndBackupNotFound)
                {
                    _vmodel = new MainViewModel();
                    var plugin = new StickerPlugin.ViewModel.MainViewModel() { ParentFacade = _vmodel };
                    _vmodel.Plugins.Add(plugin);
                }
                else
                {
                    if (state == XmlWriter.BackupState.FileOlder || state == XmlWriter.BackupState.BackupOlder || state == XmlWriter.BackupState.FileNotFoundButBackupFound)
                    {
                        MessageBox.Show("Stickers files failed (" + state + "). Stickers will not be saved!");
                    }

                    var plugin = XmlWriter.Load<StickerPlugin.ViewModel.MainViewModel>("stickers.xml");
                    _vmodel.Plugins.Add(plugin);
                }
            }
            catch (Exception exc)
            {
                _vmodel = new MainViewModel();
                var plugin = new StickerPlugin.ViewModel.MainViewModel() { ParentFacade = _vmodel };
                _vmodel.Plugins.Add(plugin);

                MessageBox.Show("Load stickers failed. " + exc);
            }

            _vmodel.Init();

            this.DataContext = _vmodel.Root;
        }

        public void SaveStickers(bool showMessage)
        {
            var state = XmlWriter.CheckBackup("stickers.xml");
            if (state == XmlWriter.BackupState.FileOlder || state == XmlWriter.BackupState.BackupOlder || state == XmlWriter.BackupState.FileNotFoundButBackupFound)
            {
                if (showMessage)
                {
                    MessageBox.Show("Stickers file failed (" + state + ").");
                }
                return;
            }

            try
            {
                XmlWriter.SaveWithBackup(_vmodel.Root, "stickers.xml");
            }
            catch (Exception exc)
            {
                if (showMessage)
                    MessageBox.Show("Save stickers failed " + exc);
            }

        }

        private void MainWindow_OnClosed(object sender, EventArgs e)
        {
            SaveStickers(true);

            // Сохраняем окно
            _config.WindowSettings.Heigth = this.Height;
            _config.WindowSettings.Width = this.Width;
            if (Left > 0 && Top > 0)
            {
                _config.WindowSettings.Left = this.Left;
                _config.WindowSettings.Top = this.Top;
            }

            if (this.WindowState == WindowState.Minimized)
            {
                _config.WindowSettings.WindowState = WindowState.Normal;
            }
            else
            {
                _config.WindowSettings.WindowState = this.WindowState;
            }

            _config.LeftSideWidth = leftSide.Width.Value;

            try
            {

                XmlWriter.Save(_config, "config.xml");
            }
            catch (Exception exc)
            {
                
            }
        }
    }
}
