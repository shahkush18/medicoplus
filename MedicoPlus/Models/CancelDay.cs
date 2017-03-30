using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MedicoPlus.Models
{
    public class CancelDay
    {
        public int CancelDayId { get; set; }
        public int DoctorLocationId { get; set; }
        public string CancelDate { get; set; }


        public DataTable SelectByDoctorLocationId()
        {
            string query = "Select * FROM CancelDay WHERE DoctorLocationId=@DoctorLocationId";
            List<SqlParameter> lstparam = new List<SqlParameter>();
            lstparam.Add(new SqlParameter("@DoctorLocationId", this.DoctorLocationId));

            DataTable dt = DataAccess.SelectData(query, lstparam);
            
            return dt;
        }
        public int InsertCancelDay() {
            string query = "Insert into CancelDay(DoctorLocationId , CancelDate) VALUES(@DoctorLocationId , @CancelDate)";
            List<SqlParameter> lstparam = new List<SqlParameter>();
            lstparam.Add(new SqlParameter("@DoctorLocationId", this.DoctorLocationId));
            lstparam.Add(new SqlParameter("@CancelDate", this.CancelDate));

            return DataAccess.ModifyData(query, lstparam);

        }

    }
}