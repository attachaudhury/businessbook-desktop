using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class TimeUtils
    {
        public static DateTime getStartDate(DateTime? d)
        {
            DateTime tempStartDate = Convert.ToDateTime(d);
            DateTime startDate = Convert.ToDateTime(tempStartDate.ToShortDateString() + " 12:00:00 AM");
            return startDate;
        }
        public static DateTime getEndDate(DateTime? d)
        {
            DateTime tempEndDate = Convert.ToDateTime(d);
            DateTime endDate = Convert.ToDateTime(tempEndDate.ToShortDateString() + " 11:59:59 PM");
            return endDate;
        }
    }
}
