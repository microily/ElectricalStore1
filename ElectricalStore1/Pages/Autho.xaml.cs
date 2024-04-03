using ElectricalStore1.Model;
using ElectricalStore1.Pages;
using Microsoft.EntityFrameworkCore;
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
                    // Здесь логика определения, какое окно откроется на основе роли сотрудника
                    switch (employee.RoleId)
                    {
                        case 1:
                            NavigationService.Navigate(new ProductPage());
                            break;
                        case 2:
                           
                            break;
                        case 3:

                            break;
                        case 4:

                            break;
                        case 5:
                            break;
                        case 6:

                            break;
                        case 7:

                            break;
                        case 8:
                            NavigationService.Navigate(new ProductPage());
                            break;
                        default:
                            MessageBox.Show("Недопустимая роль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                    }
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
