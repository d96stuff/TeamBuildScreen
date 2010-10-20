//-----------------------------------------------------------------------
// <copyright file="AttachedProperties.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreenSaver.Views
{
    #region Usings

    using System.Windows;
    using System.Windows.Input;

    #endregion

    /// <summary>
    /// Allows views to register command bindings to view models.
    /// </summary>
    public static class AttachedProperties
    {
        #region Fields

        public static DependencyProperty RegisterCommandBindingsProperty =
            DependencyProperty.RegisterAttached("RegisterCommandBindings",
            typeof(CommandBindingCollection),
            typeof(AttachedProperties),
            new PropertyMetadata(null, OnRegisterCommandBindingChanged));

        #endregion

        #region Methods

        public static void SetRegisterCommandBindings(UIElement element, CommandBindingCollection value)
        {
            if (element != null)
            {
                element.SetValue(RegisterCommandBindingsProperty, value);
            }
        }

        public static CommandBindingCollection GetRegisterCommandBindings(UIElement element)
        {
            return element != null ? (CommandBindingCollection)element.GetValue(RegisterCommandBindingsProperty) : null;
        }

        private static void OnRegisterCommandBindingChanged (DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            UIElement element = sender as UIElement;

            if (element != null)
            {
                CommandBindingCollection bindings = e.NewValue as CommandBindingCollection;
                if (bindings != null)
                {
                    element.CommandBindings.AddRange(bindings);
                }
            }
        }

        #endregion
    }
}