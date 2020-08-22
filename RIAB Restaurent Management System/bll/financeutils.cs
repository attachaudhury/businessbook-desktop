
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
    public class financeutils
    {
        public static int insertSaleTransactions(List<productsaleorpurchase> purchaseList,float totalpayment, int targetuserid)
        {
            var loggedinuserid = userutils.loggedinuser.id;
            var db = new dbctx();
            List<financeaccount> accounts = db.financeaccount.ToList();
            var possaleaccountid = accounts.Where(a => a.name == "pos sale").FirstOrDefault().id;
            var discountaccountid = accounts.Where(a => a.name == "discount").FirstOrDefault().id;
            var cashaccountid = accounts.Where(a => a.name == "cash").FirstOrDefault().id;
            var accountreciveableaccountid = accounts.Where(a => a.name == "account receivable").FirstOrDefault().id;
            var cgsaccountid = accounts.Where(a => a.name == "cgs").FirstOrDefault().id;
            var inventoryaccountid = accounts.Where(a => a.name == "inventory").FirstOrDefault().id;
      
            double totalbill = 0;
            double costofgoodssold = 0;
            foreach (var item in purchaseList)
            {
                totalbill += (item.price * item.quantity);
                product p = db.product.Find(item.id);
                double productcarrycost = 0;
                if (p.carrycost != null) {
                    productcarrycost = (double)p.carrycost;
                }
                double productpurchaseprice = 0;
                if (p.purchaseprice != null)
                {
                    productpurchaseprice = (double)p.purchaseprice;
                }
                costofgoodssold += ((double)((productpurchaseprice + productcarrycost) * item.quantity));
            }


            //New Sale Transaction
            financetransaction ftsale = new financetransaction();
            ftsale.amount = -totalbill;
            ftsale.name = "--pos sale--";
            ftsale.fk_financeaccount_financeaccount_financetransaction = possaleaccountid;
            if (targetuserid != 0)
            {
                ftsale.fk_targettouser_user_financetransaction = targetuserid;
            }
            ftsale.date = DateTime.Now;
            ftsale.status = "posted";
            ftsale.fk_createdbyuser_user_financetransaction = loggedinuserid;
            db.financetransaction.Add(ftsale);
            db.SaveChanges();
            ftsale.groupid = ftsale.id;
            db.Entry(ftsale).State = EntityState.Modified;
            db.SaveChanges();

            //New Payment Transaction against sale . if customer is paying some money
            if (totalpayment > 0)
            {
                financetransaction ftpayment = new financetransaction();
                ftpayment.amount = totalpayment;
                ftpayment.name = "--cash-- against sale # " + ftsale.id;
                ftpayment.fk_financeaccount_financeaccount_financetransaction = cashaccountid;
                if (targetuserid != 0)
                {
                    ftpayment.fk_targettouser_user_financetransaction = targetuserid;
                }
                ftpayment.date = DateTime.Now;
                ftpayment.status = "posted";
                ftpayment.groupid = ftsale.id;
                ftpayment.fk_createdbyuser_user_financetransaction = loggedinuserid;
                db.financetransaction.Add(ftpayment);
                db.SaveChanges();
            }


            // New AR Transaction if Ledger is true
            if (totalpayment!=totalbill)
            {
                financetransaction ftar = new financetransaction();
                ftar.amount = totalbill - totalpayment;
                ftar.name = "--account receivable-- against sale # " + ftsale.id; ;
                ftar.fk_financeaccount_financeaccount_financetransaction = accountreciveableaccountid;
                if (targetuserid != 0)
                {
                    ftar.fk_targettouser_user_financetransaction = targetuserid;
                }
                ftar.date = DateTime.Now;
                ftar.status = "posted";
                ftar.fk_createdbyuser_user_financetransaction = loggedinuserid;
                ftar.groupid = ftsale.id;
                db.financetransaction.Add(ftar);
                db.SaveChanges();
            }

            // new cost of goods transaction against sale
            financetransaction ftcgs = new financetransaction();
            ftcgs.amount = costofgoodssold;
            ftcgs.name = "--cgs-- against ale # " + ftsale.id; ;
            ftcgs.fk_financeaccount_financeaccount_financetransaction = cgsaccountid;
            ftcgs.date = DateTime.Now;
            ftcgs.status = "posted";
            ftcgs.fk_createdbyuser_user_financetransaction = loggedinuserid;
            ftcgs.groupid = ftsale.id;
            db.financetransaction.Add(ftcgs);
            db.SaveChanges();


            // new inventory detct transaction against against sale
            financetransaction ftid = new financetransaction();
            ftid.amount = -costofgoodssold;
            ftid.name = "--inventory--on--sale-- against Sale no " + ftsale.id; ;
            ftid.fk_financeaccount_financeaccount_financetransaction = inventoryaccountid;
            ftid.date = DateTime.Now;
            ftid.status = "posted";
            ftid.fk_createdbyuser_user_financetransaction = loggedinuserid;
            ftid.groupid = ftsale.id;
            db.financetransaction.Add(ftid);
            db.SaveChanges();


            return ftsale.id;
            
        }

        public static int insertPurchaseTransactions(List<productsaleorpurchase> purchaseList, float totalpayment, int targetuserid)
        {
            var loggedinuserid = userutils.loggedinuser.id;
            var db = new dbctx();
            List<financeaccount> accounts = db.financeaccount.ToList();
            var possaleaccountid = accounts.Where(a => a.name == "pos sale").FirstOrDefault().id;
            var discountaccountid = accounts.Where(a => a.name == "discount").FirstOrDefault().id;
            var cashaccountid = accounts.Where(a => a.name == "cash").FirstOrDefault().id;
            var accountreciveableaccountid = accounts.Where(a => a.name == "account receivable").FirstOrDefault().id;
            var accountpayableableaccountid = accounts.Where(a => a.name == "account payable").FirstOrDefault().id;
            var cgsaccountid = accounts.Where(a => a.name == "cgs").FirstOrDefault().id;
            var inventoryaccountid = accounts.Where(a => a.name == "inventory").FirstOrDefault().id;

            double totalbill = 0;
            foreach (var item in purchaseList)
            {
                totalbill += (item.price * item.quantity);
            }


            //New purchase Transaction
            financetransaction ftpurchase = new financetransaction();
            ftpurchase.amount = totalbill;
            ftpurchase.name = "--inventory--on--purchase--";
            ftpurchase.fk_financeaccount_financeaccount_financetransaction = inventoryaccountid;
            ftpurchase.fk_targettouser_user_financetransaction = targetuserid;
            ftpurchase.date = DateTime.Now;
            ftpurchase.status = "posted";
            ftpurchase.fk_createdbyuser_user_financetransaction = loggedinuserid;
            db.financetransaction.Add(ftpurchase);
            db.SaveChanges();
            ftpurchase.groupid = ftpurchase.id;
            db.Entry(ftpurchase).State = EntityState.Modified;
            db.SaveChanges();



            //New Payment Transaction against sale . if we are paying some money
            if (totalpayment > 0)
            {
                financetransaction ftpayment = new financetransaction();
                ftpayment.amount = -(totalpayment);
                ftpayment.name = "--cash-- aginst purchase # " + ftpurchase.id;
                ftpayment.fk_financeaccount_financeaccount_financetransaction = cashaccountid;
                ftpayment.fk_targettouser_user_financetransaction = targetuserid;
                ftpayment.date = DateTime.Now;
                ftpayment.status = "posted";
                ftpayment.groupid = ftpurchase.id;
                ftpayment.fk_createdbyuser_user_financetransaction = loggedinuserid;
                db.financetransaction.Add(ftpayment);
                db.SaveChanges();
            }


            // New AP Transaction if TotalRemaining has ammount
            if (totalpayment != totalbill)
            {
                financetransaction ftap = new financetransaction();
                ftap.amount = -(totalbill - totalpayment);
                ftap.name = "--account payable-- against Purchase no " + ftpurchase.id ;
                ftap.fk_financeaccount_financeaccount_financetransaction = accountpayableableaccountid;
                ftap.fk_targettouser_user_financetransaction = targetuserid;
                ftap.date = DateTime.Now;
                ftap.status = "Posted";
                ftap.groupid = ftpurchase.id;
                ftap.fk_createdbyuser_user_financetransaction = loggedinuserid;
                db.financetransaction.Add(ftap);
                db.SaveChanges();
            }
            return ftpurchase.id;

        }
    }
}
