using InventoryReactCAH.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryReactCAH.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BadgesController : ControllerBase
    {
        private readonly ItinventoryModelContext dbContext;

        public BadgesController(ItinventoryModelContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Get()
        {
            var badgeList = await dbContext.Badges.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, badgeList);
        }

        [HttpGet]
        [Route("Obtener/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var badge = await dbContext.Badges.FirstOrDefaultAsync(e => e.Id == id);
            if (badge == null)
            {
                return NotFound(new { mensaje = "Badge not found" });
            }
            return Ok(badge);
        }
        /*
        [HttpGet]
        [Route("Historico/{id:int}")]
        public async Task<IActionResult> Obtain(int id)
        {
            var badge = await dbContext.Badges.Include(b => b.History).FirstOrDefaultAsync(e => e.Id == id);
            if (badge == null)
            {
                return NotFound(new { mensaje = " Badge not found" });
            }
            return Ok(badge);

        }
        */


        [HttpPost]
        [Route("Nuevo")]
        public async Task<IActionResult> Nuevo([FromBody] Badge objeto)
        {
            await dbContext.Badges.AddAsync(objeto);
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] Badge objeto)
        {
            dbContext.Badges.Update(objeto);
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var badge = await dbContext.Badges.FirstOrDefaultAsync(e => e.Id == id);
            if (badge == null)
            {
                return NotFound(new { mensaje = "Badge not found" });
            }

            dbContext.Badges.Remove(badge);
            await dbContext.SaveChangesAsync();
            return Ok(new { mensaje = "Badge deleted successfully" });
        }
    }
}
