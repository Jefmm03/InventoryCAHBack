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
    public class TelephonesController : ControllerBase
    {

        private readonly ItinventoryModelContext dbContext;

        public TelephonesController(ItinventoryModelContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var telephoneList = await dbContext.Telephones.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, telephoneList);
        }

        [HttpGet]
        [Route("Obtener/{id:int}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            var telephone = await dbContext.Telephones.FirstOrDefaultAsync(e => e.Id == id);
            return StatusCode(StatusCodes.Status200OK, telephone);
        }

        [HttpPost]
        [Route("Nuevo")]
        [Authorize]
        public async Task<IActionResult> Nuevo([FromBody] Telephone objeto)
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

                await dbContext.Telephones.AddAsync(objeto);
                await dbContext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status201Created, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al guardar el telephone", error = ex.Message });
            }
        }


        [HttpPut]
        [Route("Editar")]
        [Authorize]
        public async Task<IActionResult> Editar([FromBody] Telephone objeto)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            var existingTelephone = await dbContext.Telephones.FindAsync(objeto.Id);

            if (existingTelephone == null)
            {
                return NotFound(new { mensaje = "Telephone not found" });
            }

            
            var modifiedBy = User.Identity.Name;

            
            if (string.IsNullOrEmpty(modifiedBy))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Usuario no autenticado o nombre de usuario no disponible" });
            }

            // Mantener el valor de CreatedAt del registro existente
            objeto.CreatedAt = existingTelephone.CreatedAt;

            
            objeto.ModifiedBy = modifiedBy;
            objeto.UpdatedAt = DateTime.UtcNow;

            // Marcar como modificada la entidad original para solo actualizar los campos modificados
            dbContext.Entry(existingTelephone).CurrentValues.SetValues(objeto);

            await dbContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        [Authorize]
        public async Task<IActionResult> Eliminar(int id)
        {
            
            var telephone = await dbContext.Telephones.FirstOrDefaultAsync(e => e.Id == id);

            if (telephone == null)
            {
                return NotFound(new { mensaje = "Telephone not found" });
            }

            
            dbContext.Telephones.Remove(telephone);

            try
            {
                
                await dbContext.SaveChangesAsync();
                return Ok(new { mensaje = "Telephone deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error deleting telephone", error = ex.Message });
            }
        }


    }
}
