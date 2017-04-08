using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MedicoPlus.Models
{
    public class Report
    {
        public int ReportId { get; set; }
        public int CityId { get; set; }
        public int AreaId { get; set; }
        public int DoctorId { get; set; }
        public string PatientName { get; set; }
        public string PatientAddress { get; set; }
        public string Mobile { get; set; }
        public int Age { get; set; }
        public string Occupation { get; set; }
        public DateTime Date { get; set; }
        public int DiseaseId { get; set; }
        public string DiseaseName { get; set; }
        public string Gender { get; set; }

        public int InsertReport()
        {
            string query = "Insert INTO Report(DoctorId,DiseaseId,PatientName,PatientAddress,AreaId,CityId,Occupation,Age,Date,Gender,Mobile) VALUES (@DoctorId,@DiseaseId,@PatientName,@PatientAddress,@AreaId,@CityId,@Occupation,@Age,@Date,@Gender,@Mobile)";
            List<SqlParameter> lstparams = new List<SqlParameter>();
            lstparams.Add(new SqlParameter("@DoctorId",this.DoctorId));
            lstparams.Add(new SqlParameter("@DiseaseId", this.DiseaseId));
            lstparams.Add(new SqlParameter("@PatientName", this.PatientName));
            lstparams.Add(new SqlParameter("@PatientAddress", this.PatientAddress));
            lstparams.Add(new SqlParameter("@AreaId", this.AreaId));
            lstparams.Add(new SqlParameter("@CityId", this.CityId));
            
            lstparams.Add(new SqlParameter("@Occupation", this.Occupation));
            lstparams.Add(new SqlParameter("@Age", this.Age));
            lstparams.Add(new SqlParameter("@Date", this.Date));
            lstparams.Add(new SqlParameter("@Gender", this.Gender));
            lstparams.Add(new SqlParameter("@Mobile", this.Mobile));

            return DataAccess.ModifyData(query, lstparams);

        }
        public DataTable SelectAllReport(int DoctorId) {
            string query = "SELECT * FROM Report WHERE DoctorId =@DoctorId";

            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@DoctorId",DoctorId));

            return DataAccess.SelectData(query, lstParams);
        }
       
         public DataTable GenerateReport()
        {
            string query = "SELECT  Report.PatientName , Report.PatientAddress , Report.Gender , Report.Mobile , Report.Age , Report.Occupation , Disease.DiseaseName , Doctor.DocName , Report.Date , Area.AreaName , City.CityName FROM Report INNER JOIN Disease ON Report.DiseaseId = Disease.DiseaseId INNER JOIN City ON Report.CityId = City.CityId INNER JOIN Area ON Report.AreaId = Area.AreaId INNER JOIN Doctor ON Report.DoctorId = Doctor.DoctorId ";

            List<SqlParameter> lstParams = new List<SqlParameter>();
           

            return DataAccess.SelectData(query,lstParams);
        }
        public DataTable GenerateFilteredReport(int AreaId,int CityId,int DiseaseId,DateTime From,DateTime To,int temp)
        {
            string query = @"SELECT  Report.PatientName , Report.PatientAddress , Report.Gender , Report.Mobile , Report.Age , Report.Occupation , Disease.DiseaseName , Doctor.DocName , Report.Date , Area.AreaName , City.CityName FROM Report INNER JOIN Disease ON Report.DiseaseId = Disease.DiseaseId INNER JOIN City ON Report.CityId = City.CityId INNER JOIN Area ON Report.AreaId = Area.AreaId INNER JOIN Doctor ON Report.DoctorId = Doctor.DoctorId 

                            WHERE (@AreaID = 0 OR @AreaID = Report.AreaId)

                             AND(@CityId = 0 OR @CityId = Report.CityId)

                                AND(@DiseaseId = 0 OR @DiseaseId = Report.DiseaseId)
                                    AND (@Temp=0 OR Report.Date BETWEEN @To AND @From)";

            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@AreaId",AreaId));
            lstParams.Add(new SqlParameter("@CityId", CityId));
            lstParams.Add(new SqlParameter("@DiseaseId", DiseaseId));
            lstParams.Add(new SqlParameter("@Temp", temp));
            lstParams.Add(new SqlParameter("@From", From.Date));
            lstParams.Add(new SqlParameter("@To", To.Date));

            return DataAccess.SelectData(query, lstParams);
        }
    }
}