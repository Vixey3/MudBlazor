using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DiplomskiBlazor.Shared
{
    public class Statistika
    {
        public int statistikaId { get; set; }
        [Required]
        public DateTime datum { get; set; }
        [Required]
        public int visina { get; set; }
        [Required]
        public double tezina { get; set; }
        public double obimStruka { get; set; }
        public double obimGrudi { get; set; }
        public double obimKukova { get; set; }

        public Korisnik? korisnik { get; set; }
        [Required]
        public int korisnikId { get; set; }
        //struk, grudi,...
    }
}