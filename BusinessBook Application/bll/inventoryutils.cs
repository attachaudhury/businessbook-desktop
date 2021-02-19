

using BusinessBook.data;
using BusinessBook.data.dapper;
using BusinessBook.data.viewmodel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BusinessBook.bll
{
    public class inventoryutils
    {
        public static void updateInventoryonsale(List<productsaleorpurchaseviewmodel> salelist,int saleid)
        {
            foreach (var item in salelist)
            {
                recursiveupdateinventoryonsale(item.id, item.quantity,saleid,"");
            }
        }

        //inventoryreportcomment argument for inserting value in inventory report to check wheather it is sold as it is on as a subproduct
        private static void recursiveupdateinventoryonsale(int productid,double productquantity, int saleid,string inventoryreportcomment)
        {
            var productrepo = new productrepo();
            data.dapper.product p = productrepo.get(productid);
            var productsubrepo = new productsubrepo();
            var productsubs = productsubrepo.getproduct_productsubs(productid);
            if(productsubs.Count == 0)
            {
                // if products has no sub product. then its inventory will be updated, it is better approach for handling inventory of deal in case of  purchase purchase
                p.quantity = p.quantity - productquantity;
                productrepo.update(p);
                updateinventoryreportonsale(productid, productquantity, saleid, inventoryreportcomment);
            }
            foreach (var productsub in productsubs)
            {
                recursiveupdateinventoryonsale(productsub.fk_product_sub_in_productsub, productquantity * productsub.quantity,saleid,", sold as sub of "+p.name);
            }
        }
        private static void updateinventoryreportonsale(int productid, double productquantity, int saleid,string comment)
        {
            var inventoryreportrepo = new inventoryreportrepo();
            data.dapper.inventoryreport ir = new inventoryreport();
            ir.quantity = -productquantity;
            ir.date = DateTime.Now;
            ir.fk_product_in_inventoryreport = productid;
            ir.note = "Detucted inventory on sale id " + saleid+comment;
            inventoryreportrepo.save(ir);
        }

        public static void updateInventoryonpurchase(List<productsaleorpurchaseviewmodel> purchaseList,int purchaseid)
        {
            foreach (var item in purchaseList)
            {
                recursiveupdateinventoryonpurchase(item.id, item.quantity, purchaseid,"");
            }
        }
        //inventoryreportcomment argument for inserting value in inventory report to check wheather it is sold as it is on as a subproduct
        private static void recursiveupdateinventoryonpurchase(int productid, double productquantity, int purchaseid, string inventoryreportcomment)
        {
            var productrepo = new productrepo();
            data.dapper.product p = productrepo.get(productid);
            var productsubrepo = new productsubrepo();
            var productsubs = productsubrepo.getproduct_productsubs(productid);
            if (productsubs.Count == 0)
            {
                // if products has no sub product. then its inventory will be updated, it is better approach for handling inventory of deal in case of  purchase purchase
                p.quantity = p.quantity + productquantity;
                productrepo.update(p);
                updateinventoryreportonpurchase(productid, productquantity, purchaseid, inventoryreportcomment);
            }
            foreach (var productsub in productsubs)
            {
                recursiveupdateinventoryonpurchase(productsub.fk_product_sub_in_productsub, productquantity * productsub.quantity, purchaseid, ", purchased as sub of " + p.name);
            }
        }
        private static void updateinventoryreportonpurchase(int productid, double productquantity, int purchaseid, string inventoryreportcomment)
        {
            var inventoryreportrepo = new inventoryreportrepo();
            data.dapper.inventoryreport ir = new inventoryreport();
            ir.quantity = productquantity;
            ir.date = DateTime.Now;
            ir.fk_product_in_inventoryreport = productid;
            ir.note = "Added inventory on purchase id " + purchaseid+inventoryreportcomment;
            inventoryreportrepo.save(ir);
        }
        public static void updateinventoryreportonproductcreate(data.dapper.product p)
        {
            if (p.quantity != 0)
            {
                var inventoryreportrepo = new inventoryreportrepo();
                data.dapper.inventoryreport ir = new inventoryreport();
                ir.quantity = p.quantity;
                ir.date = DateTime.Now;
                ir.fk_product_in_inventoryreport = p.id;
                ir.note = "Added inventory on product create";
                inventoryreportrepo.save(ir);
            }
        }

        public static void updateinventoryreportonproductupdate(int productid, double newinventory, double oldinventory)
        {
            if (newinventory==oldinventory)
            {
                return;
            }
            var inventoryreportrepo = new inventoryreportrepo();
            data.dapper.inventoryreport ir = new inventoryreport();
            ir.date = DateTime.Now;
            ir.fk_product_in_inventoryreport = productid;
            ir.quantity = newinventory- oldinventory;
            if (newinventory > oldinventory)
            {
                ir.note = "Added inventory on product update";
            }
            else if(newinventory < oldinventory)
            {
                ir.note = "Detucted inventory on product update";
            }
            inventoryreportrepo.save(ir);
        }
    }
}
