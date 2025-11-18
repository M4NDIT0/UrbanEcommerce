using BlazorEcommerce.Server.Servicios.PersonaSV;
using BlazorEcommerce.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly IPersonaServicio _personaServicio;
        public PersonaController(IPersonaServicio personaServicio)
        {
            _personaServicio = personaServicio;
        }

        [HttpGet("Lista/{Rol:alpha}/{Valor:alpha?}")]
        public async Task<IActionResult> Lista(string Rol, string Valor = "NA")
        {
            if (Valor == "NA") Valor = "";
            return Ok(await _personaServicio.Lista(Rol, Valor));
        }

        [HttpGet("Obtener/{Id:int}")]
        public async Task<IActionResult> Obtener(int Id)
        {
            return Ok(await _personaServicio.Obtener(Id));
        }

        [HttpPost("Crear")]
        public async Task<IActionResult> Crear([FromBody] PersonaDTO modelo)
        {
            return Ok(await _personaServicio.Crear(modelo));
        }

        [HttpPost("Autorizacion")]
        public async Task<IActionResult> Autorizacion([FromBody]LoginDTO modelo)
        {
            return Ok(await _personaServicio.Autorizacion(modelo));
        }

        [HttpPut("Editar")]
        public async Task<IActionResult> Editar([FromBody] PersonaDTO modelo)
        {
            return Ok(await _personaServicio.Editar(modelo));
        }

        [HttpDelete("Eliminar/{Id:int}")]
        public async Task<IActionResult> Eliminar(int Id)
        {
            return Ok(await _personaServicio.Eliminar(Id));
        }

        /// <summary>
        /// Obtiene el perfil del usuario autenticado
        /// </summary>
        [HttpGet("Perfil/{id:int}")]
        public async Task<IActionResult> ObtenerPerfil(int id)
        {
            try
            {
                return Ok(await _personaServicio.Obtener(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseDTO<PersonaDTO> 
                { 
                    EsCorrecto = false, 
                    Mensaje = $"Error: {ex.Message}" 
                });
            }
        }

        /// <summary>
        /// Actualiza el perfil del usuario autenticado
        /// </summary>
        [HttpPut("Perfil")]
        public async Task<IActionResult> ActualizarPerfil([FromBody] PersonaDTO modelo)
        {
            try
            {
                return Ok(await _personaServicio.Editar(modelo));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseDTO<PersonaDTO> 
                { 
                    EsCorrecto = false, 
                    Mensaje = $"Error: {ex.Message}" 
                });
            }
        }

        /// <summary>
        /// Cambia la contraseña del usuario autenticado
        /// </summary>
        [HttpPost("cambiar-contraseña/{idPersona:int}")]
        public async Task<IActionResult> CambiarContraseña(int idPersona, [FromBody] ChangePasswordDTO modelo)
        {
            try
            {
                var resultado = await _personaServicio.CambiarContraseña(idPersona, modelo);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseDTO<bool> 
                { 
                    EsCorrecto = false, 
                    Mensaje = $"Error: {ex.Message}" 
                });
            }
        }

    }
}
