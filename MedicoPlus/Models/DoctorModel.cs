﻿using System;
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
        public DateTime DOB { get; set; }
        public DateTime DOJ { get; set; }
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

            string query = "Insert INTO Doctor(DocName,Intro,Photo,Gender, Mobile,Email, DOB, DOJ,Status,SpecialityId,Qualification,MinConsultationFee,Documents, MaxApptPerHour,UserName,Password) VALUES (@DocName,@Intro,@Photo,@Gender, @Mobile,@Email, @DOB, @DOJ,@Status,@SpecialityId,@Qualification,@MinConsultationFee,@Documents, @MaxApptPerHour,@UserName,@Password)";
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
                this.DoctorId = Convert.ToInt32(dt.Rows[0]["DoctorId"]);
                this.Photo = Convert.ToString(dt.Rows[0]["Photo"]);
                return true;
            }
            else
            {
                return false;
            }




        }
        //My Code
        //public DataTable SelectById()
        //{
        //    string query = "SELECT * FROM Doctor WHERE DoctorId = @DoctorId";

        //    List<SqlParameter> lstParams = new List<SqlParameter>();
        //    lstParams.Add(new SqlParameter("@DoctorId", this.DoctorId));

        //    DataTable dt = DataAccess.SelectData(query, lstParams);
        //    this.DocName = Convert.ToString(dt.Rows[0]["DocName"]);
        //    this.MaxApptPerHour = Convert.ToInt32(dt.Rows[0]["MaxApptPerHour"]);

        //    return dt;
        //}
        //Karans Code
        public DataTable SelectById()
        {
            string query = "SELECT * FROM Doctor WHERE DoctorId = @id";
            List<SqlParameter> lstParam = new List<SqlParameter>();
            lstParam.Add(new SqlParameter("@id", this.DoctorId));
            DataTable dt = DataAccess.SelectData(query, lstParam);
            if (dt.Rows.Count > 0)
            {
                this.DoctorId = Convert.ToInt32(dt.Rows[0]["DoctorId"]);
                this.DocName = dt.Rows[0]["DocName"].ToString();
                this.UserName = dt.Rows[0]["Username"].ToString();
                this.Password = dt.Rows[0]["Password"].ToString();
                this.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                this.Intro = dt.Rows[0]["Intro"].ToString();
                this.Photo = dt.Rows[0]["Photo"].ToString();
                this.Gender = dt.Rows[0]["Gender"].ToString();
                this.Mobile = dt.Rows[0]["Mobile"].ToString();
                this.Email = dt.Rows[0]["Email"].ToString();
                this.DOB = Convert.ToDateTime(dt.Rows[0]["DOB"]);
                this.DOJ = Convert.ToDateTime(dt.Rows[0]["DOJ"]);
                this.Status = dt.Rows[0]["Status"].ToString();
                this.SpecialityId = Convert.ToInt32(dt.Rows[0]["SpecialityId"]);
                this.Qualification = dt.Rows[0]["Qualification"].ToString();
                this.MinConsultationFee = Convert.ToInt32(dt.Rows[0]["MinConsultationFee"]);
                this.Documents = dt.Rows[0]["Documents"].ToString();
                this.MaxApptPerHour = Convert.ToInt32(dt.Rows[0]["MaxApptPerHour"]);
            }
            return dt;
        }
        public DataTable SelectAllDoctor() {
            string query = "SELECT Doctor.DoctorId,Doctor.DocName,Doctor.Qualification,Doctor.MinConsultationFee, Speciality.SpName FROM Doctor INNER JOIN Speciality ON Doctor.SpecialityId = Speciality.SpecialityId";
            List<SqlParameter> lstParam = new List<SqlParameter>();
            return DataAccess.SelectData(query,lstParam);
        }

    }
}



    