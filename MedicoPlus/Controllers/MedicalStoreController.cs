using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedicoPlus.Models;
using System.Net.Mail;
using System.Data;

namespace MedicoPlus.Controllers
{
    public class MedicalStoreController : Controller
    {
        // GET: MedicalStore
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Signup()
        {
            AreaModel area = new AreaModel();
            ViewBag.Area = area.SelectAllArea();
            ViewBag.City = (new CityModel()).SelectAll();
            return View();
        }
        [HttpPost]
        public ActionResult Signup(FormCollection collection) {
            MedicalStoreModel med = new MedicalStoreModel();
            med.StoreName = collection["StoreName"];
            if (Request.Files["Photo"] != null)
            {
                string path = "/Photo/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["Photo"].FileName;
                Request.Files["Photo"].SaveAs(Server.MapPath(path));
                med.Photo = path;
            }
            else
            {
                med.Photo = "";
            }

            med.Phone = collection["Mobile"];
            med.Email = collection["Email"];
            med.Address = collection["Address"];
            med.AreaID = Convert.ToInt32(collection["AreaId"]);
            med.CityID = Convert.ToInt32(collection["CityId"]);
            med.MinDiscount = Convert.ToInt32(collection["MinDiscount"]);
            med.Password = collection["Password"];
            med.Status = "APPROVED";
            med.InsertMedicalStore();
            SendMail(med.Email, "You have been successfully registered with MedicoPlus", "Thanks For Registering With Us !");

            return RedirectToAction("LoginStore", "Access");


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

        public ActionResult MedicalStoreProfile() {
            MedicalStoreModel med = new MedicalStoreModel();
            med.MedicalStoreID = ((MedicalStoreModel)Session["Store"]).MedicalStoreID;
            med.SelectMedicalStoreById();

            return View(med);

        }
        public ActionResult NewOrders()
        {
            Appointment app = new Appointment();
            app.MedicalStoreId = ((MedicalStoreModel)Session["Store"]).MedicalStoreID;
            app.StoreStatus = "PENDING";
            DataTable dt = app.SelectPrescriptionsByStoreId();
            return View(dt);
        }
        public ActionResult Accept(int id) {
            Appointment app = new Appointment();
            app.StoreStatus = "ACCEPTED";
            app.AppointmentId = id;
            app.UpdateStoreStatus();
            return RedirectToAction("AcceptedOrders");
        }
        public ActionResult Reject(int id)
        {
            Appointment app = new Appointment();
            app.StoreStatus = "REJECTED";
            app.AppointmentId = id;
            app.UpdateStoreStatus();
            return RedirectToAction("NewOrders");
        }
        public ActionResult ViewPrescription(int id)
        {
            Appointment app = new Appointment();
            DataTable dt = app.SelectAppointmentById(id);
            DiseaseModel dis = new DiseaseModel();
            DataTable ds = dis.SelectAll();
            ViewBag.Disease = ds;

            return View(dt);
        }
        public ActionResult AcceptedOrders()
        {
            Appointment app = new Appointment();
            app.MedicalStoreId = ((MedicalStoreModel)Session["Store"]).MedicalStoreID;
            app.StoreStatus = "ACCEPTED";
            DataTable dt = app.SelectPrescriptionsByStoreId();
            return View(dt);
        }
        public ActionResult CompleteOrder(int id)
        {
            Appointment app = new Appointment();
            app.StoreStatus = "READY";
            app.AppointmentId = id;
            app.UpdateStoreStatus();
            return RedirectToAction("CompletedOrders");

        }

        public ActionResult CompletedOrders()
        {
            Appointment app = new Appointment();
            app.MedicalStoreId = ((MedicalStoreModel)Session["Store"]).MedicalStoreID;
            app.StoreStatus = "READY";
            DataTable dt = app.SelectPrescriptionsByStoreId();
            return View(dt);
        }
        public ActionResult PastOrder(int id)
        {
            Appointment app = new Appointment();
            app.StoreStatus = "COMPLETED";
            app.AppointmentId = id;
            app.UpdateStoreStatus();
            return RedirectToAction("PastOrders");

        }
        public ActionResult PastOrders()
        {
            Appointment app = new Appointment();
            app.MedicalStoreId = ((MedicalStoreModel)Session["Store"]).MedicalStoreID;
            app.StoreStatus = "COMPLETED";
            DataTable dt = app.SelectPrescriptionsByStoreId();
            return View(dt);
        }

    }
}