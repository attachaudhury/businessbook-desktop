using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Telerik.Windows.Controls;

namespace BusinessBook.bll
{
    public class userutils
    {
        //public static data.user loggedinuser { get; set; }
        public static data.dapper.user loggedinuserd { get; set; }
        public static string membership { get; set; }
        public static void showauthorize(Window window, string[] roles)
        {
            

            if (roles.Contains(loggedinuserd.role))
            {
                window.Show();
            }
            else
            {
                window = null;
                RadDesktopAlertManager manager = new RadDesktopAlertManager();
                var alert = new RadDesktopAlert();
                alert.Header = "Alert";
                alert.Content = "This page can only accessed by " + String.Join(",", roles);
                alert.ShowDuration = 5000;
                System.Media.SystemSounds.Hand.Play();
                manager.ShowAlert(alert);
            }
        }
    }
}
