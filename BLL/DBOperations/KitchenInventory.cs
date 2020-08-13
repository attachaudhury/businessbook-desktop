using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data.Entity;

namespace BLL.DBOperations
{
    public class KitchenInventory
    {
        public static tbl_KitchenInventory getById(int id)
        {
            RMSDBEntities db = DBContext.getInstance();
            return db.tbl_KitchenInventory.Find(id);
        }
        public static void insert(tbl_KitchenInventory ki) 
        {
            RMSDBEntities db = DBContext.getInstance();
            db.tbl_KitchenInventory.Add(ki);
            db.SaveChanges();
        }
        public static void delete(tbl_KitchenInventory ki)
        {
            RMSDBEntities db = DBContext.getInstance();
            db.tbl_KitchenInventory.Remove(ki);
            db.SaveChanges();
        }
        public static void update(tbl_KitchenInventory ki)
        {
            RMSDBEntities db = DBContext.getInstance();
            db.Entry(ki).State = EntityState.Modified;
            db.Configuration.ValidateOnSaveEnabled = false;
            db.SaveChanges();
            db.Configuration.ValidateOnSaveEnabled = true;
        }
        public static List<tbl_KitchenInventory> getAll()
        {
            RMSDBEntities db = DBContext.getInstance();
            return db.tbl_KitchenInventory.ToList();
        }
        public static List<tbl_KitchenInventory> getLowInventoryItem()
        {
            RMSDBEntities db = DBContext.getInstance();
            return db.tbl_KitchenInventory.Where(a=>a.Quantity <= a.MinimumQuantity).ToList();
        }
    }
}
