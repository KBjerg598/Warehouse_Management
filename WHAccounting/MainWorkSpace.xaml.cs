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
using System.Windows.Shapes;
using WHAccounting.Classes;
using System.IO;
using Microsoft.Win32;
using ClosedXML.Excel;
using System.Collections.ObjectModel;

namespace WHAccounting
{
    public partial class MainWorkSpace : Window
    {
        private CollectionView _collectionView;

        AppContextW db;
        CalcWindow cw;

        public MainWorkSpace()
        {
            InitializeComponent();

            dataGridView1.AutoGeneratingColumn += DataGridView1_AutoGeneratingColumn;

            LoadDataGrid();
        }

        private void LoadDataGrid()
        {
            using (db = new AppContextW())
            {
                var items = db.Items.ToList();
                dataGridView1.ItemsSource = items;
                _collectionView = (CollectionView)CollectionViewSource.GetDefaultView(dataGridView1.ItemsSource);
            }
        }

        private void DataGridView1_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "id":
                    e.Column.Header = "Ідентифікатор";
                    e.Column.IsReadOnly = true;
                    break;
                case "Vendor_code":
                    e.Column.Header = "Артикул";
                    break;
                case "Item_name":
                    e.Column.Header = "Назва товару";
                    break;
                case "Manufacturer":
                    e.Column.Header = "Виробник";
                    break;
                case "Quantity":
                    e.Column.Header = "Кількість";
                    break;
                case "Unit_cost":
                    e.Column.Header = "Ціна за одиницю";
                    break;
                case "Category":
                    e.Column.Header = "Категорія";
                    break;
                case "Manufacturer_code":
                    e.Column.Header = "Код виробника";
                    break;
                case "Item_availability":
                    e.Column.Header = "Місцезнаходження";
                    break;
                default:
                    break;
            }
        }

        private void DataGridView1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete && dataGridView1.SelectedItem is Item selectedItem)
            {
                using (var db = new AppContextW())
                {
                    var itemToRemove = db.Items.Find(selectedItem.id);
                    if (itemToRemove != null)
                    {
                        db.Items.Remove(itemToRemove);
                        db.SaveChanges();
                    }
                }
                LoadDataGrid();
            }
        }

        private void calculatorButton_Click(object sender, RoutedEventArgs e)
        {
            cw = new CalcWindow();
            cw.Show();
        }

        private void excelButton_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx",
                Title = "Сохранить в Excel",
                FileName = "Export.xlsx"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                using (var workbook = new ClosedXML.Excel.XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Data");

                    worksheet.Cell(1, 1).Value = "Артикул";
                    worksheet.Cell(1, 2).Value = "Назва товару";
                    worksheet.Cell(1, 3).Value = "Виробник";
                    worksheet.Cell(1, 4).Value = "Кількість";
                    worksheet.Cell(1, 5).Value = "Ціна за одиницю";
                    worksheet.Cell(1, 6).Value = "Категорія";
                    worksheet.Cell(1, 7).Value = "Код виробника";
                    worksheet.Cell(1, 8).Value = "Місцезнаходження";

                    int row = 2;
                    foreach (var item in dataGridView1.Items)
                    {
                        if (item is Item actualItem)
                        {
                            worksheet.Cell(row, 1).Value = actualItem.vendor_code;
                            worksheet.Cell(row, 2).Value = actualItem.item_name;
                            worksheet.Cell(row, 3).Value = actualItem.manufacturer;
                            worksheet.Cell(row, 4).Value = actualItem.quantity;
                            worksheet.Cell(row, 5).Value = actualItem.unit_cost;
                            worksheet.Cell(row, 6).Value = actualItem.category;
                            worksheet.Cell(row, 7).Value = actualItem.manufacturer_code;
                            worksheet.Cell(row, 8).Value = actualItem.item_availability;
                            row++;
                        }
                    }

                    workbook.SaveAs(saveFileDialog.FileName);
                }
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            var addItemWindow = new AddItemWindow();
            bool? result = addItemWindow.ShowDialog();

            // Если диалог закрыт успешно и создан новый объект
            if (result == true && addItemWindow.NewItem != null)
            {
                using (var db = new AppContextW())
                {
                    // Добавляем новый товар в базу данных
                    db.Items.Add(addItemWindow.NewItem);
                    db.SaveChanges();
                }

                // Обновляем DataGrid
                LoadDataGrid();
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_collectionView == null)
                return;

            _collectionView.Filter = item =>
            {
                if (item is Item itemData)
                {
                    return itemData.Item_name.Contains(searchTextBox.Text, StringComparison.OrdinalIgnoreCase);
                }
                return false;
            };
        }
    }
}