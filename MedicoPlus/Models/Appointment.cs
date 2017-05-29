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
        public string Prescription2 { get; set; }
        public string Prescription3 { get; set; }
        public string Prescription4 { get; set; }
        public string Prescription5 { get; set; }



        public int InsertAppointment()
        {
            string query = "Insert INTO Appointment(AppuserId,DoctorLocationId,SubmitDate,AppDate,Status,PatientName,Age,Phone,Address,AppTime,Occupation,Symptom,Gender) Values(@AppuserId,@DoctorLocationId,@SubmitDate,@AppDate,@Status,@PatientName,@Age,@Phone,@Address,@AppTime,@Occupation,@Symptoms,@Gender)";
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
            lstparam.Add(new SqlParameter("@Gender", this.Gender));
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
            string query = "Select Appointment.AppointmentId,Appointment.AppTime,Appointment.patientName , Appointment.AppDate , DoctorLocation.ClinicName , Doctor.DocName FROM Appointment INNER JOIN DoctorLocation ON Appointment.DoctorLocationID = DoctorLocation.DoctorLocationID INNER JOIN Doctor ON DoctorLocation.DoctorId = Doctor.DoctorId INNER JOIN Speciality ON Doctor.SpecialityId = Speciality.SpecialityId WHERE AppUserID = @AppUserID ORDER BY Appointment.SubmitDate DESC";
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
        public DataTable SelectAppointmentById(int AppointmentId)
        {
            string query = "Select  Appointment.* , DoctorLocation.ClinicName,Doctor.DocName,Doctor.DoctorId,Doctor.Email,AppUser.Email AS AppEmail FROM Appointment INNER JOIN DoctorLocation ON Appointment.DoctorLocationID = DoctorLocation.DoctorLocationID INNER JOIN Doctor ON DoctorLocation.DoctorId = Doctor.DoctorId INNER JOIN AppUser ON Appointment.AppUserId = AppUser.AppUserId WHERE AppointmentId = @AppointmentId";
            List<SqlParameter> lstparams = new List<SqlParameter>();
            lstparams.Add(new SqlParameter("@AppointmentId", AppointmentId));
            
            DataTable dt = DataAccess.SelectData(query, lstparams);

            return dt;
        }
        public int AddPrescription()
        {
            string query = "UPDATE Appointment SET DrRemarks =@DrRemarks , Prescription = @Prescription,Prescription2 = @Prescription2,Prescription3 = @Prescription3,Prescription4 = @Prescription4,Prescription5 = @Prescription5 ,DiseaseId=@DiseaseId , Status=@Status,VisitDate=@VisitDate WHERE AppointmentId = @AppointmentId";
            List<SqlParameter> lstparams = new List<SqlParameter>();
            lstparams.Add(new SqlParameter("@AppointmentId", this.AppointmentId));
            lstparams.Add(new SqlParameter("@DrRemarks", this.DrRemarks));
            lstparams.Add(new SqlParameter("@Prescription", this.Prescription));
            lstparams.Add(new SqlParameter("@Prescription2", this.Prescription2));
            lstparams.Add(new SqlParameter("@Prescription3", this.Prescription3));
            lstparams.Add(new SqlParameter("@Prescription4", this.Prescription4));
            lstparams.Add(new SqlParameter("@Prescription5", this.Prescription5));
            lstparams.Add(new SqlParameter("@DiseaseId", this.DiseaseId));
            lstparams.Add(new SqlParameter("@Status", "COMPLETED"));
            lstparams.Add(new SqlParameter("@VisitDate", this.VisitDate));
            int i = DataAccess.ModifyData(query, lstparams);
            DataTable dt = new Appointment().SelectAppointmentById(this.AppointmentId);
            Report auto = new Report();
            auto.DoctorId = Convert.ToInt32(dt.Rows[0]["DoctorId"]);
            auto.DiseaseId = Convert.ToInt32(dt.Rows[0]["DiseaseId"]);
            auto.PatientName = Convert.ToString(dt.Rows[0]["patientName"]);
            auto.PatientAddress = Convert.ToString(dt.Rows[0]["Address"]);
            AppUserModel user = new AppUserModel();
            user.AppUserId = Convert.ToInt32(dt.Rows[0]["AppUserId"]);
            user.SelectAppUserById();
            auto.AreaId = user.AreaId;
            auto.CityId = user.CityId;
            auto.Occupation = Convert.ToString(dt.Rows[0]["Occupation"]);
            auto.Mobile = user.Mobile;
            auto.Gender = Convert.ToString(dt.Rows[0]["Gender"]);
            auto.Age = Convert.ToInt32(dt.Rows[0]["Age"]);
            auto.Date = DateTime.Now;
            auto.InsertReport();


            return i;
        }
        public DataTable SelectAppointmentByStatus(int DoctorId)
        {
            string query = "Select  Appointment.* , DoctorLocation.ClinicName,Doctor.DocName,Doctor.DoctorId FROM Appointment INNER JOIN DoctorLocation ON Appointment.DoctorLocationID = DoctorLocation.DoctorLocationID INNER JOIN Doctor ON DoctorLocation.DoctorId = Doctor.DoctorId WHERE Doctor.DoctorId =@DoctorId AND Appointment.Status=@Status";
            List<SqlParameter> lstparams = new List<SqlParameter>();
            lstparams.Add(new SqlParameter("@DoctorId", DoctorId));
            lstparams.Add(new SqlParameter("@Status", "COMPLETED"));

            DataTable dt = DataAccess.SelectData(query, lstparams);

            return dt;
        }
        public DataTable SelectPrescriptionById(int userid)
        {
            string query = "Select Appointment.AppointmentId,Appointment.AppTime,Appointment.patientName , Appointment.AppDate,Appointment.StoreStatus , DoctorLocation.ClinicName , Doctor.DocName FROM Appointment INNER JOIN DoctorLocation ON Appointment.DoctorLocationID = DoctorLocation.DoctorLocationID INNER JOIN Doctor ON DoctorLocation.DoctorId = Doctor.DoctorId WHERE AppUserID = @AppUserID  AND Appointment.Status=@Status ORDER BY Appointment.SubmitDate DESC";
            List<SqlParameter> lstparams = new List<SqlParameter>();
            lstparams.Add(new SqlParameter("@AppUserId", userid));
            lstparams.Add(new SqlParameter("@Status", "COMPLETED"));

            DataTable dt = DataAccess.SelectData(query, lstparams);

            return dt;
        }
        public DataTable ForwardMedicalStore() {
            string query = "UPDATE Appointment SET MedicalStoreID = @MedicalStoreId , StoreStatus = @StoreStatus WHERE AppointmentId = @AppointmentId";
            List<SqlParameter> lstparams = new List<SqlParameter>();
            lstparams.Add(new SqlParameter("@MedicalStoreId", this.MedicalStoreId));
            lstparams.Add(new SqlParameter("@StoreStatus", this.StoreStatus));
            lstparams.Add(new SqlParameter("@AppointmentId", this.AppointmentId));
            return DataAccess.SelectData(query, lstparams);
        }
        public DataTable SelectPrescriptionsByStoreId()
        {
            string query = "SELECT Appointment.*,DoctorLocation.ClinicName,Doctor.DocName From Appointment INNER JOIN DoctorLocation ON Appointment.DoctorLocationId = DoctorLocation.DoctorLocationId INNER JOIN Doctor ON DoctorLocation.DoctorId = Doctor.DoctorId WHERE MedicalStoreID = @MedicalStoreId AND StoreStatus = @StoreStatus";
            List<SqlParameter> lstparam = new List<SqlParameter>();
            lstparam.Add(new SqlParameter("@MedicalStoreId", this.MedicalStoreId));
            lstparam.Add(new SqlParameter("@StoreStatus", this.StoreStatus));


            return DataAccess.SelectData(query, lstparam);


        }
        public int UpdateStoreStatus() {
            string query = "UPDATE Appointment SET StoreStatus = @StoreStatus WHERE AppointmentId = @AppointmentId";
            List<SqlParameter> lstparam = new List<SqlParameter>();
            
            lstparam.Add(new SqlParameter("@StoreStatus", this.StoreStatus));
            lstparam.Add(new SqlParameter("@AppointmentId", this.AppointmentId));

            return DataAccess.ModifyData(query, lstparam);
        }

    }
}