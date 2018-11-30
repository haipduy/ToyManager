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
using System.Data;
using ToyLibrary;
using ToyManager.FormChildren;

namespace ToyManager.FormController
{
    /// <summary>
    /// Interaction logic for EmployeeController.xaml
    /// </summary>
    public partial class EmployeeController : UserControl
    {
        ManagerAccount dao = new ManagerAccount();
        DataTable dtEmp = null;
        public EmployeeController()
        {
            InitializeComponent();
            GetAllEmployee();
        }
        public void GetAllEmployee()
        {
            dtEmp = dao.GetAllEmployee();
            dgEmployee.ItemsSource = dtEmp.DefaultView;
        }

        private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            frmAddEmployee AddEmp = new frmAddEmployee();
            AddEmp.ShowDialog();
            GetAllEmployee();
        }

        private void btnUpdateEmp_Click(object sender, RoutedEventArgs e)
        {
            string id = TxtID.Text;

            if (string.IsNullOrWhiteSpace(id))
            {
                MessageBox.Show("Please choice employee to update!");
            }
            else
            {
                string name = TxtFullname.Text;
                DateTime stardate = DateTime.Parse(TxtStartdate.Text);
                string address = TxtAddress.Text;
                string email = TxtEmail.Text;
                string phone = TxtPhone.Text;
                string sex = CbSex.Text;
                Employee emp = new Employee
                {
                    ID = id,
                    FullName = name,
                    StartDate = stardate,
                    Address = address,
                    Email = email,
                    Phone = phone,
                    Sex = sex
                };
                frmUpdateEmployee UpdateEmp = new frmUpdateEmployee(emp);
                UpdateEmp.ShowDialog();
                GetAllEmployee();
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            string id = TxtID.Text;
            if (id != null)
            {
                bool result = dao.RemoveEmployee(id);
                if (result)
                {
                    MessageBox.Show("Remove Employee Successfull!");
                    GetAllEmployee();
                }
                else
                {
                    MessageBox.Show("Remove Employee Fail!");
                }
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string name = TxtFilter.Text;
            DataTable dtEmp = dao.GetAllEmployeeLikeName(name);
            int count = dtEmp.Rows.Count;
            if (count == 0)
            {
                MessageBox.Show("Not found!");
            }
            else
            {
                dgEmployee.ItemsSource = dtEmp.DefaultView;
            }
        }
    }
}
