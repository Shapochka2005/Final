using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using FinalProject.NewDataBaseDataSet2TableAdapters;

namespace FinalProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UsersTableAdapter adapter = new UsersTableAdapter();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void JoinButton_Click(object sender, RoutedEventArgs e)
        {
            var allLogins = adapter.GetData().Rows;
            bool pravilnost = false;
            for (int i = 0;i < allLogins.Count; i++)
            {
                if (allLogins[i][2].ToString() == LoginBox.Text && 
                    allLogins[i][3].ToString() == PasswordBox.Password)
                {
                    pravilnost = true;
                    int roleId = (int)allLogins[i][1];
                    int ID = (int)allLogins[i][0];

                    switch (roleId)
                    {
                        case 1:
                            Window1 root = new Window1();
                            root.UserId = ID;
                            root.Show();
                            break;
                        case 2:
                            Window2 user = new Window2();
                            user.UserId = ID;
                            user.Show();
                            break;
                        case 3:
                            Window3 manager = new Window3();
                            manager.UserId = ID;
                            manager.Show();
                            break;
                    }
                    this.Close();
                }
                
            }
            if (!pravilnost)
            {
                MessageBox.Show("Вы ввели неправильный пароль!");
            }
        }
    }
}