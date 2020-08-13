using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data.Entity;
using BLL.DBOperations.TmpModels;

namespace BLL.DBOperations
{
    public class ExpenceSubHead
    {
        public static tbl_ExpenceSubHead getById(int id)
        {
            RMSDBEntities db = DBContext.getInstance();
            return db.tbl_ExpenceSubHead.Find(id);
        }
        public static void insert(tbl_ExpenceSubHead esh)
        {
            RMSDBEntities db = DBContext.getInstance();
            db.tbl_ExpenceSubHead.Add(esh);
            db.SaveChanges();
        }
        public static void delete(tbl_ExpenceSubHead esh)
        {
            RMSDBEntities db = DBContext.getInstance();
            Expence.deleteBySubExpenceHeadId(esh.Id);
            db.tbl_ExpenceSubHead.Remove(esh);
            db.SaveChanges();
        }
        public static void update(tbl_ExpenceSubHead esh)
        {
            RMSDBEntities db = DBContext.getInstance();
            db.Entry(esh).State = EntityState.Modified;
            db.Configuration.ValidateOnSaveEnabled = false;
            db.SaveChanges();
            db.Configuration.ValidateOnSaveEnabled = true;
        }
        public static void  deleteByExpenceHeadId(int expenceHeadId)
        {
            RMSDBEntities db = DBContext.getInstance();
            List<tbl_ExpenceSubHead> list = db.tbl_ExpenceSubHead.Where(a => a.ExpenseHead_Id == expenceHeadId).ToList();
            db.tbl_ExpenceSubHead.RemoveRange(list);
            db.SaveChanges();
        }
        public static List<tbl_ExpenceSubHead> getAll()
        {
            RMSDBEntities db = DBContext.getInstance();
            return db.tbl_ExpenceSubHead.ToList();
        }
        public static List<tbl_ExpenceSubHead> getAllByExpenceHeadId(int id)
        {
            RMSDBEntities db = DBContext.getInstance();
            return db.tbl_ExpenceSubHead.Where(a=>a.ExpenseHead_Id == id).ToList();
        }
        public static List<SubHeadHeadNameModel> getAllMappedToSubHeadHeadNameModel()
        {
            List<SubHeadHeadNameModel> list = new List<SubHeadHeadNameModel>();
            foreach (tbl_ExpenceSubHead esh in getAll())
            {
                SubHeadHeadNameModel mm = new SubHeadHeadNameModel();
                mm.Id = esh.Id;
                mm.Head_Id = (int)esh.ExpenseHead_Id;
                mm.Name = esh.Name;
                tbl_ExpenceHead eh = ExpenceHead.getById(mm.Head_Id);
                mm.HeadName = eh.Name;
                list.Add(mm);
            }
            return list;
            
        }
    }
}
