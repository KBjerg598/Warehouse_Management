using System;
using System.Collections.Generic;
using System.Data;
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

namespace WHAccounting
{
    public partial class CalcWindow : Window
    {
        public CalcWindow()
        {
            InitializeComponent();

            foreach (UIElement el in GridElements.Children)
            {
                if (el is Button)
                {
                    ((Button)el).Click += Button_Click;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string enteredNumber = (string)((Button)e.OriginalSource).Content;
            if (enteredNumber == "AC") calculateArea.Text = "";
            else if (enteredNumber == "=")
            {
                string val = new DataTable().Compute(calculateArea.Text, null).ToString();
                calculateArea.Text = val;
            }
            else
                calculateArea.Text += enteredNumber;
        }
    }
}
