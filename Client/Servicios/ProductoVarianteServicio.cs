using BlazorEcommerce.Shared;
using System.Net.Http.Json;

namespace BlazorEcommerce.Client.Servicios
{
    public class ProductoVarianteServicio : IProductoVarianteServicio
    {
        private readonly HttpClient _httpClient;

        public ProductoVarianteServicio(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseDTO<List<ProductoVarianteDTO>>?> ObtenerPorProducto(int idProducto)
        {
            return await _httpClient.GetFromJsonAsync<ResponseDTO<List<ProductoVarianteDTO>>>($"api/ProductoVariante/Producto/{idProducto}");
        }

        public async Task<ResponseDTO<ProductoVarianteDTO>?> Obtener(int idVariante)
        {
            return await _httpClient.GetFromJsonAsync<ResponseDTO<ProductoVarianteDTO>>($"api/ProductoVariante/{idVariante}");
        }

        public async Task<ResponseDTO<ProductoVarianteDTO>?> Crear(ProductoVarianteDTO variante)
        {
            var response = await _httpClient.PostAsJsonAsync("api/ProductoVariante", variante);
            return await response.Content.ReadFromJsonAsync<ResponseDTO<ProductoVarianteDTO>>();
        }

        public async Task<ResponseDTO<ProductoVarianteDTO>?> Editar(ProductoVarianteDTO variante)
        {
            var response = await _httpClient.PutAsJsonAsync("api/ProductoVariante", variante);
            return await response.Content.ReadFromJsonAsync<ResponseDTO<ProductoVarianteDTO>>();
        }

        public async Task<ResponseDTO<bool>?> Eliminar(int idVariante)
        {
            var response = await _httpClient.DeleteAsync($"api/ProductoVariante/{idVariante}");
            return await response.Content.ReadFromJsonAsync<ResponseDTO<bool>>();
        }

        public async Task<ResponseDTO<ProductoImagenDTO>?> AgregarImagen(ProductoImagenDTO imagen)
        {
            var response = await _httpClient.PostAsJsonAsync("api/ProductoVariante/Imagen", imagen);
            return await response.Content.ReadFromJsonAsync<ResponseDTO<ProductoImagenDTO>>();
        }

        public async Task<ResponseDTO<bool>?> EliminarImagen(int idImagen)
        {
            var response = await _httpClient.DeleteAsync($"api/ProductoVariante/Imagen/{idImagen}");
            return await response.Content.ReadFromJsonAsync<ResponseDTO<bool>>();
        }
    }
}
