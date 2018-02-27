using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Organizer
{
    public class WindowSettings
    {
        public WindowSettings()
        {
            WindowState = WindowState.Normal;
        }

        public double Top { get; set; }

        public double Left { get; set; }

        public double Width { get; set; }

        public double Heigth { get; set; }

        public WindowState WindowState { get; set; }
    }
}
