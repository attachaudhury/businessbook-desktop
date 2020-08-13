using BLL.DBOperations;
using DAL;
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

namespace RIAB_Restaurent_Management_System.Views.finance
{
    /// <summary>
    /// Interaction logic for Window_NewDepositHead.xaml
    /// </summary>
    public partial class Window_NewDepositHead : Window
    {
        public Window_NewDepositHead()
        {
            InitializeComponent();
        }

        private void btn_Save(object sender, RoutedEventArgs e)
        {
            if (tb_Name.Text != "")
            {
                tbl_DepositHead dh = new tbl_DepositHead();
                dh.Name = tb_Name.Text;
                DepositHead.insert(dh);
                Close();
            }
        }
    }
}
