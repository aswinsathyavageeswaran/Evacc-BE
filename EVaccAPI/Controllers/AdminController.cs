﻿using EVaccAPI.Models;
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
    [EnableCors(origins:"*", headers: "*",methods:"*")]
    public class AdminController : ApiController
    {
        private AdminService adminService;
        public AdminController()
        {
            adminService = new AdminService();
        }

        [HttpGet]
        [Route("evacc/admin/FilterList")]
        public AdminFilterCriteriaList GetAdminFilterCriteria()
        {
            return adminService.GetAdminFilterCriteria();
        }

        [HttpPost]
        [Route("evacc/admin/FilteredGraphData")]
        public FilterResponse GetFilteredGraphData(FilterCriteriaRequest filterCriteria)
        {
            return adminService.GetFilteredGraphData(filterCriteria);
        }

        [HttpGet]
        [Route("evacc/admin/GetAllFieldStaffs")]
        public IEnumerable<RegistrationResponse> GetAllFieldStaffs()
        {
            return adminService.GetAllFieldStaffs();
        }

        [HttpGet]
        [Route("evacc/admin/GetAllFieldStaffsForLocalAdmin/{UserId}")]
        public IEnumerable<RegistrationResponse> GetAllFieldStaffsForLocalAdmin(int UserId)
        {
            return adminService.GetAllFieldStaffsForLocalAdmin(UserId);
        }

        [HttpGet]
        [Route("evacc/admin/GetAllHospitalsForLocalAdmin/{UserId}")]
        public IEnumerable<RegistrationResponse> GetAllHospitalsForLocalAdmin(int UserId)
        {
            return adminService.GetAllHospitalsForLocalAdmin(UserId);
        }
        [HttpGet]
        [Route("evacc/admin/SearchFieldStaff/{SearchString}")]
        public IEnumerable<RegistrationResponse> SearchFieldStaff(string searchstring)
        {
            return adminService.SearchFieldStaff(searchstring);
        }

        [HttpPost]
        [Route("evacc/DeleteFieldStaff/{Userid}")]
        public bool DeleteFieldStaffComment(int userid)
        {
            return adminService.DeleteFieldStaff(userid);
        }
    }
}
