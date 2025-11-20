using BlazorEcommerce.Server.Servicios;
using BlazorEcommerce.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoVarianteController : ControllerBase
    {
        private readonly IProductoVarianteServicio _varianteServicio;

        public ProductoVarianteController(IProductoVarianteServicio varianteServicio)
        {
            _varianteServicio = varianteServicio;
        }

        [HttpGet("Producto/{idProducto:int}")]
        public async Task<IActionResult> ObtenerPorProducto(int idProducto)
        {
            var response = await _varianteServicio.ObtenerPorProducto(idProducto);
            return Ok(response);
        }

        [HttpGet("{idVariante:int}")]
        public async Task<IActionResult> Obtener(int idVariante)
        {
            var response = await _varianteServicio.Obtener(idVariante);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] ProductoVarianteDTO variante)
        {
            var response = await _varianteServicio.Crear(variante);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] ProductoVarianteDTO variante)
        {
            var response = await _varianteServicio.Editar(variante);
            return Ok(response);
        }

        [HttpDelete("{idVariante:int}")]
        public async Task<IActionResult> Eliminar(int idVariante)
        {
            var response = await _varianteServicio.Eliminar(idVariante);
            return Ok(response);
        }

        [HttpPost("Imagen")]
        public async Task<IActionResult> AgregarImagen([FromBody] ProductoImagenDTO imagen)
        {
            var response = await _varianteServicio.AgregarImagen(imagen);
            return Ok(response);
        }

        [HttpDelete("Imagen/{idImagen:int}")]
        public async Task<IActionResult> EliminarImagen(int idImagen)
        {
            var response = await _varianteServicio.EliminarImagen(idImagen);
            return Ok(response);
        }
    }
}
