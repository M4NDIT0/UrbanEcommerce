using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchivoController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        public ArchivoController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost("subir")]
        public async Task<IActionResult> SubirImagen(IFormFile archivo)
        {
            try
            {
                if (archivo == null || archivo.Length == 0)
                {
                    return BadRequest(new { mensaje = "No se recibió ningún archivo" });
                }

                // Validar tipo de archivo
                var extensionesPermitidas = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                var extension = Path.GetExtension(archivo.FileName).ToLowerInvariant();

                if (!extensionesPermitidas.Contains(extension))
                {
                    return BadRequest(new { mensaje = "Solo se permiten archivos de imagen (jpg, jpeg, png, gif, webp)" });
                }

                // Validar tamaño (máximo 5MB)
                if (archivo.Length > 5 * 1024 * 1024)
                {
                    return BadRequest(new { mensaje = "El archivo no puede superar los 5MB" });
                }

                // Crear directorio si no existe
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "productos");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Generar nombre único para el archivo
                var nombreUnico = $"{Guid.NewGuid()}{extension}";
                var rutaArchivo = Path.Combine(uploadsFolder, nombreUnico);

                // Guardar archivo
                using (var stream = new FileStream(rutaArchivo, FileMode.Create))
                {
                    await archivo.CopyToAsync(stream);
                }

                // Retornar la URL relativa del archivo
                var url = $"/images/productos/{nombreUnico}";

                return Ok(new { url = url, mensaje = "Imagen subida correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error al subir la imagen: {ex.Message}" });
            }
        }

        [HttpDelete("eliminar")]
        public IActionResult EliminarImagen([FromQuery] string url)
        {
            try
            {
                if (string.IsNullOrEmpty(url))
                {
                    return BadRequest(new { mensaje = "URL no proporcionada" });
                }

                // Extraer el nombre del archivo de la URL
                var nombreArchivo = Path.GetFileName(url);
                var rutaArchivo = Path.Combine(_environment.WebRootPath, "images", "productos", nombreArchivo);

                if (System.IO.File.Exists(rutaArchivo))
                {
                    System.IO.File.Delete(rutaArchivo);
                    return Ok(new { mensaje = "Imagen eliminada correctamente" });
                }

                return NotFound(new { mensaje = "Archivo no encontrado" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error al eliminar la imagen: {ex.Message}" });
            }
        }
    }
}
