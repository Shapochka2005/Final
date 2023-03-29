using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;
using System.Xml;
using FinalProject.NewDataBaseDataSet2TableAdapters;
using Microsoft.Win32;

namespace FinalProject
{
    public partial class Window2 : Window
    {
        BakeryTableAdapter bakery = new BakeryTableAdapter();
        Payment_methodTableAdapter payment_method = new Payment_methodTableAdapter();
        Type_of_obtainingTableAdapter obtating = new Type_of_obtainingTableAdapter();
        List<object> newList = new List<object>();
        public int UserId { get; set; }
        public Window2()
        {
            InitializeComponent();
            DataGrid3.ItemsSource = bakery.GetData();

            TypeOfPay.ItemsSource = payment_method.GetData();
            TypeOfPay.DisplayMemberPath = "Name_of_method";
            TypeOfPay.SelectedValuePath = "Payment_Id";

            TypeOfDelivery.ItemsSource = obtating.GetData();
            TypeOfDelivery.DisplayMemberPath = "TypeObtating";
            TypeOfDelivery.SelectedValuePath = "Obtating";
        }
        private void AddToCartButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = DataGrid3.SelectedItem;
            if (selectedItem != null)
            {
                newList.Add(selectedItem);
                DataGrid5.ItemsSource = null;
                DataGrid5.ItemsSource = newList;
            }

            decimal sum = 0;
            foreach (var item in DataGrid5.Items)
            {
                sum += (decimal)(item as DataRowView).Row[3];
            }
            FullCostBlock.Text = sum.ToString();
        }

        private void TypeOfPay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int type = Convert.ToInt32(TypeOfPay.SelectedValue);
            if (type == 2)
            {
                CardNumberBox.Visibility = Visibility.Visible;
            }
            else
            {
                CardNumberBox.Visibility = Visibility.Hidden;
            }
        }

        private void AddСheque_Click(object sender, RoutedEventArgs e)
        {
            if (!CardNumberBox.Text.All(char.IsDigit))
            {
                MessageBox.Show("Номер карты должен быть числовым");
            }
            else{
                var dialog = new SaveFileDialog();
                dialog.Filter = "Text files (*.txt)|*.txt";
                dialog.ShowDialog();

                if (!string.IsNullOrEmpty(dialog.FileName))
                {
                    using (var writer = new StreamWriter(dialog.FileName))
                    {
                        foreach (DataRowView row in DataGrid5.Items)
                        {
                            writer.WriteLine(row["BakeryName"] + "\t" + row["Cost"]);
                        }
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}