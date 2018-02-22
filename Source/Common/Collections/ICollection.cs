using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Common.Collections
{
    public interface ICollection
    {
        ICommand AddCommand { get; }

        ICommand RemoveCommand { get; }
    }
}
