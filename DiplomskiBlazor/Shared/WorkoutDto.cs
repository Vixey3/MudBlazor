using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomskiBlazor.Shared
{
    public class WorkoutDto
    {
        public int workoutId { get; set; }
        public string nazivWorkouta { get; set; } = string.Empty;
        public string opisWorkouta { get; set; } = string.Empty;
    }
}
