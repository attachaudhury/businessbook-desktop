using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL.DBOperations
{
    public class DealFoodItem
    {
        public static List<tbl_DealFoodItem> getAll()
        {
            RMSDBEntities db = DBContext.getInstance();
            return db.tbl_DealFoodItem.ToList();
        }

    }
}
