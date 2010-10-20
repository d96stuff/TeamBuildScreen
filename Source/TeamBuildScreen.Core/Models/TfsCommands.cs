using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace TeamBuildScreen.Core.Models
{
    public static class TfsCommands
    {
        public static RoutedUICommand SelectServer
        {
            get;
            private set;
        }

        static TfsCommands()
        {
            SelectServer = new RoutedUICommand("SelectServer", "SelectServer", typeof(TfsCommands));
        }
    }
}