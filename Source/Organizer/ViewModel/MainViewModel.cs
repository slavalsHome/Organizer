using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Xml.Serialization;
using Common.Collections;
using Common.Controls;
using Common.MvvmBase;
using Common.MvvmBase.Dialogs;
using Common.ViewModel;
using StickerPlugin.ViewModel;

namespace Organizer.ViewModel
{
    public class MainViewModel : BindableObject , IParentFacadeViewModel
    {
        public MainViewModel()
        {
            Plugins = new List<IFacadeViewModel>();

        }

        public IDialogService DialogService { get; private set; }

        public void Init()
        {
            DialogService = ServiceLocator.Resolve<IDialogService>();

            MainDataTemplateSelector.Selectors.Add(new CommonDataTemplateSelector());
            foreach (var facadeViewModel in Plugins)
            {
                facadeViewModel.ParentFacade = this;
                MainDataTemplateSelector.Selectors.Add(facadeViewModel.DataTemplateSelector);
            }

            Root = Plugins[0];
        }

        public List<IFacadeViewModel> Plugins { get; set; }

        public INotifyPropertyChanged Root { get; set; }
    }
}
