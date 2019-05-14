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
    public class LoginController : ApiController
    {
        private LoginService loginService;
        public LoginController()
        {
            loginService = new LoginService();
        }

        [HttpPost]
        [Route("evacc/login")]
        public int VerifyLogin(LoginRequest loginData)
        {
            return loginService.VerifyLogin(loginData);
        }

        [HttpGet]
        [Route("evacc/GetUserData/{userId}")]
        public UserDataResponse GetUserData(int userId)
        {
            return loginService.GetUserData(userId);
        }

        [HttpGet]
        [Route("evacc/GetUserTypes")]
        public IEnumerable<UserTypeResponse> GetUserTypes()
        {
            return loginService.GetUserTypes();
        }       
    }
}
