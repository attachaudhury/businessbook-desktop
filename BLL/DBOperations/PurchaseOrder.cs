using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DBOperations
{
    class PurchaseOrder
    {
        public static void insert(tbl_PurchaseOrder po)
        {
            RMSDBEntities db = DBContext.getInstance();
            db.tbl_PurchaseOrder.Add(po);
            db.SaveChanges();
        }
        public static void delete(tbl_PurchaseOrder po)
        {
            RMSDBEntities db = DBContext.getInstance();
            db.tbl_PurchaseOrder.Remove(po);
            db.SaveChanges();
        }
        public static List<tbl_PurchaseOrder> getAll()
        {
            RMSDBEntities db = DBContext.getInstance();
            return db.tbl_PurchaseOrder.ToList();
        }
    }
}
