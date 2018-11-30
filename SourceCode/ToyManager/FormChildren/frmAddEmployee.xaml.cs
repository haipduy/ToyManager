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
using ToyLibrary;
using System.Text.RegularExpressions;

namespace ToyManager.FormChildren
{
    /// <summary>
    /// Interaction logic for frmAddEmployee.xaml
    /// </summary>
    public partial class frmAddEmployee : Window
    {
        ManagerAccount dao = new ManagerAccount();

        public frmAddEmployee()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            string id = TxtID.Text;
            string name = TxtFullname.Text;
            string password = TxtPassword.Password;
            string startdate = TxtStartdate.Text;
            string address = TxtAddress.Text;
            string email = TxtEmail.Text;
            string phone = TxtPhone.Text;
            string sex = CbSex.Text;
            Regex regex = new Regex("[0-9]");
            if (string.IsNullOrWhiteSpace(id))
            {
                MessageBox.Show("Please input id!");
            }
            else if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Please input name!");
            }
            else if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please input Password!");
            }
            else if (string.IsNullOrWhiteSpace(startdate))
            {
                MessageBox.Show("Please input date start!");
            }
            else if (string.IsNullOrWhiteSpace(address))
            {
                MessageBox.Show("Please input address!");
            }
            else if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Please input email!");
            }
            else if (string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Please input phone!");
            }
            else if (!regex.IsMatch(phone))
            {
                MessageBox.Show("Phone must be a number!");
            }
            else if (string.IsNullOrWhiteSpace(sex))
            {
                MessageBox.Show("Please input sex!");
            }
            else
            {
                string checkID = dao.GetID(id);
                if (checkID == null)
                {
                    bool result = dao.AddNewEmployee(id, password, name, address, email, startdate, sex, phone);
                    if (result)
                    {
                        MessageBox.Show("Add new Employee Successfully!");
                    }
                    else
                    {
                        MessageBox.Show("Add new Employee Fail!");
                    }
                }
                else
                {
                    MessageBox.Show("User ID is exist!");

                }

            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
