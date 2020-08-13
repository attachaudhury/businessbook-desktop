using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BLL.DBOperations.TmpModels;

namespace BLL.DBOperations
{
    public class DetuctInventory
    {
        public static tbl_DetuctInventory getById(int id)
        {
            RMSDBEntities db = DBContext.getInstance();
            return db.tbl_DetuctInventory.Find(id);
        }        
        public static void insert(tbl_DetuctInventory di)
        {
            RMSDBEntities db = DBContext.getInstance();
            db.tbl_DetuctInventory.Add(di);
            db.SaveChanges();
        }
        public static void delete(tbl_DetuctInventory di)
        {
            RMSDBEntities db = DBContext.getInstance();
            db.tbl_DetuctInventory.Remove(di);
            db.SaveChanges();
        }
        public static List<tbl_DetuctInventory> getAll()
        {
            RMSDBEntities db = DBContext.getInstance();
            return db.tbl_DetuctInventory.ToList();
        }
        public static void detuctInventoryOfList(List<ItemOrDealSaleModel> saleList)
        {
            foreach (ItemOrDealSaleModel item in saleList)
            {
                if (item.Id < 20000)
                {
                    tbl_FoodItem foodItem = FoodItem.getById(item.Id);
                    if ((bool)foodItem.ManageInventory)
                    {
                        RMSDBEntities db = DBContext.getInstance();
                        List<tbl_DetuctInventory> diList = db.tbl_DetuctInventory.Where(a => a.FoodItem_Id == item.Id).ToList();
                        foreach (tbl_DetuctInventory item1 in diList)
                        {
                            tbl_KitchenInventory ki = KitchenInventory.getById((int)item1.KitchenInventory_id);
                            double quantity1 = (double)ki.Quantity;
                            double quantity2 = (double)item1.DeductedQuantity * item.Quantity;
                            double quantity3 = quantity1 - quantity2;
                            ki.Quantity = quantity3;
                            KitchenInventory.update(ki);
                        }
                    }
                }
                else
                {
                    tbl_Deal foodItem = Deal.getById(item.Id);
                    if ((bool)foodItem.ManageInventory)
                    {
                        RMSDBEntities db = DBContext.getInstance();
                        List<tbl_DetuctInventory> diList = db.tbl_DetuctInventory.Where(a => a.FoodItem_Id == item.Id).ToList();
                        foreach (tbl_DetuctInventory item1 in diList)
                        {
                            tbl_KitchenInventory ki = KitchenInventory.getById((int)item1.KitchenInventory_id);
                            double quantity1 = (double)ki.Quantity;
                            double quantity2 = (double)item1.DeductedQuantity * item.Quantity;
                            double quantity3 = quantity1 - quantity2;
                            ki.Quantity = quantity3;
                            KitchenInventory.update(ki);
                        }
                    }
                }
                
            }
        }
    }
}
