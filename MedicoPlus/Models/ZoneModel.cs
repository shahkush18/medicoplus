using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace MedicoPlus.Models
{
    public class ZoneModel
    {
        public int ZoneId { get; set; }
        public string ZoneName { get; set; }
        public int CityId { get; set; }
        public bool IsActive { get; set; }

        public int InsertZone()
        {
            /* SqlConnection connection1 = new SqlConnection();
             connection1.ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=MedicoPlus;Integrated Security=true";
             SqlCommand command1 = new SqlCommand();
             command1.CommandText = "INSERT INTO Zone VALUES('"+this.ZoneName+"','"+this.CityId+"','"+this.IsActive+"')";
             command1.Connection = connection1;
             connection1.Open();
             int x = command1.ExecuteNonQuery();
             connection1.Close();
             return x;*/
            string query = "Insert INTO Zone VALUES (@ZoneName,@CityId,@IsActive)";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@ZoneName", this.ZoneName));
            lstParams.Add(new SqlParameter("@CityId", this.CityId));
            lstParams.Add(new SqlParameter("@IsActive", this.IsActive));
            return DataAccess.ModifyData(query, lstParams);

        }

        public DataTable SelectAllZone()
        {
            /*SqlConnection connection1 = new SqlConnection();
            connection1.ConnectionString = "Data Source=.\\SQLEXPRESS;initial Catalog=MedicoPlus;Integrated Security=true";
            SqlCommand command1 = new SqlCommand();
            command1.CommandText = "SELECT * FROM Zone";
            command1.Connection = connection1;
            DataTable dt = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            dataAdapter.SelectCommand = command1;
            connection1.Open();
            dataAdapter.Fill(dt);
            connection1.Close();
            return dt;*/
            string query = "SELECT Zone.*,City.CityName FROM Zone INNER JOIN City ON Zone.CityId = City.CityId";

            List<SqlParameter> lstParams = new List<SqlParameter>();

            return DataAccess.SelectData(query, lstParams);
        }

        public DataTable SelectAllZoneById()
        {
            SqlConnection connection1 = new SqlConnection();
            connection1.ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=MedicoPlus;Integrated Security=true";
            SqlCommand command1 = new SqlCommand();
            command1.CommandText = "SELECT * FROM Zone WHERE CityId="+this.CityId;
            command1.Connection = connection1;
            DataTable dt = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            dataAdapter.SelectCommand = command1;
            connection1.Open();
            dataAdapter.Fill(dt); 
            connection1.Close();
            return dt;
           

        }
        public int UpdateZone()
        {
            string query = "UPDATE Zone SET ZoneName=@ZoneName WHERE ZoneId = @ZoneId";
            List<SqlParameter> lstparam = new List<SqlParameter>();
            lstparam.Add(new SqlParameter("@ZoneName",this.ZoneName));
            lstparam.Add(new SqlParameter("@ZoneId", this.ZoneId));
            return DataAccess.ModifyData(query, lstparam);

        }
        public DataTable SelectZoneById()
        {
            string query = "Select * FROM Zone WHERE ZoneId = @ZoneId";
            List<SqlParameter> lstparam = new List<SqlParameter>();
            
            lstparam.Add(new SqlParameter("@ZoneId", this.ZoneId));
            DataTable dt = DataAccess.SelectData(query, lstparam);
            this.ZoneId = Convert.ToInt32(dt.Rows[0]["ZoneId"]);
            this.ZoneName = Convert.ToString(dt.Rows[0]["ZoneName"]);

            return dt;

        }
    }
}