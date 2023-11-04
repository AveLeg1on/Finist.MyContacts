using Finist.MyContacts.Service;
using Finist.MyContacts.View;
using Finist.MyContacts.View.ViewForContats;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;


namespace Finist.MyContacts
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static MyINavigationService _NavigationService { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // 1.  MainWindow.
            var mainWindow = new MainWindow();

            // 2.  NavigationFrame из MainWindow.
            Frame mainFrame = mainWindow.NavigationFrame;


            _NavigationService = new MyNavigationService(mainFrame);

            // Регистрация страниц
            _NavigationService.Register("ContactsPage", typeof(Contacts));
            _NavigationService.Register("NumericPage", typeof(Numeric));
            _NavigationService.Register("HistoryPage", typeof(History));
            _NavigationService.Register("ContactUsesPage", typeof(ContactUses));
            _NavigationService.Register("FavoritesPage", typeof(Favorities));
            mainWindow.SetupDataContext();
            
            mainWindow.Show();
        }
    }
}
