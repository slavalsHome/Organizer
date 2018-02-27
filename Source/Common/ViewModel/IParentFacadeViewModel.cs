using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.MvvmBase.Dialogs;

namespace Common.ViewModel
{
    public interface IParentFacadeViewModel
    {
        IDialogService DialogService { get; }

        List<IFacadeViewModel> Plugins { get; }
    }
}
