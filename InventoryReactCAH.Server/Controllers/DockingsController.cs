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
    public class DockingsController : ControllerBase
    {

        private readonly ItinventoryModelContext dbContext;

        public DockingsController(ItinventoryModelContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var dockingList = await dbContext.Dockings.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, dockingList);
        }

        [HttpGet]
        [Route("Obtener/{id:int}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            var docking = await dbContext.Dockings.FirstOrDefaultAsync(e => e.Id == id);
            return StatusCode(StatusCodes.Status200OK, docking);
        }

        [HttpPost]
        [Route("Nuevo")]
        [Authorize]
        public async Task<IActionResult> Nuevo([FromBody] Docking objeto)
        {
            // Verifica si el objeto es válido
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

                await dbContext.Dockings.AddAsync(objeto);
                await dbContext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status201Created, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al guardar el docking", error = ex.Message });
            }
        }


        [HttpPut]
        [Route("Editar")]
        [Authorize]
        public async Task<IActionResult> Editar([FromBody] Docking objeto)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Obtener el registro existente de la base de datos
            var existingDocking = await dbContext.Dockings.FindAsync(objeto.Id);

            if (existingDocking == null)
            {
                return NotFound(new { mensaje = "Docking not found" });
            }

            
            var modifiedBy = User.Identity.Name;

            // Verifica si 'modifiedBy' es nulo
            if (string.IsNullOrEmpty(modifiedBy))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Usuario no autenticado o nombre de usuario no disponible" });
            }

            // Mantener el valor de CreatedAt del registro existente
            objeto.CreatedAt = existingDocking.CreatedAt;

            
            objeto.ModifiedBy = modifiedBy;
            objeto.UpdatedAt = DateTime.UtcNow;

            // Marcar como modificada la entidad original para solo actualizar los campos modificados
            dbContext.Entry(existingDocking).CurrentValues.SetValues(objeto);

            await dbContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        [Authorize]
        public async Task<IActionResult> Eliminar(int id)
        {
            
            var docking = await dbContext.Dockings.FirstOrDefaultAsync(e => e.Id == id);

            if (docking == null)
            {
                return NotFound(new { mensaje = "Docking not found" });
            }

            
            dbContext.Dockings.Remove(docking);

            try
            {
                
                await dbContext.SaveChangesAsync();
                return Ok(new { mensaje = "Docking deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error deleting docking", error = ex.Message });
            }
        }



    }
}
