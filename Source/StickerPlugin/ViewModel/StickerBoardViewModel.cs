using System;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;
using Common.Collections;
using Common.MvvmBase;
using Common.MvvmBase.Dialogs;
using Common.ViewModel;

namespace StickerPlugin.ViewModel
{
    public class StickerBoardViewModel : BindableObject, ISelectedCollectionItem, IDialogFactory
    {
        [XmlIgnore]
        public ISelectedCollection ParentCollection { get; set; }

        [XmlIgnore]
        ICollection ICollectionItem.ParentCollection
        {
            get { return ParentCollection; }
            set { ParentCollection = (ISelectedCollection)value; }
        }

        public StickerBoardViewModel()
        {
            Name = "NEW BOARD";
            Stickers = new SimpleCollection<StickerViewModel>();
            Stickers.ParentViewModel = this;

            Commands = new SimpleCollection<CommandViewModel>();
            var addCmd = new CommandViewModel();
            addCmd.Command = Stickers.AddCommand;
            addCmd.CommandParameter = null;
            addCmd.Text = "Add Sticker";
            addCmd.Image = new BitmapImage(new Uri("pack://application:,,,/StickerPlugin;component/Icons/add.png")); 
            Commands.Add(addCmd);
        }

        private string _name;

        public string Name
        {
            get { return _name; } 
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        private bool _isSelected;



        [XmlIgnore]
        public bool IsSelected
        {
            get { return _isSelected; }
            set 
            { 
                _isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public SimpleCollection<StickerViewModel> Stickers { get; set; }

        [XmlIgnore]
        public SimpleCollection<CommandViewModel> Commands { get; set; }

        public bool GetDialog(string reason, object data, out IDialogService dialogService, out INotifyPropertyChanged vmodel, out INotifyPropertyChanged parent)
        {
            dialogService = null;
            vmodel = null;
            parent = null;

            if (reason == "OnRemove")
            {
                if (data.GetType() == typeof(StickerViewModel))
                {
                    var sticker = (StickerViewModel)data;
                    if (string.IsNullOrEmpty(sticker.Text))
                        return false;

                    var dialog = new MessageDialogViewModel();
                    dialog.Header = "S-MANAGER";
                    dialog.Message = "Are you sure that you want to remove the sticker?";

                    vmodel = dialog;
                    dialogService = ((IFacadeViewModel) ParentCollection.ParentViewModel).ParentFacade.DialogService;
                    parent = ParentCollection.ParentViewModel;
                    return true;
                }
            }

            return false;
        }

    }
}
