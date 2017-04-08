using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace MedicoPlus.Models
{
    public class CityModel
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public bool IsActive { get; set; }

        public int InsertCity()
        {
            
            string query = "Insert INTO City VALUES (@CityName,@IsActive)";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@CityName", this.CityName));
            lstParams.Add(new SqlParameter("@IsActive", this.IsActive));
            return DataAccess.ModifyData(query, lstParams);
        }

        public DataTable SelectAll()
        {
            /*SqlConnection connection1 = new SqlConnection();
            connection1.ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=MedicoPlus;Integrated Security=true";
            SqlCommand command1 = new SqlCommand();
            command1.CommandText = "SELECT * FROM City";
            command1.Connection = connection1;
            DataTable dt = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            dataAdapter.SelectCommand = command1;
            connection1.Open();
            dataAdapter.Fill(dt);
            connection1.Close();
            return dt;*/
            string query = "SELECT * FROM City";

            List<SqlParameter> lstParams = new List<SqlParameter>();

            return DataAccess.SelectData(query, lstParams);
        }

        public DataTable SelectAllCityById()
        {
             SqlConnection connection1 = new SqlConnection();
             connection1.ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=MedicoPlus;Integrated Security=true";
             SqlCommand command1 = new SqlCommand();
             command1.CommandText = "SELECT * FROM City WHERE CityId="+this.CityId;
             command1.Connection = connection1;
             DataTable dt = new DataTable();
             SqlDataAdapter dataAdapter = new SqlDataAdapter();
             dataAdapter.SelectCommand = command1;
             connection1.Open();
             dataAdapter.Fill(dt);
             connection1.Close();
             this.CityName = Convert.ToString(dt.Rows[0]["CityName"]);
            this.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]);

             return dt;
          /*  string query = "SELECT * FROM City WHERE CityId=@CityId ";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@CityId", this.CityId));
            DataTable dt = DataAccess.SelectData(query, lstParams);
            if (dt.Rows.Count > 0)
            {
                this.CityName = dt.Rows[0]["CityName"].ToString();
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