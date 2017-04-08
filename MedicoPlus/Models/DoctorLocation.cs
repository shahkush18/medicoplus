using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MedicoPlus.Models
{
    public class DoctorLocation
    {
        public int DoctorLocationId { get; set; }
        public int DoctorId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string DaysOfWork  { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public int AreaId { get; set; }
        public string Phone { get; set; }
        public string Affilation { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ClinicName { get; set; }

        public int InsertDoctorLocation()
        {
            string query = "Insert INTO DoctorLocation(StartTime,DoctorId,EndTime,DaysOfWork,Address,CityId,AreaId,Phone,Affiliation,UserName,Password,ClinicName) Values(@StartTime,@DoctorId,@EndTime,@DaysOfWork,@Address,@CityId,@AreaId,@Phone,@Affilation,@UserName,@Password,@ClinicName)";
            List<SqlParameter> lstparam = new List<SqlParameter>();
            lstparam.Add(new SqlParameter("@StartTime", this.StartTime));
            lstparam.Add(new SqlParameter("@DoctorId", this.DoctorId));
            lstparam.Add(new SqlParameter("@EndTime", this.EndTime));
            lstparam.Add(new SqlParameter("@DaysOfWork", this.DaysOfWork));
            lstparam.Add(new SqlParameter("@Address",this.Address));
            lstparam.Add(new SqlParameter("@CityId", this.CityId));
            lstparam.Add(new SqlParameter("@AreaId", this.AreaId));
            lstparam.Add(new SqlParameter("@Phone", this.Phone));
            lstparam.Add(new SqlParameter("@Affilation", this.Affilation));
            lstparam.Add(new SqlParameter("@UserName", this.UserName));
            lstparam.Add(new SqlParameter("@Password", this.Password));
            lstparam.Add(new SqlParameter("@ClinicName", this.ClinicName));
            return DataAccess.ModifyData(query, lstparam);

        }


        public DataTable SelectByDoctorId()
        {
            string query = "SELECT DoctorLocation.ClinicName , City.CityName , Area.AreaName FROM DoctorLocation INNER JOIN City ON DoctorLocation.CityId = City.CityId INNER JOIN Area ON DoctorLocation.AreaId = Area.AreaId WHERE DoctorLocation.DoctorId = @DoctorId";
            List<SqlParameter> lstparams = new List<SqlParameter>();
            lstparams.Add(new SqlParameter("@DoctorId", this.DoctorId));
            DataTable dt = DataAccess.SelectData(query, lstparams);
            return dt;
        }

        public DataTable SelectByCityId()
        {
            string query = "Select * from DoctorLocation WHERE DoctorId = @DoctorId AND CityId = @CityId";
            List<SqlParameter> lstparams = new List<SqlParameter>();
            lstparams.Add(new SqlParameter("@DoctorId", this.DoctorId));
            lstparams.Add(new SqlParameter("@CityId", this.CityId));
            DataTable dt = DataAccess.SelectData(query, lstparams);
            return dt;
        }

        public int UpdateDoctorLocation()   
        {
            string query = "UPDATE DoctorLocation  SET StartTime = @StartTime , EndTime = @EndTime , DaysOfWork = @DaysOfWork , Address = @Address , CityId = @CityId , AreaId = @AreaId , Phone = @Phone , Affilation = @Affilation , UserName = @UserName , Password = @Password WHERE DoctorLocationId = @DoctorLocationId ";
            List<SqlParameter> lstparams = new List<SqlParameter>();
            lstparams.Add(new SqlParameter("@StartTime", this.StartTime));
            lstparams.Add(new SqlParameter("@EndTime", this.EndTime));
            lstparams.Add(new SqlParameter("@DaysOfWork", this.DaysOfWork));
            lstparams.Add(new SqlParameter("@Address", this.Address));
            lstparams.Add(new SqlParameter("@CityId", this.CityId));
            lstparams.Add(new SqlParameter("@AreaId", this.AreaId));
            lstparams.Add(new SqlParameter("@Phone", this.Phone));
            lstparams.Add(new SqlParameter("@Affilation", this.Affilation));
            lstparams.Add(new SqlParameter("@UserName", this.UserName));
            lstparams.Add(new SqlParameter("@Password", this.Password));
            lstparams.Add(new SqlParameter("@DoctorLocationId", this.DoctorLocationId));

            
            return DataAccess.ModifyData(query,lstparams);
        }
        public DataTable SelectByDoctorLocationId()
        {
            string query = "Select * from DoctorLocation WHERE DoctorLocationId = @DoctorLocationId";
            List<SqlParameter> lstparams = new List<SqlParameter>();
            lstparams.Add(new SqlParameter("@DoctorLocationId", this.DoctorLocationId));
            DataTable dt = DataAccess.SelectData(query, lstparams);
            this.DoctorId = Convert.ToInt32(dt.Rows[0]["DoctorId"]);
            this.StartTime = Convert.ToDateTime(dt.Rows[0]["StartTime"]);
            this.EndTime = Convert.ToDateTime(dt.Rows[0]["EndTime"]);
            this.DaysOfWork = Convert.ToString(dt.Rows[0]["DaysOfWork"]);
            this.Address = Convert.ToString(dt.Rows[0]["Address"]);
            this.CityId = Convert.ToInt32(dt.Rows[0]["CityId"]);
            this.AreaId = Convert.ToInt32(dt.Rows[0]["AreaId"]);
            this.Phone = Convert.ToString(dt.Rows[0]["Phone"]);
            this.Affilation = Convert.ToString(dt.Rows[0]["Affiliation"]);
            this.UserName = Convert.ToString(dt.Rows[0]["UserName"]);
            this.Password = Convert.ToString(dt.Rows[0]["Password"]);
            this.ClinicName = Convert.ToString(dt.Rows[0]["ClinicName"]);
            return dt;
        }

    }
}