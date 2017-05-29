using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MedicoPlus.Models
{
    public class SpecialityModel
    {
        public int SpecialityId { get; set; }
        public string SpName { get; set; }
        public bool IsActive { get; set; }

        public int InsertSpeciality()
        {
            string query = "INSERT INTO Speciality VALUES (@SpName,@IsActive)";
            List<SqlParameter> lstParam = new List<SqlParameter>();
            lstParam.Add(new SqlParameter("@SpName", this.SpName));
            lstParam.Add(new SqlParameter("@IsActive", this.IsActive));
            return DataAccess.ModifyData(query, lstParam);
        }
         
        public DataTable SelectAll()
        {
            string query = "SELECT * FROM Speciality";
            List<SqlParameter> lstParam = new List<SqlParameter>();
            return DataAccess.SelectData(query, lstParam);
        }
        public DataTable SelectSpecialityById()
        {
            string query = "SELECT * FROM Speciality WHERE SpecialityId=@SpecialityId";
            List<SqlParameter> lstParam = new List<SqlParameter>();
            lstParam.Add(new SqlParameter("@SpecialityId", this.SpecialityId));
            DataTable dt = DataAccess.SelectData(query, lstParam);
            this.SpecialityId = Convert.ToInt32(dt.Rows[0]["SpecialityId"]);
            this.SpName = Convert.ToString(dt.Rows[0]["SpName"]);
            this.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]);



            return dt;
        }
        public int UpdateSpeciality()
        {
            string query = "UPDATE Speciality SET SpName=@SpName WHERE SpecialityId=@SpecialityId";
            List<SqlParameter> lstParam = new List<SqlParameter>();
            lstParam.Add(new SqlParameter("@SpName", this.SpName));
            lstParam.Add(new SqlParameter("@SpecialityId", this.SpecialityId));
            return DataAccess.ModifyData(query, lstParam);
        }
    }
}