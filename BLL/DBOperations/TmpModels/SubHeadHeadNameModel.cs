using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DBOperations.TmpModels
{
    public class SubHeadHeadNameModel
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public int Head_Id { set; get; }
        public string HeadName { set; get; }
    }
}
