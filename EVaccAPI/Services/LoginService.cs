using EVaccAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace EVaccAPI.Services
{
    public class LoginService
    {
        DbService dbService;
        public LoginService()
        {
            dbService = DbService.GetDbService();
        }

        public int VerifyLogin(LoginRequest loginRequest)
        {
            int userId = 0;
            string query = string.Format("select UserID from UserDetails where userName='{0}' and Password = '{1}'", loginRequest.UserName, loginRequest.Password);
            var loginData = dbService.ExecuteReader(query);
            if( loginData.Rows.Count > 0)
            {
                userId = Convert.ToInt32(loginData.Rows[0]["UserID"]);
            }

            return userId;
        }

        public UserDataResponse GetUserData(int userID)
        {
            UserDataResponse userData = null;
            string query = string.Format("select UserID, UserName, UserType,FullName, Address, PinCode, Mobile, Email, RegistrationID, RegisteredPHC from UserDetails where UserId = {0}", userID);
            var loginData = dbService.ExecuteReader(query);
            if (loginData.Rows.Count > 0)
            {
                userData = new UserDataResponse();
                userData.UserId = Convert.ToInt32(loginData.Rows[0]["UserID"]);
                userData.UserName = Convert.ToString(loginData.Rows[0]["UserName"]);
                userData.UserType = Convert.ToInt32(loginData.Rows[0]["UserType"]);
                userData.FullName = Convert.ToString(loginData.Rows[0]["FullName"]);
                userData.Address = Convert.ToString(loginData.Rows[0]["Address"]);
                userData.PinCode = Convert.ToString(loginData.Rows[0]["PinCode"]);
                userData.Mobile = Convert.ToString(loginData.Rows[0]["Mobile"]);
                userData.Email = Convert.ToString(loginData.Rows[0]["Email"]);
                userData.RegistrationId = Convert.ToString(loginData.Rows[0]["RegistrationID"]);
                userData.RegisteredPHC = Convert.ToString(loginData.Rows[0]["RegisteredPHC"]);
            }
            return userData;
        }

        public IEnumerable<UserTypeResponse> GetUserTypes()
        {
            var usertypeDataList = new List<UserTypeResponse>();
            var response = dbService.ExecuteReader( "select * from UserType");
            foreach (DataRow row in response.Rows)
            {
                var userTypeData = new UserTypeResponse();
                userTypeData.TypeId = Convert.ToInt32(row["ID"]);
                userTypeData.TypeName = Convert.ToString(row["Description"]);
                usertypeDataList.Add(userTypeData);
            }
            return usertypeDataList;
        }       

    }
}