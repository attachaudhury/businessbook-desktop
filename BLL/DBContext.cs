using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public sealed class DBContext
    {
        private static RMSDBEntities db = new RMSDBEntities();

        public static RMSDBEntities getInstance()
        {
            if (db == null)
            {
                return new RMSDBEntities();
            }
            return db;
        }
        public static void removeContext()
        {
            db = null; 
        }
    }
}

