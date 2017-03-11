using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MedicoPlus.Models
{
    public class AppUserModel
    {
        public int AppUserId { get; set; }
        public string AUName { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int AreaId { get; set; }
        public int CityId { get; set; }
        public bool IsActive { get; set; }
        public string Password { get; set; }

        public int InsertAppUser()
        {
           
            string query = "Insert INTO AppUser VALUES (@AUName,@Gender,@DOB,@Phone,@Mobile,@Email,@Address,@AreaId,@CityId,@IsActive,@Password)";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@AUName", this.AUName));
            lstParams.Add(new SqlParameter("@Gender", this.Gender));
            lstParams.Add(new SqlParameter("@DOB", this.DOB));
            lstParams.Add(new SqlParameter("@Phone", this.Phone));
            lstParams.Add(new SqlParameter("@Mobile", this.Mobile));
            lstParams.Add(new SqlParameter("@Email", this.Email));
            lstParams.Add(new SqlParameter("@Address", this.Address));
            lstParams.Add(new SqlParameter("@AreaId", this.AreaId));
            lstParams.Add(new SqlParameter("@CityId", this.CityId));
            lstParams.Add(new SqlParameter("@IsActive", this.IsActive));
            lstParams.Add(new SqlParameter("@Password", this.Password));

            return DataAccess.ModifyData(query, lstParams);
        }
        public DataTable SelectAll()
        {
            
            string query = "SELECT * FROM AppUser";

            List<SqlParameter> lstParams = new List<SqlParameter>();

            return DataAccess.SelectData(query, lstParams);
        }
        public DataTable SelectAppUserById()
        {
            SqlConnection connection1 = new SqlConnection();
            connection1.ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=MedicoPlus;Integrated Security=true";
            SqlCommand command1 = new SqlCommand();
            command1.CommandText = "SELECT * FROM AppUser WHERE AppUserId=" + this.AppUserId;
            command1.Connection = connection1;
            DataTable dt = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            dataAdapter.SelectCommand = command1;
            connection1.Open();
            dataAdapter.Fill(dt);
            connection1.Close();
            return dt;
           
        }
    }
}