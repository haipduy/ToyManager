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
namespace MiTTLibrary.PresentationLayer.UserControls
{
    /// <summary>
    /// Interaction logic for OrderDetailsViewUC.xaml
    /// </summary>
    public partial class OrderDetailsViewUC : UserControl
    {
        OrderDetailsManager manager = new OrderDetailsManager();
        DataTable dataTable;
        string orderId;
        float orderTotal;
        public OrderDetailsViewUC(string orderId)
        {
            InitializeComponent();
            this.orderId = orderId;
            headLabel.Content = "View Details of Order Id " + orderId;
            loadData();
        }

        private void loadData()
        {
            orderTotal = 0;
            dataTable = manager.read1ByOrderId(this.orderId);
            changeColumnsName();
            DataColumn dataColumn = dataTable.Columns.Add("Total", typeof(float));
            foreach (DataRow dataRow in dataTable.Rows)
            {
                dataRow["Total"] = int.Parse(dataRow["Quantity"].ToString()) * float.Parse(dataRow["Price"].ToString());
                orderTotal += float.Parse(dataRow["Total"].ToString());
            }
            orderTotalValueLabel.Content = orderTotal;
            dataGrid.DataContext = dataTable.DefaultView;
        }
        private void changeColumnsName()
        {
            dataTable.Columns[0].ColumnName = "Product Id";
            dataTable.Columns[1].ColumnName = "Product Name";
            dataTable.Columns[2].ColumnName = "Quantity";
            dataTable.Columns[3].ColumnName = "Price";
        }
    }
}
