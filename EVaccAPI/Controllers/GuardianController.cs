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
    public class GuardianController : ApiController
    {
        private GuardianService guardianService;
        public GuardianController()
        {
            guardianService = new GuardianService();
        }

        [HttpPost]
        [Route("evacc/GetInfantDetails")]
        public InfantDetailsResponse GetInfantDetails(string birthId)
        {
            return guardianService.GetInfantDetails(birthId);
        }

        [HttpPost]
        [Route("evacc/AddInfant")]
        public InfantDetailsResponse AddInfant(InfantRequest infData)
        {
            return guardianService.AddInfant(infData);
        }

        [HttpGet]
        [Route("evacc/GetInfantsByGuardian/{guardianId}")]
        public IEnumerable<InfantDetailsResponse> GetInfantsByGuardian(int guardianId)
        {
            return guardianService.GetInfantsByGuardian(guardianId);
        }

        [HttpGet]
        [Route("evacc/GetVaccineNamesforNotification/{guardianId}")]
        public IEnumerable<string> GetVaccineNamesforNotification(int guardianId)
        {
            return guardianService.GetVaccineNamesforNotification(guardianId);
        }
    }
}
