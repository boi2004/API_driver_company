using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Driver_Company_5._0.Models;
using Driver_Company_5._0.Infrastructure.Data;

namespace Driver_Company_5._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivestreamsController : ControllerBase
    {
        private readonly DriverManagementContext _context;

        public LivestreamsController(DriverManagementContext context)
        {
            _context = context;
        }

        // GET: api/Livestreams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Livestream>>> GetLivestreams()
        {
            return await _context.Livestreams.ToListAsync();
        }

        // GET: api/Livestreams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Livestream>> GetLivestream(int id)
        {
            var livestream = await _context.Livestreams.FindAsync(id);

            if (livestream == null)
            {
                return NotFound();
            }

            return livestream;
        }

        // PUT: api/Livestreams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLivestream(int id, Livestream livestream)
        {
            if (id != livestream.Id)
            {
                return BadRequest();
            }

            _context.Entry(livestream).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LivestreamExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Livestreams
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Livestream>> PostLivestream(Livestream livestream)
        {
            _context.Livestreams.Add(livestream);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLivestream", new { id = livestream.Id }, livestream);
        }

        // DELETE: api/Livestreams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLivestream(int id)
        {
            var livestream = await _context.Livestreams.FindAsync(id);
            if (livestream == null)
            {
                return NotFound();
            }

            _context.Livestreams.Remove(livestream);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LivestreamExists(int id)
        {
            return _context.Livestreams.Any(e => e.Id == id);
        }
    }
}
