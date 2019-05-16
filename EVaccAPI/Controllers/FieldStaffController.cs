using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using EVaccAPI.Models;
using EVaccAPI.Services;

namespace EVaccAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class FieldStaffController : ApiController
    {
        private FieldStaffService fieldService;
        public FieldStaffController()
        {
            fieldService = new FieldStaffService();
        }

        [HttpPost]
        [Route("evacc/AddFieldStaffComment")]
        public bool AddFieldStaffComment(CommentRequest registrationData)
        {
            return fieldService.AddFieldStaffComment(registrationData);
        }

        [HttpPost]
        [Route("evacc/DeleteFieldStaffComment/{Userid}")]
        public bool DeleteFieldStaffComment(int userid)
        {
            return fieldService.DeleteFieldStaffComment(userid);
        }
    }
}
