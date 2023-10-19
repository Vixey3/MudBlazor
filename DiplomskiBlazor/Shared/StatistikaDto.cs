using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomskiBlazor.Shared
{
    public class StatistikaDto
    {
        public int statistikaId { get; set; }
        public DateTime datum { get; set; }
        public int visina { get; set; }
        public double tezina { get; set; }
        public double obimStruka { get; set; }
        public double obimGrudi { get; set; }
        public double obimKukova { get; set; }

        public int korisnikId { get; set; }
    }
}