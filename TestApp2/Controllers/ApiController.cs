using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestApp2.Models;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace TestApp2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public ApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: Api
        [HttpPost]
        public async Task<ActionResult> PostBaseModel(BaseModel baseModel)
        {
            try
            {
                _context.BaseModel.Add(baseModel);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetBaseModel", new { id = baseModel.GUID }, baseModel);
            }
            catch (Exception ex)
            {
                _logger.Info("Ошибка: {0}", ex.Message);
                return BadRequest();
            }
        }

        // GET: Api
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BaseModel>>> GetBaseModel()
        {
            return await _context.BaseModel.ToListAsync();
        }

        // GET: Api/5
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
    }
}
