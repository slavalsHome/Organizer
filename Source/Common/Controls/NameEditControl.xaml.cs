using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Common.Collections;

namespace Common.Controls
{
    /// <summary>
    /// Interaction logic for NameEditControl.xaml
    /// </summary>
    public partial class NameEditControl : UserControl
    {
        public NameEditControl()
        {
            InitializeComponent();
        }

        private void TxTextBlock_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var item = (ISelectedCollectionItem) DataContext;
            item.ParentCollection.Current = item;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (txText.Visibility == Visibility.Hidden)
            {
                var board = (INotifyPropertyChanged)DataContext;
                board.PropertyChanged -= OnVModelPropertyChanged;
                board.PropertyChanged += OnVModelPropertyChanged;

                txTextBlock.Visibility = Visibility.Hidden;
                txText.Visibility = Visibility.Visible;
                txText.SelectAll();
                txText.Focus();
            }
            else
            {
                txText.Visibility = Visibility.Hidden;
                txTextBlock.Visibility = Visibility.Visible;
            }

        }

        private void OnVModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsSelected")
            {
                var board = (ISelectedCollectionItem)sender;
                if (!board.IsSelected)
                {
                    txText.Visibility = Visibility.Hidden;
                    txTextBlock.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
