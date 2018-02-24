using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Common.Controls
{
    public class DataTemplateHelper
    {
        private sealed class TemplateGeneratorControl : ContentControl
        {
            internal static readonly DependencyProperty FactoryProperty = DependencyProperty.Register("Factory", typeof(Func<object>), typeof(DataTemplateHelper.TemplateGeneratorControl), new PropertyMetadata(null, FactoryChanged));

            private static void FactoryChanged(DependencyObject instance, DependencyPropertyChangedEventArgs args)
            {
                var control = (DataTemplateHelper.TemplateGeneratorControl)instance;
                var factory = (Func<object>)args.NewValue;
                control.Content = factory();
            }
        }

        public static DataTemplate GetTemplate<T>()
            where T : FrameworkElement, new()
        {
            Func<object> factory = () => new T();
            
            var frameworkElementFactory = new FrameworkElementFactory(typeof(TemplateGeneratorControl));
            frameworkElementFactory.SetValue(TemplateGeneratorControl.FactoryProperty, factory);

            var template = new DataTemplate(typeof(T));
            template.VisualTree = frameworkElementFactory;

            return template;
        }

    }
}
