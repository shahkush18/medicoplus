using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MedicoPlus.Models
{
    public class DoctorModel
    {
        public int DoctorId { get; set; }
        public string DocName { get; set; }
        public string Intro { get; set; }
        public string Photo { get; set; }
        public string Gender { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string DOB { get; set; }
        public string DOJ { get; set; }
        public string Status { get; set; }
        public int SpecialityId { get; set; }
        public string Qualification { get; set; }
        public int MinConsultationFee { get; set; }
        public string Documents { get; set; }
        public int MaxApptPerHour { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public int InsertDoctor()
        {

            string query = "InsertCity INTO Doctor VALUES (@DocName,@Intro,@Photo,@Gender, @Mobile,@Email, @DOB, @DOJ,@Status,@SpecialityId,@Qualification,@MinConsultationFee,Documents, @MaxApptPerHour,@UserName,@Password)";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@DocName", this.DocName));
            lstParams.Add(new SqlParameter("@Intro", this.Intro));
            lstParams.Add(new SqlParameter("@Photo", this.Photo));
            lstParams.Add(new SqlParameter("@Gender", this.Gender));
            lstParams.Add(new SqlParameter("@Mobile", this.Mobile));
            lstParams.Add(new SqlParameter("@Email", this.Email));
            lstParams.Add(new SqlParameter("@DOB", this.DOB));
            lstParams.Add(new SqlParameter("@DOJ", this.DOJ));
            lstParams.Add(new SqlParameter("@Status", this.Status));
            lstParams.Add(new SqlParameter("@SpecialityId", this.SpecialityId));
            lstParams.Add(new SqlParameter("@Qualification", this.Qualification));
            lstParams.Add(new SqlParameter("@MinConsultationFee", this.MinConsultationFee));
            lstParams.Add(new SqlParameter("@Documents", this.Documents));
            lstParams.Add(new SqlParameter("@MaxApptPerHour", this.MaxApptPerHour));
            lstParams.Add(new SqlParameter("@UserName", this.UserName));
            lstParams.Add(new SqlParameter("@Password", this.Password));
            return DataAccess.ModifyData(query, lstParams);
        }

        public bool Authenticate()
        {
            string query = "SELECT * FROM Doctor WHERE UserName = @UserName AND Password = @Password AND IsActive = 1";

            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@UserName", this.UserName));
            lstParams.Add(new SqlParameter("@Password", this.Password));
            DataTable dt = DataAccess.SelectData(query, lstParams);

            if (dt.Rows.Count > 0)
            {
                this.UserName = dt.Rows[0]["UserName"].ToString();
                this.Password = dt.Rows[0]["Password"].ToString();
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



    