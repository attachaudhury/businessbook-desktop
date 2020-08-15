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
    /// Interaction logic for transactions.xaml
    /// </summary>
    public partial class transactions : Window
    {
        public transactions()
        {
            InitializeComponent();
            var db = new RMSDBEntities();
            foreach (var item in db.financetransaction.ToList())
            {
                dg.Items.Add(item);
            }
        }
    }
}
