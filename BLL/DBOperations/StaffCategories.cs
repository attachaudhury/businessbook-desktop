using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL.DBOperations
{
    public class StaffCategories
    {
        public static void insert(tbl_StaffCategory cat)
        {
            RMSDBEntities db = DBContext.getInstance();
            db.tbl_StaffCategory.Add(cat);
            db.SaveChanges();
        }
        public static List<tbl_StaffCategory> getAll()
        {
            RMSDBEntities db = DBContext.getInstance();
            return db.tbl_StaffCategory.ToList();
        }
    }
}
