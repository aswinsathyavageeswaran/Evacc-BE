using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EVaccAPI.Models
{
    public class AdminFilterCriteriaList
    {
        public List<Vaccination> Vaccinations { get; set; }
        public List<District> Districts { get; set; }
    }

    public class Vaccination
    {
        public int VaccinationId { get; set; }
        public string VaccinationName { get; set; }
    }

    public class District
    {
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
    }
}