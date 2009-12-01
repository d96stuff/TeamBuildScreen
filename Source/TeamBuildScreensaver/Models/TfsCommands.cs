using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace TeamBuildScreenSaver.Models
{
    public static class TfsCommands
    {
        public static RoutedUICommand SelectServer
        {
            get;
            private set;
        }

        public static RoutedUICommand SubscribeToBuildCompletionEvent
        {
            get;
            private set;
        }

        public static RoutedUICommand UnsubscribeFromBuildCompletionEvent
        {
            get;
            private set;
        }

        public static RoutedUICommand SubscribeToCheckInEvent
        {
            get;
            private set;
        }

        public static RoutedUICommand UnsubscribeFromCheckInEvent
        {
            get;
            private set;
        }

        static TfsCommands()
        {
            SelectServer = new RoutedUICommand("SelectServer", "SelectServer", typeof(TfsCommands));
            SubscribeToBuildCompletionEvent = new RoutedUICommand("SubscribeToBuildCompletionEvent", "SubscribeToBuildCompletionEvent", typeof(TfsCommands));
            UnsubscribeFromBuildCompletionEvent = new RoutedUICommand("UnsubscribeFromBuildCompletionEvent", "UnsubscribeFromBuildCompletionEvent", typeof(TfsCommands));
            SubscribeToCheckInEvent = new RoutedUICommand("SubscribeToCheckInEvent", "SubscribeToCheckInEvent", typeof(TfsCommands));
            UnsubscribeFromCheckInEvent = new RoutedUICommand("UnsubscribeFromCheckInEvent", "UnsubscribeFromCheckInEvent", typeof(TfsCommands));
        }
    }
}