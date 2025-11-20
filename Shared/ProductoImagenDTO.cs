namespace BlazorEcommerce.Shared
{
    public class ProductoImagenDTO
    {
        public int IdImagen { get; set; }
        public int IdVariante { get; set; }
        public string? Url { get; set; }
        public bool EsPrincipal { get; set; } = false;
        public int Orden { get; set; } = 0;
        public DateTime? FechaCreacion { get; set; }
    }
}
