using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasspackWebApi.Models
{
    public class Tut
    {
        public int Oid { get; set; }
        public int TutorialType { get; set; }
        public string ButtonText { get; set; }
        public string Heading { get; set; }
        public string Text { get; set; }
    }
}