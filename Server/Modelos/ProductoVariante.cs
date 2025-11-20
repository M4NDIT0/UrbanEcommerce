using System;
using System.Collections.Generic;

namespace BlazorEcommerce.Server.Modelos;

public partial class ProductoVariante
{
    public int IdVariante { get; set; }

    public int IdProducto { get; set; }

    public string? NombreColor { get; set; }

    public string? CodigoHex { get; set; }

    public int Stock { get; set; }

    public string? SKU { get; set; }

    public bool Activo { get; set; } = true;

    public DateTime? FechaCreacion { get; set; }

    // Navegación
    public virtual Producto? IdProductoNavigation { get; set; }

    public virtual ICollection<ProductoImagen> Imagenes { get; set; } = new List<ProductoImagen>();
}
