using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Xml.Serialization;
using Common.Collections;
using Common.MvvmBase;

namespace Organizer.ViewModel
{
    public class StickerBoardViewModel : BindableObject, ISelectedCollectonItem<StickerBoardViewModel>
    {
        [XmlIgnore]
        public SimpleCollection<StickerBoardViewModel> ParentCollection { get; set; }

        public StickerBoardViewModel()
        {
            Name = "new board";
            Stickers = new SimpleCollection<StickerViewModel>();
        }

        #region SavingState

        public List<StickerViewModel> StickersSaving { get; set; }

        public void Save()
        {
            StickersSaving = new List<StickerViewModel>(Stickers);
            foreach (var sticker in StickersSaving)
            {
                sticker.Save();
            }
        }

        public void Load()
        {
            foreach (var sticker in StickersSaving)
            {
                Stickers.Add(sticker);
                sticker.Load();
            }
        }

        #endregion

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

        [XmlIgnore]
        public SimpleCollection<StickerViewModel> Stickers { get; set; }

        
        
    }
}
