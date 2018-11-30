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

namespace ToyManager.FormChildren
{
    /// <summary>
    /// Interaction logic for frmUpdateProduct.xaml
    /// </summary>
    public partial class frmUpdateProduct : Window
    {
        public Product MyProduct { get; set; }
        ProductDAO dao = new ProductDAO();
        public frmUpdateProduct(Product p)
        {
            MyProduct = p;
            InitializeComponent();
            InitData();
        }
        public void InitData()
        {
            DataTable dsCate = dao.LoadCategory();
            CbCategory.ItemsSource = dsCate.DefaultView;
            CbCategory.DisplayMemberPath = "CategoryName";
            DataTable dsSup =  dao.LoadSupllier();
            CbSupllier.ItemsSource = dsSup.DefaultView;
            CbSupllier.DisplayMemberPath = "SupplierName";

            TxtProductID.Text = MyProduct.ProdictID.ToString();
            TxtProductname.Text = MyProduct.ProductName.ToString();
            TxtPrice.Text = MyProduct.Price.ToString();
            TxtQuantity.Text = MyProduct.Quantity.ToString();
            TxtDescription.Text = MyProduct.Description.ToString();
            CbCategory.Text = MyProduct.CategoryName.ToString();
            CbSupllier.Text = MyProduct.SupllierName.ToString();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            string id = TxtProductID.Text;
            string name = TxtProductname.Text;
            string price = TxtPrice.Text;
            string quantity = TxtQuantity.Text;
            string categoryname = CbCategory.Text;
            string supliername = CbSupllier.Text;
            string desciption = TxtDescription.Text;
            string cateId = dao.GetCategoryID(categoryname);
            string supId = dao.GetSupllierID(supliername);
            bool result = dao.UpdateProduct(id, name, price, quantity, desciption, cateId, supId);
            if (result)
            {
                MessageBox.Show("Update product successfully!");
            }
            else
            {
                MessageBox.Show("Update product fail!");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
