using EVaccAPI.Models;
using EVaccAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EVaccAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProfileController : ApiController
    {
        private ProfileService profileService;
        public ProfileController()
        {
            profileService = new ProfileService();
        }

        [HttpPost]
        [Route("evacc/RegisterUser")]
        public int RegisterUser(RegistrationRequest registrationData)
        {
            int userId = 0;
            try
            {
                userId = profileService.RegisterUser(registrationData);
            }
            catch { }

            return userId;
        }

        [HttpPut]
        [Route("evacc/UpdateProfile")]
        public bool UpdateProfile(ProfileRequest profileData)
        {
            bool success = false;

            try
            {
                success = profileService.UpdateProfile(profileData);
            }
            catch { }
            return success;
        }

        [HttpPut]
        [Route("evacc/ChangePassword")]
        public bool ChangePassword(PasswordRequest passwordData)
        {
            bool success = false;

            try
            {
                success = profileService.ChangePassword(passwordData);
            }
            catch { }
            return success;
        }
    }
}
