using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Finist.MyContacts.Model;
using Microsoft.EntityFrameworkCore;

namespace Finist.MyContacts.ViewModel
{
    public class FavoritesVM : BaseViewModel
    {
        #region Fields

        
     private ObservableCollection<FavoritesContact> _fC;
        private FavoritesContact _selectedFavoritesContact;
        #endregion

        #region Properties

         public ObservableCollection<FavoritesContact> FC
        {
            get
            {
                return _fC;
            }
            set
            {
                Set(ref _fC, value);
            }
        }

        public FavoritesContact SelectedFavoritesContact
        {
            get
            {
                return _selectedFavoritesContact;
            }
            set
            {
                Set(ref _selectedFavoritesContact,value);
            }
        }

        #endregion
       
       

        private void FavoritesContactLoad()
        {
            using (var context = new MyDBContext())
            {
                context.FavoritesContacts.Include(x => x.Contacts)
                    .Load();
                FC = context.FavoritesContacts.Local.ToObservableCollection();
            }

        }

       public RelayCommand FavoritesContactLoadCommand => new RelayCommand(FavoritesContactLoad);

      public RelayCommand<object> DeleteInFavoritesContactCommand => new RelayCommand<object>(DeleteFovaritesContact);
        private void DeleteFovaritesContact(object parametr)
        {
            if (parametr is FavoritesContact selectedFavoritesContact)
            { using (var context=new MyDBContext())
                {
                    context.FavoritesContacts.Remove(selectedFavoritesContact);
                    context.SaveChanges();

                }
                    FavoritesContactLoad();
            }
        }
    }
}
