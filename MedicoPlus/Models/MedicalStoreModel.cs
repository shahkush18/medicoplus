using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            string query = "INSERT INTO MedicalStore(StoreName,Photo,Phone,Email,Address,AreaId,CityId,Status,MinDiscount,Password) VALUES(@StoreName,@Photo,@Phone,@Email,@Address,@AreaId,@CityId,@Status,@MinDiscount,@Password)";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@StoreName", this.StoreName));
            lstParams.Add(new SqlParameter("@Photo", this.Photo));
            lstParams.Add(new SqlParameter("@Phone", this.Phone));
            lstParams.Add(new SqlParameter("@Email", this.Email));
            lstParams.Add(new SqlParameter("@Address", this.Address));
            lstParams.Add(new SqlParameter("@AreaId", this.AreaID));
            lstParams.Add(new SqlParameter("@CityId", this.CityID));
            lstParams.Add(new SqlParameter("@Status", this.Status));
            lstParams.Add(new SqlParameter("@MinDiscount", this.MinDiscount));
            //lstParams.Add(new SqlParameter("@Gps", this.Gps));
            lstParams.Add(new SqlParameter("@Password", this.Password));
            return DataAccess.ModifyData(query, lstParams);



        }
        public DataTable SelectMedicalStoreById()
        {
            string query = "SELECT * FROM MedicalStore WHERE MedicalStoreId = @MedicalStoreId";
            List<SqlParameter> lstparams = new List<SqlParameter>();
            lstparams.Add(new SqlParameter("@MedicalStoreId", this.MedicalStoreID));
            DataTable dt = DataAccess.SelectData(query, lstparams);
            this.MedicalStoreID = Convert.ToInt32(dt.Rows[0]["MedicalStoreId"]);
            this.StoreName = Convert.ToString(dt.Rows[0]["StoreName"]);
            this.Photo = Convert.ToString(dt.Rows[0]["Photo"]);
            this.Phone = Convert.ToString(dt.Rows[0]["Phone"]);
            this.Email = Convert.ToString(dt.Rows[0]["Email"]);
            this.Address = Convert.ToString(dt.Rows[0]["Address"]);
            this.Status = Convert.ToString(dt.Rows[0]["Status"]);
            this.AreaID = Convert.ToInt32(dt.Rows[0]["AreaId"]);
            this.CityID = Convert.ToInt32(dt.Rows[0]["CityId"]);
            this.MinDiscount = Convert.ToInt32(dt.Rows[0]["MinDiscount"]);
            this.Password = Convert.ToString(dt.Rows[0]["Password"]);

            return dt;
        }
        public bool Authenticate() {
            string query = "SELECT * FROM MedicalStore WHERE Email = @Email AND Password = @Password";
            List<SqlParameter> lstparam = new List<SqlParameter>();
            lstparam.Add(new SqlParameter("@Email", this.Email));
            lstparam.Add(new SqlParameter("@Password", this.Password));
            DataTable dt = DataAccess.SelectData(query, lstparam);
            if (dt.Rows.Count > 0)
            {
                this.MedicalStoreID = Convert.ToInt32(dt.Rows[0]["MedicalStoreId"]);
                this.SelectMedicalStoreById();
                return true;

            }
            else
            {

                return false;
            }

        }
        public DataTable SelectAllMedicalStore()
        {
            string query = "SELECT MedicalStore.* , Area.AreaName , City.CityName FROM MedicalStore INNER JOIN Area ON MedicalStore.AreaId = Area.AreaId INNER JOIN City ON MedicalStore.CityId = City.CityId WHERE MedicalStore.Status = @Status";
            List<SqlParameter> lstparam = new List<SqlParameter>();
            lstparam.Add(new SqlParameter("@Status", "APPROVED"));

            return DataAccess.SelectData(query, lstparam);

        }



    }
}