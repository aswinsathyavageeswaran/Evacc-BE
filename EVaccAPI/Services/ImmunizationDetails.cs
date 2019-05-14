using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EVaccAPI.Services
{
    public class ImmunizationDetails
    {
        public int ScheduleId { get; set; }
        public string VaccineNme { get; set; }
        public string DuePeriod { get; set; }
    }
}