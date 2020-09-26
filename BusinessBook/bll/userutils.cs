using BusinessBook.data.dapper;
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
        public static softwaresetting ravicosoftuserid;
        public static softwaresetting ravicosoftusername;
        public static softwaresetting ravicosoftpassword;
        public static softwaresetting ravicosoftbusinessbookmembershipplan;
        public static softwaresetting ravicosoftbusinessbookmembershipexpirydate;
        public static softwaresetting ravicosoftbusinessbookcanrun;
        public static softwaresetting ravicosoftsmsplan;
        public static softwaresetting apiendpoint;

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

        public static bool checkravicosoftuseridexits() 
        {
            var ravicosoftuser = ravicosoftuserid;
            if (ravicosoftuser == null)
            {
                return false;
            }
            else {
                return true;
            }
        }
        public static void loadsoftwaresetting()
        {
            var ssr = new softwaresettingrepo();
            ravicosoftuserid = ssr.getbyname(commonsettings.ravicosoftuserid);
            ravicosoftusername = ssr.getbyname(commonsettings.ravicosoftusername);
            ravicosoftpassword = ssr.getbyname(commonsettings.ravicosoftpassword);
            ravicosoftbusinessbookmembershipplan = ssr.getbyname(commonsettings.ravicosoftbusinessbookmembershipplan);
            ravicosoftbusinessbookmembershipexpirydate = ssr.getbyname(commonsettings.ravicosoftbusinessbookmembershipexpirydate);
            ravicosoftbusinessbookcanrun = ssr.getbyname(commonsettings.ravicosoftbusinessbookcanrun);
            ravicosoftsmsplan = ssr.getbyname(commonsettings.ravicosoftsmsplan);
            apiendpoint = ssr.getbyname(commonsettings.apiendpoint);

        }
        public static void updateapiendpoint(string newurl)
        {
            var ssr = new softwaresettingrepo();
            if (apiendpoint == null)
            {
                var ss = new softwaresetting() { name = commonsettings.apiendpoint, valuetype = "string", stringvalue = newurl };
                apiendpoint = ssr.save(ss);
            }
            else
            {
                apiendpoint.stringvalue = newurl;
                apiendpoint = ssr.update(apiendpoint);
            }
        }
    }
    public class commonsettings
    {
        public static string ravicosoftuserid = "ravicosoftuserid";
        public static string ravicosoftusername = "ravicosoftusername";
        public static string ravicosoftpassword = "ravicosoftpassword";
        public static string ravicosoftbusinessbookmembershipplan = "ravicosoftbusinessbookmembershipplan"; // values are Package 1,Package 2,Package 3
        public static string ravicosoftbusinessbookmembershipexpirydate = "ravicosoftbusinessbookmembershipexpirydate";
        public static string ravicosoftbusinessbookcanrun = "ravicosoftbusinessbookcanrun";
        public static string ravicosoftsmsplan = "ravicosoftsmsplan"; // values are none,Package 1,Package 2,Package 3,Package 4
        public static string apiendpoint = "apiendpoint";
    }
}
