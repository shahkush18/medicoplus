using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
namespace MedicoPlus.Models
{
    public class DiseaseModel
    {
        public int DiseaseId { get; set; }
        public string DiseaseName { get; set; }
        public bool IsActive { get; set; }

        public int InsertDisease()
        {
            string query = "INSERT INTO Disease VALUES (@DiseaseName,@IsActive)";
            List<SqlParameter> lstParam = new List<SqlParameter>();
            lstParam.Add(new SqlParameter("@DiseaseName", this.DiseaseName));
            lstParam.Add(new SqlParameter("@IsActive", this.IsActive));
            return DataAccess.ModifyData(query, lstParam);
        }
        public DataTable SelectAll()
        {
            string query = "SELECT * FROM Disease";
            List<SqlParameter> lstParam = new List<SqlParameter>();
            return DataAccess.SelectData(query, lstParam);
        }

    }
}