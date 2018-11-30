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
using System.Data;

namespace ToyManager.FormController
{
    /// <summary>
    /// Interaction logic for SupllierContoller.xaml
    /// </summary>
    public partial class SupllierContoller : UserControl
    {
        SupllierDAO dao = new SupllierDAO();
        DataTable dtSup;
        public SupllierContoller()
        {
            InitializeComponent();
            GetAllSupllier();
        }
        public void GetAllSupllier()
        {
            dtSup = dao.GetallSupllier();
            dgSupllier.ItemsSource = dtSup.DefaultView;
        }

        private void btnAddSup_Click(object sender, RoutedEventArgs e)
        {
            string id = TxtSupID.Text;
            string name = TxtSubName.Text;
            string address = TxtSubAddress.Text;
            string phone = TxtSubPhone.Text;
            if (string.IsNullOrWhiteSpace(id))
            {
                MessageBox.Show("Id is invalid. Please again!");
            }
            else if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Name is invalid. Please again!");
            }
            else if (string.IsNullOrWhiteSpace(address))
            {
                MessageBox.Show("address is invalid. Please again!");
            }
            else if (string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("phone is invalid. Please again!");
            }
            else
            {
                //check id
                string SuppID = dao.GetSupllierID(id);
                if (SuppID == null)
                {
                    bool result = dao.InsertSupllier(id, name, address, phone);
                    if (result)
                    {
                        MessageBox.Show("Add new Supllier successful");
                        GetAllSupllier();
                    }
                    else
                    {
                        MessageBox.Show("Add new Supllier fail. Please again!");
                    }
                }
                else
                {
                    MessageBox.Show("Supllier is exist. Please again!");
                }
            }
        }

        private void btnUpdateSup_Click(object sender, RoutedEventArgs e)
        {
            string id = TxtSupID.Text;
            string name = TxtSubName.Text;
            string address = TxtSubAddress.Text;
            string phone = TxtSubPhone.Text;
            if (string.IsNullOrWhiteSpace(id))
            {
                MessageBox.Show("Id is invalid. Please again!");
            }
            else if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Name is invalid. Please again!");
            }
            else if (string.IsNullOrWhiteSpace(address))
            {
                MessageBox.Show("address is invalid. Please again!");
            }
            else if (string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("phone is invalid. Please again!");
            }
            else
            {
                bool result = dao.UpdateSupllier(id, name, address, phone);
                if (result)
                {
                    MessageBox.Show("Update category successful");
                    GetAllSupllier();
                }
                else
                {
                    MessageBox.Show("Update category fail. Please again!");
                }
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            string id = TxtSupID.Text;
            if (string.IsNullOrWhiteSpace(id))
            {
                MessageBox.Show("Id is invalid. Please again!");
            }
            else
            {
                bool result = dao.DeleteCategory(id);
                if (result)
                {
                    MessageBox.Show("Delete Supllier successful");
                    GetAllSupllier();
                }
                else
                {
                    MessageBox.Show("Delete Supllier fail. Please again!");
                }
            }
        }
    }
}
