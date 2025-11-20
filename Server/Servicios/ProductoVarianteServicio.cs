using AutoMapper;
using BlazorEcommerce.Server.Repositorios;
using BlazorEcommerce.Shared;

namespace BlazorEcommerce.Server.Servicios
{
    public class ProductoVarianteServicio : IProductoVarianteServicio
    {
        private readonly IGenericoRepositorio<ProductoVariante> _varianteRepositorio;
        private readonly IGenericoRepositorio<ProductoImagen> _imagenRepositorio;
        private readonly IMapper _mapper;

        public ProductoVarianteServicio(
            IGenericoRepositorio<ProductoVariante> varianteRepositorio,
            IGenericoRepositorio<ProductoImagen> imagenRepositorio,
            IMapper mapper)
        {
            _varianteRepositorio = varianteRepositorio;
            _imagenRepositorio = imagenRepositorio;
            _mapper = mapper;
        }

        public async Task<ResponseDTO<List<ProductoVarianteDTO>>> ObtenerPorProducto(int idProducto)
        {
            var response = new ResponseDTO<List<ProductoVarianteDTO>>();
            try
            {
                var variantes = _varianteRepositorio.Consultar(v => v.IdProducto == idProducto).ToList();

                // Cargar imágenes para cada variante
                foreach (var variante in variantes)
                {
                    var imagenes = _imagenRepositorio.Consultar(i => i.IdVariante == variante.IdVariante);
                    variante.Imagenes = imagenes.OrderBy(i => i.Orden).ToList();
                }

                response.EsCorrecto = true;
                response.Resultado = _mapper.Map<List<ProductoVarianteDTO>>(variantes);
            }
            catch (Exception ex)
            {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDTO<ProductoVarianteDTO>> Obtener(int idVariante)
        {
            var response = new ResponseDTO<ProductoVarianteDTO>();
            try
            {
                var variante = _varianteRepositorio.Consultar(v => v.IdVariante == idVariante).FirstOrDefault();

                if (variante != null)
                {
                    // Cargar imágenes
                    var imagenes = _imagenRepositorio.Consultar(i => i.IdVariante == idVariante);
                    variante.Imagenes = imagenes.OrderBy(i => i.Orden).ToList();

                    response.EsCorrecto = true;
                    response.Resultado = _mapper.Map<ProductoVarianteDTO>(variante);
                }
                else
                {
                    response.EsCorrecto = false;
                    response.Mensaje = "Variante no encontrada";
                }
            }
            catch (Exception ex)
            {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDTO<ProductoVarianteDTO>> Crear(ProductoVarianteDTO varianteDTO)
        {
            var response = new ResponseDTO<ProductoVarianteDTO>();
            try
            {
                var variante = _mapper.Map<ProductoVariante>(varianteDTO);
                variante.FechaCreacion = DateTime.Now;
                variante.Activo = true;

                var varianteCreada = await _varianteRepositorio.Crear(variante);
                
                // Crear imágenes si las hay
                if (varianteDTO.Imagenes != null && varianteDTO.Imagenes.Any())
                {
                    foreach (var imagenDTO in varianteDTO.Imagenes)
                    {
                        var imagen = _mapper.Map<ProductoImagen>(imagenDTO);
                        imagen.IdVariante = varianteCreada.IdVariante;
                        imagen.FechaCreacion = DateTime.Now;
                        await _imagenRepositorio.Crear(imagen);
                    }
                }

                response.EsCorrecto = true;
                response.Resultado = _mapper.Map<ProductoVarianteDTO>(varianteCreada);
            }
            catch (Exception ex)
            {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDTO<ProductoVarianteDTO>> Editar(ProductoVarianteDTO varianteDTO)
        {
            var response = new ResponseDTO<ProductoVarianteDTO>();
            try
            {
                var varianteExistente = _varianteRepositorio.Consultar(v => v.IdVariante == varianteDTO.IdVariante).FirstOrDefault();

                if (varianteExistente != null)
                {
                    varianteExistente.NombreColor = varianteDTO.NombreColor;
                    varianteExistente.CodigoHex = varianteDTO.CodigoHex;
                    varianteExistente.Stock = varianteDTO.Stock;
                    varianteExistente.SKU = varianteDTO.SKU;
                    varianteExistente.Activo = varianteDTO.Activo;

                    var actualizado = await _varianteRepositorio.Editar(varianteExistente);

                    response.EsCorrecto = true;
                    response.Resultado = _mapper.Map<ProductoVarianteDTO>(actualizado);
                }
                else
                {
                    response.EsCorrecto = false;
                    response.Mensaje = "Variante no encontrada";
                }
            }
            catch (Exception ex)
            {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDTO<bool>> Eliminar(int idVariante)
        {
            var response = new ResponseDTO<bool>();
            try
            {
                var variante = _varianteRepositorio.Consultar(v => v.IdVariante == idVariante).FirstOrDefault();

                if (variante != null)
                {
                    // Las imágenes se eliminan en cascada por la FK
                    var eliminado = await _varianteRepositorio.Eliminar(variante);
                    response.EsCorrecto = eliminado;
                    response.Resultado = eliminado;
                }
                else
                {
                    response.EsCorrecto = false;
                    response.Mensaje = "Variante no encontrada";
                }
            }
            catch (Exception ex)
            {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDTO<ProductoImagenDTO>> AgregarImagen(ProductoImagenDTO imagenDTO)
        {
            var response = new ResponseDTO<ProductoImagenDTO>();
            try
            {
                var imagen = _mapper.Map<ProductoImagen>(imagenDTO);
                imagen.FechaCreacion = DateTime.Now;

                var imagenCreada = await _imagenRepositorio.Crear(imagen);

                response.EsCorrecto = true;
                response.Resultado = _mapper.Map<ProductoImagenDTO>(imagenCreada);
            }
            catch (Exception ex)
            {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDTO<bool>> EliminarImagen(int idImagen)
        {
            var response = new ResponseDTO<bool>();
            try
            {
                var imagen = _imagenRepositorio.Consultar(i => i.IdImagen == idImagen).FirstOrDefault();

                if (imagen != null)
                {
                    var eliminado = await _imagenRepositorio.Eliminar(imagen);
                    response.EsCorrecto = eliminado;
                    response.Resultado = eliminado;
                }
                else
                {
                    response.EsCorrecto = false;
                    response.Mensaje = "Imagen no encontrada";
                }
            }
            catch (Exception ex)
            {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }
            return response;
        }
    }
}
