using RIAB_Restaurent_Management_System.Properties;
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

namespace RIAB_Restaurent_Management_System.Views.others
{
    /// <summary>
    /// Interaction logic for Window_Setting.xaml
    /// </summary>
    public partial class Window_Setting : Window
    {
        public Window_Setting()
        {
            InitializeComponent();
            tb_PageWidth.Text = Convert.ToString(MyPrinterSetting.pageWidth) ;
            tb_MarginLeft.Text = Convert.ToString(MyPrinterSetting.marginLeft);
            tb_Title.Text = MyPrinterSetting.Title;
            tb_SubTitle.Text = MyPrinterSetting.SubTitle;
            tb_Footer.Text = MyPrinterSetting.Footer;
            tb_Reciptlineheight.Text = Convert.ToString(MyPrinterSetting.Reciptlineheight);
        }

        private void btn_Save(object sender, RoutedEventArgs e)
        {
            try
            {
                int pageWidth = Convert.ToInt32(tb_PageWidth.Text);
                int marginLeft = Convert.ToInt32(tb_MarginLeft.Text);
                string title = tb_Title.Text;
                string subTitle = tb_SubTitle.Text;
                string footer = tb_Footer.Text;
                int Reciptlineheight = Convert.ToInt32(tb_Reciptlineheight.Text);
                MyPrinterSetting.saveSettings(pageWidth, marginLeft, title, subTitle, footer, Reciptlineheight);
                AutoClosingMessageBox.Show("Setting Saved","Success",2000);
                
            }
            catch
            {

            }
        }
    }
}
