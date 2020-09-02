using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApp2.Models;

namespace TestApp2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Api2Controller : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public Api2Controller(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Api2
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BaseModel>>> GetBaseModel()
        {
            return await _context.BaseModel.ToListAsync();
        }

        // GET: api/Api2/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BaseModel>> GetBaseModel(Guid id)
        {
            var baseModel = await _context.BaseModel.FindAsync(id);

            if (baseModel == null)
            {
                return NotFound();
            }

            return baseModel;
        }

        // PUT: api/Api2/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBaseModel(Guid id, BaseModel baseModel)
        {
            if (id != baseModel.GUID)
            {
                return BadRequest();
            }

            _context.Entry(baseModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BaseModelExists(id))
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

        // POST: api/Api2
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<BaseModel>> PostBaseModel(BaseModel baseModel)
        {
            _context.BaseModel.Add(baseModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBaseModel", new { id = baseModel.GUID }, baseModel);
        }

        // DELETE: api/Api2/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseModel>> DeleteBaseModel(Guid id)
        {
            var baseModel = await _context.BaseModel.FindAsync(id);
            if (baseModel == null)
            {
                return NotFound();
            }

            _context.BaseModel.Remove(baseModel);
            await _context.SaveChangesAsync();

            return baseModel;
        }

        private bool BaseModelExists(Guid id)
        {
            return _context.BaseModel.Any(e => e.GUID == id);
        }
    }
}
