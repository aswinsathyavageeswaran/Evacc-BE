using EVaccAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EVaccAPI.Services
{
    public class ProfileService
    {
        DbService dbService;
        public ProfileService()
        {
            dbService = DbService.GetDbService();
        }

        public int RegisterUser(RegistrationRequest registerData)
        {
            var userId = 0;
            var query = string.Format("select userId from UserDetails where Email='{0}' or Mobile = '{1}' or UserName = '{2}'", registerData.Email, registerData.Mobile, registerData.UserName);
            var existingUserId = Convert.ToInt32(dbService.ExecuteScalar(query));
            if (existingUserId > 0)
            {
                userId = -1;
            }
            else
            {
                query = string.Format(@"insert into UserDetails(FullName,Address,PINCode,Mobile,Email,UserName,Password,UserType,
                        RegistrationID,RegisteredPHC,EmailNotification,SMSNotification) output inserted.UserID  values 
                        ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', {7}, '{8}', '{9}', {10}, {11})",
                        registerData.FullName, registerData.Address, registerData.PinCode, registerData.Mobile, registerData.Email,
                        registerData.UserName, registerData.Password, registerData.UserType, registerData.RegistrationId, registerData.RegisteredPHC, Convert.ToInt32(registerData.EmailNotification), Convert.ToInt32(registerData.SMSNotification));
                userId = Convert.ToInt32(dbService.ExecuteScalar(query));
            }
            return userId;
        }

        public bool UpdateProfile(ProfileRequest profileData)
        {
            var success = false;
            var query = string.Format("select userId from UserDetails where (Email='{0}' or Mobile = '{1}') and userId <> {2}", profileData.Email, profileData.Mobile, profileData.UserId);
            var existingUserId = Convert.ToInt32(dbService.ExecuteScalar(query));
            if (existingUserId == 0)
            {
                query = string.Format("update UserDetails set FullName='{0}', Address='{1}', PINCode='{2}', Mobile='{3}', Email='{4}' output inserted.UserId where UserId={5}",
                    profileData.FullName, profileData.Address, profileData.PinCode, profileData.Mobile, profileData.Email, profileData.UserId);
                var affectedUserId = Convert.ToInt32(dbService.ExecuteScalar(query));
                success = (affectedUserId > 0);
            }
            return success;
        }

        public bool ChangePassword(PasswordRequest passwordData)
        {
            var success = false;
            try
            {
                var query = string.Format("update UserDetails set Password='{0}' output inserted.UserId where Password='{1}' and UserId={2}", passwordData.NewPassword, passwordData.OldPassword, passwordData.UserId);
                var affectedUserId = Convert.ToInt32(dbService.ExecuteScalar(query));
                success = (affectedUserId > 0);
            }
            catch { }
            return success;
        }
    }
}