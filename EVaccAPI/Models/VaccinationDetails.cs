using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EVaccAPI.Models
{
    public class VaccinationDetails
    {
        public int ScheduleId { get; set; }
        public DateTime VaccinatedDate { get; set; }
        public int Status { get; set; } // Success - 0, Due - 1, Pending - 2, OverDue -3;

    }
}