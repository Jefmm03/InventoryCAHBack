
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
    public class BadgesController : ControllerBase
    {
        private readonly ItinventoryModelContext dbContext;

        public BadgesController(ItinventoryModelContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var badgeList = await dbContext.Badges.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, badgeList);
        }

        [HttpGet]
        [Route("Obtener/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var badge = await dbContext.Badges.FirstOrDefaultAsync(e => e.Id == id);
            if (badge == null)
            {
                return NotFound(new { mensaje = "Badge not found" });
            }
            return Ok(badge);
        }

        [HttpPost]
        [Route("Nuevo")]
        [Authorize]
        public async Task<IActionResult> Nuevo([FromBody] Badge objeto)
        {
            // Verifica si el objeto es válido
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Obtener el nombre del usuario autenticado
                var modifiedBy = User.Identity.Name;

                
                if (string.IsNullOrEmpty(modifiedBy))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Usuario no autenticado o nombre de usuario no disponible" });
                }

                objeto.ModifiedBy = modifiedBy;
                objeto.CreatedAt = DateTime.UtcNow;
                objeto.UpdatedAt = DateTime.UtcNow;

                await dbContext.Badges.AddAsync(objeto);
                await dbContext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status201Created, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al guardar el badge", error = ex.Message });
            }
        }



        [HttpPut]
        [Route("Editar")]
        [Authorize]
        public async Task<IActionResult> Editar([FromBody] Badge objeto)
        {
            // Verificar si el objeto es válido
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Obtener el registro existente de la base de datos
            var existingBadge = await dbContext.Badges.FindAsync(objeto.Id);

            if (existingBadge == null)
            {
                return NotFound(new { mensaje = "Badge not found" });
            }

            // Obtener el nombre del usuario autenticado
            var modifiedBy = User.Identity.Name;

            // Verifica si 'modifiedBy' es nulo
            if (string.IsNullOrEmpty(modifiedBy))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Usuario no autenticado o nombre de usuario no disponible" });
            }

            // Mantener el valor de CreatedAt
            objeto.CreatedAt = existingBadge.CreatedAt;

            
            objeto.ModifiedBy = modifiedBy;
            objeto.UpdatedAt = DateTime.UtcNow;

            // Marcar como modificada la entidad original para solo actualizar los campos modificados
            dbContext.Entry(existingBadge).CurrentValues.SetValues(objeto);

            await dbContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }


        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        [Authorize]
        public async Task<IActionResult> Eliminar(int id)
        {
            
            var badge = await dbContext.Badges.FirstOrDefaultAsync(e => e.Id == id);

            if (badge == null)
            {
                return NotFound(new { mensaje = "Badge not found" });
            }

            
            dbContext.Badges.Remove(badge);

            try
            {
                
                await dbContext.SaveChangesAsync();
                return Ok(new { mensaje = "Badge deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error deleting badge", error = ex.Message });
            }
        }



    }

}

