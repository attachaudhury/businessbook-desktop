using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data.Entity;
namespace BLL.DBOperations
{
    public class FoodItemCategory
    {
        public static tbl_FoodItemCategory getById(int id)
        {
            RMSDBEntities db = DBContext.getInstance();
            return db.tbl_FoodItemCategory.Find(id);
        }
        public static void insert(tbl_FoodItemCategory category)
        {
            RMSDBEntities db = DBContext.getInstance();
            db.tbl_FoodItemCategory.Add(category);
            db.SaveChanges();
        }
        public static void delete(tbl_FoodItemCategory categroy)
        {
            RMSDBEntities db = DBContext.getInstance();
            db.tbl_FoodItemCategory.Remove(categroy);
            db.SaveChanges();
        }
        public static List<tbl_FoodItemCategory> getAll()
        {
            RMSDBEntities db = DBContext.getInstance();
            return db.tbl_FoodItemCategory.ToList();
        }
        public static void update(tbl_FoodItemCategory category)
        {
            RMSDBEntities db = DBContext.getInstance();
            db.Entry(category).State = EntityState.Modified;
            db.Configuration.ValidateOnSaveEnabled = false;
            db.SaveChanges();
            db.Configuration.ValidateOnSaveEnabled = true;
        }
    }
}
