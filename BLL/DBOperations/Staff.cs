using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data.Entity;

namespace BLL.DBOperations
{
    public class Staff
    {
        public static tbl_Staff getById(int id)
        {
            RMSDBEntities db = DBContext.getInstance();
            return db.tbl_Staff.Find(id);
        }
        public static void insert(tbl_Staff staff)
        {
            RMSDBEntities db = DBContext.getInstance();
            db.tbl_Staff.Add(staff);
            db.SaveChanges();
        }
        public static void delete(tbl_Staff staff)
        {
            RMSDBEntities db = DBContext.getInstance();
            db.tbl_Staff.Remove(staff);
            db.SaveChanges();
        }
        public static void update(tbl_Staff staff)
        {
            RMSDBEntities db = DBContext.getInstance();
            db.Entry(staff).State = EntityState.Modified;
            db.Configuration.ValidateOnSaveEnabled = false;
            db.SaveChanges();
            db.Configuration.ValidateOnSaveEnabled = true;
        }
        public static List<tbl_Staff> getAll()
        {
            RMSDBEntities db = DBContext.getInstance();
            return db.tbl_Staff.ToList();
        }
        public static List<tbl_Staff> getAllDeliveryStaff()
        {
            RMSDBEntities db = DBContext.getInstance();
            return db.tbl_Staff.Where(a=>a.StaffCategory_Id == 2).ToList();
        }
    }
}
