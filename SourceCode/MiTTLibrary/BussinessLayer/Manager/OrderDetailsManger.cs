using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace MiTTLibrary
{
    public class OrderDetailsManager : IManager
    {
        OrderDetailsDAO dao = new OrderDetailsDAO();
        public DataTable readAll()
        {
            return dao.readAll();
        }
        public bool update(DataTable dataTable)
        {
            return dao.update(dataTable);
        }
        
        public DataTable read1ByOrderId(string orderId)
        {
            return dao.read1ByOrderId(orderId);
        }
        public DataTable readByOrderId(string orderId)
        {
            return dao.readByOrderId(orderId);
        }

        public DataTable readByProductId(string productId)
        {
            return dao.readByProductId(productId);
        }
        public DataTable readAllProductIdFromProductTable()
        {
            return dao.readAllProductIdFromProductTable();
        }
        public DataTable readQuantityFromProductTableById(string productId)
        {
            return dao.readQuantityFromProductTableById(productId);
        }
        public DataTable selectProductIdAndQuantityFromProductTable()
        {
            return dao.selectProductIdAndQuantityFromProductTable();
        }
        public DataTable selectQuantityByOrderDetails(string orderId,string productId)
        {
            return dao.selectQuantityByOrderDetails(orderId,productId);
        }
        public bool updateProductQuantityFromProductTable(DataTable dataTable)
        {
            return dao.updateProductQuantityFromProductTable(dataTable);
        }
    }
}
