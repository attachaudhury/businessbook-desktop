using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data.Entity;

namespace BLL.DBOperations
{
    public class KitchenInventoryCategory
    {
        public static void insert(tbl_KitchenInventoryCategory kic) 
        {
            RMSDBEntities db = DBContext.getInstance();
            db.tbl_KitchenInventoryCategory.Add(kic);
            db.SaveChanges();
        }
        public static void delete(tbl_KitchenInventoryCategory kic)
        {
            RMSDBEntities db = DBContext.getInstance();
            db.tbl_KitchenInventoryCategory.Remove(kic);
            db.SaveChanges();
        }
        public static void update(tbl_KitchenInventoryCategory kic)
        {
            RMSDBEntities db = DBContext.getInstance();
            db.Entry(kic).State = EntityState.Modified;
            db.Configuration.ValidateOnSaveEnabled = false;
            db.SaveChanges();
            db.Configuration.ValidateOnSaveEnabled = true;
        }
        public static List<tbl_KitchenInventoryCategory> getAll()
        {
            RMSDBEntities db = DBContext.getInstance();
            return db.tbl_KitchenInventoryCategory.ToList();
        }
    }
}
