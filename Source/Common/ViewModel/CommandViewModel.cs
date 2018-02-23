using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Media;
using Common.Collections;
using Common.MvvmBase;

namespace Common.ViewModel
{
    public class CommandViewModel : BindableObject, ICollectionItem
    {
        public ICollection ParentCollection { get; set; }

        public string Text { get; set; }

        public ImageSource Image { get; set; }

        public ICommand Command { get; set; }

        public object CommandParameter { get; set; }
    }
}
