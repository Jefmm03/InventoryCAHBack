using InventoryReactCAH.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryReactCAH.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonitorsController : ControllerBase
    {

        private readonly ItinventoryModelContext dbContext;

        public MonitorsController(ItinventoryModelContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet]
        [Route("Lista")]

        public async Task<IActionResult> Get()
        {
            var monitorList = await dbContext.Monitors.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, monitorList);
        }

        [HttpGet]
        [Route("Obtener/{id:int}")]

        public async Task<IActionResult> Get(int id)
        {
            var monitor = await dbContext.Monitors.FirstOrDefaultAsync(e => e.Id == id);
            return StatusCode(StatusCodes.Status200OK, monitor);
        }

        [HttpPost]
        [Route("Nuevo")]

        public async Task<IActionResult> Nuevo([FromBody] Models.Monitor objeto)
        {
            await dbContext.Monitors.AddAsync(objeto);
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }

        [HttpPut]
        [Route("Editar")]

        public async Task<IActionResult> Editar([FromBody] Models.Monitor objeto)
        {
            dbContext.Monitors.Update(objeto);
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]

        public async Task<IActionResult> Eliminar(int id)
        {
            var monitor = await dbContext.Monitors.FirstOrDefaultAsync(e => e.Id == id);
            if (monitor == null)
            {
                return NotFound(new { mensaje = "Monitor not found" });
            }

            dbContext.Monitors.Remove(monitor);
            await dbContext.SaveChangesAsync();
            return Ok(new { mensaje = "Monitor deleted successfully" });
        }


    }
}
