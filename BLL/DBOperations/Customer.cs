using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data.Entity;

namespace BLL.DBOperations
{
    public class Customer
    {
        public static tbl_Customer getById(int id)
        {
            RMSDBEntities db = DBContext.getInstance();
            return db.tbl_Customer.Find(id);
        }
        public static tbl_Customer getByPhoneNumber(string number)
        {
            RMSDBEntities db = DBContext.getInstance();
            return db.tbl_Customer.Where(a=>a.PhoneNo == number).FirstOrDefault();
        }
        public static void insert(tbl_Customer customer) 
        {
            RMSDBEntities db = DBContext.getInstance();
            db.tbl_Customer.Add(customer);
            db.SaveChanges();
        }
        public static void delete(tbl_Customer customer)
        {
            RMSDBEntities db = DBContext.getInstance();
            db.tbl_Customer.Remove(customer);
            db.SaveChanges();
        }
        public static List<tbl_Customer> getAll()
        {
            RMSDBEntities db = DBContext.getInstance();
            return db.tbl_Customer.ToList();
        }
        public static void update(tbl_Customer customer)
        {
            RMSDBEntities db = DBContext.getInstance();
            db.Entry(customer).State = EntityState.Modified;
            db.Configuration.ValidateOnSaveEnabled = false;
            db.SaveChanges();
            db.Configuration.ValidateOnSaveEnabled = true;
        }
    }
}
