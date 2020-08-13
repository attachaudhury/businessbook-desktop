using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DBOperations
{
    public class DepositHead
    {
        public static tbl_DepositHead getById(int id)
        {
            RMSDBEntities db = DBContext.getInstance();
            return db.tbl_DepositHead.Find(id);
        }
        public static void insert(tbl_DepositHead customer)
        {
            RMSDBEntities db = DBContext.getInstance();
            db.tbl_DepositHead.Add(customer);
            db.SaveChanges();
        }
        public static void delete(tbl_DepositHead customer)
        {
            RMSDBEntities db = DBContext.getInstance();
            db.tbl_DepositHead.Remove(customer);
            db.SaveChanges();
        }
        public static List<tbl_DepositHead> getAll()
        {
            RMSDBEntities db = DBContext.getInstance();
            return db.tbl_DepositHead.ToList();
        }
        public static void update(tbl_DepositHead customer)
        {
            RMSDBEntities db = DBContext.getInstance();
            db.Entry(customer).State = EntityState.Modified;
            db.Configuration.ValidateOnSaveEnabled = false;
            db.SaveChanges();
            db.Configuration.ValidateOnSaveEnabled = true;
        }
    }
}
