using System;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;
using Common.Collections;
using Common.MvvmBase;
using Common.ViewModel;

namespace StickerPlugin.ViewModel
{
    public class StickerBoardViewModel : BindableObject, ISelectedCollectionItem
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
            Name = "new board";
            Stickers = new SimpleCollection<StickerViewModel>();

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

    }
}
