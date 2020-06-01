using System;
using System.Windows;

namespace BUSK.Charting.WPF.Components
{

    // This is a workaround to prevent a possible threading issue
    // LiveChart was designed to be easy to use, the current design 
    // avoids the usage of DataTemplates, instead we use the same object (UIElement)
    // since UI elements are running the UI thread, it is possible that 
    // when we try to modify a property in a UIElement, i.e. the labels of an axis, 
    // we can't, well we can but we need to use the UI dispatcher.

    internal static class ThreadAccess
    {
        public static T Resolve<T>(DependencyObject dependencyObject,
            DependencyProperty dependencyProperty)
        {
            if (dependencyObject.Dispatcher.CheckAccess())
                return (T) dependencyObject.GetValue(dependencyProperty);

            return (T) dependencyObject.Dispatcher.Invoke(
                new Func<T>(() => (T) dependencyObject.GetValue(dependencyProperty)));
        }
    }
}
