
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
    class purchaseutils
    {
        public static void newpurchase(List<productsaleorpurchase> purchaseList, int totalpayment, int remaining, int vendorid)
        {
            var purchaseid = financeutils.insertPurchaseTransactions(purchaseList, totalpayment, vendorid);
            Task.Run(() => {
                insertPurchasingProductsInDatabase(purchaseList, purchaseid);
                updateInventory(purchaseList);
            });
        }
        private static void insertPurchasingProductsInDatabase(List<productsaleorpurchase> purchaseList, int purchaseid)
        {
            var db = new dbctx();
            foreach (productsaleorpurchase item in purchaseList)
            {
                salepurchaseproduct saleItem = new salepurchaseproduct();
                saleItem.price = item.price;
                saleItem.quantity = item.quantity;
                saleItem.total = item.total;
                saleItem.fk_product_salepurchaseproduct_product = item.id;
                saleItem.fk_financetransaction_salepurchaseproduct_financetransaction = purchaseid;
                db.salepurchaseproduct.Add(saleItem);
            }
            db.SaveChanges();
        }
        private static void updateInventory(List<productsaleorpurchase> purchaseList)
        {
            var db = new dbctx();
            foreach (var item in purchaseList)
            {
                product p = db.product.Find(item.id);
                p.quantity = p.quantity + item.quantity;
                db.Entry(p).State = EntityState.Modified;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();
                db.Configuration.ValidateOnSaveEnabled = true;
                manageSubProductInventory(item);
            }
        }
        private static void manageSubProductInventory(productsaleorpurchase purchasingProduct)
        {
            var db = new dbctx();
            var subproducts = db.subproduct.Where(a => (a.fk_product_product_subproduct == purchasingProduct.id)).ToList();
            foreach (var item in subproducts)
            {
                product p = db.product.Find(item.fk_subproduct_product_subproduct);
                p.quantity = p.quantity + (purchasingProduct.quantity * item.quantity);
                db.Entry(p).State = EntityState.Modified;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();
                db.Configuration.ValidateOnSaveEnabled = true;
            }

        }
    }
}
