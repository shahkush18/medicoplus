using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedicoPlus.Models;
using System.Data;
using System.Diagnostics;
using System.Net.Mail;

namespace MedicoPlus.Controllers
{
    public class AppUserController : Controller
    {
        // GET: AppUser
        public ActionResult UserProfile() {
            AppUserModel user = new AppUserModel();
            user.AppUserId = ((AppUserModel)Session["User"]).AppUserId;
            user.SelectAppUserById();
            CityModel city = new CityModel();
            city.CityId = user.CityId;
            city.SelectAllCityById();
            ViewBag.City = city;
            Appointment appointment = new Appointment();
            appointment.AppUserId = ((AppUserModel)Session["User"]).AppUserId; 
            DataTable dt = appointment.SelectPendingAppointments();
            ViewBag.Appointment = dt;
            return View(user);
        }
        public ActionResult SignUp()
        {
            CityModel City = new CityModel();
            DataTable dt = City.SelectAll();
            AreaModel Area = new AreaModel();
            DataTable dtt = Area.SelectAllArea();
            List<AreaModel> list = new List<AreaModel>();
            
                for(int i = 0; i < dtt.Rows.Count; i++)
                {
                    AreaModel area = new AreaModel();
                    area.AreaId = Convert.ToInt32(dtt.Rows[i]["AreaId"]);
                    area.AreaName = dtt.Rows[i]["AreaName"].ToString();
                    list.Add(area);
                }
            
            ViewBag.list = list;
            Session["Admin"] = null;
            return View(dt);
        }
        [HttpPost]
        public ActionResult SignUp(FormCollection collection)
        {
            AppUserModel A = new AppUserModel();
            A.AUName = collection["Username"];
            A.Gender = collection["Gender"];
            A.Email = collection["Email"];
            A.AreaId =Convert.ToInt32(collection["AreaId"]);
            A.Address = collection["Address"];
            A.CityId = Convert.ToInt32(collection["CityId"]);
            A.Password = collection["Password"];
            A.IsActive = true;
            A.Phone = collection["Mobile"];
            A.Mobile = collection["Mobile"];
            if (Request.Files["Photo"] != null)
            {
                string path = "/Photo/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["Photo"].FileName;
                Request.Files["Photo"].SaveAs(Server.MapPath(path));
                A.Photo = path;
            }
            else
            {
                A.Photo = "";
            }
            A.DOB = Convert.ToDateTime(collection["DOB"]);
            A.InsertAppUser();
            SendMail(A.Email, "You have been successfully registered with MedicoPlus", "Thanks For Registering With Us !");


            return RedirectToAction("Login","Access");
        }

        public void SendMail(string to,string msg,string sub)
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



        [HttpPost]
        public ActionResult BookAppointment(FormCollection collection)
        {
            DoctorLocation docLoc = new DoctorLocation();

            docLoc.DoctorLocationId = Convert.ToInt32(collection["DoctorLocationId"]);
            //docLoc.DoctorLocationId = 1;
            docLoc.SelectByDoctorLocationId();
            DoctorModel doctor = new DoctorModel();
            doctor.DoctorId = docLoc.DoctorId;
            doctor.SelectById();
           
            Appointment aCheck = new Appointment();
            aCheck.AppDate = Convert.ToDateTime(collection["AppDate"]); 
            aCheck.AppTime = Convert.ToString(collection["AppTime"]);
            aCheck.DoctorLocationId = docLoc.DoctorLocationId;
            int day = (int)aCheck.AppDate.DayOfWeek;
            string daysOfWork = docLoc.DaysOfWork;
            bool appt = false;
            switch (day)
            {
                case 0:
                    
                    if (daysOfWork.Contains("SUN")) {
                        appt = true;
                    }
                    break;
                case 1:
                    
                    if (daysOfWork.Contains("MON"))
                    {
                        appt = true;
                    }
                    break;
                case 2:

                    if (daysOfWork.Contains("TUE"))
                    {
                        appt = true;
                    }
                    break;
                case 3:

                    if (daysOfWork.Contains("WED"))
                    {
                        appt = true;
                    }
                    break;
                case 4:

                    if (daysOfWork.Contains("THU"))
                    {
                        appt = true;
                    }
                    break;
                case 5:

                    if (daysOfWork.Contains("FRI"))
                    {
                        appt = true;
                    }
                    break;
                case 6:

                    if (daysOfWork.Contains("SAT"))
                    {
                        appt = true;
                    }
                    break;


            }

            if (aCheck.CheckNoOfAppointment() < doctor.MaxApptPerHour && appt)
            {
               
                return RedirectToAction("AppointmentFinal",new {AppTime = aCheck.AppTime , AppDate = aCheck.AppDate , DoctorLocationId = docLoc.DoctorLocationId });
            }
            else
            {
                ViewBag.error_found = "Appointment not available Try Again";
                return RedirectToAction("BookAppointment",new { id = docLoc.DoctorLocationId});
            }

           

        }
        [HttpGet]
        public ActionResult BookAppointment(int id)
        {
            
            DoctorLocation docLoc = new DoctorLocation();
            List<int> timeSlot = new List<int>();
            docLoc.DoctorLocationId = id;
            docLoc.SelectByDoctorLocationId();
            DoctorModel doc = new DoctorModel();
            doc.DoctorId = docLoc.DoctorId;
            doc.SelectById();
            for (int i = docLoc.StartTime.Hour; i < docLoc.EndTime.Hour; i++)
            {
                
                timeSlot.Add(i);
                
            }
            ViewBag.timeSlot = timeSlot;
            ViewBag.Doctor = doc;
            ViewBag.DoctorLocationId = id;


            return View(docLoc);

        }
        
        public ActionResult AppointmentFinal(DateTime AppDate, string AppTime,int DoctorLocationId)
        {
           
            ViewBag.AppDate = AppDate;
            ViewBag.AppTime = AppTime;
            ViewBag.DoctorLocationId = DoctorLocationId;

            return View();
        }
        [HttpPost]
        public ActionResult AppointmentFinal(FormCollection collection)
        {
            Appointment a = new Appointment();
            a.AppDate = Convert.ToDateTime(collection["AppDate"]);
            a.AppTime = Convert.ToString(collection["AppTime"]);
            try
            {
                
                //a.AppUserId = ((AppUserModel)Session["Appuser"]).AppUserId;
                a.AppUserId = ((AppUserModel)Session["User"]).AppUserId; 
                a.DoctorLocationId = Convert.ToInt32(collection["DoctorLocationId"]);
                
                a.SubmitDate = Convert.ToDateTime(DateTime.Now);                
                a.PatientName = Convert.ToString(collection["PatientName"]);
                a.Phone = Convert.ToString(collection["Phone"]);
                a.Symptoms = Convert.ToString(collection["Symptoms"]);
                a.Address = Convert.ToString(collection["Address"]);
                a.Status = "Pending";
                a.Occupation = Convert.ToString(collection["Occupation"]);
                DateTime DOB = Convert.ToDateTime(collection["DOB"]);
                DateTime today = DateTime.Now;
                a.Age = today.Year - DOB.Year;
                a.Gender = Convert.ToString(collection["Gender"]);
                a.InsertAppointment();
                DoctorLocation docLoc = new DoctorLocation();
                docLoc.DoctorLocationId = a.DoctorLocationId;
                docLoc.SelectByDoctorLocationId();
                DoctorModel doctor = new DoctorModel();
                doctor.DoctorId = docLoc.DoctorId;
                doctor.SelectById();
                
                //SmtpClient smtpClient = new SmtpClient("mail.gmail.com",465);

                //smtpClient.Credentials = new System.Net.NetworkCredential("medicoplus.co@gmail.com", "MedicoAdmin");
                //smtpClient.UseDefaultCredentials = true;
                //smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtpClient.EnableSsl = true;
                //MailMessage mail = new MailMessage("medicoplus.co@gmail.com", "shahkush1995@gmail.com");
                //mail.Subject = "Appointment Booked Sucessfully";
                //mail.Body = "Dear"+a.PatientName+ "\n \tYour appointment for date:"+a.AppDate+"is sucessfully booked in time slot"+a.AppTime+" for Dr:"+doctor.DocName;
                
               

                //smtpClient.Send(mail);
                return RedirectToAction("PendingAppointment");
            }
            catch (Exception E) {
                Console.Write(E);
                bool error = true;
                ViewBag.Error = error;
                return RedirectToAction("AppointmentFinal",new {AppTime = a.AppTime , AppDate = a.AppDate,DoctorLocationId = a.DoctorLocationId });
                

            }
        }
        public ActionResult ManageAppointment()
        {
            Appointment appointment = new Appointment();
            
            appointment.AppUserId = ((AppUserModel)Session["User"]).AppUserId;
            DataTable dt = appointment.SelectByAppUserId();
            DiseaseModel dis = new DiseaseModel();
            DataTable ds = dis.SelectAll();
            ViewBag.Disease = ds;
            
           
            return View(dt);
        }
        public ActionResult PendingAppointment()
        {
            Appointment appointment = new Appointment();
            appointment.AppUserId = ((AppUserModel)Session["User"]).AppUserId;
            DataTable dt = appointment.SelectPendingAppointments();


            return View(dt);
        }
        public ActionResult CancelAppointment(FormCollection collection)
        {
            Appointment appointment = new Appointment();
            appointment.AppointmentId = Convert.ToInt32(collection["AppointmentId"]);
            DataTable dt = appointment.SelectAppointmentById(appointment.AppointmentId);
            appointment.CancelAppointment();
            string msg = "Your Appointment On Date: "+dt.Rows[0]["AppDate"] +" Has been Cancelled by the Patient:"+((AppUserModel)Session["User"]).AUName;
            SendMail(Convert.ToString(dt.Rows[0]["Email"]),msg,"Appointment Cancelled");


            return RedirectToAction("PendingAppointment");
        }
        public ActionResult DoctorList()
        {   
            DoctorModel Doc = new DoctorModel();

            DataTable dt = (new DoctorLocation()).SelectByCityId(1);

            return View(dt);
        }
        public ActionResult Dashbord()
        {
            


            return View();
        }
        public ActionResult DoctorProfile(int id)
        {
            DoctorModel doc = new DoctorModel();
            doc.DoctorId = id;   
            //doc.DoctorId = Convert.ToInt32(collection["DoctorId"]);
            doc.SelectById();
            DoctorLocation docLoc = new DoctorLocation();
            docLoc.DoctorId = doc.DoctorId;
            DataTable dt = docLoc.SelectByDoctorId();
            ViewBag.Location = dt;

            return View(doc);
        }
        

        public ActionResult EditProfile()
        {
            AppUserModel am = new AppUserModel();
            am.AppUserId=((AppUserModel)Session["User"]).AppUserId;
            
            am.SelectAppUserById();
            AreaModel area = new AreaModel();
            ViewBag.area = area.SelectAllArea();
            CityModel city = new CityModel();
            ViewBag.city = city.SelectAll();


            return View(am);

        }
        [HttpPost]
        public ActionResult EditProfile(FormCollection collection)
        {
            AppUserModel A = new AppUserModel();
            A.AppUserId = ((AppUserModel)Session["User"]).AppUserId;
            A.AUName = collection["Username"];
            A.Gender = collection["Gender"];
            A.Email = collection["Email"];
            A.AreaId = Convert.ToInt32(collection["AreaId"]);
            A.Address = collection["Address"];
            A.CityId = Convert.ToInt32(collection["CityId"]);
            A.Password = collection["Password"];
            
            A.Phone = collection["Mobile"];
            A.Mobile = collection["Mobile"];
            if (Request.Files["Photo"] != null)
            {
                string path = "/Photo/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["Photo"].FileName;
                Request.Files["Photo"].SaveAs(Server.MapPath(path));
                A.Photo = path;
            }
           else
            {
                AppUserModel temp = new AppUserModel();
                temp.AppUserId = A.AppUserId;
                temp.SelectAppUserById();
                A.Photo = temp.Photo;
            }
            
            A.UpdateAppUser();


            return RedirectToAction("UserProfile", "AppUser");
        }
        public ActionResult ViewPrescription(int id) {
            Appointment app = new Appointment();
            DataTable dt = app.SelectAppointmentById(id);
            DiseaseModel dis = new DiseaseModel();
            DataTable ds = dis.SelectAll();
            ViewBag.Disease = ds;

            return View(dt);
        }
        public ActionResult Prescriptions()
        {
            Appointment appointment = new Appointment();

            appointment.AppUserId = ((AppUserModel)Session["User"]).AppUserId;
            DataTable dt = appointment.SelectPrescriptionById(appointment.AppUserId);
            
            
            return View(dt);
        }
        public ActionResult MedicalStoreList (int id)
        {
            ViewBag.AppointmentId = id;
            DataTable dt = (new MedicalStoreModel()).SelectAllMedicalStore();
            
            return View(dt);

        }
        [HttpPost]
        public ActionResult ForwardPrescription(FormCollection collection)
        {
            Appointment app = new Appointment();
            app.AppointmentId = Convert.ToInt32(collection["AppId"]);
            app.MedicalStoreId = Convert.ToInt32(collection["StoreId"]);
            app.StoreStatus = "PENDING";
            app.ForwardMedicalStore();
            return RedirectToAction("Prescriptions");

        }
    }
}