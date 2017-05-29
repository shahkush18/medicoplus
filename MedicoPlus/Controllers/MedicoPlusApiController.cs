using MedicoPlus.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedicoPlus.Controllers
{
    public class MedicoPlusApiController : Controller
    {
        // GET: MediPlus
        public ActionResult Index()
        {
            return View();
        }

        public string GetAllCity()
        {
            CityModel cities = new CityModel();
            DataTable dt = cities.SelectAll();
            string JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }
        public string GetAllDoctor()
        {
            DoctorLocation docloc = new DoctorLocation();
            DataTable dt = docloc.SelectByCityId(1);
            string JSONString = JsonConvert.SerializeObject(dt);
            JSONString = "{\"results\" : " + JSONString + " }";
            return JSONString;
        }
        public string GetAllSpeciality()
        {
            SpecialityModel spec = new SpecialityModel();
            DataTable dt = spec.SelectAll();
            string JSONString = JsonConvert.SerializeObject(dt);
            JSONString = "{\"results\" : "+ JSONString +" }";
            return JSONString;
        }
        public string GetAllArea()
        {
            AreaModel spec = new AreaModel();
            DataTable dt = spec.SelectAllArea();
            string JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }
        [HttpPost]
        public string GetYourName() {
            string names = this.Request.QueryString["name"];
            return names;
        }
        [HttpPost]
        public string Login()
        {
            

                AppUserModel U = new AppUserModel();
                U.Email = this.Request.QueryString["Email"];
                U.Password = this.Request.QueryString["Password"];
                
                if (U.AuthenticateAndroid())
                {
                    //Session["Doctor"] = D;
                    //return JsonConvert.SerializeObject(U);
                return "{\"User\" : " + JsonConvert.SerializeObject(U) + " }";
            }

                else
                {
                    return "False";
                }

            

         }
        public string SignUp() {
            AppUserModel app = new AppUserModel();
            app.AUName = this.Request.QueryString["UserName"];
            app.Email = this.Request.QueryString["Email"];
            app.Password = this.Request.QueryString["Password"];
            if(app.InsertAndroidUser() == 1)
            {
                return JsonConvert.SerializeObject(true);
            }
            else
            {
                return JsonConvert.SerializeObject(false);
            }


        }
        public string GetDoctorBySpeciality(int id)
        {
            
            
            
            return "{\"doctors\" : " + JsonConvert.SerializeObject((new DoctorLocation()).SelectBySpecialityId(id)) + " }";
            
        }
        public string GetDoctorProfileById(int id)
        {

            DoctorLocation docloc = new DoctorLocation();
            docloc.DoctorLocationId = id;

            return "{\"doctorprofile\" : " + JsonConvert.SerializeObject(docloc.SelectDoctorByLocationId()) + " }";

        }
        public string UserProfileById(int id)
        {

            AppUserModel docloc = new AppUserModel();
            docloc.AppUserId = id;

            return "{\"userprofile\" : " + JsonConvert.SerializeObject(docloc.SelectAndroidAppUserById()) + " }";

        }
        public string UserPendingAppointments(int id)
        {
            Appointment app = new Appointment();
            app.AppUserId = id;
            return "{\"pendingappointments\" : " + JsonConvert.SerializeObject(app.SelectByAppUserId()) + " }";
        }
        public string MedicalStoreList()
        {
            MedicalStoreModel med = new MedicalStoreModel();
            
            return "{\"medicalstores\" : " + JsonConvert.SerializeObject(med.SelectAllMedicalStore()) + " }";
        }
        public string MedicalStoreProfile(int id)
        {
            MedicalStoreModel med = new MedicalStoreModel();
            med.MedicalStoreID = id;

            return "{\"medicalstoreprofile\" : " + JsonConvert.SerializeObject(med.SelectMedicalStoreById()) + " }";
        }

    }   

    }
