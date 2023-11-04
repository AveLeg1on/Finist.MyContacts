using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Finist.MyContacts.Service
{
   public static class ScrollViewerExtensions
    {
        public static DependencyProperty EnableHorizontalScrollingWithMouseProperty=DependencyProperty.RegisterAttached("EnableHorizontalScrollingWithMouseProperty", typeof(bool), typeof(ScrollViewerExtensions), new PropertyMetadata(false, OnEnableHorizontalScrollingWithMouseChanched));

        private static void OnEnableHorizontalScrollingWithMouseChanched(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ScrollViewer scrollViewer && e.NewValue is bool enable && enable)
            {
                scrollViewer.PreviewMouseWheel += (sender, args) =>
                {
                    scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - args.Delta);
                    args.Handled = true;

                };

            }
            
        }

        public static bool GetEnableHorizontalScrollingWithMouse(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnableHorizontalScrollingWithMouseProperty);

        }

        public static void SetValueEnableHorizontalScrollingWithMouse(DependencyObject obj, bool value)
        {
            obj.SetValue(EnableHorizontalScrollingWithMouseProperty,value);
        }

    }
}
