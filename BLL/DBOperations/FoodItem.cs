using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data.Entity;
using BLL.DBOperations.TmpModels;
namespace BLL.DBOperations
{
    public class FoodItem
    {
        public static tbl_FoodItem getById(int id)
        {
            RMSDBEntities db = DBContext.getInstance();
            return db.tbl_FoodItem.Find(id);
        }
        public static void insert(tbl_FoodItem fooditem)
        {
            RMSDBEntities db = DBContext.getInstance();
            db.tbl_FoodItem.Add(fooditem);
            db.SaveChanges();
        }
        public static void delete(tbl_FoodItem fooditem)
        {
            RMSDBEntities db = DBContext.getInstance();
            db.tbl_FoodItem.Remove(fooditem);
            db.SaveChanges();
        }
        public static List<tbl_FoodItem> getAll()
        {
            RMSDBEntities db = DBContext.getInstance();
            return db.tbl_FoodItem.ToList();
        }
        public static void update(tbl_FoodItem fooditem)
        {
            RMSDBEntities db = DBContext.getInstance();
            db.Entry(fooditem).State = EntityState.Modified;
            db.Configuration.ValidateOnSaveEnabled = false;
            db.SaveChanges();
            db.Configuration.ValidateOnSaveEnabled = true;
        }
        public static void updateWithRecipeString(tbl_FoodItem fooditem, string recipe)
        {
            RMSDBEntities db = DBContext.getInstance();
            db.Entry(fooditem).State = EntityState.Modified;
            db.Configuration.ValidateOnSaveEnabled = false;
            db.SaveChanges();
            db.Configuration.ValidateOnSaveEnabled = true;
        }
        public static List<FoodItemSmallModel> getMappedListOfAllFoodItemToFoodItemSmallModel()
        {
            RMSDBEntities db = DBContext.getInstance();
            List<FoodItemSmallModel> list = new List<FoodItemSmallModel>();
            foreach (tbl_FoodItem item in getAll()) {
                FoodItemSmallModel listItem = new FoodItemSmallModel();
                listItem.Id = item.Id;
                listItem.Name = item.Name;
                listItem.Price = (int)item.SalePrice;
                list.Add(listItem);
            }
            return list;
        }
    }
}
