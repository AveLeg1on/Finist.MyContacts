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
using Finist.MyContacts.ViewModel;

namespace Finist.MyContacts.View
{
    /// <summary>
    /// Логика взаимодействия для Favorities.xaml
    /// </summary>
    public partial class Favorities : Page
    {
        public Favorities()
        {
            InitializeComponent();
            DataContext = new FavoritesVM();
        }
    }
}
