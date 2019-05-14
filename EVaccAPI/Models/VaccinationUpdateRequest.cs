using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EVaccAPI.Models
{
    public class VaccinationUpdateRequest
    {
        public int InfantId { get; set; }
        public int ScheduleId { get; set; }
        public DateTime VaccinatedDate { get; set; }
        public string Comments { get; set; }

    }
}