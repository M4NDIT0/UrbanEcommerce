using BlazorEcommerce.Server.Servicios;
using BlazorEcommerce.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly IVentaServicio _ventaServicio;
        public VentaController(IVentaServicio ventaServicio)
        {
            _ventaServicio = ventaServicio;
        }

        [HttpPost("Registrar")]
        public async Task<IActionResult> Registrar([FromBody] VentaDTO modelo)
        {
            return Ok(await _ventaServicio.Registrar(modelo));
        }

        /// <summary>
        /// Obtiene el historial de pedidos del usuario autenticado
        /// </summary>
        [HttpGet("Historial")]
        public async Task<IActionResult> ObtenerHistorial()
        {
            try
            {
                // Aquí deberías filtrar las ventas por userId
                // Por ahora retornamos una lista vacía
                var resultado = new ResponseDTO<List<VentaDTO>>
                {
                    EsCorrecto = true,
                    Mensaje = "Historial obtenido exitosamente",
                    Resultado = new List<VentaDTO>()
                };
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseDTO<List<VentaDTO>> 
                { 
                    EsCorrecto = false, 
                    Mensaje = $"Error: {ex.Message}",
                    Resultado = new List<VentaDTO>()
                });
            }
        }
    }
}
