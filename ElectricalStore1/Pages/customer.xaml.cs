using ElectricalStore1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для customer.xaml
    /// </summary>
    public partial class customer : Page
    {
        private ObservableCollection<Customer> CustomerData = new ObservableCollection<Customer>();
        private Employee currentUser;

        public customer(Employee currentUser = null)
        {
            InitializeComponent();
            this.currentUser = currentUser;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using (var context = new electrical_storeContext())
                {
                    var data = context.Customers.OrderBy(customer => customer.CustomerId).ToList();
                    CustomerData.Clear();
                    foreach (var item in data)
                    {
                        CustomerData.Add(item);
                    }
                }
                dataGrid.ItemsSource = CustomerData;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new electrical_storeContext())
                {
                    foreach (var customer in CustomerData)
                    {
                        if (customer.CustomerId == 0)
                        {
                            context.Customers.Add(customer);
                        }
                    }
                    context.SaveChanges();
                }

                MessageBox.Show("Изменения сохранены.");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedItems = dataGrid.SelectedItems.Cast<Customer>().ToList();

                if (selectedItems.Any())
                {
                    using (var context = new electrical_storeContext())
                    {
                        foreach (var customer in selectedItems)
                        {
                            var entity = context.Customers.Find(customer.CustomerId);
                            if (entity != null)
                            {
                                context.Customers.Remove(entity);
                            }
                        }
                        context.SaveChanges();
                    }

                    MessageBox.Show("Записи успешно удалены.");
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Выберите запись для удаления.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении записи: {ex.Message}");
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new electrical_storeContext())
                {
                    foreach (var customer in CustomerData)
                    {
                        var entity = context.Customers.Find(customer.CustomerId);
                        if (entity != null)
                        {
                            entity.FirstName = customer.FirstName;
                            entity.LastName = customer.LastName;
                            entity.Patronymic = customer.Patronymic;
                            entity.PhoneNumber = customer.PhoneNumber;
                            entity.Address = customer.Address;
                        }
                    }
                    context.SaveChanges();
                }

                MessageBox.Show("Изменения сохранены.");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            home homePage = new home(currentUser);
            NavigationService.Navigate(homePage);
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
