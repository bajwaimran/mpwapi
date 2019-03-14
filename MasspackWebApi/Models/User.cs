using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasspackWebApi.Models
{
    public class User
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string UserRole { get; set; }
        public bool Status { get; set; }
        public string Password { get; set; }
    }
}