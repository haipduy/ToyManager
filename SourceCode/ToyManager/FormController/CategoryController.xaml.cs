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
using System.Data.SqlClient;

namespace ToyManager.FormController
{
    /// <summary>
    /// Interaction logic for CategoryController.xaml
    /// </summary>
    public partial class CategoryController : UserControl
    {
        DataTable dtCate = null;
        CategoryDAO dao = new CategoryDAO();

        public CategoryController()
        {
            InitializeComponent();
            GetAllcategory();
        }
        public void GetAllcategory()
        {
            dtCate = dao.GetallCategory();
            dgCategory.ItemsSource = dtCate.DefaultView;
        }
      

        private void btnUpdatecategory_Click(object sender, RoutedEventArgs e)
        {
            string id = TxtcategoryID.Text;
            string name = TxtcategoryName.Text;
            if (string.IsNullOrWhiteSpace(id))
            {
                MessageBox.Show("Id is invalid. Please again!");
            }
            else if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Name is invalid. Please again!");
            }
            else
            {
                //check id

                // insert into DB
                bool result = dao.UpdateCategory(id, name);
                if (result)
                {
                    MessageBox.Show("Update category successful");
                    GetAllcategory();
                }
                else
                {
                    MessageBox.Show("Update category fail. Please again!");
                }
            }
        }

        private void btnAddCategory_Click(object sender, RoutedEventArgs e)
        {
            string id = TxtcategoryID.Text;
            string name = TxtcategoryName.Text;
            if (string.IsNullOrWhiteSpace(id))
            {
                MessageBox.Show("Id is invalid. Please again!");
            }
            else if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Name is invalid. Please again!");
            }
            else
            {
                //check id
                string checkID = dao.GetCategoryID(id);
                if (checkID == null)
                {
                    bool result = dao.InsertCategory(id, name);
                    if (result)
                    {
                        MessageBox.Show("Add new category successful");
                        GetAllcategory();
                    }
                    else
                    {
                        MessageBox.Show("Add new category fail. Please again!");
                    }
                }
                else
                {
                    MessageBox.Show("ID category  is exist. Please again!");
                }
                // insert into DB
            }
        }

        private void btnRemoveCategory_Click(object sender, RoutedEventArgs e)
        {
            string id = TxtcategoryID.Text;
            if (string.IsNullOrWhiteSpace(id))
            {
                MessageBox.Show("Id is invalid. Please again!");
            }
            else
            {
                bool result = dao.DeleteCategory(id);
                if (result)
                {
                    MessageBox.Show("Delete category successful");
                    GetAllcategory();
                }
                else
                {
                    MessageBox.Show("Delete category fail. Please again!");
                }
            }
        }
    }
}
