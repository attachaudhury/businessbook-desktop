
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
        public static int insertSaleTransactions(string accountname,List<productsaleorpurchaseviewmodel> saleList,double totalpayment, int targetuserid)
        {
            var loggedinuserid = userutils.loggedinuser.id;
            var db = new dbctx();
            List<financeaccount> accounts = db.financeaccount.ToList();
            var saleaccountid = accounts.Where(a => a.name == accountname).FirstOrDefault().id;
            var discountaccountid = accounts.Where(a => a.name == "discount").FirstOrDefault().id;
            var cashaccountid = accounts.Where(a => a.name == "cash").FirstOrDefault().id;
            var accountreciveableaccountid = accounts.Where(a => a.name == "account receivable").FirstOrDefault().id;
            var cgsaccountid = accounts.Where(a => a.name == "cgs").FirstOrDefault().id;
            var inventoryaccountid = accounts.Where(a => a.name == "inventory").FirstOrDefault().id;
      
            double totalbill = 0;
            double costofgoodssold = 0;
            foreach (var item in saleList)
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
            ftsale.fk_financeaccount_financeaccount_financetransaction = saleaccountid;
            if (targetuserid != 0)
            {
                ftsale.fk_targettouser_user_financetransaction = targetuserid;
            }
            ftsale.date = DateTime.Now;
            ftsale.status = "posted";
            ftsale.fk_createdbyuser_user_financetransaction = loggedinuserid;
            db.financetransaction.Add(ftsale);
            db.SaveChanges();
            
            //New Payment Transaction against sale . if customer is paying some money
            if (totalpayment > 0)
            {
                financetransaction ftpayment = new financetransaction();
                ftpayment.amount = totalpayment;
                ftpayment.fk_financeaccount_financeaccount_financetransaction = cashaccountid;
                ftpayment.date = DateTime.Now;
                ftpayment.status = "posted";
                ftpayment.fk_createdbyuser_user_financetransaction = loggedinuserid;
                db.financetransaction.Add(ftpayment);
                db.SaveChanges();
            }


            // New AR Transaction if Ledger is true
            if (totalpayment!=totalbill)
            {
                financetransaction ftar = new financetransaction();
                ftar.amount = totalbill - totalpayment;
                ftar.fk_financeaccount_financeaccount_financetransaction = accountreciveableaccountid;
                if (targetuserid != 0)
                {
                    ftar.fk_targettouser_user_financetransaction = targetuserid;
                }
                ftar.date = DateTime.Now;
                ftar.status = "posted";
                ftar.fk_createdbyuser_user_financetransaction = loggedinuserid;
                db.financetransaction.Add(ftar);
                db.SaveChanges();
            }

            // new cost of goods transaction against sale
            financetransaction ftcgs = new financetransaction();
            ftcgs.amount = costofgoodssold;
            ftcgs.fk_financeaccount_financeaccount_financetransaction = cgsaccountid;
            ftcgs.date = DateTime.Now;
            ftcgs.status = "posted";
            ftcgs.fk_createdbyuser_user_financetransaction = loggedinuserid;
            db.financetransaction.Add(ftcgs);
            db.SaveChanges();


            // new inventory detct transaction against against sale
            financetransaction ftid = new financetransaction();
            ftid.name = "--inventory--on--sale--";
            ftid.amount = -costofgoodssold;
            ftid.fk_financeaccount_financeaccount_financetransaction = inventoryaccountid;
            ftid.date = DateTime.Now;
            ftid.status = "posted";
            ftid.fk_createdbyuser_user_financetransaction = loggedinuserid;
            db.financetransaction.Add(ftid);
            db.SaveChanges();


            return ftsale.id;
            
        }

        public static int insertPurchaseTransactions(List<productsaleorpurchaseviewmodel> purchaseList, double totalpayment, int targetuserid)
        {
            var loggedinuserid = userutils.loggedinuser.id;
            var db = new dbctx();
            List<financeaccount> accounts = db.financeaccount.ToList();
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
            
            //New Payment Transaction against sale . if we are paying some money
            if (totalpayment > 0)
            {
                financetransaction ftpayment = new financetransaction();
                ftpayment.amount = -(totalpayment);
                ftpayment.fk_financeaccount_financeaccount_financetransaction = cashaccountid;
                ftpayment.date = DateTime.Now;
                ftpayment.status = "posted";
                ftpayment.fk_createdbyuser_user_financetransaction = loggedinuserid;
                db.financetransaction.Add(ftpayment);
                db.SaveChanges();
            }


            // New AP Transaction if TotalRemaining has ammount
            if ( totalbill!= totalpayment)
            {
                financetransaction ftap = new financetransaction();
                ftap.amount = -(totalbill - totalpayment);
                ftap.fk_financeaccount_financeaccount_financetransaction = accountpayableableaccountid;
                ftap.fk_targettouser_user_financetransaction = targetuserid;
                ftap.date = DateTime.Now;
                ftap.status = "Posted";
                ftap.fk_createdbyuser_user_financetransaction = loggedinuserid;
                db.financetransaction.Add(ftap);
                db.SaveChanges();
            }
            return ftpurchase.id;

        }

        public static void insertCustomerPayment(int accountid, double amount, int targetid) 
        {
            var loggedinuserid = userutils.loggedinuser.id;
            var db = new dbctx();
            List<financeaccount> accounts = db.financeaccount.ToList();
            var accountreciveableaccountid = accounts.Where(a => a.name == "account receivable").FirstOrDefault().id;
            

            financetransaction ftcash = new financetransaction();
            ftcash.amount = amount;
            ftcash.fk_financeaccount_financeaccount_financetransaction = accountid;
            ftcash.date = DateTime.Now;
            ftcash.status = "posted";
            ftcash.fk_createdbyuser_user_financetransaction = loggedinuserid;
            db.financetransaction.Add(ftcash);
            db.SaveChanges();


            financetransaction ftar = new financetransaction();
            ftar.amount = -amount;
            ftar.fk_financeaccount_financeaccount_financetransaction = accountreciveableaccountid;
            ftar.date = DateTime.Now;
            ftar.status = "posted";
            ftar.fk_createdbyuser_user_financetransaction = loggedinuserid;
            db.financetransaction.Add(ftar);
            db.SaveChanges();
        }

        public static void insertVendorPayment(int accountid, double amount, int targetid)
        {
            var loggedinuserid = userutils.loggedinuser.id;
            var db = new dbctx();
            List<financeaccount> accounts = db.financeaccount.ToList();
            var accountpayableableaccountid = accounts.Where(a => a.name == "account payable").FirstOrDefault().id; ;


            financetransaction ftcash = new financetransaction();
            ftcash.amount = -amount;
            ftcash.fk_financeaccount_financeaccount_financetransaction = accountid;
            ftcash.date = DateTime.Now;
            ftcash.status = "posted";
            ftcash.fk_createdbyuser_user_financetransaction = loggedinuserid;
            db.financetransaction.Add(ftcash);
            db.SaveChanges();


            financetransaction ftar = new financetransaction();
            ftar.amount = amount;
            ftar.fk_financeaccount_financeaccount_financetransaction = accountpayableableaccountid;
            ftar.date = DateTime.Now;
            ftar.status = "posted";
            ftar.fk_createdbyuser_user_financetransaction = loggedinuserid;
            db.financetransaction.Add(ftar);
            db.SaveChanges();
        }

        public static void insertexpence(int payingaccount,  int expenceaccount, double amount)
        {
            var loggedinuserid = userutils.loggedinuser.id;
            var db = new dbctx();
            
            financetransaction ftexpence = new financetransaction();
            ftexpence.amount = -amount;
            ftexpence.fk_financeaccount_financeaccount_financetransaction = expenceaccount;
            ftexpence.date = DateTime.Now;
            ftexpence.status = "posted";
            ftexpence.fk_createdbyuser_user_financetransaction = loggedinuserid;
            db.financetransaction.Add(ftexpence);
            db.SaveChanges();

            financetransaction ftasset = new financetransaction();
            ftasset.amount = amount;
            ftasset.fk_financeaccount_financeaccount_financetransaction = payingaccount;
            ftasset.date = DateTime.Now;
            ftasset.status = "posted";
            ftasset.fk_createdbyuser_user_financetransaction = loggedinuserid;
            db.financetransaction.Add(ftasset);
            db.SaveChanges();
        }

        public static void inserttransaction(int fromaccount, int toaccount, double amount)
        {
            var loggedinuserid = userutils.loggedinuser.id;
            var db = new dbctx();

            financetransaction ftexpence = new financetransaction();
            ftexpence.amount = -amount;
            ftexpence.fk_financeaccount_financeaccount_financetransaction = fromaccount;
            ftexpence.date = DateTime.Now;
            ftexpence.status = "posted";
            ftexpence.fk_createdbyuser_user_financetransaction = loggedinuserid;
            db.financetransaction.Add(ftexpence);
            db.SaveChanges();

            financetransaction ftasset = new financetransaction();
            ftasset.amount = amount;
            ftasset.fk_financeaccount_financeaccount_financetransaction = toaccount;
            ftasset.date = DateTime.Now;
            ftasset.status = "posted";
            ftasset.fk_createdbyuser_user_financetransaction = loggedinuserid;
            db.financetransaction.Add(ftasset);
            db.SaveChanges();
        }

    }
}
