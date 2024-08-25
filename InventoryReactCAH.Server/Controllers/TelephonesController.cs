using InventoryReactCAH.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryReactCAH.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelephonesController : ControllerBase
    {

        private readonly ItinventoryModelContext dbContext;

        public TelephonesController(ItinventoryModelContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet]
        [Route("Lista")]

        public async Task<IActionResult> Get()
        {
            var telephoneList = await dbContext.Telephones.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, telephoneList);
        }

        [HttpGet]
        [Route("Obtener/{id:int}")]

        public async Task<IActionResult> Get(int id)
        {
            var telephone = await dbContext.Telephones.FirstOrDefaultAsync(e => e.Id == id);
            return StatusCode(StatusCodes.Status200OK, telephone);
        }

        [HttpPost]
        [Route("Nuevo")]

        public async Task<IActionResult> Nuevo([FromBody] Telephone objeto)
        {
            await dbContext.Telephones.AddAsync(objeto);
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }

        [HttpPut]
        [Route("Editar")]

        public async Task<IActionResult> Editar([FromBody] Telephone objeto)
        {
            dbContext.Telephones.Update(objeto);
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]

        public async Task<IActionResult> Eliminar(int id)
        {
            var telephone = await dbContext.Telephones.FirstOrDefaultAsync(e => e.Id == id);
            if (telephone == null)
            {
                return NotFound(new { mensaje = "Telephone not found" });
            }

            dbContext.Telephones.Remove(telephone);
            await dbContext.SaveChangesAsync();
            return Ok(new { mensaje = "Telephone deleted successfully" });
        }


    }
}
