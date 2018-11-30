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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToyLibrary;

namespace ToyManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ManagerAccount dao = new ManagerAccount();
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Please enter username to login!");
            }
            else if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter password to login!");
            }
            else
            {
                string roleName = dao.CheckLogin(username, password);
                string admin = "Admintrator";
                if (admin.Equals(roleName))
                {
                    MainManager main = new MainManager();
                    main.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Username or password is invalid, please again!");
                }
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
