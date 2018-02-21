using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Xml.Serialization;
using Common.Collections;
using Common.MvvmBase;

namespace Organizer.ViewModel
{
    public class MainViewModel : BindableObject
    {
        public MainViewModel()
        {
            StickerBoards = new SimpleSelectedCollection<StickerBoardViewModel>();
        }

#region SavingState


        public List<StickerBoardViewModel> StickerBoardsSaving { get; set; }

        public void Save()
        {
            StickerBoardsSaving = new List<StickerBoardViewModel>(StickerBoards);
            foreach (var stickerBoardViewModel in StickerBoardsSaving)
            {
                stickerBoardViewModel.Save();
            }
        }

        public void Load()
        {
            foreach (var stickerBoardViewModel in StickerBoardsSaving)
            {
                StickerBoards.Add(stickerBoardViewModel);
                stickerBoardViewModel.Load();
            }
        }

#endregion

        [XmlIgnore]
        public SimpleSelectedCollection<StickerBoardViewModel> StickerBoards { get; private set; }

    }
}
