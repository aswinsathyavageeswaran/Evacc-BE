using EVaccAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace EVaccAPI.Services
{
    public class HospitalService
    {
        DbService dbService;
        public HospitalService()
        {
            dbService = DbService.GetDbService();
        }

        public IEnumerable<InfantDetailsResponse> SearchInfant(InfantSearchRequest searchRequest)
        {
            List<InfantDetailsResponse> infantList = new List<InfantDetailsResponse>();

            var query = @"SELECT BirthId, FullName, DOB, POB, MothersName, a.InfantID, b.NextVaccinationDate 
                        FROM InfantDetails a LEFT JOIN vaccinationDetails b ON a.infantId = b.InfantId
                        WHERE ";

            Dictionary<string, string> paramlist = new Dictionary<string, string>();

            if (searchRequest.DOB != default(DateTime))
                paramlist.Add("DOB", searchRequest.DOB.ToString());
            if (searchRequest.MothersName != "")
                paramlist.Add("MothersName", searchRequest.MothersName);
            if (searchRequest.PlaceofBirth != "")
                paramlist.Add("POB", searchRequest.PlaceofBirth);
            if (searchRequest.BirthId != "")
                paramlist.Add("BirthId", searchRequest.BirthId);


            int i = 0;
            foreach (var item in paramlist)
            {
                query += item.Key + "='" + item.Value + "'";
                i++;
                if (i != paramlist.Count)
                    query += " AND ";
            }

            query += " AND b.VaccinationId = (SELECT MAX(VaccinationId)  FROM vaccinationDetails where InfantID =  a.InfantID)";

            var infantDataList = dbService.ExecuteReader(query);
            foreach (DataRow row in infantDataList.Rows)
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

        
        public IEnumerable<ImmunizationDetails> GetImmunizationschedule()
        {
            List<ImmunizationDetails> immunizationList = new List<ImmunizationDetails>();
            var query = string.Format(@"SELECT SchId,Vaccine,StartOn FROM ImmunizationSchedule");
            var scheduleDataList = dbService.ExecuteReader(query);
            foreach (DataRow row in scheduleDataList.Rows)
            {
                var immunizationData = new ImmunizationDetails();
                immunizationData.ScheduleId = Convert.ToInt32(row["SchId"]);
                immunizationData.VaccineNme = Convert.ToString(row["Vaccine"]);
                immunizationData.DuePeriod = Convert.ToString(row["StartOn"]);
                immunizationList.Add(immunizationData);
            }
            return immunizationList;
        }

        public IEnumerable<VaccinationDetails> GetVaccinationDetails(int InfantId)
        {
            List<VaccinationDetails> vaccinationList = new List<VaccinationDetails>();

            //Successfull
            var query = string.Format(@"SELECT SchId, VaccinatedDate, 0 As Status FROM ListOfSuccessfullVacc WHERE InfantId = {0}", InfantId);
            var vaccinationDataList = dbService.ExecuteReader(query);
            foreach (DataRow row in vaccinationDataList.Rows)
            {
                var vaccinationtData = new VaccinationDetails();
                vaccinationtData.ScheduleId = Convert.ToInt32(row["SchId"]);
                vaccinationtData.VaccinatedDate = Convert.ToDateTime(row["VaccinatedDate"]);
                vaccinationtData.Status = Convert.ToInt16(row["Status"]);
                vaccinationList.Add(vaccinationtData);
            }

            //Due
            query = string.Format(@"SELECT SchId ,VAccStartOn, 1 As Status  FROM ListOfVaccinationDue WHERE InfantId = {0}", InfantId);
            vaccinationDataList = dbService.ExecuteReader(query);
            foreach (DataRow row in vaccinationDataList.Rows)
            {
                var vaccinationtData = new VaccinationDetails();
                vaccinationtData.ScheduleId = Convert.ToInt32(row["SchId"]);
                vaccinationtData.VaccinatedDate = Convert.ToDateTime(row["VAccStartOn"]);
                vaccinationtData.Status = Convert.ToInt16(row["Status"]);
                vaccinationList.Add(vaccinationtData);
            }

            //Pending
            query = string.Format(@"SELECT SchId , VAccStartOn,2 As Status  FROM ListOfUpcomingVacc WHERE InfantId = {0}", InfantId);
            vaccinationDataList = dbService.ExecuteReader(query);
            foreach (DataRow row in vaccinationDataList.Rows)
            {
                var vaccinationtData = new VaccinationDetails();
                vaccinationtData.ScheduleId = Convert.ToInt32(row["SchId"]);
                vaccinationtData.VaccinatedDate = Convert.ToDateTime(row["VAccStartOn"]);
                vaccinationtData.Status = Convert.ToInt16(row["Status"]);
                vaccinationList.Add(vaccinationtData);
            }

            //OverDue
            query = string.Format(@"SELECT SchId , VAccStartOn, 3 As Status  FROM ListOfVaccinationOverDue WHERE InfantId = {0}", InfantId);
            vaccinationDataList = dbService.ExecuteReader(query);
            foreach (DataRow row in vaccinationDataList.Rows)
            {
                var vaccinationtData = new VaccinationDetails();
                vaccinationtData.ScheduleId = Convert.ToInt32(row["SchId"]);
                vaccinationtData.VaccinatedDate = Convert.ToDateTime(row["VAccStartOn"]);
                vaccinationtData.Status = Convert.ToInt16(row["Status"]);
                vaccinationList.Add(vaccinationtData);
            }
            return vaccinationList;
        }

        public IEnumerable<VaccinationDetailsTable> GetVaccinationDetailsTable(int InfantId)
        {

            List<VaccinationDetailsTable> vaccinationList = new List<VaccinationDetailsTable>();

            //Successfull
            var query = string.Format(@"SELECT InfantId,SchId,Vaccine,StartOn,EndOn,VaccinatedDate As VaccinatedOrDueDate,'Done' As Status FROM ListOfSuccessfullVacc WHERE InfantId = {0}", InfantId);
            var vaccinationDataList = dbService.ExecuteReader(query);
            //Due
            query = string.Format(@"SELECT InfantId,SchId,Vaccine,StartOn,EndOn,VAccStartOn  As VaccinatedOrDueDate,'Due' As Status From ListOfVaccinationDue WHERE InfantId = {0}", InfantId);
            vaccinationDataList.Merge(dbService.ExecuteReader(query));
            //Pending
            query = string.Format(@"SELECT InfantId,SchId,Vaccine,StartOn,EndOn,VAccStartOn  As VaccinatedOrDueDate,'Pending' As Status FROM ListOfUpcomingVacc WHERE InfantId = {0}", InfantId);
            vaccinationDataList.Merge(dbService.ExecuteReader(query));
            //OverDue
            query = string.Format(@"SELECT InfantId,SchId,Vaccine,StartOn,EndOn,VAccStartOn  As VaccinatedOrDueDate,'OverDue' As Status FROM ListOfVaccinationOverDue WHERE InfantId = {0}", InfantId);
            vaccinationDataList.Merge(dbService.ExecuteReader(query));

            DataView dv = vaccinationDataList.DefaultView;
            dv.Sort = "SchId asc";
            vaccinationDataList = dv.ToTable();

            var grouped = from table in vaccinationDataList.AsEnumerable()

                          group table by new { VaccineCol = table["Vaccine"] } into groupby

                          select new

                          {

                              Value = groupby.Key,

                              ColumnValues = groupby

                          };

            foreach (var key in grouped)
            {
                var vaccinationtData = new VaccinationDetailsTable();
                vaccinationtData.VaccinationName = key.Value.VaccineCol.ToString();
                List<Schedule> scheduleList = new List<Schedule>();
                foreach (var columnValue in key.ColumnValues)
                {
                    Schedule scheduleData = new Schedule();

                    scheduleData.ScheduleId = Convert.ToInt32(columnValue["SchId"]);
                    scheduleData.Period = GetSchedulePeriod(Convert.ToDouble(columnValue["StartOn"]),Convert.ToDouble(columnValue["EndOn"]));
                           
                    scheduleData.VaccinatedOrDueDate = Convert.ToDateTime(columnValue["VaccinatedOrDueDate"]);
                    scheduleData.Status = Convert.ToString(columnValue["Status"]);

                    scheduleList.Add(scheduleData);
                }

                List<string> periodlist =  new List<string>();
                query = string.Format(@"SELECT DISTINCT StartOn,EndOn FROM ImmunizationSchedule");
                var response = dbService.ExecuteReader(query);
                foreach (DataRow row in response.Rows)
                {
                    periodlist.Add(GetSchedulePeriod(Convert.ToDouble(row["StartOn"]),Convert.ToDouble(row["EndOn"])));
                }

                Schedule actualSchedule = scheduleList[0];
                List<Schedule> tempSchedules = new List<Schedule>();
                foreach (var period in periodlist)
                {
                    if (actualSchedule.Period == period)
                    {
                        tempSchedules.Add(actualSchedule);
                    }
                    else
                    {
                        Schedule dummy = new Schedule();
                        dummy.ScheduleId = 0;
                        dummy.Period = period;
                        dummy.Status = "Dummy";
                        dummy.VaccinatedOrDueDate = DateTime.Now;
                        tempSchedules.Add(dummy);
                    }
                }

                vaccinationtData.Schedule = tempSchedules;
                vaccinationList.Add(vaccinationtData);
            }
            return vaccinationList;
        }

        private string GetSchedulePeriod(double starton, double endon)
        {
            string period = "";
            switch (Convert.ToDouble(starton))
            {
                case 0:
                    period = "At Birth";
                    break;
                case 1.5:
                case 2.5:
                case 3.5:
                    period = string.Format("{0} Weeks", starton * 4);
                    break;
                default:
                    if (starton < 12)
                        period = string.Format("{0}-{1} Months", starton, endon);
                    else
                        period = string.Format("{0}-{1} Yr", starton / 12,endon / 12);
                    break;
            }

            return period;
        }
        public bool UpdateVaccinationDeatils(IEnumerable<VaccinationUpdateRequest> vaccList)
        {
            var success = false;
            foreach (var vaccData in vaccList)
            {
                var query = string.Format("SELECT SchId FROM VaccinationDetails WHERE InfantId={0} AND SchId={1}", vaccData.InfantId,vaccData.ScheduleId);
                var existingSchId = Convert.ToInt32(dbService.ExecuteScalar(query));
                if (existingSchId != 0)
                {
                    query = string.Format("UPDATE VaccinationDetails SET VaccinatedDate='{0}',Comments ={3} WHERE InfantId={1} AND SchId={2}",
                                vaccData.VaccinatedDate,vaccData.InfantId,vaccData.ScheduleId, vaccData.Comments);
                    dbService.ExecuteScalar(query);
                    success = true;
                }
                else
                {
                    query = string.Format(
                        @"SELECT TOP(1) DATEADD(WEEK, imm.StartOn, inf.DOB) AS NextVaccinationDate FROM InfantDetails inf CROSS APPLY ImmunizationSchedule imm
                        WHERE DATEADD(WEEK, imm.StartOn, inf.DOB) >= GETDATE() AND InfantID = {0}
                    ORDER BY DATEADD(WEEK, imm.StartOn, inf.DOB)", vaccData.InfantId);
                    var nextVaccinationDate = Convert.ToDateTime(dbService.ExecuteScalar(query));

                    query = string.Format(@"INSERT INTO VaccinationDetails(SchId,InfantID,VaccinatedDate,NextVaccinationDate,Comments) output inserted.VaccinationId  
                                          VALUES ({0}, {1}, '{2}', '{3}', '{4}')",
                        vaccData.ScheduleId, vaccData.InfantId, vaccData.VaccinatedDate, nextVaccinationDate, vaccData.Comments);
                    var vaccinationId = Convert.ToInt32(dbService.ExecuteScalar(query));
                    success = true;
                }
            }
            
            return success;
        }

    }
}