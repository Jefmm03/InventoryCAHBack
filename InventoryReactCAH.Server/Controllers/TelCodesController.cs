using InventoryReactCAH.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryReactCAH.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelCodesController : ControllerBase
    {

        private readonly ItinventoryModelContext dbContext;

        public TelCodesController(ItinventoryModelContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet]
        [Route("Lista")]

        public async Task<IActionResult> Get()
        {
            var telCodeList = await dbContext.TelCodes.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, telCodeList);
        }

        [HttpGet]
        [Route("Obtener/{id:int}")]

        public async Task<IActionResult> Get(int id)
        {
            var telCode = await dbContext.TelCodes.FirstOrDefaultAsync(e => e.Id == id);
            return StatusCode(StatusCodes.Status200OK, telCode);
        }

        [HttpPost]
        [Route("Nuevo")]

        public async Task<IActionResult> Nuevo([FromBody] TelCode objeto)
        {
            await dbContext.TelCodes.AddAsync(objeto);
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }

        [HttpPut]
        [Route("Editar")]

        public async Task<IActionResult> Editar([FromBody] TelCode objeto)
        {
            dbContext.TelCodes.Update(objeto);
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]

        public async Task<IActionResult> Eliminar(int id)
        {
            var telCode = await dbContext.TelCodes.FirstOrDefaultAsync(e => e.Id == id);
            if (telCode == null)
            {
                return NotFound(new { mensaje = "TelCode not found" });
            }

            dbContext.TelCodes.Remove(telCode);
            await dbContext.SaveChangesAsync();
            return Ok(new { mensaje = "TelCode deleted successfully" });
        }


    }
}
