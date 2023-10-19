using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DiplomskiBlazor.Shared
{
    public class Workout
    {
        public int workoutId { get; set; }
        [Required]
        public string nazivWorkouta { get; set; } = string.Empty;
        [Required]
        public string opisWorkouta { get; set; } = string.Empty;
        //[JsonIgnore]
        public List<Vezba> vezbe { get; set; }
        [JsonIgnore]
        public List<Korisnik> korisnici { get; set; }
    }
}