using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Finist.MyContacts.Service
{

    public interface MyINavigationService
    {
        void NavigateTo(string pageName, object parameter = null);
        void Register(string pageName, Type pageType);
        void GoBack();
       
    }
}
