using Finist.MyContacts.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Finist.MyContacts.Model;
using Finist.MyContacts.Service;

namespace Finist.MyContacts.View.ViewForContats
{
    /// <summary>
    /// Логика взаимодействия для ContactUses.xaml
    /// </summary>
    public partial class ContactUses : Page
    {

        public ContactUses()
        {
            var _myNavigationService = App._NavigationService;
            InitializeComponent();

            // Инициализируем ViewModel.
            ContactUserVM contactUserVM = new ContactUserVM(_myNavigationService);
               DataContext = contactUserVM;

            // Удаляем использованный параметр, чтобы избежать ненужного хранения объекта в памяти.
            if (MyNavigationService.Parameters.ContainsKey("ContactUsesPage"))
            {
                MyNavigationService.Parameters.Remove("ContactUsesPage");
            }
        }
        //public ContactUses()
        //{
        //    var _myNavigationService = App._NavigationService;
        //    InitializeComponent();
        //    if (MyNavigationService.Parameters.ContainsKey("ContactUses"))
        //    {
        //        var selectedItem =
        //            MyNavigationService
        //                    .Parameters["ContactUses"] as
        //                Contact; // Замените YourItemType на фактический тип вашего элемента
        //        // Теперь у вас есть выбранный элемент, который вы можете использовать для инициализации ViewModel или какой-либо другой логики на этой странице.

        //        // Пример: 
        //        // this.DataContext = new EditViewModel(selectedItem);
        //        DataContext = new (selectedItem);
        //    }
           








        //    ContactUserVM contactUserVM = new ContactUserVM(_myNavigationService);
        //    DataContext = contactUserVM;
        //}


    }
}
