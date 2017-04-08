using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedicoPlus.Models;
using System.Data;
using ClosedXML.Excel;
using System.IO;

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
            D.SpecialityId = Convert.ToInt32(collection["Speciality"]);
            D.Qualification = collection["Qualification"];
            D.Documents = collection["Documents"];
            D.MinConsultationFee = Convert.ToInt32(collection["mcf"]);
            D.Documents= collection["Documents"];
            D.MaxApptPerHour= Convert.ToInt32(collection["maph"]);
            
            D.Password= collection["Password"];
            
            D.InsertDoctor();

            return RedirectToAction("Login","Access");
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
            D.UserName = collection["Username"];
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
        public ActionResult ExportData()
        {
            DataTable disease = new DiseaseModel().SelectAll();
            Report rep = new Report();
            DataTable dt = rep.SelectAllReport(1);
            


            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= EmployeeReport.xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
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
            docLoc.CityId = 1;
            docLoc.AreaId = 1;
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




    }
}