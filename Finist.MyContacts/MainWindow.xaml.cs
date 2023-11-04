
using System.Windows;
using Finist.MyContacts.Service;
using Finist.MyContacts.View;
using Finist.MyContacts.View.ViewForContats;
using Finist.MyContacts.ViewModel;
using Microsoft.EntityFrameworkCore;


namespace Finist.MyContacts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

           
        }
        public void SetupDataContext()
        {
            var navigationService = App._NavigationService;
            if (navigationService == null)
            {
                MessageBox.Show("Navigation Service is not initialized!");
                return;
            }

            DataContext = new WindowVM(new WindowService(this), navigationService); ;
        }

    }
}
