using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common.EntitySql;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;

namespace RIAB_Restaurent_Management_System.data.dapper
{
    public static class baserepo
    {
        public static string connectionstring = "Server=localhost;Database=bbdb;Uid=root;Pwd=;";
        public static databaseconnection databaseconnection = null;
        public static string getkeyValuestoSqlAnd(dynamic keyvaluepairs) 
        {
            string s = "";
            foreach (KeyValuePair<string, object> kvp in keyvaluepairs)
            {
                s += getkeyValueToEqualTo(kvp.Key,kvp.Value);
                s += " and ";  
             }
            var ss = s.Remove(s.Length - 5);
            return ss;
        }
        public static string getkeyValuesToSqlOr(dynamic keyvaluepairs)
        {
            string s = "";
            foreach (KeyValuePair<string, object> kvp in keyvaluepairs)
            {
                s += getkeyValueToEqualTo(kvp.Key, kvp.Value);
                s += " or ";
            }
            var ss = s.Remove(s.Length - 4);
            return ss;
        }
        public static string getkeyValueToEqualTo(string key,object value) {
            var s = "";
            s += key + "=";
            if (value is string)
            {
                s += "'" + value + "'";
            }
            else
            {
                s += value;
            }
            return s;
        }
        public static string getWhereInSql(object[] values)
        {
            var s = "";
            foreach (object value in values) {
                if (value is string)
                {
                    s += "'" + value + "'";
                }
                else
                {
                    s += value;
                }
                s += ",";
            }
            var ss = s.Remove(s.Length - 1);
            return ss;
        }

        public static Boolean initdatabase() 
        {
            RadDesktopAlertManager alertmanager = new RadDesktopAlertManager();
            var r = loaddatabseconnectionfile();
            if (!r) {
                var alert = new RadDesktopAlert();
                alert.Header = "Database Configuration File Error";
                alert.Content = "Please update "+ System.AppDomain.CurrentDomain.BaseDirectory +"/data/databaseconnection.json";
                alert.ShowDuration = 10000;
                System.Media.SystemSounds.Hand.Play();
                alertmanager.ShowAlert(alert);
                return false;
            }

            var r1 = checkdatabaseserverconnection();
            if (!r1)
            {
                var alert = new RadDesktopAlert();
                alert.Header = "Database server not running";
                alert.Content = "Please update " + System.AppDomain.CurrentDomain.BaseDirectory + "/data/databaseconnection.json";
                alert.ShowDuration = 10000;
                System.Media.SystemSounds.Hand.Play();
                alertmanager.ShowAlert(alert);
                return false ;
            }

            var r2 = checkdatabase();
            if (!r2)
            {
                var alert = new RadDesktopAlert();
                alert.Header = "Database "+databaseconnection.database+ " does not exists.";
                alert.Content = "Trying to create database. Please restart software";
                alert.ShowDuration = 10000;
                System.Media.SystemSounds.Hand.Play();
                alertmanager.ShowAlert(alert);
                createdatabase();
                return false;
            }
            return true;

        }
        public static Boolean loaddatabseconnectionfile() 
        {
            try
            {
                using (StreamReader r = new StreamReader($"data/databaseconnection.json"))
                {
                    string json = r.ReadToEnd();
                    databaseconnection = JsonConvert.DeserializeObject<databaseconnection>(json);
                    connectionstring = "Server="+ databaseconnection.server+ ";Database=" + databaseconnection.database + ";Uid=" + databaseconnection.user + ";Pwd=" + databaseconnection.password + ";";
                    checkdatabaseserverconnection();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }
        public static Boolean checkdatabaseserverconnection()
        {
            var serverconnection= "Server=" + databaseconnection.server + ";Uid=" + databaseconnection.user + ";Pwd=" + databaseconnection.password + ";";
            try
            {
                using (var connection = new MySqlConnection(serverconnection))
                {
                    connection.Open();
                    connection.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static Boolean checkdatabase()
        {
            var serverconnection = "Server=" + databaseconnection.server + ";Database=" + databaseconnection.database  + ";Uid=" + databaseconnection.user + ";Pwd=" + databaseconnection.password + ";";
            try
            {
                using (var connection = new MySqlConnection(serverconnection))
                {
                    connection.Open();
                    connection.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static void createdatabase()
        {
            var serverconnection = "Server=" + databaseconnection.server + ";Uid=" + databaseconnection.user + ";Pwd=" + databaseconnection.password + ";";
            using (var connection = new MySqlConnection(serverconnection))
            {
                connection.Open();
                FileInfo file = new FileInfo(@"data/bbdb.sql");
                string script = file.OpenText().ReadToEnd();
                string script2 = script.Replace("bbdb", databaseconnection.database);
                MySqlCommand cmd = new MySqlCommand(script2, connection);
                var r = cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

    }

    public class databaseconnection { 
    public string server { get; set; }
    public string database { get; set; }
    public string user { get; set; }
    public string password { get; set; }
    }
}
