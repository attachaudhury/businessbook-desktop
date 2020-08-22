using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIAB_Restaurent_Management_System.data
{
    public sealed class DBContext
    {
        private static dbctx db = new dbctx();

        public static dbctx getInstance()
        {
            if (db == null)
            {
                return new dbctx();
            }
            return db;
        }
        public static void removeContext()
        {
            db = null; 
        }
    }
}

