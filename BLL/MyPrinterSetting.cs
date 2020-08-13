using BLL.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MyPrinterSetting
    {
        public static int pageWidth = Settings1.Default.PrinterPageWidth;
        public static int marginLeft = Settings1.Default.PrinterMarginLeft;
        public static String Title = Settings1.Default.Title;
        public static String SubTitle = Settings1.Default.SubTitle;
        public static String Footer = Settings1.Default.Footer;
        public static int Reciptlineheight = Settings1.Default.Reciptlineheight;

        public static void saveSettings(int pageWidth, int magrinLeft,String title,String subTitle,String footer,int Reciptlineheight)
        {
            Settings1.Default.PrinterPageWidth = pageWidth;
            Settings1.Default.PrinterMarginLeft = magrinLeft;
            Settings1.Default.Title = title;
            Settings1.Default.SubTitle = subTitle;
            Settings1.Default.Footer = footer;
            Settings1.Default.Reciptlineheight = Reciptlineheight;
            Settings1.Default.Save();
    }
    }
}
