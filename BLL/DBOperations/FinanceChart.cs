using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL.DBOperations
{
    public class FinanceChart
    {
        public static void insert(tbl_FinanceChart fc)
        {
            RMSDBEntities db = DBContext.getInstance();
            db.tbl_FinanceChart.Add(fc);
            db.SaveChanges();
        }
        public static List<tbl_FinanceChart> getAll()
        {
            RMSDBEntities db = DBContext.getInstance();
            return db.tbl_FinanceChart.ToList();
        }
        public static List<tbl_FinanceChart> getAllCustomDate(DateTime fromDate, DateTime toDate)
        {
            RMSDBEntities db = DBContext.getInstance();
            return db.tbl_FinanceChart.Where(a=>a.Date >= fromDate).Where(a=>a.Date <= toDate).ToList();
        }
        public static int getLastDayClosingBalance()
        {
            RMSDBEntities db = DBContext.getInstance();
            tbl_FinanceChart fc = db.tbl_FinanceChart.OrderByDescending(a=>a.Id).FirstOrDefault();
            if (fc == null)
            {
                return 0;
            }
            else
            {
                int i = Convert.ToInt32(fc.ClosingBalance);
                return i;
            }
        }
        public static int getTotalDeposit()
        {
            RMSDBEntities db = DBContext.getInstance();
            int totalDeposit = 0;
            foreach (tbl_FinanceChart item in getAll())
            {
                totalDeposit += (int)item.Deposit;
            }

            return totalDeposit;
        }
    }
}
