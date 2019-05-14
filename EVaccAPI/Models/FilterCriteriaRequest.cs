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
        public int Status { get; set; }
        public string FromYear { get; set; }
        public string ToYear { get; set; }
    }
}