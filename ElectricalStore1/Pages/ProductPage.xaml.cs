using ElectricalStore1.Model;
using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        private ObservableCollection<Product> ProductData = new ObservableCollection<Product>();

        public ProductPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using (var context = new electrical_storeContext())
                {
                    var data = context.Products.OrderBy(product => product.ProductId).ToList();
                    ProductData.Clear();
                    foreach (var item in data)
                    {
                        ProductData.Add(item);
                    }
                }
                dataGrid.ItemsSource = ProductData;
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
                    foreach (var product in ProductData)
                    {
                        if (product.ProductId == 0) // Предполагая, что новые записи имеют ProductId равный 0
                        {
                            context.Products.Add(product);
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
                var selectedItems = dataGrid.SelectedItems.Cast<Product>().ToList();

                if (selectedItems.Any())
                {
                    using (var context = new electrical_storeContext())
                    {
                        foreach (var product in selectedItems)
                        {
                            var entity = context.Products.Find(product.ProductId);
                            if (entity != null)
                            {
                                context.Products.Remove(entity);
                            }
                        }
                        context.SaveChanges();
                    }

                    MessageBox.Show("Записи успешно удалены.");
                    LoadData(); // Обновляем данные в DataGrid после удаления
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
                    foreach (var product in ProductData)
                    {
                        var entity = context.Products.Find(product.ProductId);
                        if (entity != null)
                        {
                            entity.Name = product.Name;
                            entity.Model = product.Model;
                            entity.Manufacturer = product.Manufacturer;
                            entity.Price = product.Price;
                            entity.CategoryId = product.CategoryId;
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

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
