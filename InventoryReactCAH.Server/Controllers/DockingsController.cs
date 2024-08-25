using InventoryReactCAH.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryReactCAH.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DockingsController : ControllerBase
    {

        private readonly ItinventoryModelContext dbContext;

        public DockingsController(ItinventoryModelContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet]
        [Route("Lista")]

        public async Task<IActionResult> Get()
        {
            var dockingList = await dbContext.Dockings.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, dockingList);
        }

        [HttpGet]
        [Route("Obtener/{id:int}")]

        public async Task<IActionResult> Get(int id)
        {
            var docking = await dbContext.Dockings.FirstOrDefaultAsync(e => e.Id == id);
            return StatusCode(StatusCodes.Status200OK, docking);
        }

        [HttpPost]
        [Route("Nuevo")]

        public async Task<IActionResult> Nuevo([FromBody] Docking objeto)
        {
            await dbContext.Dockings.AddAsync(objeto);
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }

        [HttpPut]
        [Route("Editar")]

        public async Task<IActionResult> Editar([FromBody] Docking objeto)
        {
            dbContext.Dockings.Update(objeto);
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]

        public async Task<IActionResult> Eliminar(int id)
        {
            var docking = await dbContext.Dockings.FirstOrDefaultAsync(e => e.Id == id);
            if (docking == null)
            {
                return NotFound(new { mensaje = "Docking not found" });
            }

            dbContext.Dockings.Remove(docking);
            await dbContext.SaveChangesAsync();
            return Ok(new { mensaje = "Docking deleted successfully" });
        }




    }
}
