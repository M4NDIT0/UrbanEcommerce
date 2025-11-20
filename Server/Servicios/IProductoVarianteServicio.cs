using BlazorEcommerce.Shared;

namespace BlazorEcommerce.Server.Servicios
{
    public interface IProductoVarianteServicio
    {
        Task<ResponseDTO<List<ProductoVarianteDTO>>> ObtenerPorProducto(int idProducto);
        Task<ResponseDTO<ProductoVarianteDTO>> Obtener(int idVariante);
        Task<ResponseDTO<ProductoVarianteDTO>> Crear(ProductoVarianteDTO variante);
        Task<ResponseDTO<ProductoVarianteDTO>> Editar(ProductoVarianteDTO variante);
        Task<ResponseDTO<bool>> Eliminar(int idVariante);
        Task<ResponseDTO<ProductoImagenDTO>> AgregarImagen(ProductoImagenDTO imagen);
        Task<ResponseDTO<bool>> EliminarImagen(int idImagen);
    }
}
