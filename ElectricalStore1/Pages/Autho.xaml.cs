using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ElectricalStore1.Model;

namespace ElectricalStore1.Pages
{
    /// <summary>
    /// Логика взаимодействия для Autho.xaml
    /// </summary>
    public partial class Autho : Page
    {
        private readonly electrical_storeContext _context;

        public Autho()
        {
            InitializeComponent();
            _context = new electrical_storeContext(); // Создание контекста базы данных
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var login = LoginTextBox.Text;
                var password = PasswordTextBox.Password;

                // Поиск сотрудника по логину и паролю в базе данных
                var employee = _context.Employees.FirstOrDefault(emp => emp.Login == login && emp.Password == password);

                if (employee != null)
                {
                    // Передаем объект Employee в конструктор класса home
                    NavigationService.Navigate(new home(employee));
                }
                else
                {
                    MessageBox.Show("Логин или пароль введены неверно.", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
