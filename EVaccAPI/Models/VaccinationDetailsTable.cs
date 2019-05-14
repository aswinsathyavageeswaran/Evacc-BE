using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EVaccAPI.Models
{
    public class VaccinationDetailsTable
    {
        public string VaccinationName { get; set; }
        public IEnumerable<Schedule> Schedule { get; set; }
    }
}