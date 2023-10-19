using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DiplomskiBlazor.Shared
{
    public class KorisnikDto
    {
        [JsonIgnore] public int korisnikId { get; set; }
        public string korisnickoIme { get; set; } = string.Empty;
        public string lozinka { get; set; } = string.Empty;
        public string ime { get; set; } = string.Empty;
        public string prezime { get; set; } = string.Empty;
        public string telefon { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string pol { get; set; } = string.Empty;
    }
}