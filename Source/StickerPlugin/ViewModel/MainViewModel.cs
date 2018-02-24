using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;
using Common.Collections;
using Common.MvvmBase;
using Common.ViewModel;
using StickerPlugin.View;

namespace StickerPlugin.ViewModel
{
    public class MainViewModel : BindableObject, IFacadeViewModel
    {
        public MainViewModel()
        {
            DataTemplateSelector = new StickerPluginDataTemplateSelector();

            StickerBoards = new SimpleSelectedCollection<StickerBoardViewModel>();
            
            Commands = new SimpleCollection<CommandViewModel>();
            var addCmd = new CommandViewModel();
            addCmd.Command = StickerBoards.AddCommand;
            addCmd.CommandParameter = null;
            addCmd.Text = "Add Board";
            addCmd.Image = new BitmapImage(new Uri("pack://application:,,,/StickerPlugin;component/Icons/add.png"));
            Commands.Add(addCmd);

        }

        public SimpleSelectedCollection<StickerBoardViewModel> StickerBoards { get; set; }

        [XmlIgnore]
        public SimpleCollection<CommandViewModel> Commands { get; set; }

        #region IFacadeViewModel

        [XmlIgnore]
        public DataTemplateSelector DataTemplateSelector { get; private set; }

        #endregion
    }
}
