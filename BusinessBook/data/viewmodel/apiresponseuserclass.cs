using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessBook.data.viewmodel
{
    public class apiresponseuserclass
    {
        public string _id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string businessbookmembershipplan { get; set; } // values are Package 1,Package 2,Package 3
        public DateTime? businessbookmembershipexpirydate { get; set; } // values are none,Package 1,Package 2,Package 3,Package 4
        public string businessbookcanrun { get; set; }
        public string smsplan { get; set; }
    }
}
