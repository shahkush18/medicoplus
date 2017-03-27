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
        public int DiseaseId { get; set; }
        public string DiseaseName { get; set; }

        public int InsertReport()
        {
            string query = "Insert INTO Report VALUES (@DoctorId,@DiseaseId,@PatientName,@PatientAddress,@AreaId,@CityId,@DiseaseName)";
            List<SqlParameter> lstparams = new List<SqlParameter>();
            lstparams.Add(new SqlParameter("@DoctorId",this.DoctorId));
            lstparams.Add(new SqlParameter("@DiseaseId", this.DiseaseId));
            lstparams.Add(new SqlParameter("@PatientName", this.PatientName));
            lstparams.Add(new SqlParameter("@PatientAddress", this.PatientAddress));
            lstparams.Add(new SqlParameter("@AreaId", this.AreaId));
            lstparams.Add(new SqlParameter("@CityId", this.CityId));
            lstparams.Add(new SqlParameter("@DiseaseName", this.DiseaseName));

            return DataAccess.ModifyData(query, lstparams);

        }
        public DataTable SelectAllReport(int DoctorId) {
            string query = "SELECT * FROM Report WHERE DoctorId =@DoctorId";

            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@DoctorId",DoctorId));

            return DataAccess.SelectData(query, lstParams);
        }
    }
}