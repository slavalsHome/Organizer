using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Common.Controls;
using Common.ViewModel;
using StickerPlugin.ViewModel;

namespace StickerPlugin.View
{
    public class StickerPluginDataTemplateSelector : DataTemplateSelector
    {
        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            if (item.GetType() == typeof(StickerViewModel))
            {
                return DataTemplateHelper.GetTemplate<StickerControl>();
            }

            if (item.GetType() == typeof(StickerBoardViewModel))
            {
                return DataTemplateHelper.GetTemplate<NameEditControl>();
            }

            return null;
        }
    }
}
