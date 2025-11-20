namespace BlazorEcommerce.Shared
{
    public class ProductoVarianteDTO
    {
        public int IdVariante { get; set; }
        public int IdProducto { get; set; }
        public string? NombreColor { get; set; }
        public string? CodigoHex { get; set; }
        public int Stock { get; set; }
        public string? SKU { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime? FechaCreacion { get; set; }
        
        // Lista de imágenes de esta variante
        public List<ProductoImagenDTO> Imagenes { get; set; } = new List<ProductoImagenDTO>();
    }
}
