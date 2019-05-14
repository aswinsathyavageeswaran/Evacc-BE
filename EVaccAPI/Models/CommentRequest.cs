using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EVaccAPI.Models
{
    public class CommentRequest
    {
       public int Userid { get; set; }
       public string Comment { get; set; }
       public int infantId { get; set; }

    }
}