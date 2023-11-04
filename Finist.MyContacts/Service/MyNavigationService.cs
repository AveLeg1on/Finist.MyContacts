using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Finist.MyContacts.View;

namespace Finist.MyContacts.Service
{
    public class MyNavigationService : MyINavigationService
    {
        private readonly Frame _frame;
        private readonly Dictionary<string, Type> _pages = new();
        public static Dictionary<string, object> Parameters { get; private set; } = new Dictionary<string, object>();


        public MyNavigationService(Frame frame)
        {
            _frame = frame;
        }

        public void NavigateTo(string pageName, object parameter=null)
        {
            if (!_pages.ContainsKey(pageName))
            {
                throw new ArgumentException($"Page not registered: {pageName}");
            }

            if (parameter != null)
            {
                Parameters[pageName] = parameter;
            }

            Page pageInstance = Activator.CreateInstance(_pages[pageName]) as Page;
           
            _frame.Navigate(pageInstance);
           

        }

        public void Register(string pageName, Type pageType)
        {
            if (!_pages.ContainsKey(pageName))
            {
                _pages.Add(pageName, pageType);
            }
        }

        public void GoBack()
        {
            if (_frame.CanGoBack)
            {
                _frame.GoBack();
            }
        }
    }
}
