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
        public static string apipath = "http://localhost:8012/businessbookapi/";
        //public static string apipath = "https://ravicosoft.com/businessbookapi/";
        public static RestClient client = new RestClient(apipath);
        public static async void createravicosoftuser() 
        {
            try
            {
                var request = new RestRequest("registerfrombusinessbookdesktop");
                var response = await client.PostAsync<responsetype>(request);
                if (response.status == "success") 
                {
                    var user = Newtonsoft.Json.JsonConvert.DeserializeObject<responseuser>(response.data);
                    softwaresettingrepo ssr = new softwaresettingrepo();

                    var s = ssr.getbyname(commonsettings.ravicosoftuserid);
                    if (s == null) 
                    {
                        var ss = new softwaresetting();
                        ss.name = commonsettings.ravicosoftuserid;
                        ss.valuetype="string";
                        ss.stringvalue=user._id;
                        ssr.save(ss);
                    }
                }
            }
            catch (Exception ex) 
            {

            }
            
            
        }
    }
    public class responsetype { 
        public string status { get; set; }
        public string data { get; set; }
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
