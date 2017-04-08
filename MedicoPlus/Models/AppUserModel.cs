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
        public DateTime DOB { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int AreaId { get; set; }
        public int CityId { get; set; }
        public bool IsActive { get; set; }
        public string Password { get; set; }
        public string Photo { get; set; }

        public int InsertAppUser()
        {

            string query = "Insert INTO AppUser(AUName,Gender,DOB,Phone,Mobile,Email,Address,AreaId,CityId,IsActive,Password,Photo) VALUES (@AUName,@Gender,@DOB,@Phone,@Mobile,@Email,@Address,@AreaId,@CityId,@IsActive,@Password,@Photo)";
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
            lstParams.Add(new SqlParameter("@Photo", this.Photo));

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
            this.AppUserId =Convert.ToInt32(dt.Rows[0]["AppUserId"]);
            this.AUName = Convert.ToString(dt.Rows[0]["AUName"]);
            this.Gender = Convert.ToString(dt.Rows[0]["Gender"]);
            this.CityId = Convert.ToInt32(dt.Rows[0]["CityId"]);
            this.AreaId = Convert.ToInt32(dt.Rows[0]["AreaId"]);
            this.Email= Convert.ToString(dt.Rows[0]["Email"]);
            this.Phone = Convert.ToString(dt.Rows[0]["Phone"]);
            this.IsActive= Convert.ToBoolean(dt.Rows[0]["IsActive"]);
            this.Password = Convert.ToString(dt.Rows[0]["Password"]);
            this.Mobile = Convert.ToString(dt.Rows[0]["Mobile"]);
            this.Photo = Convert.ToString(dt.Rows[0]["Photo"]);
            this.DOB = Convert.ToDateTime(dt.Rows[0]["DOB"]);
            return dt;

        }
        public bool Authenticate()
        {
            string query = "SELECT * FROM AppUser WHERE AUName = @AUname AND Password = @Password AND IsActive = 1";

            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@AUname", this.AUName));
            lstParams.Add(new SqlParameter("@Password", this.Password));
            DataTable dt = DataAccess.SelectData(query, lstParams);

            if (dt.Rows.Count > 0)
            {
                this.AppUserId = Convert.ToInt32(dt.Rows[0]["AppUserid"]);
                this.AUName = dt.Rows[0]["AUName"].ToString();
                this.Password = dt.Rows[0]["Password"].ToString();
                this.Mobile = dt.Rows[0]["AreaId"].ToString();
                this.Phone = dt.Rows[0]["Phone"].ToString();
                this.Address = dt.Rows[0]["Address"].ToString();
                this.DOB = Convert.ToDateTime(dt.Rows[0]["DOB"]);
                this.Email = dt.Rows[0]["Email"].ToString();
                this.AppUserId = Convert.ToInt32(dt.Rows[0]["AppUserId"]);
                this.Gender = dt.Rows[0]["Gender"].ToString();
                this.AreaId = Convert.ToInt32(dt.Rows[0]["AreaId"]);
                this.CityId = Convert.ToInt32(dt.Rows[0]["CityId"]);
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
    