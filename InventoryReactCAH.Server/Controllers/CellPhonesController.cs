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
    public class CellPhonesController : ControllerBase
    {
        private readonly ItinventoryModelContext dbContext;

        public CellPhonesController(ItinventoryModelContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var cellPhoneList = await dbContext.CellPhones.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, cellPhoneList);
        }

        [HttpGet]
        [Route("Obtener/{id:int}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            var cellPhone = await dbContext.CellPhones.FirstOrDefaultAsync(e => e.Id == id);
            return StatusCode(StatusCodes.Status200OK, cellPhone);
        }

        [HttpPost]
        [Route("Nuevo")]
        [Authorize]
        public async Task<IActionResult> Nuevo([FromBody] CellPhone objeto)
        {
            
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

                await dbContext.CellPhones.AddAsync(objeto);
                await dbContext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status201Created, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al guardar el cellphone", error = ex.Message });
            }
        }


        [HttpPut]
        [Route("Editar")]
        [Authorize]
        public async Task<IActionResult> Editar([FromBody] CellPhone objeto)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            var existingCellPhone = await dbContext.CellPhones.FindAsync(objeto.Id);

            if (existingCellPhone == null)
            {
                return NotFound(new { mensaje = "CellPhone not found" });
            }

            
            var modifiedBy = User.Identity.Name;

            // Verifica si 'modifiedBy' es nulo
            if (string.IsNullOrEmpty(modifiedBy))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Usuario no autenticado o nombre de usuario no disponible" });
            }

            // Mantener el valor de CreatedAt del registro existente
            objeto.CreatedAt = existingCellPhone.CreatedAt;

            
            objeto.ModifiedBy = modifiedBy;
            objeto.UpdatedAt = DateTime.UtcNow;

            // Marcar como modificada la entidad original para solo actualizar los campos modificados
            dbContext.Entry(existingCellPhone).CurrentValues.SetValues(objeto);

            await dbContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }


        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        [Authorize]
        public async Task<IActionResult> Eliminar(int id)
        {
            
            var cellPhone = await dbContext.CellPhones.FirstOrDefaultAsync(e => e.Id == id);

            if (cellPhone == null)
            {
                return NotFound(new { mensaje = "Badge not found" });
            }

           
            dbContext.CellPhones.Remove(cellPhone);

            try
            {
                
                await dbContext.SaveChangesAsync();
                return Ok(new { mensaje = "CellPhone deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error deleting cellphone", error = ex.Message });
            }
        }


    }
}

        /*
        [HttpPost] 
        [Route("Restaurar/{id:int}")]
        public async Task<IActionResult> Restaurar(int id)
        {
            var cellphone = await dbContext.CellPhones.FirstOrDefaultAsync(e => e.Id == id);
            if (cellphone == null)
            {
                return NotFound(new { mensaje = "Cellphone not found" });
            }

            // Crear un histórico del objeto celular antes de restaurarlo
            CellPhone history = new CellPhone
            {
                Imei = cellphone.Imei,
                Model = cellphone.Model,
                Number = cellphone.Number,
                User = cellphone.User,
                Pin = cellphone.Pin,
                Puk = cellphone.Puk,
                IcloudPass = cellphone.IcloudPass,
                IcloudUser = cellphone.IcloudUser,
                Comment = cellphone.Comment,
                ParentId = cellphone.Id,
                CreatedAt = cellphone.CreatedAt,
                UpdatedAt = DateTime.Now,
               // ModifiedBy = "System" 
            };

            dbContext.CellPhones.Add(history);

            // Restaurar el celular original
            cellphone.DeletedAt = null;
            cellphone.UpdatedAt = DateTime.Now;
            //cellphone.ModifiedBy = "System"; 

            await dbContext.SaveChangesAsync();

            return Ok(new { mensaje = "Cellphone restored successfully" });
        }
        */