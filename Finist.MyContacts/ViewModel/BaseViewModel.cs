using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Finist.MyContacts.ViewModel
{
   public class BaseViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

       
        protected void Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = "")
        {
            if (!EqualityComparer<T>.Default.Equals((field,value)))
            {
                field = value;
                OnPropertyChanged(PropertyName);
            }
        }
        /// <summary>
        /// Оповещение нескольких свойств 
        /// </summary>
        /// <param name="names"></param>
        protected void Notify(params string[] names)
        {
            foreach (var name in names)
            {
                OnPropertyChanged(name);

            }
        }
    }
}
