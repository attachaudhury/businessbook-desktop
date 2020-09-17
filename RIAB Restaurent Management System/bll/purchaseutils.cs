
using RIAB_Restaurent_Management_System.data;
using RIAB_Restaurent_Management_System.data.dapper;
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
        public static void newpurchase(List<productsaleorpurchaseviewmodel> purchaseList, double totalpayment, int vendorid)
        {
            var purchaseid = financeutils.insertPurchaseTransactions(purchaseList, totalpayment, vendorid);
            Task.Run(() => {
                insertPurchasingProductsInDatabase(purchaseList, purchaseid);
                updateInventory(purchaseList);
            });
        }
        private static void insertPurchasingProductsInDatabase(List<productsaleorpurchaseviewmodel> purchaseList, int purchaseid)
        {
            var salepurchaseproducrepo = new salepurchaseproductrepo();
            //var db = new dbctx();
            foreach (productsaleorpurchaseviewmodel item in purchaseList)
            {
                data.dapper.salepurchaseproduct saleItem = new data.dapper.salepurchaseproduct();
                saleItem.price = item.price;
                saleItem.quantity = item.quantity;
                saleItem.total = item.total;
                saleItem.fk_product_in_salepurchaseproduct = item.id;
                saleItem.fk_financetransaction_in_salepurchaseproduct = purchaseid;
                //db.salepurchaseproduct.Add(saleItem);
                salepurchaseproducrepo.save(saleItem);
            }
            //db.SaveChanges();
        }
        private static void updateInventory(List<productsaleorpurchaseviewmodel> purchaseList)
        {
            //var db = new dbctx();
            var productrepo = new data.dapper.productrepo();
            foreach (var item in purchaseList)
            {
                data.dapper.product p = productrepo.get(item.id);
                p.quantity = p.quantity + item.quantity;
                productrepo.update(p);
                //db.Entry(p).State = EntityState.Modified;
                //db.Configuration.ValidateOnSaveEnabled = false;
                //db.SaveChanges();
                //db.Configuration.ValidateOnSaveEnabled = true;
                manageSubProductInventory(item);
            }
        }
        private static void manageSubProductInventory(productsaleorpurchaseviewmodel purchasingProduct)
        {
            var productrepo = new productrepo();
            var subproductrepo = new subproductrepo();
            var subproducts = subproductrepo.getproduct_subproducts(purchasingProduct.id);
            //var db = new dbctx();
            //var subproducts = db.subproduct.Where(a => (a.fk_product_main_in_subproduct == purchasingProduct.id)).ToList();
            foreach (var item in subproducts)
            {
                data.dapper.product p = productrepo.get(item.fk_product_sub_in_subproduct);
                //data.dapper.product p = db.product.Find(item.fk_product_sub_in_subproduct);
                p.quantity = p.quantity + (purchasingProduct.quantity * item.quantity);
                productrepo.update(p);
                //db.Entry(p).State = EntityState.Modified;
                //db.Configuration.ValidateOnSaveEnabled = false;
                //db.SaveChanges();
                //db.Configuration.ValidateOnSaveEnabled = true;
            }

        }
    }
}
