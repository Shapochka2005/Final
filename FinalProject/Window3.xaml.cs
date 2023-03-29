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
using FinalProject.NewDataBaseDataSet2TableAdapters;

namespace FinalProject
{
    public partial class Window3 : Window
    {
        OneOrderTableAdapter oneOrder = new OneOrderTableAdapter();
        deliver_informationTableAdapter deliver = new deliver_informationTableAdapter();
        ProductsTableAdapter products = new ProductsTableAdapter();
        public int UserId { get; set; }
        public Window3()
        {
            InitializeComponent();
            OrdersGrid.ItemsSource = oneOrder.GetData();
            DeliveryGrid.ItemsSource = deliver.GetData();
            ProductsGrid.ItemsSource = products.GetData();

            DeliverIdBox.ItemsSource = deliver.GetData();
            DeliverIdBox.DisplayMemberPath = "Name_of_organisation";
            DeliverIdBox.SelectedValuePath = "deliver_informationId";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void AddOrganisation_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidInput())
            {
                deliver.InsertOrganisation(OrganisationNameBox.Text, OrganisationcountryBox.Text, PhoneNumberOrgBox.Text);
                DeliveryGrid.ItemsSource = deliver.GetData();
            }
        }
        private void DeleteOrganisation_Click(object sender, RoutedEventArgs e)
        {
            object id = (DeliveryGrid.SelectedItem as DataRowView).Row[0].ToString();
            deliver.DeletOrganisation(Convert.ToInt32(id));
            DeliveryGrid.ItemsSource = deliver.GetData();
        }
        private void EditOrganisation_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidInput()) {
                object id1 = (DeliveryGrid.SelectedItem as DataRowView).Row[0].ToString();
                deliver.UpdateOrganisation(OrganisationNameBox.Text, OrganisationcountryBox.Text, PhoneNumberOrgBox.Text, Convert.ToInt32(id1));
                DeliveryGrid.ItemsSource = deliver.GetData();
            }
        }
        private bool IsValidInput()
        {
            if (!PhoneNumberOrgBox.Text.All(char.IsDigit))
            {
                MessageBox.Show("Номер телефона должен состоять только из цифр.");
                return false;
            }
            if (string.IsNullOrEmpty(OrganisationNameBox.Text))
            {
                MessageBox.Show("Пользователь конечно же не будет вводить название!");
                return false;
            }
            if (string.IsNullOrEmpty(OrganisationcountryBox.Text))
            {
                MessageBox.Show("Да, конечно, зачем нужна страна, да и какие страны, Путин президент мира, мир - РОССИЯ");
                return false;
            }
            if (string.IsNullOrEmpty(PhoneNumberOrgBox.Text))
            {
                MessageBox.Show("Зачем вводить номер телефона");
                return false;
            }
            return true;
        }

        private void DeliveryGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DeliveryGrid.SelectedItem != null)
            {
                OrganisationNameBox.Text = (DeliveryGrid.SelectedItem as DataRowView).Row[1].ToString();
                OrganisationcountryBox.Text = (DeliveryGrid.SelectedItem as DataRowView).Row[2].ToString();
                PhoneNumberOrgBox.Text = (DeliveryGrid.SelectedItem as DataRowView).Row[3].ToString();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ProductNameBox.Text))
            {
                MessageBox.Show("Пользователь конечно же не будет вводить название продукта!");
                return;
            }
            if (string.IsNullOrEmpty(ProvidernameBox.Text))
            {
                MessageBox.Show("Да, конечно, зачем нужно имя доставщика");
                return;
            }
            if (DeliverIdBox.SelectedValue == null)
            {
                MessageBox.Show("Зачем выбирать ID, если есть такое красивое пустое поле");
                return;
            }
            products.InsertProduct(Convert.ToInt32(DeliverIdBox.SelectedValue), ProductNameBox.Text, ProvidernameBox.Text);
            ProductsGrid.ItemsSource = products.GetData();
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            object id3 = (ProductsGrid.SelectedItem as DataRowView).Row[0].ToString();
            products.DeleteProduct(Convert.ToInt32(id3));
            ProductsGrid.ItemsSource = products.GetData();
        }

        private void ProductsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductsGrid.SelectedItem != null)
            {
                ProductNameBox.Text = (ProductsGrid.SelectedItem as DataRowView).Row[2].ToString();
                ProvidernameBox.Text = (ProductsGrid.SelectedItem as DataRowView).Row[3].ToString();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ProductNameBox.Text))
            {
                MessageBox.Show("Пользователь конечно же не будет вводить название продукта!");
                return;
            }
            if (string.IsNullOrEmpty(ProvidernameBox.Text))
            {
                MessageBox.Show("Да, конечно, зачем нужно имя доставщика");
                return;
            }
            if (DeliverIdBox.SelectedValue == null)
            {
                MessageBox.Show("Зачем выбирать ID, если есть такое красивое пустое поле");
                return;
            }
            object id4 = (ProductsGrid.SelectedItem as DataRowView).Row[0].ToString();
            products.UpdateProduct(Convert.ToInt32(DeliverIdBox.SelectedValue), ProductNameBox.Text, ProvidernameBox.Text, Convert.ToInt32(id4));
            ProductsGrid.ItemsSource = products.GetData();
        }
    }
}
