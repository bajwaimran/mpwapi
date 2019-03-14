using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasspackWebApi.Models
{
    public class Connection
    {
        public string IP { get; set; }
        public string Datenbank { get; set; }
        public string Port { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public int Mitarbeiter { get; set; }
        public string Aktuell { get; set; }
    }
}