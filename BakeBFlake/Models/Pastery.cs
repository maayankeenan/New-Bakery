using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BakeBFlake.Models
{
    public class Pastery
    {
        public Int32 Id { get; set; }
        public String Name { get; set;}
        public Double Price { get; set; }
        public String Type { get; set; }
        public String ImageLink { get; set; }
        public String Comments { get; set; }
        public Boolean Vegan { get; set;}
        public Boolean GlotanFree { get; set; }
    }
}