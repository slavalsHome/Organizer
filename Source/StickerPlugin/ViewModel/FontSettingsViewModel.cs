using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using Common.MvvmBase;

namespace StickerPlugin.ViewModel
{
    public class FontSettingsViewModel : BindableObject
    {
        public FontSettingsViewModel()
        {
            FontSize = 12;
            
        }

        /*public FontWeight FontWeight { get; set; }

        public FontStyle FontStyle { get; set; }*/

        private int _fontSize;

        public int FontSize
        {
            get { return _fontSize; }
            set
            {
                _fontSize = value;
                OnPropertyChanged("FontSize");

                Is12 = false;
                Is16 = false;
                Is20 = false;
                Is24 = false;

                if (_fontSize == 12)
                {
                    Is12 = true;
                }
                if (_fontSize == 16)
                {
                    Is16 = true;
                }
                if (_fontSize == 20)
                {
                    Is20 = true;
                }
                if (_fontSize == 24)
                {
                    Is24 = true;
                }


                OnPropertyChanged("Is12");
                OnPropertyChanged("Is16");
                OnPropertyChanged("Is20");
                OnPropertyChanged("Is24");
            }
        }

        [XmlIgnore]
        public bool Is12 { get; set; }
        [XmlIgnore]
        public bool Is16 { get; set; }
        [XmlIgnore]
        public bool Is20 { get; set; }
        [XmlIgnore]
        public bool Is24 { get; set; }

        #region Add Item

        private ICommand _changeFontSizeCommand;

        [XmlIgnore]
        public ICommand ChangeFontSizeCommand
        {
            get
            {
                if (_changeFontSizeCommand == null)
                {
                    _changeFontSizeCommand = new RelayCommand(OnChangeFontSizeCommand);
                }
                return _changeFontSizeCommand;
            }
        }


        protected virtual void OnChangeFontSizeCommand(object obj)
        {
            int isize;
            if (int.TryParse(obj.ToString(), out isize))
            {
                FontSize = isize;
            }

        }

        #endregion
    }
}
