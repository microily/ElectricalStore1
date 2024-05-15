using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Windows;
using System.Windows.Controls;
using ElectricalStore1.Model;
using Microsoft.Win32;
using OfficeOpenXml;
using Xceed.Words.NET; // Добавьте эту директиву



namespace ElectricalStore1.Pages
{
    public partial class employee : Page
    {
        private ObservableCollection<Employee> EmployeeData = new ObservableCollection<Employee>();
        private Employee currentUser;

        public employee(Employee currentUser = null)
        {
            InitializeComponent();
            this.currentUser = currentUser;
            LoadData();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }
        private void ExportToWordButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedEmployee = dataGrid.SelectedItem as Employee;
                if (selectedEmployee != null)
                {
                    GenerateContract(selectedEmployee);
                }
                else
                {
                    MessageBox.Show("Выберите сотрудника для формирования договора.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при формировании договора: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadData()
        {
            try
            {
                using (var context = new electrical_storeContext())
                {
                    var data = context.Employees.OrderBy(employee => employee.EmployeeId).ToList();
                    EmployeeData.Clear();
                    foreach (var item in data)
                    {
                        EmployeeData.Add(item);
                    }
                }
                dataGrid.ItemsSource = EmployeeData;
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
                    foreach (var employee in EmployeeData)
                    {
                        if (employee.EmployeeId == 0)
                        {
                            context.Employees.Add(employee);
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
                var selectedItems = dataGrid.SelectedItems.Cast<Employee>().ToList();

                if (selectedItems.Any())
                {
                    using (var context = new electrical_storeContext())
                    {
                        foreach (var employee in selectedItems)
                        {
                            var entity = context.Employees.Find(employee.EmployeeId);
                            if (entity != null)
                            {
                                context.Employees.Remove(entity);
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
                    foreach (var employee in EmployeeData)
                    {
                        var entity = context.Employees.Find(employee.EmployeeId);
                        if (entity != null)
                        {
                            entity.FirstName = employee.FirstName;
                            entity.LastName = employee.LastName;
                            entity.RoleId = employee.RoleId;
                            entity.Patronymic = employee.Patronymic;
                            entity.Salary = employee.Salary;
                            entity.Login = employee.Login;
                            entity.Password = employee.Password;
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

        private void GenerateContract(Employee employee)
        {
            try
            {
                string templatePath = @"C:\Users\microily\source\repos\ElectricalStore1\ElectricalStore1\Dogovor\Dogovor.docx";

                // Создание нового документа DocX
                using (DocX doc = DocX.Load(templatePath))
                {
                    // Замена меток в документе на данные выбранного сотрудника
                    doc.ReplaceText("[EmployeeId]", employee.EmployeeId.ToString());
                    doc.ReplaceText("[FirstName]", employee.FirstName);
                    doc.ReplaceText("[LastName]", employee.LastName);
                    doc.ReplaceText("[Patronymic]", employee.Patronymic ?? "");
                    doc.ReplaceText("[Дата заключения договора]", DateTime.Now.ToShortDateString());
                    doc.ReplaceText("[Salary]", employee.Salary.ToString());

                    // Путь для сохранения документа
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Word Files|*.docx;*.doc";
                    saveFileDialog.Title = "Выберите место для сохранения договора";
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        string savePath = saveFileDialog.FileName;

                        // Сохранение документа
                        doc.SaveAs(savePath);

                        MessageBox.Show("Договор успешно сформирован и сохранен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при формировании договора: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        // Метод для поиска и замены текста в документе Word
        private void ReplaceText(Microsoft.Office.Interop.Word.Document doc, string findText, string replaceText)
        {
            var range = doc.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: findText, ReplaceWith: replaceText, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
        }

        private void ExportToExcelButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx|All Files|*.*",
                    Title = "Сохранить файл Excel",
                    FileName = "EmployeeData.xlsx"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    FileInfo excelFile = new FileInfo(saveFileDialog.FileName);

                    using (ExcelPackage package = new ExcelPackage(excelFile))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Employee Data");

                        for (int i = 0; i < dataGrid.Columns.Count; i++)
                        {
                            worksheet.Cells[1, i + 1].Value = dataGrid.Columns[i].Header;
                        }

                        for (int i = 0; i < EmployeeData.Count; i++)
                        {
                            worksheet.Cells[i + 2, 1].Value = EmployeeData[i].EmployeeId;
                            worksheet.Cells[i + 2, 2].Value = EmployeeData[i].FirstName;
                            worksheet.Cells[i + 2, 3].Value = EmployeeData[i].LastName;
                            worksheet.Cells[i + 2, 4].Value = EmployeeData[i].RoleId;
                            worksheet.Cells[i + 2, 5].Value = EmployeeData[i].Patronymic;
                            worksheet.Cells[i + 2, 6].Value = EmployeeData[i].Salary;
                            worksheet.Cells[i + 2, 7].Value = EmployeeData[i].Login;
                            worksheet.Cells[i + 2, 8].Value = EmployeeData[i].Password;
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
