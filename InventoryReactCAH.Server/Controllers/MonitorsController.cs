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
    public class MonitorsController : ControllerBase
    {

        private readonly ItinventoryModelContext dbContext;

        public MonitorsController(ItinventoryModelContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var monitorList = await dbContext.Monitors.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, monitorList);
        }

        [HttpGet]
        [Route("Obtener/{id:int}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            var monitor = await dbContext.Monitors.FirstOrDefaultAsync(e => e.Id == id);
            return StatusCode(StatusCodes.Status200OK, monitor);
        }


        [HttpPost]
        [Route("Nuevo")]
        [Authorize]
        public async Task<IActionResult> Nuevo([FromBody] Models.Monitor objeto)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                
                var modifiedBy = User.Identity.Name;

                // Verifica si 'modifiedBy' es nulo
                if (string.IsNullOrEmpty(modifiedBy))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Usuario no autenticado o nombre de usuario no disponible" });
                }

                objeto.ModifiedBy = modifiedBy;
                objeto.CreatedAt = DateTime.UtcNow;
                objeto.UpdatedAt = DateTime.UtcNow;

                await dbContext.Monitors.AddAsync(objeto);
                await dbContext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status201Created, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al guardar el monitor", error = ex.Message });
            }
        }


        [HttpPut]
        [Route("Editar")]
        [Authorize]
        public async Task<IActionResult> Editar([FromBody] Models.Monitor objeto)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Obtener el registro existente de la base de datos
            var existingMonitor = await dbContext.Monitors.FindAsync(objeto.Id);

            if (existingMonitor == null)
            {
                return NotFound(new { mensaje = "Monitor not found" });
            }

            
            var modifiedBy = User.Identity.Name;

            
            if (string.IsNullOrEmpty(modifiedBy))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Usuario no autenticado o nombre de usuario no disponible" });
            }

            // Mantener el valor de CreatedAt del registro existente
            objeto.CreatedAt = existingMonitor.CreatedAt;

            
            objeto.ModifiedBy = modifiedBy;
            objeto.UpdatedAt = DateTime.UtcNow;

            // Marcar como modificada la entidad original para solo actualizar los campos modificados
            dbContext.Entry(existingMonitor).CurrentValues.SetValues(objeto);

            await dbContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        [Authorize]
        public async Task<IActionResult> Eliminar(int id)
        {
           
            var monitor = await dbContext.Monitors.FirstOrDefaultAsync(e => e.Id == id);

            if (monitor == null)
            {
                return NotFound(new { mensaje = "Monitor not found" });
            }

            
            dbContext.Monitors.Remove(monitor);

            try
            {
                
                await dbContext.SaveChangesAsync();
                return Ok(new { mensaje = "Monitor deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error deleting monitor", error = ex.Message });
            }
        }



    }
}
