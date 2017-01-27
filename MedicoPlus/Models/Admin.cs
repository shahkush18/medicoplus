using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MedicoPlus.Models
{
    public class Admin
    {
        public int AdminId { get; set; }
        public string AdminUName { get; set; }
        public string AdminPassword { get; set; }
        public bool IsActive { get; set; }

        public int Insert()
        {
            //string query = "INSERT INTO Employee VALUES('" + this.Name + "', '" + this.Email + "', '" + this.Phone + "')";
            string query = "INSERT INTO Admin VALUES(@AdminUName,@AdminPassword, @IsActive)";

            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@AdminUName", this.AdminUName));
            lstParams.Add(new SqlParameter("@AdminPassword", this.AdminPassword));
            lstParams.Add(new SqlParameter("@IsActive", this.IsActive));

            return DataAccess.ModifyData(query, lstParams);
        }
        public bool Authenticate()
        {
            string query = "SELECT * FROM Admin WHERE AdminUName = @AdminUName AND AdminPassword = @AdminPassword AND IsActive = 1";

            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@AdminUName", this.AdminUName));
            lstParams.Add(new SqlParameter("@AdminPassword", this.AdminPassword));
            DataTable dt = DataAccess.SelectData(query, lstParams);

            if (dt.Rows.Count > 0)
            {
                this.AdminUName = dt.Rows[0]["AdminUName"].ToString();
                this.AdminPassword = dt.Rows[0]["AdminPassword"].ToString();
                this.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]);

                return true;
            }
            else
            {
                return false;
            }


        }
    }
    
}