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

namespace WHAccounting
{
    public partial class AddItemWindow : Window
    {
        public Item NewItem { get; private set; }

        public AddItemWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int vendorCode = int.Parse(vendorCodeTextBox.Text);
                string itemName = itemNameTextBox.Text;
                string manufacturer = manufacturerTextBox.Text;
                int quantity = int.Parse(quantityTextBox.Text);
                decimal unitCost = decimal.Parse(unitCostTextBox.Text);
                string category = categoryTextBox.Text;
                string manufacturerCode = manufacturerCodeTextBox.Text;
                string availability = availabilityTextBox.Text;

                NewItem = new Item
                {
                    Vendor_code = vendorCode,
                    Item_name = itemName,
                    Manufacturer = manufacturer,
                    Quantity = quantity,
                    Unit_cost = unitCost,
                    Category = category,
                    Manufacturer_code = manufacturerCode,
                    Item_availability = availability
                };

                this.DialogResult = true;
                this.Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("Рядки не можуть бути порожніми");
            }
        }
    }
}
