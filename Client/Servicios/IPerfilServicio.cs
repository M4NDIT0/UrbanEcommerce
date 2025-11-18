using BlazorEcommerce.Shared;

namespace BlazorEcommerce.Client.Servicios
{
    /// <summary>
    /// Interfaz para operaciones relacionadas con el perfil del usuario
    /// </summary>
    public interface IPerfilServicio
    {
        /// <summary>
        /// Obtiene los datos del perfil del usuario autenticado
        /// </summary>
        Task<ResponseDTO<PersonaDTO>> ObtenerPerfil();

        /// <summary>
        /// Actualiza los datos del perfil del usuario
        /// </summary>
        Task<ResponseDTO<PersonaDTO>> ActualizarPerfil(PersonaDTO modelo);

        /// <summary>
        /// Cambia la contraseña del usuario
        /// </summary>
        Task<ResponseDTO<bool>> CambiarContraseña(ChangePasswordDTO modelo);

        /// <summary>
        /// Obtiene el historial de pedidos del usuario
        /// </summary>
        Task<ResponseDTO<List<VentaDTO>>> ObtenerHistorialPedidos();
    }
}
