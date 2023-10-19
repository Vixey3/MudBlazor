using DiplomskiBlazor.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiplomskiBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VezbaController : ControllerBase
    {
        private readonly DataContext _context;

        public VezbaController(DataContext context)
        {
            _context = context;
        }


        /*[HttpGet]
        public async Task<ActionResult<List<Vezba>>> Get(int dtID)
        {
            var vezba = await _context.Vezbe.Include(c => c.deoTela).ToListAsync();
            return Ok(vezbe);
        }
        */
        [Route("vezbe")]
        [HttpGet]
        public async Task<ActionResult<List<Vezba>>> GetVezbe()
        {
            var vezbe = await _context.Vezbe.Include(c => c.deoTela).ToListAsync();
            return Ok(vezbe);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vezba>> GetSingleVezba(int id)
        {
            var vezba = await _context.Vezbe.Include(k => k.deoTela).FirstOrDefaultAsync(k => k.vezbaId == id);
            if (vezba == null)
            {
                return NotFound("Vezba nije pronadjena");
            }
            return Ok(vezba);
        }

        [HttpGet("vw/{idk}")]
        public async Task<ActionResult<List<Workout>>> GetVezbeWorkout(int idk)
        {

            var vezbe = await _context.Vezbe.Include(w => w.workouts)
                .Include(s => s.deoTela)
                .FirstOrDefaultAsync(k => k.vezbaId == idk);
            if (vezbe == null)
            {
                return NotFound("Vezba nije pronadjena");
            }

            var work = vezbe.workouts;
            if (work == null)
            {
                return NotFound("Vezba nema workout");
            }
            return Ok(work);
        }

        [HttpPost]
        public async Task<ActionResult<List<Vezba>>> CreateVezba(VezbaDto v)
        {
            var deoTela = await _context.DeloviTela.FindAsync(v.deoTelaId);
            var vezba = new Vezba();
            vezba.nazivVezbe = v.nazivVezbe;
            vezba.opisVezbe = v.opisVezbe;
            vezba.brojSerijaPonavljanja = v.brojSerijaPonavljanja;
            vezba.deoTela = deoTela;
            vezba.deoTelaId = v.deoTelaId;
            vezba.workouts = new List<Workout>();

            _context.Vezbe.Add(vezba);
            await _context.SaveChangesAsync();

            return Ok(await GetDbVezbe());
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<List<Vezba>>> UpdateVezba(VezbaDto k, int id)
        {
            var dbVezba = await _context.Vezbe
                .Include(k => k.deoTela)
                .FirstOrDefaultAsync(k => k.vezbaId == id);
            if (dbVezba == null)
            {
                return NotFound("Za trazeni ID ne postoji vezba");
            }

            dbVezba.nazivVezbe = k.nazivVezbe;
            dbVezba.opisVezbe = k.opisVezbe;
            dbVezba.brojSerijaPonavljanja = k.brojSerijaPonavljanja;
            dbVezba.deoTelaId = k.deoTelaId;
            await _context.SaveChangesAsync();

            return Ok(await GetDbVezbe());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Vezba>>> DeleteVezba(int id)
        {
            var dbVezba = await _context.Vezbe
                .Include(k => k.deoTela)
                .FirstOrDefaultAsync(k => k.vezbaId == id);
            if (dbVezba == null)
            {
                return NotFound("Za trazeni ID ne postoji vezba");
            }


            _context.Vezbe.Remove(dbVezba);
            await _context.SaveChangesAsync();

            return Ok(await GetDbVezbe());
        }


        private async Task<List<Vezba>> GetDbVezbe()
        {
            return await _context.Vezbe.Include(c => c.deoTela).ToListAsync();
        }

    }
}