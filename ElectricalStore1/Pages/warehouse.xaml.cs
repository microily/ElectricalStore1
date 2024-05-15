using ElectricalStore1.Model;
using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    /// Логика взаимодействия для warehouse.xaml
    /// </summary>
    public partial class warehouse : Page
    {
        private ObservableCollection<Warehouse> WarehouseData = new ObservableCollection<Warehouse>();
        private Employee currentUser;

        public warehouse(Employee currentUser = null)
        {
            InitializeComponent();
            this.currentUser = currentUser;
            LoadData();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        private void LoadData()
        {
            try
            {
                using (var context = new electrical_storeContext())
                {
                    var data = context.Warehouses.OrderBy(warehouse => warehouse.WarehouseId).ToList();
                    WarehouseData.Clear();
                    foreach (var item in data)
                    {
                        WarehouseData.Add(item);
                    }
                }
                dataGrid.ItemsSource = WarehouseData;
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
                    foreach (var warehouse in WarehouseData)
                    {
                        if (warehouse.WarehouseId == 0)
                        {
                            context.Warehouses.Add(warehouse);
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
                var selectedItems = dataGrid.SelectedItems.Cast<Warehouse>().ToList();

                if (selectedItems.Any())
                {
                    using (var context = new electrical_storeContext())
                    {
                        foreach (var warehouse in selectedItems)
                        {
                            var entity = context.Warehouses.Find(warehouse.WarehouseId);
                            if (entity != null)
                            {
                                context.Warehouses.Remove(entity);
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
                    foreach (var warehouse in WarehouseData)
                    {
                        var entity = context.Warehouses.Find(warehouse.WarehouseId);
                        if (entity != null)
                        {
                            entity.ProductId = warehouse.ProductId;
                            entity.QuantityInStock = warehouse.QuantityInStock;
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