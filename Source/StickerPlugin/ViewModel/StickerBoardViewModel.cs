﻿using System.Xml.Serialization;
using Common.Collections;
using Common.MvvmBase;

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



    }
}