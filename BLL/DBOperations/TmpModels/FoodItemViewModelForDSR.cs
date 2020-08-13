using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DBOperations.TmpModels
{
    public class FoodItemViewModelForDSR
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public int SalePrice { set; get; }
        public int Quantity { set; get; }
        public int Total { set; get; }
        public DateTime Date { set; get; }
        public int SaleId { set; get; }
        public string CategoryName { set; get; }
    }
}
