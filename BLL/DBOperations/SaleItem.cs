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
    public class SaleItem
    {
        public static void insert(tbl_SaleItem saleItem)
        {
            RMSDBEntities db = DBContext.getInstance();
            db.tbl_SaleItem.Add(saleItem);
            db.SaveChanges();
        }
        public static List<tbl_SaleItem> getAllBySaleId(int saleId)
        {
            RMSDBEntities db = DBContext.getInstance();
            return db.tbl_SaleItem.Where(a=>a.Sale_id == saleId).ToList();
        }
        public static void deleteSaleItemsBySaleId(int saleId)
        {
            RMSDBEntities db = DBContext.getInstance();
            List<tbl_SaleItem> list = db.tbl_SaleItem.Where(a => a.Sale_id == saleId).ToList();
            db.tbl_SaleItem.RemoveRange(list);
            db.SaveChanges();
        }
        
        public static List<tbl_SaleItem> getAll()
        {
            RMSDBEntities db = DBContext.getInstance();
            return db.tbl_SaleItem.ToList();
        }
        public static List<tbl_SaleItem> getAllByDate(DateTime fromDate, DateTime toDate)
        {
            RMSDBEntities db = DBContext.getInstance();
            return db.tbl_SaleItem.Where(a=>a.tbl_Sale.Date_Time>=fromDate).Where(a => a.tbl_Sale.Date_Time <= toDate).ToList();
        }
        public static List<FoodItemViewModelForDSR> getAllMappedToFoodITemViewModelForDSR()
        {
            List<tbl_SaleItem> soldItems = getAll();
            List<FoodItemViewModelForDSR> mappedList = new List<FoodItemViewModelForDSR>();
            foreach (tbl_SaleItem soldItem in soldItems)
            {
                FoodItemViewModelForDSR item = new FoodItemViewModelForDSR();
                item.Id = (int)soldItem.Item_id;
                item.Quantity = (int)soldItem.Quantity;
                item.SaleId = (int)soldItem.Sale_id;
                item.Date = (DateTime)soldItem.tbl_Sale.Date_Time;
                
                if (item.Id >= 20000)
                {
                    tbl_Deal deal = Deal.getById(item.Id);
                    item.Name = deal.Name;
                    item.SalePrice = (int)deal.SalePrice;
                    item.CategoryName = deal.tbl_FoodItemCategory.Name;
                    item.Total = item.SalePrice * item.Quantity;
                    mappedList.Add(item);
                }
                else
                {
                    tbl_FoodItem foodItem = FoodItem.getById(item.Id);
                    item.Name = foodItem.Name;
                    item.SalePrice = (int)foodItem.SalePrice;
                    item.CategoryName = foodItem.tbl_FoodItemCategory.Name;
                    item.Total = item.SalePrice * item.Quantity;
                    mappedList.Add(item);
                }
            }
            return mappedList;
        }
        public static List<FoodItemViewModelForDSR> getAllMappedToFoodITemViewModelForDSRByDate(DateTime fromDate,DateTime toDate)
        {
            List<tbl_SaleItem> soldItems = getAllByDate(fromDate,toDate);
            List<FoodItemViewModelForDSR> mappedList = new List<FoodItemViewModelForDSR>();
            foreach (tbl_SaleItem soldItem in soldItems)
            {
                FoodItemViewModelForDSR item = new FoodItemViewModelForDSR();
                item.Id = (int)soldItem.Item_id;
                item.Quantity = (int)soldItem.Quantity;
                item.SaleId = (int)soldItem.Sale_id;
                item.Date = (DateTime)soldItem.tbl_Sale.Date_Time;

                if (item.Id >= 20000)
                {
                    tbl_Deal deal = Deal.getById(item.Id);
                    item.Name = deal.Name;
                    item.SalePrice = (int)deal.SalePrice;
                    item.CategoryName = deal.tbl_FoodItemCategory.Name;
                    item.Total = item.SalePrice * item.Quantity;
                    mappedList.Add(item);
                }
                else
                {
                    tbl_FoodItem foodItem = FoodItem.getById(item.Id);
                    item.Name = foodItem.Name;
                    item.SalePrice = (int)foodItem.SalePrice;
                    item.CategoryName = foodItem.tbl_FoodItemCategory.Name;
                    item.Total = item.SalePrice * item.Quantity;
                    mappedList.Add(item);
                }
            }
            return mappedList;
        }
    }
}
