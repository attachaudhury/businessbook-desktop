using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DBOperations.TmpModels
{
    public class ItemOrDealSaleModel
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public int SalePrice { set; get; }
        public int Quantity { set; get; }
        public int Total { set; get; }
    }
}
