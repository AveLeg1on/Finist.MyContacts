using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Finist.MyContacts.Model;
using Finist.MyContacts.Service;
using Finist.MyContacts.View.ViewForContats;
using Microsoft.EntityFrameworkCore;
using Notification.Wpf;


namespace Finist.MyContacts.ViewModel
{
   public class ContactVM:Contact
   {
        #region Fields

        private readonly MyINavigationService _myINavigationService;
        private ObservableCollection<Contact> _contactsCollection=new ObservableCollection<Contact>();
        private bool _isContextMenuOpen;
        private Contact _selectedContactItem;
        private string _StringSearch;
        private ImageBrush _imageBrush; 


        #endregion

        #region Properties

        public ImageBrush ImageBrush
        {
            get
            {
                return _imageBrush;
            }
            set
            {
                Set(ref _imageBrush,value);
            }
        }
        public string StringSearch
        {
            get
            {
                return _StringSearch;
            }
            set
            {
                Set(ref _StringSearch,value);
            }
        }
      

        public Contact SelectedContactItem
        {
            get { return _selectedContactItem; }
            set
            {
                Set(ref _selectedContactItem,value);
                EditContactCommand.NotifyCanExecuteChanged();
            }
        }
        public ObservableCollection<Contact>  ContactsCollection
        {
            get
            {
                return _contactsCollection;
            }
            set
            {
                Set(ref _contactsCollection,value);
              
            }
        }

        #endregion
        public ContactVM( MyINavigationService myINavigationService)
        {
            _myINavigationService =
                myINavigationService ?? throw new ArgumentNullException(nameof(_myINavigationService));

           

            using (var db =new MyDBContext())
            {
                 db.Contacts.Load();
            ContactsCollection = db.Contacts.Local.ToObservableCollection();
            }
           
        }

        #region Command
        //Команда для добавления
        public RelayCommand AddContactCommand => new RelayCommand(OpenNewPageForAddEntry);
       
            //Команда для редактирования
        public RelayCommand EditContactCommand => new RelayCommand(OpenPageForEditEntry, CanEditItem);
        public RelayCommand DeleteContactCommand => new RelayCommand(Deleter, CanDeleteItem);
        public RelayCommand SmartSearchTextBoxvCommand => new RelayCommand(SmartSearch);

        public RelayCommand AddToFavoritesCommand => new RelayCommand(FavoritesContactAdd, CanAddFavorites);

       




        #endregion

        #region Methods 
        private void FavoritesContactAdd()
        {
            using (var context=new MyDBContext())
            {
                var cont=context.FavoritesContacts.Where(x => x.ContactID == SelectedContactItem.Id);
                if (cont.ToList().Count == 0)
                {
                  FavoritesContact favorite = new FavoritesContact()
                { ContactID = SelectedContactItem.Id

                };
                context.FavoritesContacts.Add(favorite);
                context.SaveChanges();
                }
                else
                {
                    var notificationManager = new NotificationManager();
                    notificationManager.Show("Данный контакт уже в избранных","Message", NotificationType.Error, "WindowArea");
            return;
                }
                
            }
           
        }
        #region CanMethods
  private bool CanAddFavorites()
        {
            return SelectedContactItem != null;
        }
  private bool CanEditItem()
        {
            return SelectedContactItem != null;
        }
  private bool CanDeleteItem()
        {
            return SelectedContactItem != null;
        }
#endregion
      
        private void SmartSearch()
        {
            using (MyDBContext context = new MyDBContext())
            {
                if (string.IsNullOrEmpty(StringSearch))
                {
                    context.Contacts.Load();
                    ContactsCollection = context.Contacts.Local.ToObservableCollection();
                    return;
                }

                var matchingContacts = context.Contacts
                    .Where(c => c.Name.Contains(StringSearch)
                                || c.PhoneMobile.Contains(StringSearch)
                                || c.Surename.Contains(StringSearch))
                    .ToList();

                ContactsCollection = new ObservableCollection<Contact>(matchingContacts);
            }

        }

        private void OpenPageForEditEntry()
        {
            if (SelectedContactItem!=null)
            {


                _myINavigationService.NavigateTo("ContactUsesPage", SelectedContactItem);
            }
        }
      
       

        private void Deleter()
        {
            
            using (var context=new MyDBContext())
            {
                context.Contacts.Remove(SelectedContactItem);
              
                context.SaveChanges();
                context.Contacts.Load();
                ContactsCollection = context.Contacts.Local.ToObservableCollection();

            }
            
        }


        private void OpenNewPageForAddEntry()
        {
         _myINavigationService.NavigateTo("ContactUsesPage");
        }
       
        #endregion

    }
}
