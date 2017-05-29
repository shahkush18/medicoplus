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
        public ActionResult LoginUser()
        {
            Session["Admin"] = null;
            Session["Doctor"] = null;
            Session["User"] = null;
            return View();
        }
        [HttpPost]
        public ActionResult AuthenticateUser(FormCollection collection)

        {
            AppUserModel U = new AppUserModel();
            U.Email = collection["Email"];
            U.Password = collection["Password"];
            if (U.Authenticate())
            {
                Session["User"] = U;
                return RedirectToAction("Dashbord", "AppUser");
            }

            else
            {
                return RedirectToAction("LoginUser", "Access");
            }



        }
        public ActionResult LoginDoctor()
        {
            Session["Admin"] = null;
            Session["Doctor"] = null;
            Session["User"] = null;
            return View();
        }
        public ActionResult AuthenticateDoctor(FormCollection collection)

        {
            
            DoctorModel D = new DoctorModel();
            D.Email = collection["Email"];
            D.Password = collection["Password"];
           
           if (D.Authenticate())
            {
                Session["Doctor"] = D;
                return RedirectToAction("DoctorProfile", "Doctor");
            }
            

            else
            {
                return RedirectToAction("LoginDoctor", "Access");
            }



        }
        public ActionResult LoginStore()
        {
            Session["Admin"] = null;
            Session["Doctor"] = null;
            Session["User"] = null;
            return View();
        }
        [HttpPost]
        public ActionResult AuthenticateStore(FormCollection collection)

        {

            MedicalStoreModel D = new MedicalStoreModel();
            D.Email = collection["Email"];
            D.Password = collection["Password"];

            if (D.Authenticate())
            {
                Session["Store"] = D;
                return RedirectToAction("MedicalStoreProfile", "MedicalStore");
            }


            else
            {
                return RedirectToAction("LoginStore", "Access");
            }



        }
    }
}