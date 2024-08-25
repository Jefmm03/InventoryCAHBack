using InventoryReactCAH.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryReactCAH.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaticIpsController : ControllerBase
    {

        private readonly ItinventoryModelContext dbContext;

        public StaticIpsController(ItinventoryModelContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet]
        [Route("Lista")]

        public async Task<IActionResult> Get()
        {
            var staticIpList = await dbContext.StaticIps.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, staticIpList);
        }

        [HttpGet]
        [Route("Obtener/{id:int}")]

        public async Task<IActionResult> Get(int id)
        {
            var staticIp = await dbContext.StaticIps.FirstOrDefaultAsync(e => e.Id == id);
            return StatusCode(StatusCodes.Status200OK, staticIp);
        }

        [HttpPost]
        [Route("Nuevo")]

        public async Task<IActionResult> Nuevo([FromBody] StaticIp objeto)
        {
            await dbContext.StaticIps.AddAsync(objeto);
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }

        [HttpPut]
        [Route("Editar")]

        public async Task<IActionResult> Editar([FromBody] StaticIp objeto)
        {
            dbContext.StaticIps.Update(objeto);
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]

        public async Task<IActionResult> Eliminar(int id)
        {
            var staticIp = await dbContext.StaticIps.FirstOrDefaultAsync(e => e.Id == id);
            if (staticIp == null)
            {
                return NotFound(new { mensaje = "StaticIp not found" });
            }

            dbContext.StaticIps.Remove(staticIp);
            await dbContext.SaveChangesAsync();
            return Ok(new { mensaje = "StaticIp deleted successfully" });
        }



    }
}
