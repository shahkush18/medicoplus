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
    }
}