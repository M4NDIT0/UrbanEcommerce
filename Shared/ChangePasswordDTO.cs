using System.ComponentModel.DataAnnotations;

namespace BlazorEcommerce.Shared
{
    /// <summary>
    /// DTO para cambio de contraseña
    /// </summary>
    public class ChangePasswordDTO
    {
        [Required(ErrorMessage = "Ingrese la contraseña actual")]
        public string? ContraseñaActual { get; set; }

        [Required(ErrorMessage = "Ingrese la nueva contraseña")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre 6 y 100 caracteres")]
        public string? ContraseñaNueva { get; set; }

        [Required(ErrorMessage = "Confirme la nueva contraseña")]
        [Compare("ContraseñaNueva", ErrorMessage = "Las contraseñas no coinciden")]
        public string? ConfirmarContraseña { get; set; }
    }
}
