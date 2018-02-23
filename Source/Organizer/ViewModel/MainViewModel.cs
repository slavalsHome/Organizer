using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Xml.Serialization;
using Common.Collections;
using Common.MvvmBase;
using Common.ViewModel;
using StickerPlugin.ViewModel;

namespace Organizer.ViewModel
{
    public class MainViewModel : BindableObject
    {
        public MainViewModel()
        {
            Plugins = new List<IFacadeViewModel>();
        }

        public List<IFacadeViewModel> Plugins { get; set; }

    }
}
