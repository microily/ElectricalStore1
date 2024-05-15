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
        private void EmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentUser != null && (_currentUser.RoleId == 1 || _currentUser.RoleId == 8))
            {
                employee employeePage = new employee(_currentUser); // Подставьте свою страницу для работы с сотрудниками
                NavigationService.Navigate(employeePage);
            }
            else
            {
                MessageBox.Show("Недостаточно прав доступа", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void VisitorsButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем роль текущего пользователя
            if (_currentUser != null && (_currentUser.RoleId == 1 || _currentUser.RoleId == 2 || _currentUser.RoleId == 7 || _currentUser.RoleId == 8))
            {
                // Создаем новую страницу для работы с клиентами и передаем текущего пользователя
                customer customerPage = new customer(_currentUser);
                NavigationService.Navigate(customerPage);
            }
            else
            {
                // Если у пользователя нет соответствующих прав, выводим сообщение об ошибке
                MessageBox.Show("Недостаточно прав доступа", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void WarehouseButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем роль текущего пользователя
            if (_currentUser != null && (_currentUser.RoleId == 1 || _currentUser.RoleId == 3 || _currentUser.RoleId == 6 || _currentUser.RoleId == 8))
            {
                // Создаем новую страницу склада и передаем текущего пользователя
                warehouse warehousePage = new warehouse(_currentUser);
                NavigationService.Navigate(warehousePage);
            }
            else
            {
                // Если у пользователя нет соответствующих прав, выводим сообщение об ошибке
                MessageBox.Show("Недостаточно прав доступа", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
