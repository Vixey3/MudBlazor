using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiplomskiBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutController : ControllerBase
    {
        private readonly DataContext _context;

        public WorkoutController(DataContext context)
        {
            _context = context;
        }

        [Route("workouts")]
        [HttpGet]
        public async Task<ActionResult<List<Workout>>> GetWorkouts()
        {
            var workouts = await _context.Workouts.Include(c => c.korisnici).Include(k => k.vezbe).ToListAsync();
            return Ok(workouts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Workout>> GetSingleWorkout(int id)
        {
            var work = await _context.Workouts.Include(c => c.korisnici).Include(k => k.vezbe).FirstOrDefaultAsync(k => k.workoutId == id);
            if (work == null)
            {
                return NotFound("Workout nije pronadjen");
            }
            return Ok(work);
        }

        [HttpGet("wk/{idk}")]
        public async Task<ActionResult<List<Korisnik>>> GetWorkoutKorisnik(int idk)
        {
            var workout = await _context.Workouts.Include(w => w.korisnici)
                .Include(s => s.vezbe)
                .FirstOrDefaultAsync(k => k.workoutId == idk);
            if (workout == null)
            {
                return NotFound("workout nije pronadjen");
            }

            var koris = workout.korisnici;
            if (koris == null)
            {
                return NotFound("Workout nema korisnike");
            }
            return Ok(koris);
        }

        [HttpGet("wv/{idk}")]
        public async Task<ActionResult<List<Vezba>>> GetWorkoutVezbe(int idk)
        {

            var workout = await _context.Workouts.Include(w => w.korisnici)
                .Include(s => s.vezbe)
                .ThenInclude(v => v.deoTela)
                .FirstOrDefaultAsync(k => k.workoutId == idk);
            if (workout == null)
            {
                return NotFound("Workout nije pronadjen");
            }

            var vez = workout.vezbe;
            if (vez == null)
            {
                return NotFound("Workout nema vezbe");
            }
            return Ok(vez);
        }

        [HttpPut("addvVezbeWorkout/{id}")]
        public async Task<ActionResult<List<Workout>>> AddVezbeWorkout(List<int> lista, int id)
        {
            var work = await _context.Workouts
                .Include(c => c.korisnici)
                .Include(k => k.vezbe)
                .ThenInclude(dt => dt.deoTela)
                .FirstOrDefaultAsync(k => k.workoutId == id);

            work.vezbe = new List<Vezba>();
            foreach (var p in lista)
            {
                var vezba = await _context.Vezbe.Include(k => k.deoTela).FirstOrDefaultAsync(k => k.vezbaId == p);
                work.vezbe.Add(vezba);
            }
            await _context.SaveChangesAsync();
            return Ok(await GetDbWorkouts());
        }

        [HttpPost]
        public async Task<ActionResult<List<Workout>>> CreateWorkout(WorkoutDto k)
        {
            var work = new Workout();
            work.opisWorkouta = k.opisWorkouta;
            work.nazivWorkouta = k.nazivWorkouta;
            work.vezbe = new List<Vezba>();
            work.korisnici = new List<Korisnik>();

            _context.Workouts.Add(work);
            await _context.SaveChangesAsync();

            return Ok(await GetDbWorkouts());
        }

        //[Route("registracija")]
        //[HttpPost]
        //register model
        [HttpPut("{id}")]
        public async Task<ActionResult<List<Workout>>> UpdateWorkout(WorkoutDto k, int id)
        {
            var dbWork = await _context.Workouts
                .Include(c => c.korisnici)
                .Include(k => k.vezbe)
                .FirstOrDefaultAsync(k => k.workoutId == id);
            if (dbWork == null)
            {
                return NotFound("Za trazeni ID ne postoji workout");
            }

            dbWork.opisWorkouta = k.opisWorkouta;
            dbWork.nazivWorkouta = k.nazivWorkouta;

            await _context.SaveChangesAsync();

            return Ok(await GetDbWorkouts());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Workout>>> DeleteWorkout(int id)
        {
            var dbWorkout = await _context.Workouts
                .Include(c => c.korisnici)
                .Include(k => k.vezbe)
                .FirstOrDefaultAsync(k => k.workoutId == id);
            if (dbWorkout == null)
            {
                return NotFound("Za trazeni ID ne postoji workout");
            }


            _context.Workouts.Remove(dbWorkout);
            await _context.SaveChangesAsync();

            return Ok(await GetDbWorkouts());
        }

        private async Task<List<Workout>> GetDbWorkouts()
        {
            return await _context.Workouts.Include(c => c.korisnici).Include(k => k.vezbe).ToListAsync();
        }
    }
}