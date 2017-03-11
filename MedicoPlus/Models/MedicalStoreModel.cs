using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicoPlus.Models
{
    public class MedicalStoreModel
    {
        public int MedicalStoreID { get; set; }
        public string StoreName { get; set; }
        public string Photo { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int AreaID { get; set; }
        public int CityID { get; set; }
        public string Status { get; set; }
        public int MinDiscount { get; set; }
        public string Gps { get; set; }
        public string Password { get; set; }

        public int InsertMedicalStore()
        {
            return 0;

        }

       
    }
}