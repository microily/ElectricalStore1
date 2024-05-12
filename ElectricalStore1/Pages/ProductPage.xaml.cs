using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ElectricalStore1.Model;
using Microsoft.Win32;
using OfficeOpenXml;

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

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
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

        private void ExportToExcelButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx|All Files|*.*",
                    Title = "Сохранить файл Excel",
                    FileName = "ProductData.xlsx"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    FileInfo excelFile = new FileInfo(saveFileDialog.FileName);

                    using (ExcelPackage package = new ExcelPackage(excelFile))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Product Data");

                        // Записываем заголовки столбцов
                        for (int i = 0; i < dataGrid.Columns.Count; i++)
                        {
                            worksheet.Cells[1, i + 1].Value = dataGrid.Columns[i].Header;
                        }

                        // Записываем данные из ProductData в Excel
                        for (int i = 0; i < ProductData.Count; i++)
                        {
                            worksheet.Cells[i + 2, 1].Value = ProductData[i].ProductId;
                            worksheet.Cells[i + 2, 2].Value = ProductData[i].Name;
                            worksheet.Cells[i + 2, 3].Value = ProductData[i].Model;
                            worksheet.Cells[i + 2, 4].Value = ProductData[i].Manufacturer;
                            worksheet.Cells[i + 2, 5].Value = ProductData[i].Price;
                            worksheet.Cells[i + 2, 6].Value = ProductData[i].CategoryId;
                        }

                        package.Save();
                    }

                    MessageBox.Show("Данные успешно сохранены в файл Excel.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при экспорте данных в Excel: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
