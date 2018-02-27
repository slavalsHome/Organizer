using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Common.Collections
{
    public interface ICollection
    {
        INotifyPropertyChanged ParentViewModel { get; }

        ICommand AddCommand { get; }

        ICommand RemoveCommand { get; }
    }
}
