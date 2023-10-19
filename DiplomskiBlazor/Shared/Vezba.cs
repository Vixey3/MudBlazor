using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DiplomskiBlazor.Shared
{
    public class Vezba
    {
        public int vezbaId { get; set; }
        public string nazivVezbe { get; set; } = string.Empty;
        public string opisVezbe { get; set; } = string.Empty;
        public string brojSerijaPonavljanja { get; set; } = string.Empty;

        public DeoTela? deoTela { get; set; }
        public int deoTelaId { get; set; }
        [JsonIgnore]
        //Broj serija i ponavljanja
        public List<Workout> workouts { get; set; }
    }
}