using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EVaccAPI.Models
{
    public class InfantSearchRequest
    {
        public String BirthId { get; set; }
        public String MothersName { get; set; }
        public DateTime DOB { get; set; }
        public String PlaceofBirth { get; set; }

    }
}