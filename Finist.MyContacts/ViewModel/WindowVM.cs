using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using CommunityToolkit.Mvvm.Input;
using Finist.MyContacts.Model;
using Finist.MyContacts.Service;
using System.Runtime;
using System.Windows.Controls;
using System.Windows.Navigation;
using Finist.MyContacts.View;

namespace Finist.MyContacts.ViewModel
{
    public class WindowVM:BaseViewModel
    {
        #region Fields

         private readonly IWindowService _windowService;

        private readonly MyINavigationService _myINavigationService;
      
        #endregion

        #region Properties


        public MyINavigationService MyNavigationService => _myINavigationService;



        #endregion

        public WindowVM(IWindowService windowService, MyINavigationService myINavigationService)
        {
            _myINavigationService = myINavigationService;
             
            _windowService=windowService;
            ShowViewContactsCommand = new RelayCommand(NavigateContactsPage);
        }

       
        #region Command
        public RelayCommand ShowViewContactsCommand { get; set; }
        public RelayCommand ShowViewHistoryCommand => new RelayCommand(NavigateHistoryPage);
        public RelayCommand DragMoveCommand => new RelayCommand(_windowService.DragMove);
        public RelayCommand MaximizeHeightCommand => new RelayCommand(_windowService.MaximizeHeight);
        public RelayCommand MinimizeWindowCommand => new RelayCommand(_windowService.MinimizeWindow);
        public RelayCommand MaximizeWindowCommand => new RelayCommand(_windowService.MaximizeWindow);
        public RelayCommand CloseWindowCommand => new RelayCommand(_windowService.CloseWindow);
        public RelayCommand CreateDBCommand =>new RelayCommand(CreateDB);
        public RelayCommand ShowVewFavoritesCommand => new RelayCommand(NavigateFavoritesPage);

     
        #endregion

        #region Methods

        private void NavigateFavoritesPage() => _myINavigationService.NavigateTo("FavoritesPage");
        
        private void NavigateHistoryPage() => MyNavigationService.NavigateTo("HistoryPage");

        private void CreateDB()
        {
            using (MyDBContext db = new())
            {
                db.Database.EnsureCreated();
            }
        }
        private  void NavigateContactsPage()  => MyNavigationService.NavigateTo("ContactsPage");
      
           
       
      
            
       

        #endregion
       
    }
}
