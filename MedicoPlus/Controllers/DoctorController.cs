using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedicoPlus.Models;
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

    }
}