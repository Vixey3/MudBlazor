using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiplomskiBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeoTelaController : ControllerBase
    {
        private readonly DataContext _context;

        public DeoTelaController(DataContext context)
        {
            _context = context;
        }

        [Route("delovitela")]
        [HttpGet]
        public async Task<ActionResult<List<DeoTela>>> GetDeloviTela()
        {
            var deloviTela = await _context.DeloviTela.ToListAsync();
            return Ok(deloviTela);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DeoTela>> GetSingleDeoTela(int id)
        {
            var deoTela = await _context.DeloviTela.FirstOrDefaultAsync(k => k.deoTelaId == id);
            if (deoTela == null)
            {
                return NotFound("Deo tela nije pronadjen");
            }
            return Ok(deoTela);
        }

        [HttpPost]
        public async Task<ActionResult<List<DeoTela>>> CreateDeoTela(DeoTela k) // korisnikDto se slao
        {
            var deotela = new DeoTela();
            deotela.nazivTela = k.nazivTela;
            deotela.vezbe = new List<Vezba>();

            _context.DeloviTela.Add(deotela);
            await _context.SaveChangesAsync();

            return Ok(await GetDbDeloviTela());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<DeoTela>>> UpdateDeoTela(DeoTela k, int id) // korisnikDto se slao
        {
            var dbDeoTela = await _context.DeloviTela
                .FirstOrDefaultAsync(k => k.deoTelaId == id);
            if (dbDeoTela == null)
            {
                return NotFound("Za trazeni ID ne postoji deo tela");
            }

            dbDeoTela.nazivTela = k.nazivTela;

            await _context.SaveChangesAsync();

            return Ok(await GetDbDeloviTela());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<DeoTela>>> DeleteDeoTela(int id)
        {
            var dbDeoTela = await _context.DeloviTela
                .FirstOrDefaultAsync(k => k.deoTelaId == id);
            if (dbDeoTela == null)
            {
                return NotFound("Za trazeni ID ne postoji deo tela");
            }


            _context.DeloviTela.Remove(dbDeoTela);
            await _context.SaveChangesAsync();

            return Ok(await GetDbDeloviTela());
        }

        private async Task<List<DeoTela>> GetDbDeloviTela()
        {
            return await _context.DeloviTela.ToListAsync();
        }
    }
}