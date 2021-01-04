using BusinessBook.data.dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;

namespace BusinessBook.bll
{
    class otherutils
    {
        static RadDesktopAlertManager manager = new RadDesktopAlertManager();
        
        public static void notify(string title,string text, int timems)
        {
            var alert = new RadDesktopAlert();
            alert.Header = title;
            alert.Content = text;
            alert.ShowDuration = timems;
            System.Media.SystemSounds.Hand.Play();
            manager.ShowAlert(alert);
        }

        public static bool checkmessagevalidation(string message, string[] numbers) 
        {
            if (message == "")
            {
                otherutils.notify("Info", "Type message", 10000);
                return false;
            }
            if (numbers.Length == 0)
            {
                otherutils.notify("Info", "No valid numbers selected", 10000);
                return false;
            }
            var smsplan = userutils.ravicosoftsmsplan;
            if (smsplan == null || smsplan.stringvalue == "none" || smsplan.stringvalue == "" || smsplan.stringvalue == "undefined")
            {
                otherutils.notify("Info", "Please update your message plan to send sms", 10000);
                return false;
            }
            return true;
        }

        public static string[] parsenumbersfromdynamiclist(dynamic obj) 
        {
            var numbers = new List<string>();
            if (obj is string)
            {
                var parsednumber = parsenumber(obj);
                if (parsednumber != "")
                {
                    numbers.Add(parsednumber);
                }
            }
            else if (obj is string[])
            {
                for (int i = 0; i < obj.length; i++)
                {
                    var parsednumber = parsenumber(obj[i]);
                    if (parsednumber != "")
                    {
                        numbers.Add(parsednumber);
                    }

                }
            }
            else if (obj is List<string>)
            {
                for (int i = 0; i < obj.Count(); i++)
                {
                    var parsednumber = parsenumber(obj[i]);
                    if (parsednumber != "")
                    {
                        numbers.Add(parsednumber);
                    }

                }
            }
            else if (obj is data.dapper.user) 
            {
                var parsednumber = parsenumberfromuserobject(obj);
                if (parsednumber != "")
                {
                    numbers.Add(parsednumber);
                }
            }
            else
            {
                for (int i = 0; i < obj.Count; i++)
                {
                    var ob = obj[i];
                    var parsednumber = parsenumberfromuserobject(ob);
                    if (parsednumber != "")
                    {
                        numbers.Add(parsednumber);
                    }
                }
                
            }
            return numbers.ToArray();
        }

        public static string[] parsenumbersfromcommaorspaceseperatedstring(string commaorspaceseperatednumbers)
        {

            var numbers = new List<string>();
            string[] obj = commaorspaceseperatednumbers.Split(new[] { ',', ' ' },StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < obj.Length; i++)
            {
                var parsednumber = parsenumber(obj[i]);
                if (parsednumber != "")
                {
                    numbers.Add(parsednumber);
                }

            }
            return numbers.ToArray();
        }
        public static string parsenumberfromuserobject(data.dapper.user user)
        {
            string res = "";
            var phone1exists = true;
            var phone2exists = true;
            if (user.phone == null || user.phone == "") 
            {
                phone1exists = false;
            }
            if (user.phone2 == null || user.phone2 == "")
            {
                phone2exists = false;
            }

            if (!phone1exists && !phone2exists)
            {
                return res;
            }
            else 
            {
                var parsednumber = "";
                if (phone1exists)
                {
                    parsednumber = parsenumber(user.phone);
                }
                if (parsednumber=="") 
                {
                    if (phone2exists) 
                    {
                        parsednumber = parsenumber(user.phone2);
                    }
                }
                if (parsednumber!="") 
                {
                    res = parsednumber;
                }
                return res;
            }
        }
        public static string parsenumber(string num)
        {
            var parsednumber = "";
            var numberlength = num.Length;

            var acceptedlengths = new int[] { 10, 11, 13, 14 };
            if (acceptedlengths.Contains(numberlength)) 
            {
                if (numberlength == 10)
                {
                    var substring = num.Substring(0, 1);
                    if (substring == "3")
                    {
                        parsednumber = "+92" + num;
                    }
                }
                else if (numberlength == 11)
                {
                    var substring = num.Substring(0, 2);
                    if (substring == "03")
                    {
                        var numberwithout0 = num.Substring(1, numberlength - 1);
                        parsednumber = "+92" + numberwithout0;
                    }
                }
                else if (numberlength == 13)
                {
                    var substring = num.Substring(0, 4);
                    if (substring == "+923")
                    {
                        parsednumber = num;
                    }
                }
                else if (numberlength == 14)
                {
                    var substring = num.Substring(0, 5);
                    if (substring == "00923")
                    {
                        var numberwithout00 = num.Substring(2, numberlength - 2);
                        parsednumber = "+" + numberwithout00;
                    }
                }
            }
            return parsednumber;
        }
    }
}
