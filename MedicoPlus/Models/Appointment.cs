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
        public DateTime SubmitDate { get; set; }
        public DateTime AppDate { get; set; }
        public DateTime VisitDate { get; set; }
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
        public string AppTime { get; set; }
        public string Occupation { get; set; }

        public int InsertAppointment()
        {
            string query = "Insert INTO Appointment(AppuserId,DoctorLocationId,SubmitDate,AppDate,Status,PatientName,Age,Phone,Address,AppTime,Occupation,Symptom) Values(@AppuserId,@DoctorLocationId,@SubmitDate,@AppDate,@Status,@PatientName,@Age,@Phone,@Address,@AppTime,@Occupation,@Symptoms)";
            List<SqlParameter> lstparam = new List<SqlParameter>();
            lstparam.Add(new SqlParameter("@AppuserId", this.AppUserId));
            lstparam.Add(new SqlParameter("@DoctorLocationId", this.DoctorLocationId));
            lstparam.Add(new SqlParameter("@SubmitDate",this.SubmitDate));
            lstparam.Add(new SqlParameter("@AppDate", this.AppDate));
            lstparam.Add(new SqlParameter("@Status", "PENDING"));
            lstparam.Add(new SqlParameter("@PatientName", this.PatientName));
            lstparam.Add(new SqlParameter("@Age", this.Age));
            lstparam.Add(new SqlParameter("@Phone", this.Phone));
            lstparam.Add(new SqlParameter("@Address", this.Address));
            lstparam.Add(new SqlParameter("@AppTime", this.AppTime));
            lstparam.Add(new SqlParameter("@Occupation", this.Occupation));
            lstparam.Add(new SqlParameter("@Symptoms", this.Symptoms));
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
        public int CheckNoOfAppointment() {

            string query = "SELECT * FROM APPOINTMENT WHERE DoctorLocationId = @DoctorLocationId AND AppTime =@AppTime AND AppDate = @AppDate";
            List<SqlParameter> lstparams = new List<SqlParameter>();
            lstparams.Add(new SqlParameter("@DoctorLocationId", this.DoctorLocationId));
            lstparams.Add(new SqlParameter("@AppDate", this.AppDate));
            lstparams.Add(new SqlParameter("@AppTime", this.AppTime));
            return (DataAccess.SelectData(query,lstparams)).Rows.Count;
        }
        public DataTable SelectByAppUserId()
        {
            string query = "Select Appointment.AppTime,Appointment.patientName , Appointment.AppDate , DoctorLocation.ClinicName , Doctor.DocName FROM Appointment INNER JOIN DoctorLocation ON Appointment.DoctorLocationID = DoctorLocation.DoctorLocationID INNER JOIN Doctor ON DoctorLocation.DoctorId = Doctor.DoctorId WHERE AppUserID = @AppUserID ORDER BY Appointment.SubmitDate DESC";
            List<SqlParameter> lstparams = new List<SqlParameter>();
            lstparams.Add(new SqlParameter("@AppUserId", this.AppUserId));
            DataTable dt = DataAccess.SelectData(query, lstparams);
            return dt;
        }
        public DataTable SelectPendingAppointments()
        {
            string query = "Select Appointment.AppTime ,Appointment.AppointmentID , Appointment.patientName , Appointment.AppDate , DoctorLocation.ClinicName , Doctor.DocName FROM Appointment INNER JOIN DoctorLocation ON Appointment.DoctorLocationID = DoctorLocation.DoctorLocationID INNER JOIN Doctor ON DoctorLocation.DoctorId = Doctor.DoctorId WHERE AppUserID = @AppUserID AND Appointment.Status = @Status ORDER BY Appointment.SubmitDate DESC";
            List<SqlParameter> lstparams = new List<SqlParameter>();
            lstparams.Add(new SqlParameter("@AppUserId", this.AppUserId));
            lstparams.Add(new SqlParameter("@Status", "PENDING"));
            DataTable dt = DataAccess.SelectData(query, lstparams);
            return dt;
        }
        public int CancelAppointment()
        {
            string query = "DELETE FROM Appointment WHERE AppointmentId=@AppointmentId";
            List<SqlParameter> lstparams = new List<SqlParameter>();
            lstparams.Add(new SqlParameter("@AppointmentId", this.AppointmentId));
            
            //DataTable dt = DataAccess.ModifyData(query, lstparams);
            return DataAccess.ModifyData(query, lstparams); 
        }
        public DataTable SelectPendingDoctorAppointments(int DoctorId)
        {
            string query = "Select Appointment.AppTime ,Appointment.AppointmentID , Appointment.patientName , Appointment.AppDate , DoctorLocation.ClinicName , Doctor.DocName FROM Appointment INNER JOIN DoctorLocation ON Appointment.DoctorLocationID = DoctorLocation.DoctorLocationID INNER JOIN Doctor ON DoctorLocation.DoctorId = Doctor.DoctorId WHERE DoctorLocation.DoctorId = @DoctorId AND Appointment.Status = @Status ORDER BY Appointment.SubmitDate DESC";
            List<SqlParameter> lstparams = new List<SqlParameter>();
            lstparams.Add(new SqlParameter("@DoctorId", DoctorId));
            lstparams.Add(new SqlParameter("@Status", "PENDING"));
            DataTable dt = DataAccess.SelectData(query, lstparams);
            return dt;
        }
    }
}