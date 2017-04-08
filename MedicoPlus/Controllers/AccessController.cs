using MedicoPlus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedicoPlus.Controllers
{
    public class AccessController : Controller
    {
        // GET: Access
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            Session["Admin"] = null;
            Session["Doctor"] = null;
            return View();
        }
        [HttpPost]
        public ActionResult Authenticate(FormCollection collection)

        {
            Admin A = new Admin();
            A.AdminUName = collection["Username"];
            A.AdminPassword = collection["Password"];
            DoctorModel D = new DoctorModel();
            D.UserName = collection["Username"];
            D.Password = collection["Password"];
            AppUserModel U = new AppUserModel();
            U.AUName = collection["Username"];
            U.Password = collection["Password"];
            if (A.Authenticate())
            {
                Session["Admin"] = A;
                return RedirectToAction("Index", "Admin");
            }
            else if (D.Authenticate())
            {
                Session["Doctor"] = D;
                return RedirectToAction("DoctorProfile", "Doctor");
            }
            else if (U.Authenticate())
            {
                Session["User"] = U;
                return RedirectToAction("Dashbord", "AppUser");
            }

            else
            {
                return RedirectToAction("Login", "Access");
            }



        }
    }
}