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
using BLL;
using BLL.DBOperations;
using DAL;

namespace RIAB_Restaurent_Management_System.Views.kitchen
{
    /// <summary>
    /// Interaction logic for Form_LinkKitchenInventory.xaml
    /// </summary>
    public partial class Form_LinkKitchenInventory : Window
    {
        public Form_LinkKitchenInventory()
        {
            InitializeComponent();
            dg_AllLinkedItems.ItemsSource = DetuctInventory.getAll();
        }

        private void btn_LinkFoodsToInventory(object sender, RoutedEventArgs e)
        {
            new Form_LinkFoodsToInventory().Show();
            Close();
        }

        private void btn_Delete(object sender, RoutedEventArgs e)
        {
            if (dg_AllLinkedItems.SelectedItem != null)
            {
                tbl_DetuctInventory di = (tbl_DetuctInventory)dg_AllLinkedItems.SelectedItem;
                DetuctInventory.delete(di);
                dg_AllLinkedItems.ItemsSource = null;
                dg_AllLinkedItems.ItemsSource = DetuctInventory.getAll();
            }
        }
    }
}
