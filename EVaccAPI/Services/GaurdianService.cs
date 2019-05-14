using EVaccAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EVaccAPI.Services
{
    public class GuardianService
    {
        DbService dbService;
        public GuardianService()
        {
            dbService = DbService.GetDbService();
        }

        public InfantDetailsResponse GetInfantDetails(string birthId)
        {
            InfantDetailsResponse infantDetails = null;
            try
            {
                var query = string.Format(@"select BirthId, FullName, DOB, POB, MothersName, a.InfantID, b.NextVaccinationDate from InfantDetails a left join vaccinationDetails b on a.infantId = b.InfantId
                        where BirthId ='{0}' AND b.VaccinationId = (SELECT MAX(VaccinationId) FROM vaccinationDetails where InfantID =  a.InfantID)", birthId);
                var infantData = dbService.ExecuteReader(query);
                if (infantData.Rows.Count > 0)
                {
                    infantDetails = new InfantDetailsResponse();
                    infantDetails.BirthId = Convert.ToString(infantData.Rows[0]["BirthId"]);
                    infantDetails.FullName = Convert.ToString(infantData.Rows[0]["FullName"]);
                    infantDetails.DOB = Convert.ToString(infantData.Rows[0]["DOB"]);
                    infantDetails.POB = Convert.ToString(infantData.Rows[0]["POB"]);
                    infantDetails.MothersName = Convert.ToString(infantData.Rows[0]["MothersName"]);
                    infantDetails.InfantId = Convert.ToInt32(infantData.Rows[0]["InfantID"]);
                    infantDetails.NextVaccinationDate = Convert.ToString(infantData.Rows[0]["NextVaccinationDate"]);
                }
            }
            catch { }
            return infantDetails;
        }

        public InfantDetailsResponse AddInfant(InfantRequest infData)
        {
            try
            {
                var query = string.Format("update infantDetails set GuardianId = {0} where InfantId={1}", infData.GuardianId,
                    infData.InfantId);
                dbService.ExecuteNonQuery(query);
                return GetInfantDetailsForAdd(infData.InfantId);
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<InfantDetailsResponse> GetInfantsByGuardian(int guardianId)
        {
            List<InfantDetailsResponse> infantList = new List<InfantDetailsResponse>();
            var query = string.Format(@"select BirthId, FullName, DOB, POB, MothersName, a.InfantID, b.NextVaccinationDate from InfantDetails a left join vaccinationDetails b on a.infantId = b.InfantId
                        where GuardianId ='{0}' AND b.VaccinationId = (SELECT MAX(VaccinationId)  FROM vaccinationDetails where InfantID =  a.InfantID)", guardianId);
            var infantDataList = dbService.ExecuteReader(query);
            foreach(DataRow row in infantDataList.Rows)
            {
                var infantData = new InfantDetailsResponse();
                infantData.BirthId = Convert.ToString(row["BirthId"]);
                infantData.FullName = Convert.ToString(row["FullName"]);
                infantData.DOB = Convert.ToString(row["DOB"]);
                infantData.POB = Convert.ToString(row["POB"]);
                infantData.MothersName = Convert.ToString(row["MothersName"]);
                infantData.InfantId = Convert.ToInt32(row["InfantID"]);
                infantData.NextVaccinationDate = Convert.ToString(row["NextVaccinationDate"]);
                infantList.Add(infantData);
            }

            return infantList;
        }
        
        public IEnumerable<string> GetVaccineNamesforNotification(int guardianId)
        {
            List<string> VaccineNames = new List<string>();
            var query = string.Format(@"SELECT Vaccine,VaccStartOn,VaccEndOn FROM ListOfVaccinationDue WHERE GuardianId = {0}", guardianId);
            var DueVaccineList = dbService.ExecuteReader(query);
            foreach (DataRow row in DueVaccineList.Rows)
            {
                if(DateTime.Now >= Convert.ToDateTime(row["VaccStartOn"]) && DateTime.Now <= Convert.ToDateTime(row["VaccEndOn"]))
                {
                    VaccineNames.Add(Convert.ToString(row["Vaccine"]));
                }
            }
            return VaccineNames;
        }

        private InfantDetailsResponse GetInfantDetailsForAdd(int infantId)
        {
            InfantDetailsResponse infantDetails = null;
            try
            {
                var query = string.Format(@"select BirthId, FullName, DOB, POB, MothersName, a.InfantID, b.NextVaccinationDate from InfantDetails a left join vaccinationDetails b on a.infantId = b.InfantId
                        where  a.InfantID = {0} AND b.VaccinationId = (SELECT MAX(VaccinationId)  FROM vaccinationDetails where InfantID =  a.InfantID)", infantId);
                var infantData = dbService.ExecuteReader(query);
                if (infantData.Rows.Count > 0)
                {
                    infantDetails = new InfantDetailsResponse();
                    infantDetails.BirthId = Convert.ToString(infantData.Rows[0]["BirthId"]);
                    infantDetails.FullName = Convert.ToString(infantData.Rows[0]["FullName"]);
                    infantDetails.DOB = Convert.ToString(infantData.Rows[0]["DOB"]);
                    infantDetails.POB = Convert.ToString(infantData.Rows[0]["POB"]);
                    infantDetails.MothersName = Convert.ToString(infantData.Rows[0]["MothersName"]);
                    infantDetails.InfantId = Convert.ToInt32(infantData.Rows[0]["InfantID"]);
                    infantDetails.NextVaccinationDate = Convert.ToString(infantData.Rows[0]["NextVaccinationDate"]);
                }
            }
            catch { }
            return infantDetails;
        }

    }
}