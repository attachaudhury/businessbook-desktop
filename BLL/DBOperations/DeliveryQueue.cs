using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data.Entity;
using BLL.DBOperations.TmpModels;
namespace BLL.DBOperations
{
    public class DeliveryQueue
    {
        public static void insert(tbl_DeliveryQueue dq) 
        {
            RMSDBEntities db = DBContext.getInstance();
            db.tbl_DeliveryQueue.Add(dq);
            db.SaveChanges();
        }
        public static void deleteById(int id)
        {
            RMSDBEntities db = DBContext.getInstance();
            db.tbl_DeliveryQueue.Remove(db.tbl_DeliveryQueue.Find(id));
            db.SaveChanges();
        }
        public static void updateStatus(int id)
        {
            RMSDBEntities db = DBContext.getInstance();
            tbl_DeliveryQueue dq = db.tbl_DeliveryQueue.Find(id);
            dq.Delivered = true;
            db.Entry(dq).State = EntityState.Modified;
            db.Configuration.ValidateOnSaveEnabled = false;
            db.SaveChanges();
            db.Configuration.ValidateOnSaveEnabled = true;

        }
        public static List<tbl_DeliveryQueue> getAllNotDelivered()
        {
            RMSDBEntities db = DBContext.getInstance();
            return db.tbl_DeliveryQueue.Where(a=>a.Delivered == false).ToList();
        }
        public static List<DeliveryQueueModel> getAllNotDeliveredMappedToDeliveryQueueModel()
        {
            List<DeliveryQueueModel> list = new List<DeliveryQueueModel>();
            foreach(tbl_DeliveryQueue item in getAllNotDelivered()){
                DeliveryQueueModel dmq = new DeliveryQueueModel();
                dmq.Id = item.Id;
                dmq.SaleId = (int)item.Sale_Id;
                dmq.CustomerId = (int)item.Customer_Id;
                tbl_Sale sale = Sale.getById(dmq.SaleId);
                dmq.TotalBill = (int)sale.Amount;
                try
                {
                    tbl_Staff staff  = Staff.getById((int)item.DeliveryBoyId);
                    dmq.DeliveryBoyName = staff.Name;
                } catch { }
                list.Add(dmq);
            }
            return list;
        }
        public static int getAmmountOfDeliveryOfaBoyWithDate(int deleiveryBoyId, DateTime fromDate, DateTime toDate)
        {
            RMSDBEntities db = DBContext.getInstance();
            List<tbl_DeliveryQueue> list = db.tbl_DeliveryQueue.Where(a => a.DeliveryBoyId == deleiveryBoyId).Where(a=>a.DatenTime >= fromDate).Where(a => a.DatenTime <= toDate).ToList();
            int ammount = 0;
            foreach (tbl_DeliveryQueue item in list)
            {
                tbl_Sale sale = Sale.getById((int)item.Sale_Id);
                ammount += (int)sale.Amount;
            }
            return ammount;
        }
    }
}
