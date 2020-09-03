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

namespace RIAB_Restaurent_Management_System.Views.others
{
    /// <summary>
    /// Interaction logic for registersoftware.xaml
    /// </summary>
    public partial class registersoftware : Window
    {
        public registersoftware()
        {
            InitializeComponent();
        }
        private void registerbtn(object sender, RoutedEventArgs e)
        {
            
        }
        private void btn_CloseApplication(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
