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
using System.Text.RegularExpressions;

namespace MiTTLibrary.PresentationLayer.UserControls
{
    /// <summary>
    /// Interaction logic for OrderDetailManagementByOrderIdUC.xaml
    /// </summary>
    public partial class OrderDetailsManagementByOrderIdUC : UserControl
    {
        OrderDetailsManager manager = new OrderDetailsManager();
        DataTable dataTable;
        string orderId;
        IEnumerable<string> enumerableProductId;
        DataTable productIdAndQuantityDataTable;
        public OrderDetailsManagementByOrderIdUC(string orderId)
        {
            InitializeComponent();
            this.orderId = orderId;
            loadData();
            

        }
        private void loadData()
        {
            try
            {
                dataTable = manager.readByOrderId(orderId);
                dataGrid.DataContext = dataTable.DefaultView;
                dataTable.Columns[0].ReadOnly = true;
                saveButton.IsEnabled = false;
                enumerableProductId = convertToIEnumerable(manager.readAllProductIdFromProductTable());
                productIdComboBox.ItemsSource = enumerableProductId;
                productIdAndQuantityDataTable = manager.selectProductIdAndQuantityFromProductTable();
                
                headLabel.Content = "Manage Details of Order Id " + orderId;
                dataGrid.SelectionMode = DataGridSelectionMode.Single;

            }
            catch (Exception)
            {
                MessageBox.Show("Load Data Fail");
            }

        }
        private IEnumerable<string> convertToIEnumerable(DataTable dataTable)
        {
            var list = new List<string>();
            foreach (DataRow row in dataTable.Rows)
            {
                list.Add(row["ID"].ToString());
            }
            return list.AsEnumerable();
        }
        private void reloadButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                loadData();
            }
            catch (Exception)
            {
                MessageBox.Show("ReLoad Fail");
            }

        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to save?", "Save", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                bool success = false;
                string s = "";
                try
                {
                    int newQuantity = 0;
                    int oldQuantity = 0;
                    int productQuantity = 0;
                    string productId;
                    bool valid = true;

                    DataTable orderdetailsQuantityDataTable;
                    List<int> rowIndexList = new List<int>();
                    for (int j = 0; j < enumerableProductId.Count(); j++)
                    {
                        newQuantity = 0;
                        oldQuantity = 0;
                        productQuantity = 0;
                        int rowIndex = -1;
                        productId = enumerableProductId.ElementAt(j);
                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {

                            if (dataTable.Rows[i][1].Equals(productId))
                            {

                                newQuantity += int.Parse(dataTable.Rows[i][2].ToString());
                                if (rowIndex == -1)
                                {
                                    rowIndex = i;
                                }
                                else
                                {
                                    Console.WriteLine(dataTable.Rows[i][2].ToString());
                                    rowIndexList.Add(i);
                                    dataTable.Rows[rowIndex][2] = newQuantity;
                                }

                            }
                        }

                        orderdetailsQuantityDataTable = manager.selectQuantityByOrderDetails(orderId, productId);
                        oldQuantity = orderdetailsQuantityDataTable == null ? 0 : int.Parse(orderdetailsQuantityDataTable.Rows[0][0].ToString());

                        for (int i = 0; i < productIdAndQuantityDataTable.Rows.Count; i++)
                        {
                            if (productIdAndQuantityDataTable.Rows[i][0].Equals(productId))
                            {
                                productQuantity = int.Parse(productIdAndQuantityDataTable.Rows[i][1].ToString());
                                productQuantity = productQuantity - (newQuantity - oldQuantity);
                                if (productQuantity >= 0)
                                {
                                    productIdAndQuantityDataTable.Rows[i][1] = productQuantity;
                                }
                                else
                                {
                                    valid = false;
                                    s = s + "*** Product is not enough quantity to sell! ***" + "\n";
                                    s = s + "Product Id: " + productId + "\n";
                                    s = s + "Product quantity (In stock): " + (productQuantity + (newQuantity - oldQuantity)) + "\n";
                                    s = s + "Product quantity (You want to sell): " + (newQuantity - oldQuantity) + "\n";
                                }
                                break;
                            }
                        }
                    }
                    if (valid)
                    {
                        for (int i = rowIndexList.Count - 1; i >= 0; i--)
                        {
                            dataTable.Rows[rowIndexList.ElementAt(i)].Delete();
                        }
                        success = manager.updateProductQuantityFromProductTable(productIdAndQuantityDataTable);
                        success = success && manager.update(dataTable);
                    }
                }
                catch (Exception)
                {
                }
                finally
                {
                    loadData();
                    saveButton.IsEnabled = false;
                    MessageBox.Show(s + "      Save " + (success ? "Success" : "Fail"));
                }
            }
            
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    DataRowView row = (DataRowView)dataGrid.SelectedItem;
                    DataRow dataRow = row.Row;
                    dataRow.Delete();
                    saveButton.IsEnabled = true;
                }
                
            }
            catch (Exception)
            {
                MessageBox.Show("Delete Fail");
            }

        }

        private void dataGrid_InitializingNewItem(object sender, InitializingNewItemEventArgs e)
        {
            try
            {
                DataRowView row = (DataRowView)e.NewItem;
                DataRow dataRow = row.Row;
                dataRow[0] = orderId;
                dataRow[1] = getNotDuplicatedProductId();
                dataRow[2] = 0;

            }
            catch (Exception)
            {
                MessageBox.Show("Fail");
            }

        }
        private string getNotDuplicatedProductId()
        {
            foreach (string item in enumerableProductId)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    if (dataTable.Rows[i][1].Equals(item))
                    {
                        break;
                    }
                    if (i == dataTable.Rows.Count - 1)
                    {
                        
                        return item;
                    }
                }
            }
            return null;
        }
        private void dataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                string header = e.Column.Header.ToString();
                if (header.Equals("Product Id"))
                {
                    ComboBox comboBox = (ComboBox)e.EditingElement;
                    string s = comboBox.Text;
                    if (string.IsNullOrEmpty(s))
                    {
                        MessageBox.Show("Product Id must be not NullOrEmpty");
                        e.Cancel = true;
                    }   
                }
                else if (header.Equals("Quantity"))
                {
                    TextBox textBox = (TextBox)e.EditingElement;
                    string s = textBox.Text;
                    if (string.IsNullOrEmpty(s))
                    {
                        MessageBox.Show("Quantity must be not NullOrEmpty");
                        e.Cancel = true;
                    }
                    else if (!Regex.IsMatch(s, "[0-9]"))
                    {
                        MessageBox.Show("Quantity must be number");
                        e.Cancel = true;
                    }
                    else
                    {

                    }

                }
                if (e.Cancel == false)
                {
                    saveButton.IsEnabled = true;
                }
                else
                {
                    saveButton.IsEnabled = false;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Fail");
            }


        }
    }
}
