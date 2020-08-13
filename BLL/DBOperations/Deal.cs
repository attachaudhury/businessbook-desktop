using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BLL.DBOperations.TmpModels;
namespace BLL.DBOperations

{
    public class Deal
    {
        public static void insert(string dealName, int salePrice, List<FoodItemSmallModel> list, bool manageInventory, int catId)
        {
            RMSDBEntities db = DBContext.getInstance();
            tbl_Deal deal = new tbl_Deal();
            deal.Name = dealName;
            deal.SalePrice = salePrice;
            deal.ManageInventory = manageInventory;
            deal.Category_Id = catId;
            db.tbl_Deal.Add(deal);
            db.SaveChanges();
            foreach(FoodItemSmallModel item in list)
            {
                tbl_DealFoodItem dfi = new tbl_DealFoodItem();
                dfi.Deal_Id = deal.Id;
                dfi.FoodItem_Id = item.Id;
                db.tbl_DealFoodItem.Add(dfi);
                db.SaveChanges();
            }
        }
        public static tbl_Deal getById(int dealId)
        {
            RMSDBEntities db = DBContext.getInstance();
            return db.tbl_Deal.Find(dealId);
        }
        public static List<FoodItemSmallModel> getMAppedListOfDealDetailsToFoodItemSmallModelByDealId(int dealId)
        {
            RMSDBEntities db = DBContext.getInstance();
            List<tbl_DealFoodItem> list = db.tbl_DealFoodItem.Where(a => a.Deal_Id == dealId).ToList();
            List<FoodItemSmallModel> mappedList = new List<FoodItemSmallModel>();
            foreach(tbl_DealFoodItem item in list)
            {
                FoodItemSmallModel tmp = new FoodItemSmallModel();
                tmp.Id = (int)item.FoodItem_Id;
                tmp.Name = db.tbl_FoodItem.Find(item.FoodItem_Id).Name;
                tmp.Price = (int)db.tbl_FoodItem.Find(item.FoodItem_Id).SalePrice;
                mappedList.Add(tmp);
            }
            return mappedList;
        }
        public static void delete(int dealId)
        {
            RMSDBEntities db = DBContext.getInstance();
            tbl_Deal d = getById(dealId);
            db.tbl_Deal.Remove(d);
            db.SaveChanges();
            List<tbl_DealFoodItem> list = db.tbl_DealFoodItem.Where(a => a.Deal_Id == dealId).ToList();
            db.tbl_DealFoodItem.RemoveRange(list);
            db.SaveChanges();
        }
        public static void update(tbl_Deal deal, List<FoodItemSmallModel> list)
        {
            int dealId = deal.Id;
            RMSDBEntities db = DBContext.getInstance();
            db.Entry(deal).State = System.Data.Entity.EntityState.Modified;
            db.Configuration.ValidateOnSaveEnabled = false;
            db.SaveChanges();
            db.Configuration.ValidateOnSaveEnabled = true;
            List<tbl_DealFoodItem> oldlist = db.tbl_DealFoodItem.Where(a => a.Deal_Id == dealId).ToList();
            db.tbl_DealFoodItem.RemoveRange(oldlist);
            db.SaveChanges();
            foreach (FoodItemSmallModel item in list)
            {
                tbl_DealFoodItem dfi = new tbl_DealFoodItem();
                dfi.Deal_Id = deal.Id;
                dfi.FoodItem_Id = item.Id;
                db.tbl_DealFoodItem.Add(dfi);
                db.SaveChanges();
            }
        }
        public static List<tbl_Deal> getAll()
        {
            RMSDBEntities db = DBContext.getInstance();
            return db.tbl_Deal.ToList();
        }
    }
}
