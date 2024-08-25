using InventoryReactCAH.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryReactCAH.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RicohsController : ControllerBase
    {

        private readonly ItinventoryModelContext dbContext;

        public RicohsController(ItinventoryModelContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet]
        [Route("Lista")]

        public async Task<IActionResult> Get()
        {
            var ricohList = await dbContext.Ricohs.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, ricohList);
        }

        [HttpGet]
        [Route("Obtener/{id:int}")]

        public async Task<IActionResult> Get(int id)
        {
            var ricoh = await dbContext.Ricohs.FirstOrDefaultAsync(e => e.Id == id);
            return StatusCode(StatusCodes.Status200OK, ricoh);
        }

        [HttpPost]
        [Route("Nuevo")]

        public async Task<IActionResult> Nuevo([FromBody] Ricoh objeto)
        {
            await dbContext.Ricohs.AddAsync(objeto);
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }

        [HttpPut]
        [Route("Editar")]

        public async Task<IActionResult> Editar([FromBody] Ricoh objeto)
        {
            dbContext.Ricohs.Update(objeto);
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]

        public async Task<IActionResult> Eliminar(int id)
        {
            var ricoh = await dbContext.Ricohs.FirstOrDefaultAsync(e => e.Id == id);
            if (ricoh == null)
            {
                return NotFound(new { mensaje = "Ricoh not found" });
            }

            dbContext.Ricohs.Remove(ricoh);
            await dbContext.SaveChangesAsync();
            return Ok(new { mensaje = "Ricoh deleted successfully" });
        }


    }
}
