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
    public class Sale
    {
        public static tbl_Sale getById(int id) 
        {
            RMSDBEntities db = DBContext.getInstance();
            return db.tbl_Sale.Find(id);
        }
        public static void performANewSale(List<ItemOrDealSaleModel> saleList,double discount,int totalBill,int remaining, int customerId, bool receipt1,bool receipt2, bool receipt3, int saletype, string customerAddress,int deliveryBoyId) 
        {
            RMSDBEntities db = DBContext.getInstance();
            tbl_Sale sale = new tbl_Sale();
            sale.Staff_id = 0;
            sale.Date_Time = DateTime.Now;
            
            tbl_Customer c = Customer.getById(customerId);
            if (c!=null )
            {
                sale.Customer_Id = customerId;
            }

            sale.Amount = totalBill;
            sale.SaleType = saletype;
            db.tbl_Sale.Add(sale);
            db.SaveChanges();
            //3 is for delivery. therefor we have to add in Delivery Queue
            if (saletype == 3)
            {
                tbl_DeliveryQueue dq = new tbl_DeliveryQueue();
                dq.Customer_Id = customerId;
                dq.Sale_Id = sale.Id;
                dq.Delivered = false;
                dq.DatenTime = DateTime.Now;
                if (deliveryBoyId != 0)
                {
                    dq.DeliveryBoyId = deliveryBoyId;
                }
                
                DeliveryQueue.insert(dq);
            }
            // printing
            if (receipt1) {
                PrintingUtils.printSaleReceipt(sale.Id, saleList, totalBill,remaining,saletype,customerAddress);
            }
            if (receipt2)
            {
                PrintingUtils.printSaleReceipt(sale.Id, saleList, totalBill, remaining, saletype, customerAddress);
            }
            if (receipt3)
            {
                PrintingUtils.printSaleReceipt(sale.Id, saleList, totalBill, remaining, saletype, customerAddress);
            }
            foreach (ItemOrDealSaleModel item in saleList)
            {
                tbl_SaleItem saleItem = new tbl_SaleItem();
                saleItem.Item_id = item.Id;
                saleItem.Sale_id = sale.Id;
                saleItem.Quantity = item.Quantity;
                SaleItem.insert(saleItem);
            }
            DetuctInventory.detuctInventoryOfList(saleList);
        }
        public static void deleteASaleById(int saleId)
        {
            RMSDBEntities db = DBContext.getInstance();
            tbl_Sale sale = db.tbl_Sale.Find(saleId);
            db.tbl_Sale.Remove(sale);
            db.SaveChanges();
            SaleItem.deleteSaleItemsBySaleId(saleId);
        }
        public static List<tbl_Sale> getAllByCustomerID(int customerId)
        {
            RMSDBEntities db = DBContext.getInstance();
            return db.tbl_Sale.Where(a=>a.Customer_Id == customerId).ToList();
        }
        public static List<tbl_Sale> getAll()
        {
            RMSDBEntities db = DBContext.getInstance();
            return db.tbl_Sale.ToList();
        }
        public static List<tbl_Sale> getAllToday()
        {
            RMSDBEntities db = DBContext.getInstance();
            DateTime startDateTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " 12:00:00 AM");
            DateTime endDateTime = DateTime.Now;

            return db.tbl_Sale.Where(a => a.Date_Time >= startDateTime).Where(a => a.Date_Time <= endDateTime).ToList();
        }
        public static List<tbl_Sale> getAllTodayOnRestaurent()
        {
            RMSDBEntities db = DBContext.getInstance();
            DateTime startDateTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " 12:00:00 AM");
            DateTime endDateTime = DateTime.Now;

            return db.tbl_Sale.Where(a => a.Date_Time >= startDateTime).Where(a => a.Date_Time <= endDateTime).Where(a=>a.SaleType==1).ToList();
        }
        public static List<tbl_Sale> getAllTodayOnDelivery()
        {
            RMSDBEntities db = DBContext.getInstance();
            DateTime startDateTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " 12:00:00 AM");
            DateTime endDateTime = DateTime.Now;

            return db.tbl_Sale.Where(a => a.Date_Time >= startDateTime).Where(a => a.Date_Time <= endDateTime).Where(a => a.SaleType == 3).ToList();
        }
        public static List<tbl_Sale> getAllThisMonth()
        {
            RMSDBEntities db = DBContext.getInstance();
            DateTime now = DateTime.Now;
            var thisMonthStartDateTime = new DateTime(now.Year, now.Month, 1);
            DateTime endDateTime = DateTime.Now;
            return db.tbl_Sale.Where(a => a.Date_Time >= thisMonthStartDateTime).Where(a => a.Date_Time <= endDateTime).ToList();
        }
        public static List<tbl_Sale> getAllCustomDate(DateTime startDate, DateTime endDate)
        {
            RMSDBEntities db = DBContext.getInstance();
            return db.tbl_Sale.Where(a => a.Date_Time >= startDate).Where(a => a.Date_Time <= endDate).ToList();
        }
        public static int getAllTodayAmmount()
        {
            int ammount = 0;
            foreach (tbl_Sale sale in getAllToday())
            {
                ammount += (int)sale.Amount;
            }
            return ammount;
        }
        public static int getAllTodayOnRestaurentAmmount()
        {
            int ammount = 0;
            foreach (tbl_Sale sale in getAllTodayOnRestaurent())
            {
                ammount += (int)sale.Amount;
            }
            return ammount;
        }
        public static int getAllTodayOnDeliveryAmmount()
        {
            int ammount = 0;
            foreach (tbl_Sale sale in getAllTodayOnDelivery())
            {
                ammount += (int)sale.Amount;
            }
            return ammount;
        }
        public static int getAllThisMonthAmmount()
        {
            int ammount = 0;
            foreach (tbl_Sale sale in getAllThisMonth())
            {
                ammount += (int)sale.Amount;
            }
            return ammount;
        }
        public static int getAllTotalAmmount()
        {
            int ammount = 0;
            foreach (tbl_Sale sale in getAll())
            {
                ammount += (int)sale.Amount;
            }
            return ammount;
        }
        public static int getAllCustomDateAmmount(DateTime startDate, DateTime endDate)
        {
            int ammount = 0;
            foreach (tbl_Sale sale in getAllCustomDate(startDate, endDate))
            {
                ammount += (int)sale.Amount;
            }
            return ammount;
        }



        // Code for new version. Additions from 8 june 2017
        public static List<ItemOrDealSaleModel> getMappedListofDealsAndItemsToItemOrDealSaleModel()
        {
            List<ItemOrDealSaleModel> list = new List<ItemOrDealSaleModel>();
            List<tbl_FoodItem> allFoodItems = FoodItem.getAll();
            List<tbl_Deal> allDeals = Deal.getAll();
            foreach (tbl_FoodItem foodItem in allFoodItems)
            {
                ItemOrDealSaleModel a = new ItemOrDealSaleModel();
                a.Id = foodItem.Id;
                a.Name = foodItem.Name;
                a.Quantity = 1;
                a.SalePrice = (int)foodItem.SalePrice;
                a.Total = (int)foodItem.SalePrice;
                list.Add(a);
            }
            foreach (tbl_Deal deal in allDeals)
            {
                ItemOrDealSaleModel b = new ItemOrDealSaleModel();
                b.Id = deal.Id;
                b.Name = deal.Name;
                b.Quantity = 1;
                b.SalePrice = (int)deal.SalePrice;
                b.Total = (int)deal.SalePrice;
                list.Add(b);
            }
            return list;
        }
        public static List<ItemOrDealSaleModel> getMappedListofDealsToItemOrDealSaleModel()
        {
            List<ItemOrDealSaleModel> list = new List<ItemOrDealSaleModel>();
            List<tbl_Deal> allDeals = Deal.getAll();
            foreach (tbl_Deal deal in allDeals)
            {
                ItemOrDealSaleModel b = new ItemOrDealSaleModel();
                b.Id = deal.Id;
                b.Name = deal.Name;
                b.Quantity = 1;
                b.SalePrice = (int)deal.SalePrice;
                b.Total = (int)deal.SalePrice;
                list.Add(b);
            }
            return list;
        }
        public static void printDuplicateRecipt(int saleId)
        {
            tbl_Sale sale = Sale.getById(saleId);
            tbl_Customer customer = Customer.getById((int)sale.Customer_Id);
            List<tbl_SaleItem> soldItems = SaleItem.getAllBySaleId(saleId);
            List<ItemOrDealSaleModel> allMappedItems =  Sale.getMappedListofDealsAndItemsToItemOrDealSaleModel();
            List<ItemOrDealSaleModel> newSaleList = new List<ItemOrDealSaleModel>();
            foreach (tbl_SaleItem item in soldItems)
            {
                ItemOrDealSaleModel s = new ItemOrDealSaleModel();
                ItemOrDealSaleModel s1 = allMappedItems.Where(a=>a.Id == (int)item.Item_id).FirstOrDefault();
                s.Id = s1.Id;
                s.Name = s1.Name;
                s.Quantity = (int)item.Quantity;
                s.SalePrice = s1.SalePrice ;
                s.Total = s1.SalePrice * s.Quantity;

                newSaleList.Add(s);
            }
            //int salesId, List< ItemOrDealSaleModel > list, int totalBill,int remaining, int saleType,string customerAddress
            string customerAddress;
            if (customer != null)
            {
                customerAddress = customer.Address + " " + customer.PhoneNo;
            }
            else
            {
                customerAddress = "";
            }

            PrintingUtils.printSaleReceipt(sale.Id, newSaleList, (int)sale.Amount, 0, (int)sale.SaleType, customerAddress);

        }
        
        public static List<ItemSaleSearchModel> itemSalesSearchByDate(int itemId, DateTime fromDate, DateTime toDate)
        {
            List<ItemSaleSearchModel> list = new List<ItemSaleSearchModel>();
            List<DateTime> daysList = new List<DateTime>();
            daysList.Add(fromDate);
            while (fromDate.AddDays(1) <= toDate)
            {
                fromDate = fromDate.AddDays(1);
                daysList.Add(fromDate);
            }

            if (itemId < 20000)
            {
                tbl_FoodItem fi = FoodItem.getById(itemId);
                foreach (DateTime date in daysList)
                {
                    int quantity = getTotalQuantityOfItemSoldOnSpecificDate(itemId, date);
                    if (quantity > 0)
                    {
                        ItemSaleSearchModel n = new ItemSaleSearchModel();
                        n.Date = date;
                        n.Quantity = getTotalQuantityOfItemSoldOnSpecificDate(itemId, date);
                        n.Total = (int)fi.SalePrice * n.Quantity;
                        list.Add(n);
                    }

                }
            }
            else
            {
                tbl_Deal deal = Deal.getById(itemId);
                foreach (DateTime date in daysList)
                {
                    int quantity = getTotalQuantityOfItemSoldOnSpecificDate(itemId, date);
                    if (quantity > 0)
                    {
                        ItemSaleSearchModel n = new ItemSaleSearchModel();
                        n.Date = date;
                        n.Quantity = getTotalQuantityOfItemSoldOnSpecificDate(itemId, date);
                        n.Total = (int)deal.SalePrice * n.Quantity;
                        list.Add(n);
                    }

                }
            }
            return list;
        }
        public static int getTotalQuantityOfItemSoldOnSpecificDate(int itemId,DateTime date)
        {
            int quantity = 0;
            DateTime fromDate = Convert.ToDateTime(date.ToShortDateString()+ " 12:00:00 AM");
            DateTime toDate = Convert.ToDateTime(date.ToShortDateString() + " 11:59:59 PM");
            List<tbl_Sale> customDateSaleList = getAllCustomDate(fromDate, toDate);
            foreach (tbl_Sale sale in customDateSaleList)
            {
                foreach (tbl_SaleItem saleItem in SaleItem.getAllBySaleId(sale.Id))
                {
                    if (saleItem.Item_id == itemId)
                    {
                        quantity += (int)saleItem.Quantity;
                    }

                }
            }
            return quantity;
        }
   
    }
}
