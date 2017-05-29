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
            string query = "SELECT * FROM Area WHERE AreaId = @AreaId";
            List<SqlParameter> lstparams = new List<SqlParameter>();
            lstparams.Add(new SqlParameter("@AreaId", this.AreaId));
            DataTable dt = DataAccess.SelectData(query,lstparams);
            this.ZoneId = Convert.ToInt32(dt.Rows[0]["ZoneId"]);
            this.AreaName = Convert.ToString(dt.Rows[0]["AreaName"]);
            this.AreaId = Convert.ToInt32(dt.Rows[0]["AreaId"]);

            return dt;
          

        }
        public int UpdateArea()
        {
            string query = "UPDATE Area SET AreaName = @AreaName , ZoneId = @ZoneId , IsActive = @IsActive WHERE AreaId = @AreaId";
            List<SqlParameter> lstparams = new List<SqlParameter>();
            lstparams.Add(new SqlParameter("@AreaName", this.AreaName));
            lstparams.Add(new SqlParameter("@ZoneId", this.ZoneId));
            lstparams.Add(new SqlParameter("@AreaId", this.AreaId));
            lstparams.Add(new SqlParameter("@IsActive", this.IsActive));


            return DataAccess.ModifyData(query, lstparams);
        }
        public DataTable SelectAllAreaByZoneId()
        {
            string query = "SELECT * FROM Area WHERE ZoneId = @ZoneId";
            List<SqlParameter> lstparams = new List<SqlParameter>();
            lstparams.Add(new SqlParameter("@ZoneId", this.ZoneId));
            DataTable dt = DataAccess.SelectData(query, lstparams);
            

            return dt;


        }
    }
}