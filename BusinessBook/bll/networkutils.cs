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

                    var userpassword = userutils.ravicosoftuserpassword;
                    if (userpassword == null)
                    {
                        var ss = new softwaresetting();
                        ss.name = commonsettings.ravicosoftuserpassword;
                        ss.valuetype = "string";
                        ss.stringvalue = user.password;
                        userutils.ravicosoftuserpassword = ssr.save(ss);
                    }
                    else
                    {
                        userpassword.valuetype = "string";
                        userpassword.stringvalue = user.password;
                        userutils.ravicosoftuserpassword = ssr.update(userpassword);
                    }

                    var membershiptype = userutils.membershiptype;
                    if (membershiptype == null)
                    {
                        var ss = new softwaresetting();
                        ss.name = commonsettings.membershiptype;
                        ss.valuetype = "string";
                        ss.stringvalue = user.membershiptype;
                        userutils.membershiptype = ssr.save(ss);
                    }
                    else
                    {
                        userpassword.valuetype = "string";
                        userpassword.stringvalue = user.membershiptype;
                        userutils.membershiptype = ssr.update(membershiptype);
                    }
                    membershiptype = userutils.membershiptype;

                    if (membershiptype.stringvalue != "free")
                    {
                        if (user.membershipexpirydate != null)
                        {
                            var membershipexpirydate = userutils.membershipexpirydate;
                            if (membershipexpirydate == null)
                            {
                                var ss = new softwaresetting();
                                ss.name = commonsettings.membershipexpirydate;
                                ss.valuetype = "date";
                                ss.datevalue = user.membershipexpirydate;
                                userutils.membershipexpirydate = ssr.save(ss);
                            }
                            else
                            {
                                membershipexpirydate.valuetype = "string";
                                membershipexpirydate.datevalue = user.membershipexpirydate;
                                userutils.membershiptype = ssr.update(membershipexpirydate);
                            }
                        }

                    }



                    var canrunsoftware = userutils.canrunsoftware;
                    if (canrunsoftware == null)
                    {
                        var ss = new softwaresetting();
                        ss.name = commonsettings.canrunsoftware;
                        ss.valuetype = "bool";
                        ss.boolvalue = user.canrunsoftware;
                        userutils.canrunsoftware = ssr.save(ss);
                    }
                    else
                    {
                        canrunsoftware.valuetype = "bool";
                        canrunsoftware.boolvalue = user.canrunsoftware;
                        userutils.canrunsoftware = ssr.update(canrunsoftware);
                    }



                    var cansendsms = userutils.cansendsms;
                    if (cansendsms == null)
                    {
                        var ss = new softwaresetting();
                        ss.name = commonsettings.cansendsms;
                        ss.valuetype = "bool";
                        ss.boolvalue = user.cansendsms;
                        userutils.cansendsms = ssr.save(ss);
                    }
                    else
                    {
                        cansendsms.valuetype = "bool";
                        cansendsms.boolvalue = user.cansendsms;
                        userutils.cansendsms = ssr.update(cansendsms);
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
        public DateTime? createddate { get; set; }
        public string membershiptype { get; set; }
        public DateTime? membershipexpirydate { get; set; }
        public string password { get; set; }
        public Boolean? canrunsoftware { get; set; }
        public Boolean? cansendsms { get; set; }
    }
}
