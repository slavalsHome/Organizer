using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;
using Common.Collections;
using Common.MvvmBase;
using Common.MvvmBase.Dialogs;
using Common.ViewModel;
using StickerPlugin.View;

namespace StickerPlugin.ViewModel
{
    public class MainViewModel : BindableObject, IFacadeViewModel, IDialogFactory
    {
        public MainViewModel()
        {
            DataTemplateSelector = new StickerPluginDataTemplateSelector();

            StickerBoards = new SimpleSelectedCollection<StickerBoardViewModel>();
            StickerBoards.ParentViewModel = this;
            
            Commands = new SimpleCollection<CommandViewModel>();
            var addCmd = new CommandViewModel();
            addCmd.Command = StickerBoards.AddCommand;
            addCmd.CommandParameter = null;
            addCmd.Text = "";
            addCmd.Image = new BitmapImage(new Uri("pack://application:,,,/StickerPlugin;component/Icons/add.png"));
            Commands.Add(addCmd);
        }

        public SimpleSelectedCollection<StickerBoardViewModel> StickerBoards { get; set; }


        #region Collapsed

        public bool BoardsIsCollapsed { get; set; }

        private ICommand _boardsCollapseCommand;

        public ICommand BoardsCollapseCommand
        {
            get
            {
                if (_boardsCollapseCommand == null)
                {
                    _boardsCollapseCommand = new RelayCommand(OnBoardsCollapse);
                }
                return _boardsCollapseCommand;
            }
        }

        private void OnBoardsCollapse(object obj)
        {
            BoardsIsCollapsed = !BoardsIsCollapsed;
            OnPropertyChanged("BoardsIsCollapsed");
        }

        #endregion

        [XmlIgnore]
        public SimpleCollection<CommandViewModel> Commands { get; set; }

        #region IFacadeViewModel

        [XmlIgnore]
        public IParentFacadeViewModel ParentFacade { get; set; }

        [XmlIgnore]
        public DataTemplateSelector DataTemplateSelector { get; private set; }

        #endregion



        public bool GetDialog(string reason, object data, out IDialogService dialogService, out INotifyPropertyChanged vmodel, out INotifyPropertyChanged parent)
        {
            dialogService = null;
            vmodel = null;
            parent = null;

            if (reason == "OnRemove")
            {
                if (data.GetType() == typeof (StickerBoardViewModel))
                {
                    var board = (StickerBoardViewModel)data;
                    if (board.Stickers.Count == 0)
                        return false;

                    var dialog = new MessageDialogViewModel();
                    dialog.Header = "S-MANAGER";
                    dialog.Message = "Are you sure that you want to remove the sticker board?";
                    vmodel = dialog;

                    parent = this;
                    dialogService = ParentFacade.DialogService;
                    return true;
                }

            }

            return false;
        }
    }
}
