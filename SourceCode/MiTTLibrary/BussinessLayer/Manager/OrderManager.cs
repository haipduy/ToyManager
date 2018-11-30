using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace MiTTLibrary
{
    public class OrderManager : IManager
    {
        OrderDAO dao = new OrderDAO();
        public DataTable readAll()
        {
            return dao.readAll();
        }
        public bool update(DataTable dataTable)
        {
            return dao.update(dataTable);
        }
        public DataTable readAllUserIdFromAccountTable()
        {
            return dao.readAllUserIdFromAccountTable();
        }
    }
}
