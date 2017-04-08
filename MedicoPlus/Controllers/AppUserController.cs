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
            AppUserModel A = new Models.AppUserModel();
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


            return RedirectToAction("Login","Access");
        }
           

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            AppUserModel A = new AppUserModel();
            A.AUName = collection["Username"];
            A.Password = collection["Password"];
            if (A.Authenticate())
            {
                Session[""] = A;
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        [HttpPost]
        public ActionResult BookAppointment(FormCollection collection)
        {
            DoctorLocation docLoc = new DoctorLocation();

            //docLoc.DoctorLocationId = Convert.ToInt32(collection["DoctorLocationId"]);
            docLoc.DoctorLocationId = 1;
            docLoc.SelectByDoctorLocationId();
            DoctorModel doctor = new DoctorModel();
            doctor.DoctorId = docLoc.DoctorId;
            doctor.SelectById();
           
            Appointment aCheck = new Appointment();
            aCheck.AppDate = Convert.ToDateTime(collection["AppDate"]); 
            aCheck.AppTime = Convert.ToString(collection["AppTime"]);
            aCheck.DoctorLocationId = 1;
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
                //Appointment a = new Appointment();
                //a.AppUserId = ((AppUserModel)Session["Appuser"]).AppUserId;
                //a.DoctorLocationId = Convert.ToInt32(collection["DoctorLocationId"]);
                //a.SubmitDate = Convert.ToDateTime(DateTime.Now);
                //a.AppDate = Convert.ToDateTime(collection["AppDate"]);
                //a.AppTime = Convert.ToString(collection["AppTime"]);
                //a.PatientName = Convert.ToString(collection["PatientName"]);
                //a.Age = Convert.ToInt32(collection["Age"]);
                //a.InsertAppointment();

                //Appointment a = new Appointment();
                //a.AppUserId = 1;
                //a.DoctorLocationId = 1;
                //a.SubmitDate = Convert.ToDateTime(DateTime.Now);
                //a.AppDate = Convert.ToDateTime(collection["AppDate"]);
                //a.AppTime = Convert.ToString(collection["AppTime"]);
                //a.Phone = "012345678";
                //a.PatientName = "Kush";
                //a.Address = "A/103 bhagyalaxmi Society Vadodara";
                //a.Age = 21;
                //a.Status = "Pending";
                //a.InsertAppointment();
                return RedirectToAction("AppointmentFinal",new {AppTime = aCheck.AppTime , AppDate = aCheck.AppDate });
            }
            else
            {
                ViewBag.error_found = "Appointment not available Try Again";
                return RedirectToAction("BookAppointment");
            }

           

        }
        [HttpGet]
        public ActionResult BookAppointment()
        {
            
            DoctorLocation docLoc = new DoctorLocation();
            List<int> timeSlot = new List<int>();
            docLoc.DoctorLocationId = 1;
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


            return View(docLoc);

        }
        
        public ActionResult AppointmentFinal(DateTime AppDate, string AppTime)
        {
           
            ViewBag.AppDate = AppDate;
            ViewBag.AppTime = AppTime;


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
                //a.DoctorLocationId = Convert.ToInt32(collection["DoctorLocationId"]);
                a.DoctorLocationId = 1;
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
                return RedirectToAction("ManageAppointment");
            }
            catch (Exception E) {
                Console.Write(E);
                bool error = true;
                ViewBag.Error = error;
                return RedirectToAction("AppointmentFinal",new {AppTime = a.AppTime , AppDate = a.AppDate });
                

            }
        }
        public ActionResult ManageAppointment()
        {
            Appointment appointment = new Appointment();
            
            appointment.AppUserId = ((AppUserModel)Session["User"]).AppUserId;
            DataTable dt = appointment.SelectByAppUserId();
            
           
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
            appointment.CancelAppointment();
           


            return RedirectToAction("PendingAppointment");
        }
        public ActionResult DoctorList()
        {
            DoctorModel Doc = new DoctorModel();
            
            DataTable dt = Doc.SelectAllDoctor();


            return View(dt);
        }
        public ActionResult Dashbord()
        {
            


            return View();
        }



    }
}