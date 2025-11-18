using BlazorEcommerce.Shared;
using BlazorEcommerce.Client.Extensiones;
using System.Net.Http.Json;

namespace BlazorEcommerce.Client.Servicios
{
    /// <summary>
    /// Servicio para operaciones de perfil del usuario
    /// Maneja la comunicación con el servidor para operaciones de perfil
    /// </summary>
    public class PerfilServicio : IPerfilServicio
    {
        private readonly HttpClient _httpClient;
        private readonly IPersonaServicio _personaServicio;
        private readonly Blazored.SessionStorage.ISessionStorageService _sessionStorage;

        public PerfilServicio(HttpClient httpClient, IPersonaServicio personaServicio, Blazored.SessionStorage.ISessionStorageService sessionStorage)
        {
            _httpClient = httpClient;
            _personaServicio = personaServicio;
            _sessionStorage = sessionStorage;
        }

        /// <summary>
        /// Obtiene el perfil del usuario autenticado desde el servidor
        /// </summary>
        public async Task<ResponseDTO<PersonaDTO>> ObtenerPerfil()
        {
            try
            {
                // Obtener el ID del usuario autenticado desde session storage
                var sesion = await _sessionStorage.ObtenerStorage<BlazorEcommerce.Shared.SesionDTO>("sesionUsuario");
                
                if (sesion == null)
                    return new ResponseDTO<PersonaDTO> { EsCorrecto = false, Mensaje = "Usuario no autenticado" };
                
                var resultado = await _httpClient.GetFromJsonAsync<ResponseDTO<PersonaDTO>>($"api/persona/perfil/{sesion.IdPersona}");
                return resultado ?? new ResponseDTO<PersonaDTO> { EsCorrecto = false, Mensaje = "Error desconocido" };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<PersonaDTO> { EsCorrecto = false, Mensaje = $"Error: {ex.Message}" };
            }
        }

        /// <summary>
        /// Actualiza los datos del perfil del usuario en el servidor
        /// </summary>
        public async Task<ResponseDTO<PersonaDTO>> ActualizarPerfil(PersonaDTO modelo)
        {
            try
            {
                var respuesta = await _httpClient.PutAsJsonAsync("api/persona/perfil", modelo);
                if (respuesta.IsSuccessStatusCode)
                {
                    var resultado = await respuesta.Content.ReadFromJsonAsync<ResponseDTO<PersonaDTO>>();
                    return resultado ?? new ResponseDTO<PersonaDTO> { EsCorrecto = false, Mensaje = "Error desconocido" };
                }
                return new ResponseDTO<PersonaDTO> { EsCorrecto = false, Mensaje = "Error al actualizar el perfil" };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<PersonaDTO> { EsCorrecto = false, Mensaje = $"Error: {ex.Message}" };
            }
        }

        /// <summary>
        /// Cambia la contraseña del usuario autenticado
        /// </summary>
        public async Task<ResponseDTO<bool>> CambiarContraseña(ChangePasswordDTO modelo)
        {
            try
            {
                // Obtener el ID del usuario autenticado desde session storage
                var sesion = await _sessionStorage.ObtenerStorage<BlazorEcommerce.Shared.SesionDTO>("sesionUsuario");
                
                if (sesion == null)
                    return new ResponseDTO<bool> { EsCorrecto = false, Mensaje = "Usuario no autenticado" };
                
                var respuesta = await _httpClient.PostAsJsonAsync($"api/persona/cambiar-contraseña/{sesion.IdPersona}", modelo);
                if (respuesta.IsSuccessStatusCode)
                {
                    var resultado = await respuesta.Content.ReadFromJsonAsync<ResponseDTO<bool>>();
                    return resultado ?? new ResponseDTO<bool> { EsCorrecto = false, Mensaje = "Error desconocido" };
                }
                return new ResponseDTO<bool> { EsCorrecto = false, Mensaje = "Error al cambiar la contraseña" };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<bool> { EsCorrecto = false, Mensaje = $"Error: {ex.Message}" };
            }
        }

        /// <summary>
        /// Obtiene el historial de pedidos del usuario
        /// </summary>
        public async Task<ResponseDTO<List<VentaDTO>>> ObtenerHistorialPedidos()
        {
            try
            {
                var resultado = await _httpClient.GetFromJsonAsync<ResponseDTO<List<VentaDTO>>>("api/venta/historial");
                return resultado ?? new ResponseDTO<List<VentaDTO>> { EsCorrecto = false, Mensaje = "Error desconocido" };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<List<VentaDTO>> { EsCorrecto = false, Mensaje = $"Error: {ex.Message}" };
            }
        }
    }
}
