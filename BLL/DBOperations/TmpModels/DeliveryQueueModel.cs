using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DBOperations.TmpModels
{
    public class DeliveryQueueModel
    {
        public int Id { set; get; }
        public int SaleId { set; get; }
        public int CustomerId { set; get; }
        public int TotalBill { set; get; }
        public string DeliveryBoyName { set; get; }
    }
}
