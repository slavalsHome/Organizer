using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Common.Controls
{
    public class MainDataTemplateSelector : DataTemplateSelector
    {
        static MainDataTemplateSelector()
        {
            Selectors = new List<DataTemplateSelector>();
        }

        public static List<DataTemplateSelector> Selectors { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            foreach (var dataTemplateSelector in Selectors)
            {
                var template = dataTemplateSelector.SelectTemplate(item, container);
                if (template != null)
                    return template;
            }

            throw new Exception("No Data Template Found!");
        }

    }
}
