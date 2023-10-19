using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiplomskiBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KorisnikController : ControllerBase
    {
        private readonly DataContext _context;

        public KorisnikController(DataContext context)
        {
            _context = context;
        }

        [Route("korisnici")]
        [HttpGet]
        public async Task<ActionResult<List<Korisnik>>> GetKorisnici()
        {
            var korisnici = await _context.Korisnici.Include(s => s.statistike).ToListAsync();
            return Ok(korisnici);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Korisnik>> GetSingleKorisnik(int id)
        {
            var korisnik = await _context.Korisnici.Include(s => s.statistike).FirstOrDefaultAsync(k => k.korisnikId == id);
            if (korisnik == null)
            {
                return NotFound("Korisnik nije pronadjen");
            }
            return Ok(korisnik);
        }
        //[Route("korisnikworkout")]
        [HttpGet("kw/{idk}")]
        public async Task<ActionResult<List<Workout>>> GetKorisnikWorkout(int idk)
        {
            var korisnik = await _context.Korisnici.Include(w => w.workouts)
                .Include(s => s.statistike)
                .FirstOrDefaultAsync(k => k.korisnikId == idk);
            if (korisnik == null)
            {
                return NotFound("Korisnik nije pronadjen");
            }

            var works = korisnik.workouts;
            if (works == null)
            {
                return NotFound("Korisnik nema nijedan workout");
            }
            return Ok(works);
        }

        [Route("dodajworkoutkorisniku/{id}")]
        [HttpPut]
        public async Task<ActionResult<List<Korisnik>>> AddWorkoutKorisnik(List<int> lista, int id)
        {
            var korisnik = await _context.Korisnici.Include(w => w.workouts)
                .Include(s => s.statistike)
                .FirstOrDefaultAsync(k => k.korisnikId == id);

            foreach (var p in lista)
            {
                var work = await _context.Workouts.Include(c => c.korisnici).Include(k => k.vezbe).FirstOrDefaultAsync(k => k.workoutId == p);
                korisnik.workouts.Add(work);
            }


            await _context.SaveChangesAsync();
            return Ok(await GetDbKorisnici());
        }

        [HttpPost]
        public async Task<ActionResult<List<Korisnik>>> CreateKorisnik(KorisnikDto k)
        {
            var korisnik = new Korisnik();
            korisnik.korisnickoIme = k.korisnickoIme;
            korisnik.lozinka = k.lozinka;
            korisnik.ime = k.ime;
            korisnik.prezime = k.prezime;
            korisnik.pol = k.pol;
            korisnik.telefon = k.telefon;
            korisnik.email = k.email;
            korisnik.statistike = new List<Statistika>();
            korisnik.workouts = new List<Workout>();

            _context.Korisnici.Add(korisnik);
            await _context.SaveChangesAsync();

            return Ok(await GetDbKorisnici());
        }
        //[Route("registracija")]
        //[HttpPost]
        //register model
        [HttpPut("{id}")]
        public async Task<ActionResult<List<Korisnik>>> UpdateKorisnik(KorisnikDto k, int id)
        {
            var dbKorisnik = await _context.Korisnici
                .Include(s => s.statistike)
                .FirstOrDefaultAsync(k => k.korisnikId == id);
            if (dbKorisnik == null)
            {
                return NotFound("Za trazeni ID ne postoji korisnik");
            }

            dbKorisnik.korisnickoIme = k.korisnickoIme;
            dbKorisnik.lozinka = k.lozinka;
            dbKorisnik.ime = k.ime;
            dbKorisnik.prezime = k.prezime;
            dbKorisnik.telefon = k.telefon;
            dbKorisnik.email = k.email;
            dbKorisnik.pol = k.pol;

            await _context.SaveChangesAsync();

            return Ok(await GetDbKorisnici());
        }
        [Route("obrisiworkoutkorisniku/{id}/{idWorkouta}")]
        [HttpDelete]
        public async Task<ActionResult<List<Korisnik>>> DeleteWorkout(int id, int idWorkouta)
        {
            var dbKorisnik = await _context.Korisnici
                .Include(s => s.statistike)
                .Include(w => w.workouts)
                .FirstOrDefaultAsync(k => k.korisnikId == id);
            if (dbKorisnik == null)
            {
                return NotFound("Za trazeni ID ne postoji korisnik");
            }

            var work = await _context.Workouts.Include(c => c.korisnici).Include(k => k.vezbe).FirstOrDefaultAsync(k => k.workoutId == idWorkouta);

            dbKorisnik.workouts.Remove(work);

            await _context.SaveChangesAsync();

            return Ok(await GetDbKorisnici());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Korisnik>>> DeleteKorisnik(int id)
        {
            var dbKorisnik = await _context.Korisnici
                .Include(s => s.statistike)
                .FirstOrDefaultAsync(k => k.korisnikId == id);
            if (dbKorisnik == null)
            {
                return NotFound("Za trazeni ID ne postoji korisnik");
            }


            _context.Korisnici.Remove(dbKorisnik);
            await _context.SaveChangesAsync();

            return Ok(await GetDbKorisnici());
        }

        private async Task<List<Korisnik>> GetDbKorisnici()
        {
            return await _context.Korisnici.Include(s => s.statistike).Include(w => w.workouts).ToListAsync();
        }
    }
}
