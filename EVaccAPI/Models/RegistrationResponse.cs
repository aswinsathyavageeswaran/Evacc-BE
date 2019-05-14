using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EVaccAPI.Models
{
    public class RegistrationResponse
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PinCode { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string RegistrationId { get; set; }
        public string RegisteredPHC { get; set; }
    }
}