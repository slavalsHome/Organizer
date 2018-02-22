using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Organizer.ViewModel;

namespace Organizer.View
{
    /// <summary>
    /// Interaction logic for StickerControl.xaml
    /// </summary>
    public partial class StickerControl : UserControl
    {
        public StickerControl()
        {
            InitializeComponent();
        }

        private void Thumb_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            var vmodel = (StickerViewModel) DataContext;
            vmodel.Left = vmodel.Left + e.HorizontalChange;
            vmodel.Top = vmodel.Top + e.VerticalChange;
        }

        private void Thumb_OnDragDelta_2(object sender, DragDeltaEventArgs e)
        {
            var vmodel = (StickerViewModel)DataContext;
            vmodel.Width = vmodel.Width + e.HorizontalChange;
            vmodel.Height = vmodel.Height + e.VerticalChange;
        }

        private void UIElement_OnLostFocus(object sender, RoutedEventArgs e)
        {
            txText.Visibility = Visibility.Hidden;
            txTextBlock.Visibility = Visibility.Visible;
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            txTextBlock.Visibility = Visibility.Hidden;
            txText.Visibility = Visibility.Visible;
            txText.Focus();
        }
    }
}
