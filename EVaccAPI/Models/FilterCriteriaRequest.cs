using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EVaccAPI.Models
{
    public class FilterCriteriaRequest
    {
        public List<int> VaccineIdList { get; set; }
        public List<int> DistrictIdList { get; set; }
        public List<int> StatusList { get; set; } //0-Done, 1-Due,2-Over Due
        public DateTime FromYear { get; set; }
        public DateTime ToYear { get; set; }
    }
}