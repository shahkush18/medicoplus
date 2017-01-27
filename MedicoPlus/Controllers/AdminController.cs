using System.Web.Mvc;
using MedicoPlus.Models;
using System.Data;
using System;

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
            }else
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
                return RedirectToAction("Login");
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

        public ActionResult CityDetails(int id)
        {
            ZoneModel Z = new ZoneModel();
            Z.CityId = id;
            DataTable dt=Z.SelectAllZoneById();

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
            DataTable dt = A.SelectAllAreaById();

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

    }
}