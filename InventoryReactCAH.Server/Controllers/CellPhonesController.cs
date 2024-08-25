using InventoryReactCAH.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryReactCAH.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CellPhonesController : ControllerBase
    {
        private readonly ItinventoryModelContext dbContext;

        public CellPhonesController(ItinventoryModelContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet]
        [Route("Lista")]

        public async Task<IActionResult> Get()
        {
            var cellPhoneList = await dbContext.CellPhones.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, cellPhoneList);
        }

        [HttpGet]
        [Route("Obtener/{id:int}")]

        public async Task<IActionResult> Get(int id)
        {
            var cellPhone = await dbContext.CellPhones.FirstOrDefaultAsync(e => e.Id == id);
            return StatusCode(StatusCodes.Status200OK, cellPhone);
        }

        [HttpPost]
        [Route("Nuevo")]

        public async Task<IActionResult> Nuevo([FromBody] CellPhone objeto)
        {
            await dbContext.CellPhones.AddAsync(objeto);
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }

        [HttpPut]
        [Route("Editar")]

        public async Task<IActionResult> Editar([FromBody] CellPhone objeto)
        {
            dbContext.CellPhones.Update(objeto);
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]

        public async Task<IActionResult> Eliminar(int id)
        {
            var cellPhone = await dbContext.CellPhones.FirstOrDefaultAsync(e => e.Id == id);
            if (cellPhone == null)
            {
                return NotFound(new { mensaje = "Cellphone not found" });
            }

            dbContext.CellPhones.Remove(cellPhone);
            await dbContext.SaveChangesAsync();
            return Ok(new { mensaje = "CellPhone deleted successfully" });
        }

    }
}
