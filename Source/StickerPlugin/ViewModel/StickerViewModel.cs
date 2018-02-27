using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Xml.Serialization;
using Common.Collections;
using Common.MvvmBase;

namespace StickerPlugin.ViewModel
{
    public class StickerViewModel : BindableObject, ICollectionItem
    {
        private const int HeaderHeight = 26;

        [XmlIgnore]
        public ICollection ParentCollection { get; set; }

        public StickerViewModel()
        {
            Font = new FontSettingsViewModel();

            Height = 100;
            Width = 200;
            Top = 10;
            Left = 10;
            OpenedHeight = Height;
            Header = "...";
        }

        public FontSettingsViewModel Font { get; set; }

        private string _header;
        public string Header
        {
            get { return _header; }
            set
            {
                _header = value;
                OnPropertyChanged("Header");
            }
        }

        private string _text;
        public string Text
        {
            get { return _text; }
            set 
            { 
                _text = value;
                OnPropertyChanged("Text");
            }
        }

        private double _height;
        private double _width;
        private double _top;
        private double _left;

        public double Height
        {
            get { return _height; }
            set
            {
                _height = value;
                OnPropertyChanged("Height");
            }
        }

        public double Width
        {
            get { return _width; }
            set
            {
                _width = value;
                OnPropertyChanged("Width");
            }
        }

        public double Top
        {
            get { return _top; }
            set
            {
                _top = value;
                OnPropertyChanged("Top");
            }
        }

        public double Left
        {
            get { return _left; }
            set
            {
                _left = value;
                OnPropertyChanged("Left");
            }
        }

        #region Collapsed

        public bool IsCollapsed { get; set; }

        public double OpenedHeight { get; set; }

        private ICommand _isCollapsed;

        [XmlIgnore]
        public ICommand CollapseCommand
        {
            get
            {
                if (_isCollapsed == null)
                {
                    _isCollapsed = new RelayCommand(OnCollapse);
                }
                return _isCollapsed;
            }
        }

        private void OnCollapse(object obj)
        {
            if (IsCollapsed)
            {
                // сделаем поверх всех
                var collection = ParentCollection as ObservableCollection<StickerViewModel>;
                if (collection != null)
                {
                    int inew = collection.Count - 1;
                    if (inew > 0)
                    {
                        int iold = collection.IndexOf(this);
                        if (inew != iold)
                        {
                            collection.Move(iold, inew);
                        }
                    }
                }

                IsCollapsed = false;
                OnPropertyChanged("IsCollapsed");
                Height = OpenedHeight;
            }
            else
            {
                IsCollapsed = true;
                OpenedHeight = Height;
                Height = HeaderHeight;
                OnPropertyChanged("IsCollapsed");
            }
        }

        #endregion
    }
}
