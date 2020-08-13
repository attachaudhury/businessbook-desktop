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

namespace RIAB_Restaurent_Management_System.Views.person
{
    /// <summary>
    /// Interaction logic for List.xaml
    /// </summary>
    public partial class List : Window
    {
        public List(string roletype)
        {
            InitializeComponent();
            var db = new RMSDBEntities();
            if (roletype == "staff")
            {
                dg_AllStaff.ItemsSource = db.user.Where(a => a.role != "customer").ToList();
            }
            else
            {
                dg_AllStaff.ItemsSource = db.user.Where(a => a.role == "customer").ToList();
            }
        }
    }
}
