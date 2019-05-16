using EVaccAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EVaccAPI.Services
{
    public class AdminService
    {
        DbService dbService;
        public AdminService()
        {
            dbService = DbService.GetDbService();
        }

        public AdminFilterCriteriaList GetAdminFilterCriteria()
        {
            var vaccinationList = new List<Vaccination>();
            var districtList = new List<District>();

            var query = "select * from District";
            var districtData = dbService.ExecuteReader(query);

            query = "select SchId, Vaccine from ImmunizationSchedule";
            var vaccinationData = dbService.ExecuteReader(query);

            foreach (DataRow district in districtData.Rows)
            {
                var districtItem = new District()
                {
                    DistrictId = Convert.ToInt32(district["DistrictId"]),
                    DistrictName = Convert.ToString(district["Name"])
                };
                districtList.Add(districtItem);
            }
            foreach (DataRow vaccination in vaccinationData.Rows)
            {
                var vaccinationItem = new Vaccination()
                {
                    VaccinationId = Convert.ToInt32(vaccination["SchId"]),
                    VaccinationName = Convert.ToString(vaccination["Vaccine"])
                };
                vaccinationList.Add(vaccinationItem);
            }

            var adminFilterList = new AdminFilterCriteriaList()
            {
                Districts = districtList,
                Vaccinations = vaccinationList
            };
            return adminFilterList;
        }

        public FilterResponse GetFilteredGraphData(FilterCriteriaRequest filterCriteria)
        {
            var vaccinationToQuery = string.Empty;
            var districtToQuery = string.Empty;
            
            if (filterCriteria.VaccineIdList.Count > 0)
            {
                vaccinationToQuery = "SchId in (" + string.Join(",", filterCriteria.VaccineIdList) + ")";
            }
            if (filterCriteria.DistrictIdList.Count > 0)
            {
                districtToQuery = "DistrictId in (" + string.Join(",", filterCriteria.DistrictIdList) + ")";
            }

            FilterResponse filterres = new FilterResponse();
            if (filterCriteria.StatusList.Count > 0)
            {
               
                foreach ( int status in filterCriteria.StatusList)
                {
                    if (status == 0)//Done
                    {
                        var query = string.Format(@"SELECT Count(*) FROM ListOfSuccessfullVacc WHERE VaccinatedDate>='{0}' AND VaccinatedDate<='{1}' AND {2} AND {3}", filterCriteria.FromYear,filterCriteria.ToYear,districtToQuery,vaccinationToQuery);
                        filterres.DoneCount = Convert.ToInt32(dbService.ExecuteScalar(query));
                    }
                    else if (status == 1)//Due
                    {
                        var query = string.Format(@"SELECT Count(*) FROM ListOfVaccinationDue WHERE VAccStartOn>='{0}' AND VAccStartOn<='{1}'AND {2} AND {3} ", filterCriteria.FromYear, filterCriteria.ToYear, districtToQuery, vaccinationToQuery);
                        filterres.DueCount = Convert.ToInt32(dbService.ExecuteScalar(query));
                    }
                    else if (status == 2)//OverDue/Missed
                    {
                        var query = string.Format(@"SELECT Count(*) FROM ListOfVaccinationOverDue WHERE VAccStartOn>='{0}' AND VAccStartOn<='{1}' AND {2} AND {3}", filterCriteria.FromYear, filterCriteria.ToYear, districtToQuery, vaccinationToQuery);
                        filterres.MissedCount = Convert.ToInt32(dbService.ExecuteScalar(query));
                    }
                }
            }

            //int Totalstatuscount = filterres.DueCount + filterres.DoneCount + filterres.MissedCount;
            //filterres.DoneCount = (filterres.DoneCount / Totalstatuscount) * 100;
            //filterres.DueCount = (filterres.DueCount / Totalstatuscount) * 100;
            //filterres.MissedCount = (filterres.MissedCount / Totalstatuscount) * 100;

            return filterres;
        }

        public IEnumerable<RegistrationResponse> GetAllFieldStaffs()
        {
            var userDetailsList = new List<RegistrationResponse>();
            var query = "select UserId, FullName, Address, PinCode, Mobile, Email, UserName, RegistrationId, RegisteredPHC from UserDetails where UserType = 4 AND IsDeleted = 0";
            var userData = dbService.ExecuteReader(query);
            foreach (DataRow row in userData.Rows)
            {
                var userDetail = new RegistrationResponse()
                {
                    UserId = Convert.ToInt32(row["UserId"]),
                    FullName = Convert.ToString(row["FullName"]),
                    Address = Convert.ToString(row["Address"]),
                    PinCode = Convert.ToString(row["PinCode"]),
                    Mobile = Convert.ToString(row["Mobile"]),
                    Email = Convert.ToString(row["Email"]),
                    UserName = Convert.ToString(row["UserName"]),
                    RegistrationId = Convert.ToString(row["RegistrationId"]),
                    RegisteredPHC = Convert.ToString(row["RegisteredPHC"])
                };
                userDetailsList.Add(userDetail);
            }
            return userDetailsList;
        }

        public IEnumerable<RegistrationResponse> GetAllHospitalsForLocalAdmin(int UserId)
        {
            var userDetailsList = new List<RegistrationResponse>();
            var query = string.Format(@"select UserId, FullName, Address, PinCode, Mobile, Email, UserName, RegistrationId, RegisteredPHC from UserDetails where UserType = 2
                        AND RegisteredPHC = (select RegisteredPHC from UserDetails where UserId = {0})", UserId);
            var userData = dbService.ExecuteReader(query);
            foreach (DataRow row in userData.Rows)
            {
                var userDetail = new RegistrationResponse()
                {
                    UserId = Convert.ToInt32(row["UserId"]),
                    FullName = Convert.ToString(row["FullName"]),
                    Address = Convert.ToString(row["Address"]),
                    PinCode = Convert.ToString(row["PinCode"]),
                    Mobile = Convert.ToString(row["Mobile"]),
                    Email = Convert.ToString(row["Email"]),
                    UserName = Convert.ToString(row["UserName"]),
                    RegistrationId = Convert.ToString(row["RegistrationId"]),
                    RegisteredPHC = Convert.ToString(row["RegisteredPHC"])
                };
                userDetailsList.Add(userDetail);
            }
            return userDetailsList;
        }

        public IEnumerable<RegistrationResponse> GetAllFieldStaffsForLocalAdmin(int UserId)
        {
            var userDetailsList = new List<RegistrationResponse>();
            var query = string.Format(@"select UserId, FullName, Address, PinCode, Mobile, Email, UserName, RegistrationId, RegisteredPHC from UserDetails where UserType = 4
                        AND RegisteredPHC = (select RegisteredPHC from UserDetails where UserId = {0} AND IsDeleted = 0)", UserId);
            var userData = dbService.ExecuteReader(query);
            foreach (DataRow row in userData.Rows)
            {
                var userDetail = new RegistrationResponse()
                {
                    UserId = Convert.ToInt32(row["UserId"]),
                    FullName = Convert.ToString(row["FullName"]),
                    Address = Convert.ToString(row["Address"]),
                    PinCode = Convert.ToString(row["PinCode"]),
                    Mobile = Convert.ToString(row["Mobile"]),
                    Email = Convert.ToString(row["Email"]),
                    UserName = Convert.ToString(row["UserName"]),
                    RegistrationId = Convert.ToString(row["RegistrationId"]),
                    RegisteredPHC = Convert.ToString(row["RegisteredPHC"])
                };
                userDetailsList.Add(userDetail);
            }
            return userDetailsList;
        }

      
        public IEnumerable<RegistrationResponse> SearchFieldStaff(string searchstring)
        {
            try
            {
                var userDetailsList = new List<RegistrationResponse>();
                var query = string.Format(@"select UserId, FullName, Address, PinCode, Mobile, Email, UserName, RegistrationId, RegisteredPHC
                            from UserDetails where UserType = 4 AND (FullName = '{0}' or RegistrationId = '{0}' or RegisteredPHC = '{0}' or Mobile = '{0}' AND IsDeleted=0)",searchstring);
                var userData = dbService.ExecuteReader(query);
                foreach (DataRow row in userData.Rows)
                {
                    var userDetail = new RegistrationResponse()
                    {
                        UserId = Convert.ToInt32(row["UserId"]),
                        FullName = Convert.ToString(row["FullName"]),
                        Address = Convert.ToString(row["Address"]),
                        PinCode = Convert.ToString(row["PinCode"]),
                        Mobile = Convert.ToString(row["Mobile"]),
                        Email = Convert.ToString(row["Email"]),
                        UserName = Convert.ToString(row["UserName"]),
                        RegistrationId = Convert.ToString(row["RegistrationId"]),
                        RegisteredPHC = Convert.ToString(row["RegisteredPHC"])
                    };
                    userDetailsList.Add(userDetail);
                }
                return userDetailsList;
            }
            catch
            {
                return null;
            }
        }

        public bool DeleteFieldStaff(int userid)
        {
            try
            {
                var query = string.Format(@"Update UserDetails Set IsDeleted = 1 where UserId = {0}", userid);
                dbService.ExecuteNonQuery(query);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}