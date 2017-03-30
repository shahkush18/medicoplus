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

        public ActionResult CreateDoctor()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateDoctor(FormCollection collection)
        {
            DoctorModel D = new DoctorModel();
            D.DocName = collection["DocName"];
            D.UserName = collection["UserName"];
            D.Intro = collection["Intro"];
            D.Photo = collection["Photo"];
            D.Gender = collection["Gender"];
            D.Mobile = collection["Mobile"];
            D.Email = collection["Email"];
            D.DOB = collection["DOB"];
            D.DOJ = collection["DOJ"];
            D.Status = collection["Status"];
            D.SpecialityId = Convert.ToInt16(collection["Speciality"]);
            D.Qualification = collection["Qualification"];
            D.MinConsultationFee = Convert.ToInt16(collection["MinConsultationFee "]);
             D.Documents= collection["Documents"];
            D.MaxApptPerHour= Convert.ToInt16(collection["MaxApptPerHour"]);
           // D.UserName= collection[""]);
            D.Password= collection["Password"];
            D.IsActive = (collection["IsActive"] == "true");
            D.InsertDoctor();

            return View("DoctorProfile");

        }

        public ActionResult DoctorProfile()
        {
            return View();
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

            return View(dt);

           

        }
        [HttpPost]
        public ActionResult AddReport(FormCollection collection)
        {
            Report rep = new Report();
            rep.AreaId = 1;
            rep.CityId = 1;
            DoctorModel d = (DoctorModel)Session["Doctor"];
            rep.DoctorId = 1;
            rep.DiseaseId = Convert.ToInt32(collection["DiseaseId"]);
            rep.PatientName = Convert.ToString(collection["PatientName"]);
            rep.PatientAddress = Convert.ToString(collection["PatientAddress"]);
            DiseaseModel dis = new DiseaseModel();
            dis.DiseaseId = Convert.ToInt32(collection["DiseaseId"]);
            DiseaseModel dis2 = dis.SelectDiseaseById();
            rep.DiseaseName = dis2.DiseaseName;

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
            dr.DoctorId = 1;
            
            return View();
        }
        [HttpPost]
        public ActionResult AddLocation(FormCollection collection)
        {
            DoctorLocation docLoc = new DoctorLocation();
            docLoc.DoctorId = 1;
            docLoc.StartTime = Convert.ToDateTime(collection["StartTime"]);
            docLoc.EndTime= Convert.ToDateTime(collection["EndTime"]);
            docLoc.DaysOfWork = Convert.ToString(collection["DaysOfWork"]);
            docLoc.Address = Convert.ToString(collection["Address"]);
            docLoc.CityId = Convert.ToInt32(collection["CityId"]);
            docLoc.AreaId = Convert.ToInt32(collection["AreaId"]);
            docLoc.UserName = Convert.ToString(collection["Address"]);
            docLoc.Password = Convert.ToString(collection["Password"]);
            docLoc.Affilation = Convert.ToString(collection["Affiliation"]);
            docLoc.Phone = Convert.ToString(collection["Phone"]);

            docLoc.InsertDoctorLocation();
            return RedirectToAction("AddLocation");
        }




    }
}