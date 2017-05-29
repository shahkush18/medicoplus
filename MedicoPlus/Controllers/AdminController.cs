using System.Web.Mvc;
using MedicoPlus.Models;
using System.Data;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MedicoPlus.Controllers
{
    public class AdminController : Controller
    {
        //Admin Verification
        /*protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["Admin"] == null)
            {
                filterContext.Result = RedirectToAction("LogIn", "Admin");
            }
            else
            {
                base.OnActionExecuting(filterContext);
            }
        }*/
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["Admin"] != null)
            {
                return View();
            } else
            {
                return RedirectToAction("LogIn");
            }
        }
        //Admin
        public ActionResult SignUp()
        {
            Session["Admin"] = null;
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(FormCollection collection)
        {
            Admin A = new Models.Admin();
            A.AdminUName = collection["Username"];
            A.AdminPassword = collection["Password"];
            A.IsActive = (collection["IsActive"] == "true");
            A.Insert();

            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            Session["Admin"] = null;
            return View();
        }

        [HttpPost]
        public ActionResult AuthenticateAdmin(FormCollection collection)
        {
            Admin A = new Admin();
            A.AdminUName = collection["Username"];
            A.AdminPassword = collection["Password"];
            if (A.Authenticate())
            {
                Session["Admin"] = A;
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return RedirectToAction("Login", "Access");
            }
        }


        //CITY
        public ActionResult CreateCity()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCity(FormCollection collection)

        {
            CityModel C = new CityModel();
            C.CityName = collection["CName"];
            C.IsActive = (collection["IsActive"] == "true");
            C.InsertCity();
            return RedirectToAction("GetAllCity");
        }


        public ActionResult GetAllCity()
        {
            CityModel C = new CityModel();
            DataTable dt = C.SelectAll();
            return View(dt);
        }
        public JsonResult GetCity()
        {
            CityModel C = new CityModel();
            DataTable dt = C.SelectAll();
            List<CityModel> olist = new List<CityModel>();
            foreach (DataRow row in dt.Rows)
            {
                olist.Add(new CityModel { CityId = Convert.ToInt16(row["CityId"]), CityName = Convert.ToString(row["CityName"]), IsActive = Convert.ToBoolean(row["IsActive"]) });
            }
            return Json(olist, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CityDetails(int id)
        {
            ZoneModel Z = new ZoneModel();
            Z.CityId = id;
            DataTable dt = Z.SelectAllZoneById();

            CityModel C = new CityModel();
            C.CityId = id;
            C.SelectAllCityById();
            ViewBag.CityModel = C;
            return View(dt);
        }



        //Zone
        public ActionResult CreateZone()
        {
            CityModel C = new CityModel();
            DataTable dt = C.SelectAll();
            return View(dt);
        }
        [HttpPost]
        public ActionResult CreateZone(FormCollection collection)
        {
            ZoneModel Z = new ZoneModel();
            Z.CityId = Convert.ToInt32(collection["CityId"]);
            Z.ZoneName = collection["ZName"];
            Z.IsActive = (collection["IsActive"] == "true");
            Z.InsertZone();
            return RedirectToAction("GetAllZone");
        }
        public ActionResult GetAllZone()
        {
            ZoneModel Z = new ZoneModel();
            DataTable dt = Z.SelectAllZone();
            return View(dt);
        }

        public ActionResult ZoneDetails(int id)
        {
            AreaModel A = new AreaModel();
            A.ZoneId = id;
            DataTable dt = A.SelectAllAreaByZoneId();

            ZoneModel Z = new ZoneModel();
            Z.ZoneId = id;
            Z.SelectAllZoneById();
            ViewBag.ZoneModel = Z;
            return View(dt);
        }

        //Area
        public ActionResult CreateArea()
        {
            ZoneModel Z = new ZoneModel();
            DataTable dt = Z.SelectAllZone();
            return View(dt);
        }
        [HttpPost]
        public ActionResult CreateArea(FormCollection collection)
        {
            AreaModel A = new AreaModel();
            A.AreaName = collection["AName"];
            A.IsActive = (collection["IsActive"] == "true");
            A.ZoneId = Convert.ToInt32(collection["ZoneId"]);
            A.InsertArea();
            return RedirectToAction("GetAllArea");
        }

        public ActionResult GetAllArea()
        {
            AreaModel A = new Models.AreaModel();
            DataTable dt = A.SelectAllArea();
            return View(dt);
        }


        //Speciality

        public ActionResult CreateSpeciality()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateSpeciality(FormCollection collection)
        {
            SpecialityModel S = new SpecialityModel();
            S.SpName = collection["SpName"];
            S.IsActive = (collection["IsActive"] == "true");
            S.InsertSpeciality();
            return RedirectToAction("GetAllSpeciality");
        }
        public ActionResult GetAllSpeciality()
        {
            SpecialityModel S = new SpecialityModel();
            DataTable dt = S.SelectAll();
            return View(dt);
        }

        //Disease

        public ActionResult CreateDisease()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateDisease(FormCollection collection)
        {
            DiseaseModel D = new DiseaseModel();
            D.DiseaseName = collection["DiseaseName"];
            D.IsActive = (collection["IsActive"] == "true");
            D.InsertDisease();
            return RedirectToAction("GetAllDisease");
        }
        public ActionResult GetAllDisease()
        {
            DiseaseModel D = new DiseaseModel();
            DataTable dt = D.SelectAll();
            return View(dt);
        }
        public ActionResult GenerateReports()
        {
            Report rep = new Report();
            DataTable dt = rep.GenerateReport();

            DiseaseModel disease = new DiseaseModel();
            DataTable dt1 = disease.SelectAll();
            dt1.Rows.Add(0, "ALL");
            //dt1.DefaultView.Sort = "DiseaseId";
            ViewBag.Disease = dt1;

            AreaModel Area = new AreaModel();
            DataTable dt2 = Area.SelectAllArea();
            dt2.Rows.Add(0, "ALL");
            dt2.DefaultView.Sort = "AreaId";
            ViewBag.Area = dt2;

            DataTable dt3 = (new CityModel()).SelectAll();
            dt3.Rows.Add(0, "ALL");
            dt3.DefaultView.Sort = "CityId";
            ViewBag.City = dt3;
            DataTable dt4 = (new DoctorLocation()).SelectByCityId(1);
            ViewBag.Clinic = dt4;
            return View(dt);
        }
        [HttpPost]
        public ActionResult GenerateReports(FormCollection collection)
        {
            DiseaseModel disease = new DiseaseModel();
            DataTable dt1 = disease.SelectAll();
            dt1.Rows.Add(0, "ALL");
            dt1.DefaultView.Sort = "DiseaseId";
            ViewBag.Disease = dt1;

            AreaModel Area = new AreaModel();
            DataTable dt2 = Area.SelectAllArea();
            dt2.Rows.Add(0, "ALL");
            dt2.DefaultView.Sort = "AreaId";
            ViewBag.Area = dt2;

            DataTable dt3 = (new CityModel()).SelectAll();
            dt3.Rows.Add(0, "ALL");
            dt3.DefaultView.Sort = "CityId";
            ViewBag.City = dt3;
            DataTable dt4 = (new DoctorModel()).SelectAllDoctor();
            ViewBag.Clinic = dt4;
            Report rep = new Report();
            DateTime to;
            DateTime from;
            DataTable dt;
            try
            {
                to = Convert.ToDateTime(collection["TO"]);
                from = Convert.ToDateTime(collection["From"]);
                dt = rep.GenerateFilteredReport(Convert.ToInt32(collection["AreaId"]), Convert.ToInt32(collection["CityId"]), Convert.ToInt32(collection["DiseaseId"]), Convert.ToDateTime(collection["TO"]), Convert.ToDateTime(collection["From"]), 1, Convert.ToInt32(collection["DoctorId"]));
            }
            catch (Exception e) {
                Console.WriteLine(e);
                to = DateTime.Now;
                from = DateTime.Now;
                dt = rep.GenerateFilteredReport(Convert.ToInt32(collection["AreaId"]), Convert.ToInt32(collection["CityId"]), Convert.ToInt32(collection["DiseaseId"]), Convert.ToDateTime(collection["TO"]), Convert.ToDateTime(collection["From"]), 0, Convert.ToInt32(collection["DoctorId"]));
            }


            return View(dt);
        }
        public ActionResult FormP() {
            DiseaseModel disease = new DiseaseModel();
            DataTable dt1 = disease.SelectAll();

            //dt1.DefaultView.Sort = "DiseaseId";
            ViewBag.Disease = dt1;

            Report rep = new Report();
            DataTable dt = rep.GenerateReport();

            return View(dt);

        }
        public ActionResult VerifyDoctor()
        {


            DoctorModel doc = new DoctorModel();
            doc.Status = "PENDING";
            DataTable dt = doc.SelectDoctorByStatus();


            return View(dt);

        }
        [HttpPost]
        public ActionResult UpdateDoctor(FormCollection collection)
        {


            DoctorModel doc = new DoctorModel();
            doc.DoctorId = Convert.ToInt32(collection["DoctorId"]);
            doc.Status = Convert.ToString(collection["Status"]);
            doc.IsActive = true;
            doc.UpdateStatus();



            return RedirectToAction("VerifyDoctor");

        }
        public ActionResult DoctorList() {
            DoctorModel Doc = new DoctorModel();

            DataTable dt = Doc.SelectAllDoctor();


            return View(dt);


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
        [HttpGet]
        public ActionResult EditCity(int id)
        {
            CityModel obj = new CityModel();
            obj.CityId = id;
            obj.SelectAllCityById();
            return View(obj);

        }
        [HttpPost]
        public ActionResult EditCity(FormCollection collection)
        {
            CityModel obj = new CityModel();
            obj.CityId = Convert.ToInt32(collection["CityId"]);
            obj.CityName = Convert.ToString(collection["CityName"]);
            obj.UpdateCity();
            return RedirectToAction("");

        }
        public ActionResult EditArea(int id)
        {
            AreaModel obj = new AreaModel();
            obj.AreaId = id;
            obj.SelectAllAreaById();
            ZoneModel zones = new ZoneModel();
            DataTable dt = zones.SelectAllZone();
            ViewBag.zones = dt;
            return View(obj);

        }
        [HttpPost]
        public ActionResult EditArea(FormCollection collection)
        {
            AreaModel obj = new AreaModel();
            obj.AreaId = Convert.ToInt32(collection["AreaId"]);
            obj.AreaName = Convert.ToString(collection["AreaName"]);
            obj.ZoneId = Convert.ToInt32(collection["ZoneId"]);
            obj.IsActive = Convert.ToBoolean(collection["IsActive"]);
            obj.UpdateArea();
            return RedirectToAction("");

        }
        public ActionResult EditSpeciality(int id)
        {
            SpecialityModel obj = new SpecialityModel();
            obj.SpecialityId = id;
            obj.SelectSpecialityById();
            
            
            return View(obj);

        }
        [HttpPost]
        public ActionResult EditSpeciality(FormCollection collection)
        {
            SpecialityModel obj = new SpecialityModel();
            obj.SpecialityId = Convert.ToInt32(collection["SpecialityId"]);
            obj.SpName = Convert.ToString(collection["SpName"]);
            
            obj.IsActive = Convert.ToBoolean(collection["IsActive"]);
            obj.UpdateSpeciality();
            return RedirectToAction("");

        }
        public ActionResult EditDisease(int id)
        {
            DiseaseModel obj = new DiseaseModel();
            obj.DiseaseId = id;
            obj.SelectDiseaseById();


            return View(obj);

        }
        [HttpPost]
        public ActionResult EditDisease(FormCollection collection)
        {
            DiseaseModel obj = new DiseaseModel();
            obj.DiseaseId = Convert.ToInt32(collection["DiseaseId"]);
            obj.DiseaseName = Convert.ToString(collection["DiseaseName"]);

            obj.IsActive = Convert.ToBoolean(collection["IsActive"]);
            obj.UpdateDisease();
            return RedirectToAction("");

        }
        public ActionResult EditZone(int id)
        {
            ZoneModel obj = new ZoneModel();
            obj.ZoneId = id;
            obj.SelectZoneById();
            
            return View(obj);

        }
        [HttpPost]
        public ActionResult EditZone(FormCollection collection)
        {
            ZoneModel obj = new ZoneModel();
            obj.ZoneId = Convert.ToInt32(collection["ZoneId"]);
            obj.ZoneName = Convert.ToString(collection["ZoneName"]);
            obj.UpdateZone();
            return RedirectToAction("GetAllZone");

        }
    }
}