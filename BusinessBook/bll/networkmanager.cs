using BusinessBook.data.dapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace BusinessBook.bll
{
    public class networkutils
    {
        public static string apipath = "http://localhost:8011/businessbookapi/";
        //public static string apipath = "https://ravicosoft.com/businessbookapi/";
        public static RestClient client = new RestClient(apipath);
        public static async void registerfrombusinessbookdesktop()
        {
            try
            {
                softwaresettingrepo ssr = new softwaresettingrepo();
                var ravicosoftuser = ssr.getbyname(commonsettings.ravicosoftuserid);
                if (ravicosoftuser == null)
                {
                    var request = new RestRequest("registerfrombusinessbookdesktop");
                    var response = await client.PostAsync<responsetypeDataUser>(request);
                    if (response.status == "success")
                    {
                        var user = Newtonsoft.Json.JsonConvert.DeserializeObject<responseuser>(response.data);

                        var s = ssr.getbyname(commonsettings.ravicosoftuserid);
                        if (s == null)
                        {
                            var ss = new softwaresetting();
                            ss.name = commonsettings.ravicosoftuserid;
                            ss.valuetype = "string";
                            ss.stringvalue = user._id;
                            ssr.save(ss);
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }


        }
        public static async void updatesoftwareshouldrun()
        {
            try
            {
                softwaresettingrepo ssr = new softwaresettingrepo();
                var ravicosoftuser = ssr.getbyname(commonsettings.ravicosoftuserid);
                if (ravicosoftuser != null)
                {
                    var request = new RestRequest("updatesoftwareshouldrun");
                    request.AddJsonBody(new { userid = ravicosoftuser.stringvalue });
                    var response = await client.PostAsync<responsetypeDataDynamic>(request);
                    if (response.status == "success")
                    {
                        var s = ssr.getbyname(commonsettings.softwareshouldrun);
                        if (s == null)
                        {
                            var ss = new softwaresetting();
                            ss.name = commonsettings.softwareshouldrun;
                            ss.valuetype = "bool";
                            ss.boolvalue = response.data;
                            ssr.save(ss);
                        }
                        else
                        {
                            s.boolvalue = response.data;
                            ssr.update(s);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }
        public static async void updatemembershiptype()
        {
            try
            {
                softwaresettingrepo ssr = new softwaresettingrepo();
                var ravicosoftuser = ssr.getbyname(commonsettings.ravicosoftuserid);
                if (ravicosoftuser != null)
                {
                    var request = new RestRequest("updatemembershiptype");
                    request.AddJsonBody(new { userid = ravicosoftuser.stringvalue });
                    var response = await client.PostAsync<responsetypeDataDynamic>(request);
                    if (response.status == "success")
                    {
                        var s = ssr.getbyname(commonsettings.membershiptype);
                        if (s == null)
                        {
                            var ss = new softwaresetting();
                            ss.name = commonsettings.membershiptype;
                            ss.valuetype = "string";
                            ss.stringvalue = response.data;
                            ssr.save(ss);
                        }
                        else
                        {
                            s.stringvalue = response.data;
                            ssr.update(s);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static async void updatemembershipexpirydate()
        {
            try
            {
                softwaresettingrepo ssr = new softwaresettingrepo();
                var ravicosoftuser = ssr.getbyname(commonsettings.ravicosoftuserid);
                if (ravicosoftuser != null)
                {
                    var request = new RestRequest("updatemembershipexpirydate");
                    request.AddJsonBody(new { userid = ravicosoftuser.stringvalue });
                    var response = await client.PostAsync<responsetypeDataDynamic>(request);
                    if (response.status == "success")
                    {
                        var expireydate = DateTime.Parse(response.data);
                        var s = ssr.getbyname(commonsettings.membershipexpirydate);
                        if (s == null)
                        {
                            
                            var ss = new softwaresetting();
                            ss.name = commonsettings.membershipexpirydate;
                            ss.valuetype = "date";
                            ss.datevalue = expireydate;
                            ssr.save(ss);
                        }
                        else
                        {
                            s.datevalue = expireydate;
                            ssr.update(s);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
    public class responsetypeDataUser
    {
        public string status { get; set; }
        public string data { get; set; }
        public string ex { get; set; }
    }
    public class responsetypeDataDynamic
    {
        public string status { get; set; }
        public dynamic data { get; set; }
        public string ex { get; set; }
    }
    public class responseuser
    {
        public string _id { get; set; }
        public DateTime createddate { get; set; }
        public string membershiptype { get; set; }
        public long membershipexpirydate { get; set; }
        public string password { get; set; }
        public Boolean softwareshouldrun { get; set; }
    }
}
