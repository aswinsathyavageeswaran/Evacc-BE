using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EVaccAPI.Models
{
    public class FilterResponse
    {
        public int DoneCount { get; set; }
        public int DueCount { get; set; }
        public int MissedCount { get; set; }
    }
}