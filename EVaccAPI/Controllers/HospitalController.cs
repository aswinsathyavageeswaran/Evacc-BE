using EVaccAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EVaccAPI.Models;
using System.Web.Http.Cors;

namespace EVaccAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class HospitalController : ApiController
    {
        private HospitalService hospitalService;
        public HospitalController()
        {
            hospitalService = new HospitalService();
        }

        [HttpPost]
        [Route("evacc/SearchInfant")]
        public IEnumerable<InfantDetailsResponse> SearchInfant(InfantSearchRequest searchData)
        {
            return hospitalService.SearchInfant(searchData);
        }
        
        [HttpGet]
        [Route("evacc/GetImmunizationschedule")]
        public IEnumerable<ImmunizationDetails> GetImmunizationschedule()
        {
            return hospitalService.GetImmunizationschedule();
        }

        [HttpGet]
        [Route("evacc/GetVaccinationDetails/{infantId}")]
        public IEnumerable<VaccinationDetails> GetVaccinationDetails(int InfantId)
        {
            return hospitalService.GetVaccinationDetails(InfantId);
        }

        [HttpGet]
        [Route("evacc/GetVaccinationDetailsTable/{infantId}")]
        public IEnumerable<VaccinationDetailsTable> GetVaccinationDetailsTable(int InfantId)
        {
            return hospitalService.GetVaccinationDetailsTable(InfantId);
        }

        [HttpPost]
        [Route("evacc/UpdateVaccinationDeatils")]
        public bool UpdateVaccinationDeatils(IEnumerable<VaccinationUpdateRequest> vaccList)
        {
            return hospitalService.UpdateVaccinationDeatils(vaccList);
        }

    }
}
