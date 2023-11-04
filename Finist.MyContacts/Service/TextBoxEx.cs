using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.ComponentModel;

namespace Finist.MyContacts.Service
{

    public class TextBoxEx : TextBox
    {
        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register(
            nameof(Placeholder), typeof(string), typeof(TextBoxEx), new PropertyMetadata(""));

        public TextBoxEx()
        {
            DefaultStyleKey = typeof(TextBoxEx);
            this.DataContextChanged += TextBoxEx_DataContextChanged;
            //this.TextChangedByBinding += TextBoxEx_TextChangedByBinding;
            //this.LostFocus += TextBoxEx_LostFocus;
            this.DataContextChanged += TextBoxEx_DataContextChanged;
         
            if (this.DataContext is INotifyPropertyChanged vm)
            {
                vm.PropertyChanged += ViewModel_PropertyChanged;
            }
        }

        public static bool GetHidePlaceholderOnFocus(DependencyObject obj)
        {
            return (bool)obj.GetValue(HidePlaceholderOnFocusProperty);
        }
        //private void TextBoxEx_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    // Убедимся, что у нас есть ViewModel связанный с TextBoxEx
        //    if (this.DataContext is INotifyPropertyChanged vm)
        //    {
        //        // Теперь вызываем метод, который проверяет PlaceholderMessage
        //        ViewModel_PropertyChanged(vm, new PropertyChangedEventArgs(this.GetBindingExpression(TextProperty)?.ResolvedSourcePropertyName));
        //    }
        //}
        public static void SetHidePlaceholderOnFocus(DependencyObject obj, bool value)
        {
            obj.SetValue(HidePlaceholderOnFocusProperty, value);
        }

        public static readonly DependencyProperty HidePlaceholderOnFocusProperty =
            DependencyProperty.RegisterAttached("HidePlaceholderOnFocus", typeof(bool), typeof(TextBoxEx),
                new PropertyMetadata(false, OnHidePlaceholderOnFocusChanged));

        #region FocusTextBoxForPalceholder

        private static void InnerTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (string.IsNullOrEmpty(textBox.Text) && textBox.Parent is Grid parentGrid)
            {
                var textBlock = parentGrid.Children.OfType<TextBlock>().FirstOrDefault();
                if (textBlock != null)
                {
                    textBlock.Visibility = Visibility.Visible;
                }


            }
        }
        //private static void InnerTextBox_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    var textBox = sender as TextBox;
        //    if (textBox.Parent is Grid parentGrid)
        //    {
        //        var textBlock = parentGrid.Children.OfType<TextBlock>().FirstOrDefault();
        //        if (textBlock != null)
        //        {
        //            textBlock.Visibility = Visibility.Visible;
        //        }
        //    }
        //}

        private static void InnerTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox.Parent is Grid parentGrid)
            {
                var textBlock = parentGrid.Children.OfType<TextBlock>().FirstOrDefault();
                if (textBlock != null)
                {
                    textBlock.Visibility = Visibility.Collapsed;
                }
            }

        }
        private static void OnHidePlaceholderOnFocusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox && e.NewValue is bool newValue && newValue)
            {
                textBox.GotFocus += InnerTextBox_GotFocus;
                textBox.LostFocus += InnerTextBox_LostFocus;

            }
        }

        #endregion


        public event EventHandler TextChangedByBinding;
        protected virtual void OnTextChangedByBinding()
        {
            TextChangedByBinding?.Invoke(this, EventArgs.Empty);
        }
        private void TextBoxEx_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue is INotifyPropertyChanged oldContext)
            {
                oldContext.PropertyChanged -= ViewModel_PropertyChanged;
            }

            if (e.NewValue is INotifyPropertyChanged newContext)
            {
                newContext.PropertyChanged += ViewModel_PropertyChanged;
            }
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
           
            if (e.PropertyName == "PlaceholderMessage")
            {
                UpdatePlaceholderVisibility();
            }
        
    }
       
        private void UpdatePlaceholderVisibility()
        {
            // Если текстовое поле пустое, показываем Placeholder
            var visibility = string.IsNullOrWhiteSpace(this.Text) ?  Visibility.Collapsed: Visibility.Visible;

            if (this.Parent is Grid parentGrid)
            {
                var textBlock = parentGrid.Children.OfType<TextBlock>().FirstOrDefault();
                if (textBlock != null)
                {
                    textBlock.Visibility = visibility;
                }
            }
        }



    }
}




