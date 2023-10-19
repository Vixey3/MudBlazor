using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiplomskiBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatistikaController : ControllerBase
    {
        private readonly DataContext _context;

        public StatistikaController(DataContext context)
        {
            _context = context;
        }

        [Route("statistike")]
        [HttpGet]
        public async Task<ActionResult<List<Statistika>>> GetStatistike()
        {
            var statistike = await _context.Statistike.Include(c => c.korisnik).ToListAsync();
            return Ok(statistike);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Statistika>> GetSingleStatistika(int id)
        {
            var statistika = await _context.Statistike.Include(c => c.korisnik).FirstOrDefaultAsync(k => k.statistikaId == id);
            if (statistika == null)
            {
                return NotFound("Statistika nije pronadjen");
            }
            return Ok(statistika);
        }

        [HttpPost]
        public async Task<ActionResult<List<Statistika>>> CreateStatistika(StatistikaDto k)
        {
            var stats = new Statistika();
            var user = await _context.Korisnici.FindAsync(k.korisnikId);
            stats.visina = k.visina;
            stats.tezina = k.tezina;
            stats.obimGrudi = k.obimGrudi;
            stats.obimKukova = k.obimKukova;
            stats.obimStruka = k.obimStruka;
            stats.datum = k.datum;
            stats.korisnik = user;
            stats.korisnikId = k.korisnikId;

            _context.Statistike.Add(stats);
            await _context.SaveChangesAsync();

            return Ok(await GetDbStatistike());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Statistika>>> UpdateStatistika(StatistikaDto k, int id)
        {
            var dbStats = await _context.Statistike
                .Include(c => c.korisnik)
                .FirstOrDefaultAsync(k => k.statistikaId == id);
            if (dbStats == null)
            {
                return NotFound("Za trazeni ID ne postoji statistika");
            }

            dbStats.tezina = k.tezina;
            dbStats.obimGrudi = k.obimGrudi;
            dbStats.obimKukova = k.obimKukova;
            dbStats.obimStruka = k.obimStruka;
            await _context.SaveChangesAsync();

            return Ok(await GetDbStatistike());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Statistika>>> DeleteStatistika(int id)
        {
            var dbStatistika = await _context.Statistike
                .Include(c => c.korisnik)
                .FirstOrDefaultAsync(k => k.statistikaId == id);
            if (dbStatistika == null)
            {
                return NotFound("Za trazeni ID ne postoji statistika");
            }


            _context.Statistike.Remove(dbStatistika);
            await _context.SaveChangesAsync();

            return Ok(await GetDbStatistike());
        }

        private async Task<List<Statistika>> GetDbStatistike()
        {
            return await _context.Statistike.Include(c => c.korisnik).ToListAsync();
        }
    }
}