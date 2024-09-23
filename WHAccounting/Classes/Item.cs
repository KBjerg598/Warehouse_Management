using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHAccounting.Classes
{
    public class Item
    {
        [Key]
        public int id { get; set; }

        public string item_name, manufacturer, category, manufacturer_code, item_availability;
        public int vendor_code, quantity;
        public decimal unit_cost;

        public int Vendor_code
        {
            get { return vendor_code; }
            set { vendor_code = value; }
        }
        public string Item_name
        {
            get { return item_name; }
            set { item_name = value; }
        }

        public string Manufacturer
        {
            get { return manufacturer; }
            set { manufacturer = value; }
        }

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public decimal Unit_cost
        {
            get { return unit_cost; }
            set { unit_cost = value; }
        }

        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        public string Manufacturer_code
        {
            get { return manufacturer_code; }
            set { manufacturer_code = value; }
        }

        public string Item_availability
        {
            get { return item_availability; }
            set { item_availability = value; }
        }

        public Item() { }
        public Item(int vendor_code, string item_name, string manufacturer, int quantity, decimal unit_cost, string category, string manufacturer_code, string item_availability)
        {
            this.vendor_code = vendor_code;
            this.item_name = item_name;
            this.manufacturer = manufacturer;
            this.quantity = quantity;
            this.unit_cost = unit_cost;
            this.category = category;
            this.manufacturer_code = manufacturer_code;
            this.item_availability = item_availability;
        }
    }
}
