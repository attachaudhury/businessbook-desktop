using BLL;
using DAL;
using RIAB_Restaurent_Management_System.data.viewmodel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIAB_Restaurent_Management_System.bll
{
    public class saleutils
    {
        public static void newsale(List<productsaleorpurchase> saleList, int totalpayment, int remaining, int customerId, bool receipt1, bool receipt2, bool receipt3, int saletype, string customerAddress, int deliveryBoyId)
        {
            var saleid = financeutils.insertSaleTransactions(saleList, totalpayment, customerId);
            if (receipt1)
            {
                printing.printSaleReceipt(saleid, saleList, totalpayment, remaining, saletype, customerAddress);
            }
            if (receipt2)
            {
                printing.printSaleReceipt(saleid, saleList, totalpayment, remaining, saletype, customerAddress);
            }
            if (receipt3)
            {
                printing.printSaleReceipt(saleid, saleList, totalpayment, remaining, saletype, customerAddress);
            }
            
            Task.Run(() => {
                insertSellingProductsInDatabase(saleList, saleid);
                updateInventory(saleList);
            });
        }
        private static void insertSellingProductsInDatabase(List<productsaleorpurchase> saleList, int saleid)
        {
            RMSDBEntities db = new RMSDBEntities();
            foreach (productsaleorpurchase item in saleList)
            {
                salepurchaseproduct saleItem = new salepurchaseproduct();
                saleItem.price = item.price;
                saleItem.quantity = item.quantity;
                saleItem.total = item.total;
                saleItem.fk_product_salepurchaseproduct_product = item.id;
                saleItem.fk_financetransaction_salepurchaseproduct_financetransaction = saleid;
                db.salepurchaseproduct.Add(saleItem);
            }
            db.SaveChanges();
        }
        private static void updateInventory(List<productsaleorpurchase> salelist)
        {
            RMSDBEntities db = new RMSDBEntities();
            foreach (var item in salelist)
            {
                product p = db.product.Find(item.id);
                p.quantity = p.quantity - item.quantity;
                db.Entry(p).State = EntityState.Modified;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();
                db.Configuration.ValidateOnSaveEnabled = true;
                manageSubProductInventory(item);
            }
        }
        private static void manageSubProductInventory(productsaleorpurchase sellingProduct)
        {
            RMSDBEntities db = new RMSDBEntities();
            var subproducts = db.subproduct.Where(a => (a.fk_product_product_subproduct == sellingProduct.id)).ToList();
            foreach (var item in subproducts)
            {
                product p = db.product.Find(item.fk_subproduct_product_subproduct);
                p.quantity = p.quantity - (sellingProduct.quantity*item.quantity);
                db.Entry(p).State = EntityState.Modified;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();
                db.Configuration.ValidateOnSaveEnabled = true;
            }
            
        }
        public static void printDuplicateRecipt(int saleid)
        {
            var db = new RMSDBEntities();
            var ft = db.financetransaction.Find(saleid);
            user customer = null;
            if (ft.fk_targettouser_user_financetransaction != null) {
                customer = db.user.Find(ft.fk_targettouser_user_financetransaction);
            }
            var soldproducts = db.salepurchaseproduct.Where(a => a.fk_financetransaction_salepurchaseproduct_financetransaction == saleid).ToList();

            float totalbill = 0;
            var salelist = new List<productsaleorpurchase>();

            foreach (var item in soldproducts)
            {
                totalbill = totalbill + (float)(item.price * item.quantity);
                var dbproduct = db.product.Find(item.fk_product_salepurchaseproduct_product);
                var p = new productsaleorpurchase();
                p.id = dbproduct.id;
                p.name = dbproduct.name;
                p.price = (double)item.price;
                p.quantity = (double)item.quantity;
                p.total = (double)item.total;
            };

            //int salesId, List< ItemOrDealSaleModel > list, int totalBill,int remaining, int saleType,string customerAddress
            string customerAddress ="";
            if (customer != null)
            {
                customerAddress = customer.address + " " + customer.phone;
            }
            

            printing.printSaleReceipt(saleid, salelist, (int)totalbill, 0, 3, customerAddress);

        }
    }
}
