using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Common.ViewModel
{
    public interface IFacadeViewModel : INotifyPropertyChanged
    {
        DataTemplateSelector DataTemplateSelector { get; }
    }
}
