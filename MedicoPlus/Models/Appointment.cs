using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MedicoPlus.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int AppUserId { get; set; }
        public int DoctorLocationId { get; set; }
        public string SubmitDate { get; set; }
        public string AppDate { get; set; }
        public string VisitDate { get; set; }
        public string Status { get; set; }
        public string PatientName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int DiseaseId { get; set; }
        public string Symptoms { get; set; }
        public string  DrRemarks { get; set; }
        public string Prescription { get; set; }
        public string FollowUpDays { get; set; }
        public string Origin { get; set; }
        public int MedicalStoreId { get; set; }
        public string StoreStatus { get; set; }
        public string Feedback { get; set; }
        public string Rating { get; set; }

        public int InsertAppointment()
        {
            string query = "Insert INTO Appointment(AppuserId,DoctorLocationId,SubmitDate,AppDate,Status,PatientName,Age,Phone,Address) Values(@AppuserId,@DoctorLocationId,@SubmitDate,@AppDate,@StatusPatientName,@Age,@Phone,@Address)";
            List<SqlParameter> lstparam = new List<SqlParameter>();
            lstparam.Add(new SqlParameter("@AppuserId", this.AppUserId));
            lstparam.Add(new SqlParameter("@DoctorLocationId", this.DoctorLocationId));
            lstparam.Add(new SqlParameter("@SubmitDate", this.SubmitDate));
            lstparam.Add(new SqlParameter("@AppDate", this.AppDate));
            lstparam.Add(new SqlParameter("@Staus", "PENDING"));
            lstparam.Add(new SqlParameter("@PatientName", this.PatientName));
            lstparam.Add(new SqlParameter("@Age", this.Age));
            lstparam.Add(new SqlParameter("@Phone", this.Phone));
            lstparam.Add(new SqlParameter("@Address", this.Address));
            return DataAccess.ModifyData(query,lstparam);
            
        }
        public DataTable SelectAppointmentByDoctorLocationId()
        {
            string query = "Select * from Appointment WHERE DoctorLocationId = @DoctorLocationId";
            List<SqlParameter> lstparams = new List<SqlParameter>();
            lstparams.Add(new SqlParameter("@DoctorLocationId",this.DoctorLocationId));
            DataTable dt = DataAccess.SelectData(query, lstparams);
            return dt;
        }
        public DataTable SelectAppointmentByMedicalStoreId()
        {
            string query = "Select * from Appointment WHERE MedicalStoreId = @MedicalStoreId";
            List<SqlParameter> lstparams = new List<SqlParameter>();
            lstparams.Add(new SqlParameter("@MedicalStoreId", this.MedicalStoreId));
            DataTable dt = DataAccess.SelectData(query, lstparams);
            return dt;
        }

    }
}