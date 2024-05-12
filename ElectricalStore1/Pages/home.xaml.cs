using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ElectricalStore1.Model;

namespace ElectricalStore1.Pages
{
    public partial class home : Page
    {
        private readonly electrical_storeContext _context;
        private Employee _currentUser;

        public home(Employee currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Autho());
        }

        private void ProductButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentUser != null && (_currentUser.RoleId == 1 || _currentUser.RoleId == 8))
            {
                ProductPage productPage = new ProductPage(_currentUser);
                NavigationService.Navigate(productPage);
            }
            else
            {
                MessageBox.Show("Недостаточно прав доступа", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
