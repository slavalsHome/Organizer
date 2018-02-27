using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Organizer
{
    public class Config
    {
        public Config()
        {
            WindowSettings = new WindowSettings();
        }

        public WindowSettings WindowSettings { get; set; }

        public double LeftSideWidth { get; set; }
    }
}
