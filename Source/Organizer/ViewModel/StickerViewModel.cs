using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Common.Collections;
using Common.MvvmBase;

namespace Organizer.ViewModel
{
    public class StickerViewModel : BindableObject, ICollectionItem
    {
        [XmlIgnore]
        public ICollection ParentCollection { get; set; }

        public StickerViewModel()
        {
            Height = 100;
            Width = 100;
            Top = 10;
            Left = 10;
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
    }
}
