using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EVaccAPI.Models
{
    public class Schedule
    {
        public int ScheduleId { get; set; }
        public string Period { get; set; }
        public string Status { get; set; } //Success - 0, Due - 1, Pending - 2, OverDue -3;
        public DateTime VaccinatedOrDueDate { get; set; }
    }
}