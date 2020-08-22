using RIAB_Restaurent_Management_System.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIAB_Restaurent_Management_System.bll
{
    public class MyPrinterSetting
    {
        public static int pageWidth = Settings.Default.PrinterPageWidth;
        public static int marginLeft = Settings.Default.PrinterMarginLeft;
        public static String Title = Settings.Default.Title;
        public static String SubTitle = Settings.Default.SubTitle;
        public static String Footer = Settings.Default.Footer;
        public static int Reciptlineheight = Settings.Default.Reciptlineheight;

        public static void saveSettings(int pageWidth, int magrinLeft,String title,String subTitle,String footer,int Reciptlineheight)
        {
            Settings.Default.PrinterPageWidth = pageWidth;
            Settings.Default.PrinterMarginLeft = magrinLeft;
            Settings.Default.Title = title;
            Settings.Default.SubTitle = subTitle;
            Settings.Default.Footer = footer;
            Settings.Default.Reciptlineheight = Reciptlineheight;
            Settings.Default.Save();
    }
    }
}
