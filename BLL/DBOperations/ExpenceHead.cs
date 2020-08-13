using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data.Entity;

namespace BLL.DBOperations
{
    public class ExpenceHead
    {
        public static tbl_ExpenceHead getById(int id)
        {
            RMSDBEntities db = DBContext.getInstance();
            return db.tbl_ExpenceHead.Find(id);
        }
        public static void insert(tbl_ExpenceHead eh) 
        {
            RMSDBEntities db = DBContext.getInstance();
            db.tbl_ExpenceHead.Add(eh);
            db.SaveChanges();
        }
        public static void delete(tbl_ExpenceHead eh)
        {
            RMSDBEntities db = DBContext.getInstance();
            ExpenceSubHead.deleteByExpenceHeadId(eh.Id);
            Expence.deleteByExpenceHeadId(eh.Id);
            db.tbl_ExpenceHead.Remove(eh);
            db.SaveChanges();
        }
        public static void update(tbl_ExpenceHead eh)
        {
            RMSDBEntities db = DBContext.getInstance();
            db.Entry(eh).State = EntityState.Modified;
            db.Configuration.ValidateOnSaveEnabled = false;
            db.SaveChanges();
            db.Configuration.ValidateOnSaveEnabled = true;
        }
        public static List<tbl_ExpenceHead> getAll()
        {
            RMSDBEntities db = DBContext.getInstance();
            return db.tbl_ExpenceHead.ToList();
        }
    }
}
