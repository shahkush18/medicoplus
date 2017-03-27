using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedicoPlus.Models;
using System.Data;
namespace MedicoPlus.Controllers
{
    public class AppUserController : Controller
    {
        // GET: AppUser
        public ActionResult UserProfile() {
            AppUserModel user = new AppUserModel();
            user.AppUserId = 1;
            user.SelectAppUserById();
            CityModel city = new CityModel();
            city.CityId = user.CityId;
            city.SelectAllCityById();
            ViewBag.City = city;
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
            A.AreaId = Convert.ToInt32(collection["AreaId"]);
            A.Address = collection["Address"];
            A.CityId = Convert.ToInt32(collection["CityId"]);
            A.Password = collection["Password"];
            A.IsActive = false;
            A.Phone = collection["Phone"];
            A.Mobile = collection["Mobile"];



            return RedirectToAction("Index","AdminController");
        }
        public ActionResult Login()
        {
            Session["AppUser"] = null;
            return View();
        }   

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            AppUserModel A = new AppUserModel();
            A.AUName = collection["Username"];
            A.Password = collection["Password"];
            if (A.Authenticate())
            {
                Session["AppUserModel"] = A;
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult BookAppointment(FormCollection collection)
        {
            Appointment a = new Appointment();
            a.AppUserId = ((AppUserModel)Session["Appuser"]).AppUserId;
            a.DoctorLocationId = Convert.ToInt32(collection["DoctorLocationId"]);
            a.SubmitDate = Convert.ToString(DateTime.Now);
            a.AppDate = Convert.ToString(collection["AppDate"]);
            a.PatientName = Convert.ToString(collection["PatientName"]);
            a.Age = Convert.ToInt32(collection["Age"]);



            return RedirectToAction("AppuserController", "Index");

        }

    }
}