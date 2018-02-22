using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Xml.Serialization;
using Common.Collections;
using Common.MvvmBase;
using StickerPlugin.ViewModel;

namespace Organizer.ViewModel
{
    public class MainViewModel : BindableObject
    {
        public MainViewModel()
        {
            StickerBoards = new SimpleSelectedCollection<StickerBoardViewModel>();
        }

        public SimpleSelectedCollection<StickerBoardViewModel> StickerBoards { get; set; }

    }
}
