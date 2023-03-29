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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using FinalProject.NewDataBaseDataSet2TableAdapters;

namespace FinalProject
{
    public partial class Window1 : Window
    {
        UsersTableAdapter user = new UsersTableAdapter();
        User_roleTableAdapter role = new User_roleTableAdapter();
        BakeryTableAdapter bakery = new BakeryTableAdapter();
        ProductCategoryTableAdapter productCategory = new ProductCategoryTableAdapter();
        public int UserId { get; set; }
        public Window1()
        {
            InitializeComponent();
            DataGrid1.ItemsSource = user.GetData();
            DataGrid2.ItemsSource = role.GetData();
            DataGrid4.ItemsSource = bakery.GetData();


            RoleComboBox.ItemsSource = role.GetData();
            RoleComboBox.DisplayMemberPath = "Roles";
            RoleComboBox.SelectedValuePath = "Role_Id";

            IdBox.ItemsSource = productCategory.GetData();
            IdBox.DisplayMemberPath = "Category";
            IdBox.SelectedValuePath = "CategoryId";
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidInput())
            {
                user.InsertUser(Convert.ToInt32(RoleComboBox.SelectedValue), UserBox.Text, PasswordBox.Text, NameBox.Text, SurnameBox.Text, NumberBox.Text);
                DataGrid1.ItemsSource = user.GetData();
            }
        }

            private void RoleEdit_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid2.SelectedItem != null)
            {
                object id1 = (DataGrid2.SelectedItem as DataRowView).Row[0];
                role.UpdateRole(RoleBox.Text, Convert.ToInt32(id1));
                DataGrid2.ItemsSource = role.GetData();
            }
        }

        private void DataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGrid1.SelectedItem != null)
            {
                UserBox.Text = (DataGrid1.SelectedItem as DataRowView).Row[2].ToString();
                PasswordBox.Text = (DataGrid1.SelectedItem as DataRowView).Row[3].ToString();
                NumberBox.Text = (DataGrid1.SelectedItem as DataRowView).Row[6].ToString();
                NameBox.Text = (DataGrid1.SelectedItem as DataRowView).Row[4].ToString();
                SurnameBox.Text = (DataGrid1.SelectedItem as DataRowView).Row[5].ToString();
            }
        }
        private bool IsValidInput()
        {
            if (!NumberBox.Text.All(char.IsDigit))
            {
                MessageBox.Show("Номер телефона должен состоять только из цифр.");
                return false;
            }
            return true;
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            object id2 = (DataGrid1.SelectedItem as DataRowView).Row[0];
            user.UpdateUser(Convert.ToInt32(RoleComboBox.SelectedValue), UserBox.Text, PasswordBox.Text, NameBox.Text, SurnameBox.Text, NumberBox.Text, Convert.ToInt32(id2));
            DataGrid1.ItemsSource = user.GetData();
        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            object id3 = (DataGrid1.SelectedItem as DataRowView).Row[0];
            user.DeleteUser(Convert.ToInt32(id3));
            DataGrid1.ItemsSource = user.GetData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsValideInput())
            {
                bakery.InsertBakery(NameBox1.Text, Convert.ToInt32(IdBox.SelectedValue), Convert.ToDecimal(CostBox.Text), Convert.ToDecimal(WeightBox.Text));
                DataGrid4.ItemsSource = bakery.GetData();
            }
        }

        private void EditButton1_Click(object sender, RoutedEventArgs e)
        {
            if (IsValideInput())
            {
                object id4 = (DataGrid4.SelectedItem as DataRowView).Row[0];
                bakery.UpdateBakery(NameBox1.Text, Convert.ToInt32(IdBox.SelectedValue), Convert.ToDecimal(CostBox.Text), Convert.ToDecimal(WeightBox.Text), Convert.ToInt32(id4));
                DataGrid4.ItemsSource = bakery.GetData();
            }
        }

        private void DelButton1_Click(object sender, RoutedEventArgs e)
        {
            if (IsValideInput())
            {
                object id5 = (DataGrid4.SelectedItem as DataRowView).Row[0];
                bakery.DeleteBakery(Convert.ToInt32(id5));
                DataGrid4.ItemsSource = bakery.GetData();
            }
        }

        private bool IsValideInput()
        {
            if (!CostBox.Text.All(char.IsDigit))
            {
                MessageBox.Show("Стоимость должна быть числовой");
                return false;
            }

            if (!WeightBox.Text.All(char.IsDigit))
            {
                MessageBox.Show("Вес должен быть числовым");
                return false;
            }
            return true;
        }

        private void DataGrid2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGrid2.SelectedItem != null)
            {
                RoleBox.Text = (DataGrid2.SelectedItem as DataRowView).Row[1].ToString();
            }
        }

        private void DataGrid4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGrid4.SelectedItem != null)
            {
                NameBox1.Text = (DataGrid4.SelectedItem as DataRowView).Row[1].ToString();
                CostBox.Text = (DataGrid4.SelectedItem as DataRowView).Row[3].ToString();
                WeightBox.Text = (DataGrid4.SelectedItem as DataRowView).Row[4].ToString();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}