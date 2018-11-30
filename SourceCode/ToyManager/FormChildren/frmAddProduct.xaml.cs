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
using System.Data;
using System.Text.RegularExpressions;


namespace ToyManager.FormChildren
{
    /// <summary>
    /// Interaction logic for frmAddProduct.xaml
    /// </summary>
    public partial class frmAddProduct : Window
    {
        public Product MyProduct { get; set; }
        ProductDAO dao = new ProductDAO();
        public frmAddProduct()
        {
            InitializeComponent();
            DataTable dsCate = dao.LoadCategory();
            CbCategory.ItemsSource = dsCate.DefaultView;
            CbCategory.DisplayMemberPath = "CategoryName";
            DataTable dsSup = dao.LoadSupllier();
            CbSupllier.ItemsSource = dsSup.DefaultView;
            CbSupllier.DisplayMemberPath = "SupplierName";
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string id = TxtProductID.Text;
            string name = TxtProductname.Text;
            string price = TxtPrice.Text;
            string quantity = TxtQuantity.Text;
            string description = TxtDescription.Text;
            string categoryname = CbCategory.Text;
            string suplliername = CbSupllier.Text;
            Regex regex = new Regex("[0-9]");
            if (string.IsNullOrWhiteSpace(id))
            {
                MessageBox.Show("Please input id!");
            }
            else if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Please input name!");
            }
            else if (string.IsNullOrWhiteSpace(price))
            {
                MessageBox.Show("Please input Price!");
            }
            else if (!regex.IsMatch(price))
            {
                MessageBox.Show("Price must be a number!");
            }
            else if (string.IsNullOrWhiteSpace(quantity))
            {
                MessageBox.Show("Please input Quantity!");
            }
            else if (!regex.IsMatch(quantity))
            {
                MessageBox.Show("Quantity must be a number!");
            }
            else if (string.IsNullOrWhiteSpace(description))
            {
                MessageBox.Show("Please input date Description!");
            }
            else if (string.IsNullOrWhiteSpace(categoryname))
            {
                MessageBox.Show("Please Select category!");
            }
            else if (string.IsNullOrWhiteSpace(suplliername))
            {
                MessageBox.Show("Please Select Supllier!");
            }
            else
            {
                string categoryID = dao.GetCategoryID(categoryname);
                string SupID = dao.GetSupllierID(suplliername);
                string checkID = dao.GetProductID(id);
                if (checkID == null)
                {
                    bool result = dao.AddNewProduct(id, name, price, quantity, description, categoryID, SupID);
                    if (result)
                    {
                        MessageBox.Show("Add new product Successfull!");
                    }
                    else
                    {
                        MessageBox.Show("Add new product Successfull!");
                    }
                }
                else
                {
                    MessageBox.Show("Product ID is exist!");
                }

            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
