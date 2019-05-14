using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EVaccAPI.Models
{
    public class InfantDetailsResponse
    {
        public int InfantId { get; set; }
        public string BirthId { get; set; }
        public string FullName { get; set; }
        public string DOB { get; set; }
        public string POB { get; set; }
        public string MothersName { get; set; }
        public string NextVaccinationDate { get; set; }
    }
}