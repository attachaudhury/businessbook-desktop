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



        public static softwaresetting ravicosoftuserid;
        public static softwaresetting ravicosoftusername;
        public static softwaresetting ravicosoftuserpassword;
        public static softwaresetting membershiptype;
        public static softwaresetting membershipexpirydate;
        public static softwaresetting canrunsoftware;
        public static softwaresetting cansendsms;
        public static softwaresetting apiendpoint;
        public static void loadsoftwaresetting()
        {
            var ssr = new softwaresettingrepo();
            ravicosoftuserid = ssr.getbyname(commonsettings.ravicosoftuserid);
            ravicosoftusername = ssr.getbyname(commonsettings.ravicosoftusername);
            ravicosoftuserpassword = ssr.getbyname(commonsettings.ravicosoftuserpassword);
            membershiptype = ssr.getbyname(commonsettings.membershipexpirydate);
            membershipexpirydate = ssr.getbyname(commonsettings.membershipexpirydate);
            canrunsoftware = ssr.getbyname(commonsettings.canrunsoftware);
            cansendsms = ssr.getbyname(commonsettings.cansendsms);
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
        public static string ravicosoftuserpassword = "ravicosoftuserpassword";
        public static string membershiptype = "membershiptype";
        public static string membershipexpirydate = "membershipexpirydate";
        public static string canrunsoftware = "canrunsoftware";
        public static string cansendsms = "cansendsms";
        public static string apiendpoint = "apiendpoint";
    }
}
