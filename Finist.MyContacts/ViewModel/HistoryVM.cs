using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using Finist.MyContacts.Model;
using Microsoft.EntityFrameworkCore;

namespace Finist.MyContacts.ViewModel
{
   public class HistoryVM:BaseViewModel
    {
        private ObservableCollection<Contact> _historyRegistration;

        public ObservableCollection< Contact>
            HistoryRegistration
        {
            get
            {
                return _historyRegistration;
            }
            set
            {
                Set(ref _historyRegistration, value);
            }
        }
        public HistoryVM()
        {
            using (var context=new MyDBContext())
            {
                context.Contacts.Load();
                HistoryRegistration = context.Contacts.Local.ToObservableCollection();
            }
        }
 
        // public RelayCommand HistoryContactRegistrationLoadCommand= new RelayCommand(LOd)
    }
}
