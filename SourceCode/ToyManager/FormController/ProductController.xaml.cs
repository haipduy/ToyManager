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
using ToyManager.FormChildren;


namespace ToyManager.FormController
{
    /// <summary>
    /// Interaction logic for ProductController.xaml
    /// </summary>
    public partial class ProductController : UserControl
    {
        ProductDAO dao = new ProductDAO();
        DataTable dtProduct;
        public ProductController()
        {
            InitializeComponent();
            GetAllproduct();
        }
        public void GetAllproduct()
        {
            dtProduct = dao.GetAllProduct();
            dgProduct.ItemsSource = dtProduct.DefaultView;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            frmAddProduct addproduct = new frmAddProduct();
            addproduct.ShowDialog();
            GetAllproduct();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            string id = TxtProductId.Text;
            if (string.IsNullOrWhiteSpace(id))
            {
                MessageBox.Show("Please choice Product to update!");
            }
            else
            {
                string name = TxtProductname.Text;
                string price = TxtPrice.Text;
                string quantity = TxtQuantity.Text;
                string categoryname = CbCategoryname.Text;
                string supliername = CbSupliiername.Text;
                string desciption = TxtDescription.Text;
                Product p = new Product
                {
                    ProdictID = id,
                    ProductName = name,
                    Price = price,
                    Quantity = quantity,
                    CategoryName = categoryname,
                    SupllierName = supliername,
                    Description = desciption

                };
                frmUpdateProduct updateproduct = new frmUpdateProduct(p);
                updateproduct.ShowDialog();
                GetAllproduct();
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            string id = TxtProductId.Text;
            if (id != null)
            {
                bool result = dao.removeProduct(id);
                if (result)
                {
                    MessageBox.Show("Remove Product Successfull!");
                    GetAllproduct();
                }
                else
                {
                    MessageBox.Show("Remove Product Fail!");
                }
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string name = TxtFilter.Text;
            DataTable dtPro = dao.GetAllProductLikeName(name);
            int count = dtPro.Rows.Count;
            if (count == 0)
            {
                MessageBox.Show("Not found!");
            }
            else
            {
                dgProduct.ItemsSource = dtPro.DefaultView;
            }

        }
    }
}
