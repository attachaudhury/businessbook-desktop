using BLL;
using DAL;
using RIAB_Restaurent_Management_System.data.viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIAB_Restaurent_Management_System.bll
{
    public class saleutils
    {
        public static void newsale(List<productsaleorpurchase> saleList, double discount, int totalBill, int remaining, int customerId, bool receipt1, bool receipt2, bool receipt3, int saletype, string customerAddress, int deliveryBoyId)
        {
            RMSDBEntities db = new RMSDBEntities();
            tbl_Sale sale = new tbl_Sale();
            sale.Staff_id = 0;
            sale.Date_Time = DateTime.Now;

            user c = db.user.Find(customerId);
            if (c != null)
            {
                sale.Customer_Id = customerId;
            }

            sale.Amount = totalBill;
            sale.SaleType = saletype;
            db.tbl_Sale.Add(sale);
            db.SaveChanges();
            
            // printing
            if (receipt1)
            {
                printing.printSaleReceipt(sale.Id, saleList, totalBill, remaining, saletype, customerAddress);
            }
            if (receipt2)
            {
                printing.printSaleReceipt(sale.Id, saleList, totalBill, remaining, saletype, customerAddress);
            }
            if (receipt3)
            {
                printing.printSaleReceipt(sale.Id, saleList, totalBill, remaining, saletype, customerAddress);
            }
            //foreach (ItemOrDealSaleModel item in saleList)
            //{
            //    tbl_SaleItem saleItem = new tbl_SaleItem();
            //    saleItem.Item_id = item.Id;
            //    saleItem.Sale_id = sale.Id;
            //    saleItem.Quantity = item.Quantity;
            //    SaleItem.insert(saleItem);
            //}
            //DetuctInventory.detuctInventoryOfList(saleList);
        }
    }
}
