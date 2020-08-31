

using RIAB_Restaurent_Management_System.data;
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
        public static void possale(List<productsaleorpurchaseviewmodel> saleList,double change, data.user customer, int numberofrecipiets, bool printCustomerInfoOnReciept)
        {
            var totalpayment = saleList.Sum(a => a.total);
            var customerid = 0;
            var customeraddress = "";
            if (customer != null) 
            {
                customerid = customer.id;
                customeraddress = customer.address + " " + customer.phone + " " + customer.phone2;
            }
            var saleid = financeutils.insertSaleTransactions(saleList, totalpayment, customerid);
            for (int i = 0; i < numberofrecipiets; i++)
            {
                printing.printSaleReceipt(saleid, saleList, totalpayment,totalpayment+change, change, printCustomerInfoOnReciept, customeraddress);

            }

            Task.Run(() => {
                insertSellingProductsInDatabase(saleList, saleid);
                updateInventory(saleList);
            });
        }
        public static void newsale(List<productsaleorpurchaseviewmodel> saleList, double totalpayment, int customerId)
        {
            var saleid = financeutils.insertSaleTransactions(saleList, totalpayment, customerId);
            Task.Run(() => {
                insertSellingProductsInDatabase(saleList, saleid);
                updateInventory(saleList);
            });
        }
        private static void insertSellingProductsInDatabase(List<productsaleorpurchaseviewmodel> saleList, int saleid)
        {
            var db = new dbctx();
            foreach (productsaleorpurchaseviewmodel item in saleList)
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
        private static void updateInventory(List<productsaleorpurchaseviewmodel> salelist)
        {
            var db = new dbctx();
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
        private static void manageSubProductInventory(productsaleorpurchaseviewmodel sellingProduct)
        {
            var db = new dbctx();
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
            var db = new dbctx();
            var ft = db.financetransaction.Find(saleid);
            user customer = null;
            if (ft.fk_targettouser_user_financetransaction != null) {
                customer = db.user.Find(ft.fk_targettouser_user_financetransaction);
            }
            var soldproducts = db.salepurchaseproduct.Where(a => a.fk_financetransaction_salepurchaseproduct_financetransaction == saleid).ToList();

            float totalbill = 0;
            var salelist = new List<productsaleorpurchaseviewmodel>();

            foreach (var item in soldproducts)
            {
                totalbill = totalbill + (float)(item.price * item.quantity);
                var dbproduct = db.product.Find(item.fk_product_salepurchaseproduct_product);
                var p = new productsaleorpurchaseviewmodel();
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
            printing.printSaleReceipt(saleid, salelist, (int)totalbill, (int)totalbill, 0, false, customerAddress);

        }
    }
}
