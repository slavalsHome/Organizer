using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Common.Collections;
using Common.MvvmBase;

namespace Organizer.ViewModel
{
    public class StickerViewModel : BindableObject, ICollectionItem<StickerViewModel>
    {
        [XmlIgnore]
        public SimpleCollection<StickerViewModel> ParentCollection { get; set; }

        public StickerViewModel()
        {
            Height = 100;
            Width = 100;
            Top = 10;
            Left = 10;
        }

        #region SavingState

        public void Save()
        {

        }

        public void Load()
        {

        }

        #endregion


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

        public double Height { get; set; }
        public double Width { get; set; }

        public double Top { get; set; }
        public double Left { get; set; }

        public void RefreshProperties()
        {
            OnPropertyChanged("Height");
            OnPropertyChanged("Width");
            OnPropertyChanged("Top");
            OnPropertyChanged("Left");
        }


        
    }
}
