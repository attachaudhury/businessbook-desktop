using Dapper;
using Dapper.Contrib.Extensions;
using MySql.Data.MySqlClient;
using Remotion.Linq.Clauses.ResultOperators;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIAB_Restaurent_Management_System.data.dapper
{
    [System.ComponentModel.DataAnnotations.Schema.Table("user")]
    public class user
    {
        public int id { get; set; }
        public string address { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string username { get; set; }
        public string phone { get; set; }
        public string phone2 { get; set; }
        public string role { get; set; }
    }
    public class userrepo
    {
        string conn = baserepo.connectionstring;
        public void  test()
        {
            //user u = new user {id=2, username = "atta" };
            // this.save(u);
            //var users = this.get();
            //dapper.user user = this.get(2);
            //user.name = "atta";
            //user.username = "atta";
            //user.password = "atta@123";
            //this.update(user);
            //var i = 1;
        }
        public List<dapper.user> get() {
            var sql = "select * from user;";
            using (var connection = new MySqlConnection(conn))
            {
                // var res = connection.Query<user>(sql).ToList();
                var res = connection.GetAll<dapper.user>().ToList();
                return res;
            }
        }
        public dapper.user get(int id)
        {
            var sql = "select * from user where id=" + id+";";

            using (var connection = new MySqlConnection(conn))
            {
                //var res = connection.Query<user>(sql).FirstOrDefault();
                var res = connection.Get<dapper.user>(id);

                return res;
            }
        }
        public dapper.user get(string username,string password)
        {
            var sql = "select * from user where username='" + username + "' and password = '"+password+"';";

            using (var connection = new MySqlConnection(conn))
            {
                var res = connection.Query<user>(sql).FirstOrDefault();
                return res;
            }
        }
        public List<dapper.user> getbywherein(string key,List<dynamic> values)
        {
            string a= baserepo.getWhereInSql(values);
            string sql = "select * from user where "+key+" in ("+a+");";

            using (var connection = new MySqlConnection(conn))
            {
                var res = connection.Query<user>(sql).ToList();
                return res;
            }
        }

        public void save(dapper.user user)
        {

            using (var connection = new MySqlConnection(conn))
            {
                var identity = connection.Insert<dapper.user>(user);
                var i = 0;

            }
        }
        public void update(dapper.user user)
        {

            using (var connection = new MySqlConnection(conn))
            {
                var identity = connection.Update<dapper.user>(user);
                var i = 0;

            }
        }

    }
}
