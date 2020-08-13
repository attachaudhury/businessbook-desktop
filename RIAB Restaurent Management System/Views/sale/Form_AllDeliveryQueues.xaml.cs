using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DAL;
using BLL;
using BLL.DBOperations;
using BLL.DBOperations.TmpModels;

namespace RIAB_Restaurent_Management_System.Views.sale
{
    public partial class Form_AllDeliveryQueues : Window
    {
        public Form_AllDeliveryQueues()
        {
            InitializeComponent();
            initFormOperations();
        }
        void initFormOperations()
        {
            dg_AllQueues.Items.Clear();
            foreach (DeliveryQueueModel item in DeliveryQueue.getAllNotDeliveredMappedToDeliveryQueueModel()) 
            {
                dg_AllQueues.Items.Add(item);
            }
        }

        private void btn_Delivered(object sender, RoutedEventArgs e)
        {
            if (dg_AllQueues.SelectedItem != null) {
                DeliveryQueueModel dmq = (DeliveryQueueModel)dg_AllQueues.SelectedItem;
                DeliveryQueue.updateStatus(dmq.Id);
                initFormOperations();
            }
        }

        private void btn_Canceled(object sender, RoutedEventArgs e)
        {
            if (dg_AllQueues.SelectedItem != null)
            {
                DeliveryQueueModel dmq = (DeliveryQueueModel)dg_AllQueues.SelectedItem;
                Sale.deleteASaleById(dmq.SaleId);
                DeliveryQueue.deleteById(dmq.Id);
                initFormOperations();
            }
        }
    }
}
