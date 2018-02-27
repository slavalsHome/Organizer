using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Common.MvvmBase.Dialogs
{
    public interface IDialogFactory : INotifyPropertyChanged
    {
        bool GetDialog(string reason, object data, out IDialogService dialogService, out INotifyPropertyChanged vmodel, out INotifyPropertyChanged parent);
    }
}
