using InventoryReactCAH.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryReactCAH.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RicohsController : ControllerBase
    {

        private readonly ItinventoryModelContext dbContext;

        public RicohsController(ItinventoryModelContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var ricohList = await dbContext.Ricohs.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, ricohList);
        }

        [HttpGet]
        [Route("Obtener/{id:int}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            var ricoh = await dbContext.Ricohs.FirstOrDefaultAsync(e => e.Id == id);
            return StatusCode(StatusCodes.Status200OK, ricoh);
        }

        [HttpPost]
        [Route("Nuevo")]
        [Authorize]
        public async Task<IActionResult> Nuevo([FromBody] Ricoh objeto)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                
                var modifiedBy = User.Identity.Name;

                if (string.IsNullOrEmpty(modifiedBy))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Usuario no autenticado o nombre de usuario no disponible" });
                }

                objeto.ModifiedBy = modifiedBy;
                objeto.CreatedAt = DateTime.UtcNow;
                objeto.UpdatedAt = DateTime.UtcNow;

                await dbContext.Ricohs.AddAsync(objeto);
                await dbContext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status201Created, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al guardar el ricoh", error = ex.Message });
            }
        }


        [HttpPut]
        [Route("Editar")]
        [Authorize]
        public async Task<IActionResult> Editar([FromBody] Ricoh objeto)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            var existingRicoh = await dbContext.Ricohs.FindAsync(objeto.Id);

            if (existingRicoh == null)
            {
                return NotFound(new { mensaje = "Ricoh not found" });
            }

            
            var modifiedBy = User.Identity.Name;

            
            if (string.IsNullOrEmpty(modifiedBy))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Usuario no autenticado o nombre de usuario no disponible" });
            }

            // Mantener el valor de CreatedAt del registro existente
            objeto.CreatedAt = existingRicoh.CreatedAt;

            
            objeto.ModifiedBy = modifiedBy;
            objeto.UpdatedAt = DateTime.UtcNow;

            // Marcar como modificada la entidad original para solo actualizar los campos modificados
            dbContext.Entry(existingRicoh).CurrentValues.SetValues(objeto);

            await dbContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        [Authorize]
        public async Task<IActionResult> Eliminar(int id)
        {
            
            var ricoh = await dbContext.Ricohs.FirstOrDefaultAsync(e => e.Id == id);

            if (ricoh == null)
            {
                return NotFound(new { mensaje = "Ricoh not found" });
            }

            
            dbContext.Ricohs.Remove(ricoh);

            try
            {
                // Guardar los cambios en la base de datos
                await dbContext.SaveChangesAsync();
                return Ok(new { mensaje = "Ricoh deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error deleting ricoh", error = ex.Message });
            }
        }

    }
}
