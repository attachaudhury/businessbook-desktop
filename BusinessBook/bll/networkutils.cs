using BusinessBook.data.dapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Management;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace BusinessBook.bll
{
    public class networkutils
    {
        public static string apiendpointdefault = "http://localhost:8011/businessbookapi";
        //public static string apiendpointdefault = "http://ravicosoft.com/businessbookapi";
        public static async void updatelocalsetting()
        {
            try
            {
                softwaresettingrepo ssr = new softwaresettingrepo();
                var ravicosoftuser = ssr.getbyname(commonsettings.ravicosoftuserid);

                var apiendpoint = apiendpointdefault;
                if (userutils.apiendpoint != null)
                {
                    apiendpoint = userutils.apiendpoint.stringvalue;
                }
                RestClient client = new RestClient(apiendpoint);
                var request = new RestRequest("updatelocalsetting");
                if (ravicosoftuser != null)
                {
                    request.AddJsonBody(new { userid = ravicosoftuser.stringvalue });
                }
                var response = await client.PostAsync<responsetype>(request);
                if (response.status == "success")
                {
                    var user = Newtonsoft.Json.JsonConvert.DeserializeObject<responseuser>(response.data);

                    var userid = userutils.ravicosoftuserid;
                    if (userid == null)
                    {
                        var ss = new softwaresetting();
                        ss.name = commonsettings.ravicosoftuserid;
                        ss.valuetype = "string";
                        ss.stringvalue = user._id;
                        userutils.ravicosoftuserid = ssr.save(ss);
                    }
                    else
                    {
                        userid.valuetype = "string";
                        userid.stringvalue = user._id;
                        userutils.ravicosoftuserid = ssr.update(userid);
                    }


                    var username = userutils.ravicosoftusername;
                    if (username == null)
                    {
                        var ss = new softwaresetting();
                        ss.name = commonsettings.ravicosoftusername;
                        ss.valuetype = "string";
                        ss.stringvalue = user.username;
                        userutils.ravicosoftusername = ssr.save(ss);
                    }
                    else
                    {
                        username.valuetype = "string";
                        username.stringvalue = user.username;
                        userutils.ravicosoftuserid = ssr.update(username);
                    }

                    var userpassword = userutils.ravicosoftpassword;
                    if (userpassword == null)
                    {
                        var ss = new softwaresetting();
                        ss.name = commonsettings.ravicosoftpassword;
                        ss.valuetype = "string";
                        ss.stringvalue = user.password;
                        userutils.ravicosoftpassword = ssr.save(ss);
                    }
                    else
                    {
                        userpassword.valuetype = "string";
                        userpassword.stringvalue = user.password;
                        userutils.ravicosoftpassword = ssr.update(userpassword);
                    }

                    var membershiptype = userutils.ravicosoftbusinessbookmembershipplan;
                    if (membershiptype == null)
                    {
                        var ss = new softwaresetting();
                        ss.name = commonsettings.ravicosoftbusinessbookmembershipplan;
                        ss.valuetype = "string";
                        ss.stringvalue = user.businessbookmembershipplan;
                        userutils.ravicosoftbusinessbookmembershipplan = ssr.save(ss);
                    }
                    else
                    {
                        userpassword.valuetype = "string";
                        userpassword.stringvalue = user.businessbookmembershipplan;
                        userutils.ravicosoftbusinessbookmembershipplan = ssr.update(membershiptype);
                    }


                    membershiptype = userutils.ravicosoftbusinessbookmembershipplan;

                    if (membershiptype.stringvalue != "Package 1")
                    {
                        if (user.businessbookmembershipexpirydate != null)
                        {
                            var membershipexpirydate = userutils.ravicosoftbusinessbookmembershipexpirydate;
                            if (membershipexpirydate == null)
                            {
                                var ss = new softwaresetting();
                                ss.name = commonsettings.ravicosoftbusinessbookmembershipexpirydate;
                                ss.valuetype = "date";
                                ss.datevalue = user.businessbookmembershipexpirydate;
                                userutils.ravicosoftbusinessbookmembershipexpirydate = ssr.save(ss);
                            }
                            else
                            {
                                membershipexpirydate.valuetype = "date";
                                membershipexpirydate.datevalue = user.businessbookmembershipexpirydate;
                                userutils.ravicosoftbusinessbookmembershipexpirydate = ssr.update(membershipexpirydate);
                            }
                        }
                    }



                    var canrunsoftware = userutils.ravicosoftbusinessbookcanrun;
                    if (canrunsoftware == null)
                    {
                        var ss = new softwaresetting();
                        ss.name = commonsettings.ravicosoftbusinessbookcanrun;
                        ss.valuetype = "bool";
                        ss.boolvalue = user.businessbookcanrun;
                        userutils.ravicosoftbusinessbookcanrun = ssr.save(ss);
                    }
                    else
                    {
                        canrunsoftware.valuetype = "bool";
                        canrunsoftware.boolvalue = user.businessbookcanrun;
                        userutils.ravicosoftbusinessbookcanrun = ssr.update(canrunsoftware);
                    }



                    var cansendsms = userutils.ravicosoftsmsplan;
                    if (cansendsms == null)
                    {
                        var ss = new softwaresetting();
                        ss.name = commonsettings.ravicosoftsmsplan;
                        ss.valuetype = "string";
                        ss.stringvalue = user.smsplan;
                        userutils.ravicosoftsmsplan = ssr.save(ss);
                    }
                    else
                    {
                        cansendsms.valuetype = "string";
                        cansendsms.stringvalue = user.smsplan;
                        userutils.ravicosoftsmsplan = ssr.update(cansendsms);
                    }
                    

                }
            }
            catch (Exception ex)
            {

            }
        }

        public static async void updateonlinesetting(dynamic obj)
        {
            try
            {
                softwaresettingrepo ssr = new softwaresettingrepo();
                var ravicosoftuser = ssr.getbyname(commonsettings.ravicosoftuserid);
                if (ravicosoftuser == null)
                {
                    return;
                }
                var apiendpoint = apiendpointdefault;
                if (userutils.apiendpoint != null)
                {
                    apiendpoint = userutils.apiendpoint.stringvalue;
                }
                RestClient client = new RestClient(apiendpoint);
                var request = new RestRequest("updateonlinesetting");
                obj.userid = ravicosoftuser.stringvalue;
                request.AddJsonBody(obj);
                var response = await client.PostAsync<responsetype>(request);
                if (response.status == "success")
                {
                    var user = Newtonsoft.Json.JsonConvert.DeserializeObject<responseuser>(response.data);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static async void sendsms(string message,dynamic obj)
        {
            try
            {
                var issendingvalid = otherutils.checkmessagevalidation(message,obj);
                var isuservalid = userutils.checkravicosoftuseridexits();
                if (!issendingvalid || !isuservalid) 
                {
                    otherutils.notify("Info","You can not send sms, Please vist ravicosoft.com for any help",3000);
                    return;
                }

                var apiendpoint = getapiendpoint();
                RestClient client = new RestClient(apiendpoint);
                var request = new RestRequest("smsfrombusinessbook");
                List<string> number = otherutils.parsenumbersfromdynamic(obj);
                var requestobject = new { userid = userutils.ravicosoftuserid.stringvalue,message=message,numbers=number.ToArray()};
                request.AddJsonBody(requestobject);
                var response = await client.PostAsync<responsetype>(request);
            }
            catch (Exception ex)
            {

            }
        }
        public static string getapiendpoint() {
            var apiendpoint = apiendpointdefault;
            if (userutils.apiendpoint != null)
            {
                apiendpoint = userutils.apiendpoint.stringvalue;
            }
            return apiendpoint;
        }

    }
    public class responsetype
    {
        public string status { get; set; }
        public string data { get; set; }
        public string ex { get; set; }
    }
    public class responseuser
    {
        public string _id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string businessbookmembershipplan { get; set; } // values are Package 1,Package 2,Package 3
        public DateTime? businessbookmembershipexpirydate { get; set; } // values are none,Package 1,Package 2,Package 3,Package 4
        public Boolean? businessbookcanrun { get; set; }
        public string smsplan { get; set; }
    }
}
