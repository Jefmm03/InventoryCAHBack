using InventoryReactCAH.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryReactCAH.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CpusController : ControllerBase
    {

        private readonly ItinventoryModelContext dbContext;

        public CpusController(ItinventoryModelContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet]
        [Route("Lista")]

        public async Task<IActionResult> Get()
        {
            var cpuList = await dbContext.Cpus.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, cpuList);
        }

        [HttpGet]
        [Route("Obtener/{id:int}")]

        public async Task<IActionResult> Get(int id)
        {
            var cpu = await dbContext.Cpus.FirstOrDefaultAsync(e => e.Id == id);
            return StatusCode(StatusCodes.Status200OK, cpu);
        }

        [HttpPost]
        [Route("Nuevo")]

        public async Task<IActionResult> Nuevo([FromBody] Models.Cpu objeto)
        {
            await dbContext.Cpus.AddAsync(objeto);
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }

        [HttpPut]
        [Route("Editar")]

        public async Task<IActionResult> Editar([FromBody] Models.Cpu objeto)
        {
            dbContext.Cpus.Update(objeto);
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]

        public async Task<IActionResult> Eliminar(int id)
        {
            var cpu = await dbContext.Cpus.FirstOrDefaultAsync(e => e.Id == id);
            if (cpu == null)
            {
                return NotFound(new { mensaje = "Cpu not found" });
            }

            dbContext.Cpus.Remove(cpu);
            await dbContext.SaveChangesAsync();
            return Ok(new { mensaje = "Cpu deleted successfully" });
        }



    }
}
