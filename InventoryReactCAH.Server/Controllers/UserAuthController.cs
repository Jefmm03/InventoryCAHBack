
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InventoryReactCAH.Server.DataAccess;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;

namespace InventoryReactCAH.Server.Controllers
{
    [Route("api/userAuth")]
    [ApiController]
    public class UserAuthController : ControllerBase
    {
        private readonly DataUsers dataUsers;
        private IConfiguration _config;

        public UserAuthController(DataUsers _dataUsers, IConfiguration _IConfiguration)
        {
            this.dataUsers = _dataUsers;
            this._config = _IConfiguration;
        }

        [HttpGet("Login/{userName}/{password}")]
        public async Task<IActionResult> Login(string userName, string password)
        {
            try
            {
                // Primero, validar si el usuario existe en la base de datos
                if (!this.dataUsers.UserExistsInDatabase(userName))
                {
                    return NotFound("User does not exist in the database."); 
                }

                // Si el usuario existe, validar las credenciales en Active Directory
                bool result = this.dataUsers.ValidateCredentials(userName, password, _config["ADDomain"], _config["ADUser"], _config["ADPass"]);

                if (result)
                {
                    // Si las credenciales son válidas, generar un token JWT
                    var token = GenerateJwtToken(userName);
                    return Ok(new { token });
                }
                else
                {
                    return Unauthorized("Invalid credentials.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        private string GenerateJwtToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, username),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.Name, username) 
    };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(40),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }





    }
}

