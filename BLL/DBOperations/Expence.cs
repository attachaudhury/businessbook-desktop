using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data.Entity;

namespace BLL.DBOperations
{
    public class Expence
    {
        public static void insert(tbl_Expence e)
        {
            RMSDBEntities db = DBContext.getInstance();
            db.tbl_Expence.Add(e);
            db.SaveChanges();
        }
        public static void deleteById(int id)
        {
            RMSDBEntities db = DBContext.getInstance();
            tbl_Expence e = db.tbl_Expence.Find(id);
            db.tbl_Expence.Remove(e);
            db.SaveChanges();
        }
        public static void deleteByExpenceHeadId(int expenceHeadId)
        {
            RMSDBEntities db = DBContext.getInstance();
            List<tbl_Expence> list = db.tbl_Expence.Where(a=>a.ExpenseHead_Id == expenceHeadId).ToList();
            db.tbl_Expence.RemoveRange(list);
            db.SaveChanges();
        }
        public static void deleteBySubExpenceHeadId(int expenceSubHeadId)
        {
            RMSDBEntities db = DBContext.getInstance();
            List<tbl_Expence> list = db.tbl_Expence.Where(a => a.ExpenceSubHead_Id == expenceSubHeadId).ToList();
            db.tbl_Expence.RemoveRange(list);
            db.SaveChanges();
        }
        public static List<tbl_Expence> getAll()
        {
            RMSDBEntities db = DBContext.getInstance();
            return db.tbl_Expence.ToList();
        }
        public static List<tbl_Expence> getAllToday()
        {
            RMSDBEntities db = DBContext.getInstance();
            DateTime startDateTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " 12:00:00 AM");
            DateTime endDateTime = DateTime.Now;

            return db.tbl_Expence.Where(a => a.DatenTime >= startDateTime).Where(a => a.DatenTime <= endDateTime).ToList();
        }
        public static List<tbl_Expence> getAllThisMonth()
        {
            RMSDBEntities db = DBContext.getInstance();
            DateTime now = DateTime.Now;
            var thisMonthStartDateTime = new DateTime(now.Year, now.Month, 1);
            DateTime endDateTime = DateTime.Now;
            return db.tbl_Expence.Where(a => a.DatenTime >= thisMonthStartDateTime).Where(a => a.DatenTime <= endDateTime).ToList();
        }
        public static List<tbl_Expence> getAllCustomDate(DateTime startDate, DateTime endDate)
        {
            RMSDBEntities db = DBContext.getInstance();
            return db.tbl_Expence.Where(a => a.DatenTime >= startDate).Where(a => a.DatenTime <= endDate).ToList();
        }
        public static int getTodayTotalAmmount()
        {
            int ammount = 0;
            foreach (tbl_Expence expence in getAllToday()) {
                ammount += (int)expence.Amount;
            }
            return ammount;
        }
        public static int getThisMonthTotalAmmount()
        {
            int ammount = 0;
            foreach (tbl_Expence expence in getAllThisMonth())
            {
                ammount += (int)expence.Amount;
            }
            return ammount;
        }
        public static int getCustomDateTotalAmmount(DateTime startDate, DateTime endDate)
        {
            int ammount = 0;
            foreach (tbl_Expence expence in getAllCustomDate(startDate, endDate))
            {
                ammount += (int)expence.Amount;
            }
            return ammount;
        }
        public static int getAllTotalAmmount()
        {
            int ammount = 0;
            foreach (tbl_Expence expence in getAll())
            {
                ammount += (int)expence.Amount;
            }
            return ammount;
        }
        public static List<tbl_Expence> getAllByHeadIdAndDateTime(int headId,DateTime startDate, DateTime endDate)
        {
            RMSDBEntities db = DBContext.getInstance();
            return db.tbl_Expence.Where(a=>a.ExpenseHead_Id == headId).Where(a => a.DatenTime >= startDate).Where(a => a.DatenTime <= endDate).ToList();
        }
        public static List<tbl_Expence> getAllBySubHeadIdAndDateTime(int subHeadId, DateTime startDate, DateTime endDate)
        {
            RMSDBEntities db = DBContext.getInstance();
            return db.tbl_Expence.Where(a => a.ExpenceSubHead_Id == subHeadId).Where(a => a.DatenTime >= startDate).Where(a => a.DatenTime <= endDate).ToList();
        }
    }
}
