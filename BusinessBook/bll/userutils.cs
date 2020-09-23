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
        public static void authorizerole(Window window, string[] roles)
        {

            if (roles.Contains(loggedinuserd.role))
            {
                window.Show();
            }
            else
            {
                RadDesktopAlertManager manager = new RadDesktopAlertManager();
                var alert = new RadDesktopAlert();
                alert.Header = "Alert";
                alert.Content = "This page can only accessed by " + String.Join(",", roles);
                alert.ShowDuration = 5000;
                System.Media.SystemSounds.Hand.Play();
                manager.ShowAlert(alert);
            }
        }
        public static void authorizeroleandmembership(Window window, string[] roles, string[] membershiptypes)
        {
            bool roleok = false;
            bool membershipokok = false;
            if (roles.Contains(loggedinuserd.role))
            {
                roleok = true;
            }
            if (membershiptypes.Contains(membership))
            {
                membershipokok = true;
            }
            if (roleok && membershipokok) 
            {
                window.Show();
            }
            else
            {
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
