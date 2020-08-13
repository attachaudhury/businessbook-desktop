using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL.DBOperations
{
    public class DealItem
    {
        public static List<tbl_DealItem> getAll()
        {
            RMSEntities db = DBContext.getInstance();
            return db.tbl_DealItem.ToList();
        }

    }
}
