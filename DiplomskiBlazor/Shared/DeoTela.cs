using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DiplomskiBlazor.Shared
{
    public class DeoTela
    {
        public int deoTelaId { get; set; }
        public string nazivTela { get; set; } = string.Empty;
        [JsonIgnore]
        public List<Vezba> vezbe { get; set; }
    }
}