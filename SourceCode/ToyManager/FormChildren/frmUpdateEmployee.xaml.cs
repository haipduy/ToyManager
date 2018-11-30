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
    /// Interaction logic for frmUpdateEmployee.xaml
    /// </summary>
    public partial class frmUpdateEmployee : Window
    {
        ManagerAccount dao = new ManagerAccount();
        public Employee EmployeeEdit { get; set; }
        public frmUpdateEmployee(Employee emp)
        {
            EmployeeEdit = emp;
            InitializeComponent();
            InitData();
        }
        public void InitData()
        {
            TxtID.Text = EmployeeEdit.ID.ToString();
            TxtFullname.Text = EmployeeEdit.FullName.ToString();
            TxtAddress.Text = EmployeeEdit.Address.ToString();
            TxtPhone.Text = EmployeeEdit.Phone.ToString();
            TxtStartdate.Text = EmployeeEdit.StartDate.ToString();
            TxtEmail.Text = EmployeeEdit.Email.ToString();
            CbSex.Text = EmployeeEdit.Sex.ToString();

        }

        private void btnSetDef_Click(object sender, RoutedEventArgs e)
        {
            string id = TxtID.Text;
            bool result = dao.setPasswordDefault(id);
            if (result)
            {
                MessageBox.Show("Set default Successfully!");
            }
            else
            {
                MessageBox.Show("Set Fail!");
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string id = TxtID.Text;
            string name = TxtFullname.Text;
            string startdate = TxtStartdate.Text;
            string address = TxtAddress.Text;
            string email = TxtEmail.Text;
            string phone = TxtPhone.Text;
            string sex = CbSex.Text;
            Regex regex = new Regex("[0-9]");
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Please input name!");
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
                bool result = dao.UpdteEmployee(id, name, startdate, address, email, phone, sex);
                if (result)
                {
                    MessageBox.Show("Update Successfully!");
                }
                else
                {
                    MessageBox.Show("Update Fail!");
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
