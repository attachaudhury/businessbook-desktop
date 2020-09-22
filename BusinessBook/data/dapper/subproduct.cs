using Dapper;
using Dapper.Contrib.Extensions;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessBook.data.dapper
{
    [System.ComponentModel.DataAnnotations.Schema.Table("subproduct")]

    public class subproduct
    {
        public int id { get; set; }
        public int fk_product_main_in_subproduct { get; set; }
        public int fk_product_sub_in_subproduct { get; set; }
        public double quantity { get; set; }



    }
    public class subproductextented:subproduct
    {
        public virtual string productname { get; set; }
        public virtual string subproductname { get; set; }

    }
    public class subproductrepo
    {
        string conn = baserepo.connectionstring;
        public void  test()
        {
            //dapper.subproduct p = new subproduct { fk_product_main_in_subproduct=3,fk_product_sub_in_subproduct=1,quantity=2};
            //dapper.subproduct p2 = new subproduct { fk_product_main_in_subproduct=3,fk_product_sub_in_subproduct=2,quantity=2};
            //var save = this.save(p);
            //var save2 = this.save(p2);
            var subproduct = this.get(1);
            var subproducts = this.get();
            var i = 0;
        }
        public List<dapper.subproduct> get() {
            using (var connection = new MySqlConnection(conn))
            {
                var res = connection.GetAll<dapper.subproduct>().ToList();
                return res;
            }
        }
        public dapper.subproduct get(int id)
        {
            using (var connection = new MySqlConnection(conn))
            {
                var res = connection.Get<dapper.subproduct>(id);
                return res;
            }
        }
        public List<dapper.subproductextented> getproduct_subproducts(int id)
        {
            var sql = "select sp.id,fk_product_main_in_subproduct,fk_product_sub_in_subproduct,sp.quantity,sp_p.name as productname,sp_sp.name as subproductname from subproduct sp inner join product sp_p on sp.fk_product_main_in_subproduct = sp_p.id inner join product sp_sp on sp.fk_product_sub_in_subproduct = sp_sp.id where fk_product_main_in_subproduct = " + id+";";
            //var sql = "select * from subproduct where fk_product_main_in_subproduct="+id+";";
            using (var connection = new MySqlConnection(conn))
            {
                var res = connection.Query<dapper.subproductextented>(sql).ToList();
                return res;
            }
        }
        public int save(dapper.subproduct subproduct)
        {

            using (var connection = new MySqlConnection(conn))
            {
                var res = connection.Insert<dapper.subproduct>(subproduct);
                return (int)res;
            }
        }
        public bool update(dapper.subproduct subproduct)
        {

            using (var connection = new MySqlConnection(conn))
            {
                var identity = connection.Update<dapper.subproduct>(subproduct);
                return identity;
            }
        }
        public bool delete(dapper.subproduct subproduct)
        {

            using (var connection = new MySqlConnection(conn))
            {
                var identity = connection.Delete<dapper.subproduct>(subproduct);
                return identity;
            }
        }
    }
}
