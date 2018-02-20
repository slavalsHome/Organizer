using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Xml.Serialization;
using Common.MvvmBase;

namespace Organizer.ViewModel
{
    public class StickerBoardViewModel : BindableObject
    {
        public StickerBoardViewModel()
        {
            Stickers = new ObservableCollection<StickerViewModel>();
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
                sticker.Parent = this;
                Stickers.Add(sticker);
                sticker.Load();
            }
        }

        #endregion

        [XmlIgnore]
        public MainViewModel Parent { get; set; }

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
        public ObservableCollection<StickerViewModel> Stickers { get; set; }

        #region AddSticker

        private ICommand _addStickerCommand;

        [XmlIgnore]
        public ICommand AddSticker
        {
            get
            {
                if (_addStickerCommand == null)
                {
                    _addStickerCommand = new RelayCommand(OnAddSticker, CanAddSticker);
                }
                return _addStickerCommand;
            }
        }

        private bool CanAddSticker(object obj)
        {
            return true;
        }

        private void OnAddSticker(object obj)
        {
            //Trace.WriteLine("OnAddSticker");

            var sticker = new StickerViewModel();
            sticker.Parent = this;
            sticker.Text = "";
            sticker.Top = 10;
            sticker.Left = 10;
            sticker.Height = 150;
            sticker.Width = 150;

            Stickers.Add(sticker);
        }

        #endregion

        #region RemoveSticker

        private ICommand _removeStickerCommand;

        [XmlIgnore]
        public ICommand RemoveStickerCommand
        {
            get
            {
                if (_removeStickerCommand == null)
                {
                    _removeStickerCommand = new RelayCommand(OnRemoveSticker, CanRemoveSticker);
                }
                return _removeStickerCommand;
            }
        }

        private bool CanRemoveSticker(object obj)
        {
            return true;
        }

        private void OnRemoveSticker(object obj)
        {
            var sticker = obj as StickerViewModel;
            if (sticker != null)
                Stickers.Remove(sticker);
        }

        #endregion
    }
}
