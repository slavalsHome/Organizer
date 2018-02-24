using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Common.ViewModel;

namespace Common.Controls
{
    public class CommonDataTemplateSelector : DataTemplateSelector
    {
        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            if (item.GetType() == typeof (CommandViewModel))
            {
                return DataTemplateHelper.GetTemplate<ButtonControl>();
            }
            
            return null;
        }
    }
}
