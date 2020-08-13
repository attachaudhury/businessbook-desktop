using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DBOperations
{
    public class Deposit
    {
        public static tbl_Deposit getById(int id)
        {
            RMSDBEntities db = DBContext.getInstance();
            return db.tbl_Deposit.Find(id);
        }
        public static void insert(tbl_Deposit customer)
        {
            RMSDBEntities db = DBContext.getInstance();
            db.tbl_Deposit.Add(customer);
            db.SaveChanges();
        }
        public static void delete(tbl_Deposit customer)
        {
            RMSDBEntities db = DBContext.getInstance();
            db.tbl_Deposit.Remove(customer);
            db.SaveChanges();
        }
        public static List<tbl_Deposit> getAll()
        {
            RMSDBEntities db = DBContext.getInstance();
            return db.tbl_Deposit.ToList();
        }
        public static List<tbl_Deposit> getAllToday()
        {
            RMSDBEntities db = DBContext.getInstance();
            DateTime startDateTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " 12:00:00 AM");
            DateTime endDateTime = DateTime.Now;
            return db.tbl_Deposit.Where(a => a.DatenTime >= startDateTime).Where(a => a.DatenTime <= endDateTime).ToList();
        }
        public static void update(tbl_Deposit customer)
        {
            RMSDBEntities db = DBContext.getInstance();
            db.Entry(customer).State = EntityState.Modified;
            db.Configuration.ValidateOnSaveEnabled = false;
            db.SaveChanges();
            db.Configuration.ValidateOnSaveEnabled = true;
        }
        public static double getAllTodayAmmount()
        {
            RMSDBEntities db = DBContext.getInstance();
            double ammount = 0;
            foreach (tbl_Deposit deposit in getAllToday())
            {
                ammount += (double)deposit.Ammount;
            }
            return ammount;
        }
    }
}
