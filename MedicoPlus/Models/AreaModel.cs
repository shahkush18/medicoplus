using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
namespace MedicoPlus.Models
{
    public class AreaModel
    {
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public int ZoneId { get; set; }
        public bool IsActive { get; set; }


        public int InsertArea()
        {
            /*SqlConnection connection1 = new SqlConnection();
            connection1.ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=MedicoPlus;Integrated Security=true";
            SqlCommand command1 = new SqlCommand();
            command1.CommandText = "INSERT INTO Area VALUES('"+this.AreaName+"','"+this.ZoneId+"','"+this.IsActive+"')";
            command1.Connection = connection1;
            connection1.Open();
            int x = command1.ExecuteNonQuery();
            connection1.Close();
            return x;*/
            string query = "Insert INTO Area VALUES (@AreaName,@ZoneId,@IsActive)";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@AreaName", this.AreaName));
            lstParams.Add(new SqlParameter("@IsActive", this.IsActive));
            lstParams.Add(new SqlParameter("@ZoneId", this.ZoneId));
            return DataAccess.ModifyData(query, lstParams);


        }

        public DataTable SelectAllArea()
        {
            /*SqlConnection connection1 = new SqlConnection();
            connection1.ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=MedicoPlus;Integrated Security=true";
            SqlCommand command1 = new SqlCommand();
            command1.CommandText = "SELECT * FROM Area";
            command1.Connection = connection1;
            DataTable dt = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            dataAdapter.SelectCommand = command1;
            connection1.Open();
            dataAdapter.Fill(dt);
            connection1.Close();
            return dt;*/
            string query = "SELECT * FROM Area";

            List<SqlParameter> lstParams = new List<SqlParameter>();

            return DataAccess.SelectData(query, lstParams);
        }

        public DataTable SelectAllAreaById()
        {
            SqlConnection connection1 = new SqlConnection();
            connection1.ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=MedicoPlus1;Integrated Security=true";
            SqlCommand command1 = new SqlCommand();
            command1.CommandText = "SELECT * FROM Area WHERE ZoneId=" + this.ZoneId;
            command1.Connection = connection1;
            DataTable dt = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            dataAdapter.SelectCommand = command1;
            connection1.Open();
            dataAdapter.Fill(dt);
            connection1.Close();
            return dt;
           /* string query = "SELECT * FROM Area WHERE ZoneId=@zoneId ";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@ZoneId", this.ZoneId));
            DataTable dt = DataAccess.SelectData(query, lstParams);
            if (dt.Rows.Count > 0)
            {
                this.AreaName = dt.Rows[0]["AreaName"].ToString();
                this.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]);

                return true;
            }
            else
            {
                return false;
            }*/

        }
    }
}