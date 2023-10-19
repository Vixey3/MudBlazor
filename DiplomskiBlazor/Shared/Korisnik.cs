using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DiplomskiBlazor.Shared
{
    public class Korisnik
    {

        public int korisnikId { get; set; }
        [Required]
        public string? korisnickoIme { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Lozinka mora imati najmanje 6 karaktera")]
        public string? lozinka { get; set; }
        [Required]
        public string? ime { get; set; }
        [Required]
        public string? prezime { get; set; }
        [Phone]
        public string? telefon { get; set; }
        [EmailAddress(ErrorMessage = "Email adresa nije u odgovarajucem formatu")]
        public string? email { get; set; }
        [Required]
        public string? pol { get; set; }
        
        public List<Statistika> statistike { get; set; }
        
        public List<Workout> workouts { get; set; }
    }
}