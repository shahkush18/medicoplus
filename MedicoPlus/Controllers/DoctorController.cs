using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedicoPlus.Models;
using System.Data;
using ClosedXML.Excel;
using Newtonsoft.Json;

using System.IO;
using System.Net.Mail;

namespace MedicoPlus.Controllers
{
    public class DoctorController : Controller
    {
        // GET: Doctor
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignupDoctor()
        {
            SpecialityModel sp = new SpecialityModel();
            DataTable dt = sp.SelectAll();
            ViewBag.Speciality = dt;
            return View();
        }
        [HttpPost]
        public ActionResult SignupDoctor(FormCollection collection)
        {
            DoctorModel D = new DoctorModel();
            D.DocName = collection["DocName"];
            D.UserName = collection["DocName"];
            D.Intro = collection["Intro"];
            if (Request.Files["Photo"] != null)
            {
                string path = "/Photo/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["Photo"].FileName;
                Request.Files["Photo"].SaveAs(Server.MapPath(path));
                D.Photo = path;
            }
            else
            {
                D.Photo = "";
            }
            D.Gender = collection["Gender"];
            D.Mobile = collection["Mobile"];
            D.Email = collection["Email"];
            D.DOB = Convert.ToDateTime(collection["DOB"]);
            D.DOJ = DateTime.Now;
            D.Status = "PENDING";
            D.SpecialityId = Convert.ToInt32(collection["SpecialityId"]);
            D.Qualification = collection["Qualification"];
            D.Documents = collection["Documents"];
            D.MinConsultationFee = Convert.ToInt32(collection["mcf"]);
            D.Documents= collection["Documents"];
            D.MaxApptPerHour= Convert.ToInt32(collection["maph"]);
            D.RegistrationNo = Convert.ToString(collection["RegNo"]);

            D.Password= collection["Password"];
            
            D.InsertDoctor();
            SendMail(D.Email, "You have been successfully registered with MedicoPlus", "Thanks For Registering With Us !");

            return RedirectToAction("LoginDoctor","Access");
        }
        public void SendMail(string to, string msg, string sub)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(to);
            mail.From = new MailAddress("medicoplus.co@gmail.com");
            mail.Subject = sub;
            string Body = msg;
            mail.Body = Body;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential
            ("medicoplus.co@gmail.com", "MedicoAdmin");
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }


        public ActionResult DoctorProfile()
        {
            DoctorModel doc = new DoctorModel();
            doc.DoctorId = ((DoctorModel)Session["Doctor"]).DoctorId;
            doc.SelectById();
            DoctorLocation docLoc = new DoctorLocation();
            docLoc.DoctorId = doc.DoctorId;
            DataTable dt = docLoc.SelectByDoctorId();
            ViewBag.Location = dt;
          
            return View(doc);
        }
        
        public ActionResult DLogIn()
        {
            Session["Doctor"] = null;
            return View();
        }
        [HttpPost]
        public ActionResult AuthenticateDoctor(FormCollection collection)
        {
            DoctorModel D = new DoctorModel();
            D.Email = collection["Email"];
            D.Password = collection["Password"];
            if (D.Authenticate())
            {
                Session["Doctor"] = D;
                return RedirectToAction("DoctorProfile", "Doctor");
            }
            else
            {
                return RedirectToAction("DLogin");
            }
        }
        public ActionResult AddReport()
        {
            DataTable dt = new DiseaseModel().SelectAll();
            Report rep = new Report();

            DataTable reports = rep.SelectAllReport(1);

            ViewBag.Reports = reports;
            CityModel city = new CityModel();
            ViewBag.City = city.SelectAll();
            AreaModel Area = new AreaModel();
            ViewBag.Area = Area.SelectAllArea();

            return View(dt);

           

        }
        [HttpPost]
        public ActionResult AddReport(FormCollection collection)
        {
           
            Report rep = new Report();
            rep.AreaId = Convert.ToInt32(collection["AreaId"]);
            rep.CityId = Convert.ToInt32(collection["CityId"]);
            rep.DoctorId = ((DoctorModel)Session["Doctor"]).DoctorId;
            rep.DiseaseId = Convert.ToInt32(collection["DiseaseId"]);
            rep.PatientName = Convert.ToString(collection["PatientName"]);
            rep.PatientAddress = Convert.ToString(collection["PatientAddress"]);
            rep.Occupation = Convert.ToString(collection["Occupation"]);
            rep.Mobile = Convert.ToString(collection["Mobile"]);
            rep.Gender = Convert.ToString(collection["Gender"]);
            rep.Age = Convert.ToInt32(collection["Age"]);
            rep.Date = Convert.ToDateTime(collection["Date"]);


            rep.InsertReport();

            return RedirectToAction("AddReport");
        }
       
        public ActionResult AddLocation()
        {
            DoctorModel dr = new DoctorModel();
            dr.DoctorId = ((DoctorModel)Session["Doctor"]).DoctorId;
            CityModel city = new CityModel();
            ViewBag.City = city.SelectAll();
            AreaModel area = new AreaModel();
            ViewBag.Area = area.SelectAllArea();

            return View();
        }
        [HttpPost]
        public ActionResult AddLocation(FormCollection collection)
        {
            CityModel city = new CityModel();
            ViewBag.City = city.SelectAll();
            AreaModel area = new AreaModel();
            ViewBag.Area = area.SelectAllArea();
            DoctorLocation docLoc = new DoctorLocation();
            docLoc.DoctorId = ((DoctorModel)Session["Doctor"]).DoctorId;
            docLoc.StartTime = Convert.ToDateTime(collection["StartTime"]);
            docLoc.EndTime= Convert.ToDateTime(collection["EndTime"]);
            docLoc.DaysOfWork = Convert.ToString(collection["DaysOfWork"]);
            docLoc.Address = Convert.ToString(collection["Address"]);
            docLoc.CityId = Convert.ToInt32(collection["CityId"]); ;
            docLoc.AreaId = Convert.ToInt32(collection["AreaId"]);
            docLoc.UserName = Convert.ToString(collection["Address"]);
            docLoc.Password = Convert.ToString(collection["Password"]);
            docLoc.Affilation = Convert.ToString(collection["Affiliation"]);
            docLoc.Phone = Convert.ToString(collection["Phone"]);
            docLoc.ClinicName = Convert.ToString(collection["ClinicName"]);

            docLoc.InsertDoctorLocation();
            return RedirectToAction("DoctorProfile");
        }
        public ActionResult UpcomingAppointments()
        {
            Appointment appointment = new Appointment();
            DoctorModel dr = (DoctorModel)Session["Doctor"];

            DataTable dt = appointment.SelectPendingDoctorAppointments(dr.DoctorId);


            return View(dt);
        }
         public ActionResult PreviousReports()
        {
            DataTable dt = new DiseaseModel().SelectAll();
            Report rep = new Report();

            DataTable reports = rep.SelectAllReport(((DoctorModel)Session["Doctor"]).DoctorId);

            ViewBag.Reports = reports;
            CityModel city = new CityModel();
            ViewBag.City = city.SelectAll();
            AreaModel Area = new AreaModel();
            ViewBag.Area = Area.SelectAllArea();

            return View(dt);

           

        }
        [HttpGet]
        public ActionResult AddPrescription(int id)
        {
            Appointment a = new Appointment();
            DataTable dt = a.SelectAppointmentById(id);
            DiseaseModel dis = new DiseaseModel();
            DataTable disease = dis.SelectAll();
            ViewBag.Disease = disease;
            
            return View(dt);



        }
        [HttpPost]
        public ActionResult AddPrescription(FormCollection collection)
        {

            string prescription1 = Convert.ToString(collection["Med1"])+"     " + Convert.ToString(collection["Quant1"]) + "     " + Convert.ToString(collection["Time1"]);
            string prescription2 = Convert.ToString(collection["Med2"]) + "     " + Convert.ToString(collection["Quant2"]) + "     " + Convert.ToString(collection["Time2"]);
            string prescription3 = Convert.ToString(collection["Med3"]) + "     " + Convert.ToString(collection["Quant3"]) + "     " + Convert.ToString(collection["Time3"]);
            string prescription4 = Convert.ToString(collection["Med4"]) + "     " + Convert.ToString(collection["Quant4"]) + "     " + Convert.ToString(collection["Time4"]);
            string prescription5 = Convert.ToString(collection["Med5"]) + "     " + Convert.ToString(collection["Quant5"]) + "     " + Convert.ToString(collection["Time5"]);

            Appointment a = new Appointment();
            a.AppointmentId = Convert.ToInt32(collection["AppointmentId"]);
            a.DrRemarks = Convert.ToString(collection["remarks"]);
            a.Prescription = prescription1;
            a.Prescription2 = prescription2;
            a.Prescription3 = prescription3;
            a.Prescription4 = prescription4;
            a.Prescription5= prescription5;
            a.DiseaseId = Convert.ToInt32(collection["disease"]);
            a.VisitDate = DateTime.Now;
            a.AddPrescription();

        

            return RedirectToAction("PreviousReports");



        }
        public ActionResult Prescriptions(FormCollection collection)
        {
           DataTable dt = new Appointment().SelectAppointmentByStatus(((DoctorModel)Session["Doctor"]).DoctorId);


            return View(dt);



        }
        public string GetAllCities()
        {
            CityModel cities = new CityModel();
            DataTable dt = cities.SelectAll();
            string JSONString = JsonConvert.SerializeObject(dt);

            return JSONString;
        }
        public ActionResult CancelAppointment(FormCollection collection)
        {
            Appointment appointment = new Appointment();
            appointment.AppointmentId = Convert.ToInt32(collection["AppointmentId"]);
            DataTable dt = appointment.SelectAppointmentById(appointment.AppointmentId);
            appointment.CancelAppointment();
            string msg = "Your Appointment With Doctor :" + ((DoctorModel)Session["Doctor"]).DocName + " Which Was on the " + dt.Rows[0]["AppDate"] + " Has been Cancelled. Sorry For the inconvince. \n You can book it for the next day or new Slot.";
            SendMail(Convert.ToString(dt.Rows[0]["AppEmail"]), msg, "Your Appointment Has Been Cancelled");



            return RedirectToAction("UpcomingAppointments");
        }

        public ActionResult PreviousAppointments()
        {
            Appointment appointment = new Appointment();
            DoctorModel dr = (DoctorModel)Session["Doctor"];

            DataTable dt = appointment.SelectAppointmentByStatus(dr.DoctorId);


            return View(dt);
        }
        [HttpGet]
        public ActionResult EditProfile()
        {
            SpecialityModel sp = new SpecialityModel();
            DataTable dt = sp.SelectAll();
            ViewBag.sp = dt;
            DoctorModel doc = new DoctorModel();
            doc.DoctorId = ((DoctorModel)Session["Doctor"]).DoctorId;
            doc.SelectById();
            


            return View(doc);
        }
        [HttpPost]
        public ActionResult EditProfile(FormCollection collection)
        {
            
            
            DoctorModel D = new DoctorModel();
            D.DoctorId = ((DoctorModel)Session["Doctor"]).DoctorId;
            D.DocName = collection["DocName"];
            D.UserName = collection["DocName"];
            D.Intro = collection["Intro"];
            if (Request.Files["Photo"] != null)
            {
                string path = "/Photo/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["Photo"].FileName;
                Request.Files["Photo"].SaveAs(Server.MapPath(path));
                D.Photo = path;
            }
            else
            {
               
                D.Photo = "  "  ;
            }
            D.Gender = collection["Gender"];
            D.Mobile = collection["Mobile"];
            D.Email = collection["Email"];
           
            D.SpecialityId = Convert.ToInt32(collection["SpecialityId"]);
            D.Qualification = collection["Qualification"];
            D.Documents = collection["Documents"];
            D.MinConsultationFee = Convert.ToInt32(collection["mcf"]);
            D.Documents = collection["Documents"];
            D.MaxApptPerHour = Convert.ToInt32(collection["maph"]);
            D.RegistrationNo = Convert.ToString(collection["RegNo"]);

            D.Password = collection["Password"];

            D.UpdateDoctor();

            return RedirectToAction("DoctorProfile", "Doctor");
        }



    }
}