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
using System.Collections;
using System.Windows;
using MessageBox = System.Windows.MessageBox;

namespace MiTTLibrary.PresentationLayer.UserControls
{
    /// <summary>
    /// Interaction logic for OrderManagementUC.xaml
    /// </summary>
    public partial class OrderManagementUC : UserControl
    {
        OrderManager manager = new OrderManager();
        DataTable dataTable;
        IEnumerable<string> enumerableUserId;
        string orderId;
        public OrderManagementUC()
        {
            InitializeComponent();
            loadData();
            dataGrid.SelectionMode = DataGridSelectionMode.Single;
        }
        private void loadData()
        {
            try
            {
                dataTable = manager.readAll();
                dataGrid.DataContext = dataTable.DefaultView;
                saveButton.IsEnabled = false;
                enumerableUserId = convertToIEnumerable(manager.readAllUserIdFromAccountTable());
                userIdComboBox.ItemsSource = enumerableUserId;
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
                list.Add(row["UserId"].ToString());
            }
            return list.AsEnumerable();
        }
        private void reloadButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                loadData();
                if (orderId!=null)
                {
                    addOrderDetailsUserControl(orderId);
                }
                
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
                try
                {
                    success = manager.update(dataTable);
                    saveButton.IsEnabled = false;
                }
                catch (Exception)
                {
                }
                finally
                {
                    string s = (success ? "Successful" : "Fail");
                    MessageBox.Show("Save " + s);
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

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            bool success = false;
            try
            {
                string orderId = idTextBox.Text;
                foreach (DataRowView dataRowView in (DataView)dataGrid.ItemsSource)
                    if (dataRowView["Id"].Equals(orderId))
                    {
                        dataGrid.SelectedItem = dataRowView;
                        dataGrid.Focus();
                        success = true;
                        break;
                    }
                if (!success)
                {
                    MessageBox.Show("Not found");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Search Fail");
            }


        }


        private void manageOrderDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int rowIndex = dataGrid.SelectedIndex;
                this.orderId = dataTable.Rows[rowIndex][0].ToString();
                addOrderDetailsUserControl(orderId);
            }
            catch (Exception)
            {
                MessageBox.Show("Manage Fail");
            }
        }
        private void addOrderDetailsUserControl(string orderId)
        {
            managementGrid.Children.Clear();
            viewGrid.Children.Clear();
            managementGrid.Children.Add(new OrderDetailsManagementByOrderIdUC(orderId));
            viewGrid.Children.Add(new OrderDetailsViewUC(orderId));
        }
        private void dataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                string header = e.Column.Header.ToString();
                if (header.Equals("Order Id"))
                {
                    TextBox textBox = (TextBox)e.EditingElement;
                    string s = textBox.Text;
                    if (string.IsNullOrEmpty(s))
                    {
                        MessageBox.Show("Order Id must be not NullOrEmpty");
                        e.Cancel = true;
                    }
                }
                else if (header.Equals("User Id"))
                {
                    ComboBox comboBox = (ComboBox)e.EditingElement;
                    string s = comboBox.Text;
                    if (string.IsNullOrEmpty(s))
                    {
                        MessageBox.Show("User Id must be not NullOrEmpty");
                        e.Cancel = true;
                    }
                }
                else if (header.Equals("Customer Name"))
                {
                    TextBox textBox = (TextBox)e.EditingElement;
                    string s = textBox.Text;
                    if (string.IsNullOrEmpty(s))
                    {
                        MessageBox.Show("Customer Name must be not NullOrEmpty");
                        e.Cancel = true;
                    }
                }
                else if (header.Equals("Receiving Phone"))
                {
                    TextBox textBox = (TextBox)e.EditingElement;
                    string s = textBox.Text;
                    if (string.IsNullOrEmpty(s))
                    {
                        MessageBox.Show("Receiving Phone must be not NullOrEmpty");
                        e.Cancel = true;
                    }
                    else if (!Regex.IsMatch(s, "[0-9]"))
                    {
                        MessageBox.Show("Receiving Phone must be number");
                        e.Cancel = true;
                    }
                }
                else if (header.Equals("Receiving Address"))
                {
                    TextBox textBox = (TextBox)e.EditingElement;
                    string s = textBox.Text;
                    if (string.IsNullOrEmpty(s))
                    {
                        MessageBox.Show("Receiving Address must be not NullOrEmpty");
                        e.Cancel = true;
                    }
                }
                else if (header.Equals("Note"))
                {
                    TextBox textBox = (TextBox)e.EditingElement;
                    string s = textBox.Text;
                    if (string.IsNullOrEmpty(s))
                    {
                        MessageBox.Show("Note must be not NullOrEmpty");
                        e.Cancel = true;
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
                MessageBox.Show("dataGrid_CellEditEnding Fail");
            }


        }

        private void dataGrid_InitializingNewItem(object sender, InitializingNewItemEventArgs e)
        {
            try
            {
                DataRowView row = (DataRowView)e.NewItem;
                DataRow dataRow = row.Row;
                dataRow[7] = false;
                dataRow[6] = DateTime.Now;
                dataRow[1] = enumerableUserId.ElementAt(0);
            }
            catch (Exception)
            {
                MessageBox.Show("dataGrid_InitializingNewItem Fail");
            }
        }


    }
}
