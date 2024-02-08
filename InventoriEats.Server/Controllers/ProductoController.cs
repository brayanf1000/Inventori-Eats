using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using InventoriEats.Server.Models;
using InventoriEats.Shared;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InventoriEats.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        //Inyeccion de dependencias
        private readonly InventoriEatsContext _dbContexto;

        //Constructor
        public ProductoController(InventoriEatsContext dbContext)
        {
            _dbContexto = dbContext;
        }

        [HttpGet]
        [Route("ListaProductos")]
        public async Task<IActionResult> ListaProductos()
        {
            var responseApi = new ResponseApi<List<ProductoDTO>>();
            var ListaProductoDTO = new List<ProductoDTO>();

            try
            {
                foreach (var item in await _dbContexto.Productos.Include(x => x.IdCategoriaNavigation).ToListAsync())
                {
                    // Realiza las operaciones necesarias con cada elemento
                    ListaProductoDTO.Add(new ProductoDTO
                    {
                        IdProducto = item.IdProducto,
                        Nombre = item.Nombre,
                        Precio = item.Precio,
                        Stock = item.Stock,
                        IdCategoria = item.IdCategoria,
                        Descripcion = item.Descripcion,
                        FechaCreacion = (DateTime)item.FechaCreacion,
                        Categoria = new CategoriaDTO()
                        {
                            IdCategoria = item.IdCategoriaNavigation.IdCategoria,
                            NombreCategoria = item.IdCategoriaNavigation.NombreCategoria
                        }
                    });
                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = ListaProductoDTO;

            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            // Devuelve el resultado deseado (aquí deberías devolver responseApi o ListaCategoria)
            return Ok(responseApi);
        }

        [HttpGet]
        [Route("Producto/{id}")]
        public async Task<IActionResult> ListaProductosByName(int id)
        {
            var responseApi = new ResponseApi<ProductoDTO>();
            var ProductoDTO = new ProductoDTO();

            try
            {

                var dbProducto = await _dbContexto.Productos.FirstOrDefaultAsync(x => x.IdProducto == id);

                if (dbProducto != null)
                {
                    ProductoDTO.IdProducto = dbProducto.IdProducto;
                    ProductoDTO.Nombre = dbProducto.Nombre;
                    ProductoDTO.Precio = dbProducto.Precio;
                    ProductoDTO.Stock = dbProducto.Stock;
                    ProductoDTO.IdCategoria = dbProducto.IdCategoria;
                    ProductoDTO.Descripcion = dbProducto.Descripcion;
                    ProductoDTO.FechaCreacion = (DateTime)dbProducto.FechaCreacion;

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = ProductoDTO;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Producto no encontrado";
                }

            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            // Devuelve el resultado deseado (aquí deberías devolver responseApi o ListaCategoria)
            return Ok(responseApi);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateProducto(ProductoDTO producto)
        {
            var responseApi = new ResponseApi<int>();

            try
            {

                var dbProducto = new Producto
                {
                    IdProducto = producto.IdProducto,
                    Nombre = producto.Nombre,
                    Precio = (decimal)producto.Precio,
                    Stock = (int)producto.Stock,
                    IdCategoria = producto.IdCategoria,
                    Descripcion = producto.Descripcion,
                    FechaCreacion = producto.FechaCreacion,
                };

                _dbContexto.Productos.Add(dbProducto);
                await _dbContexto.SaveChangesAsync();

                if (dbProducto.IdProducto != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbProducto.IdProducto;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No se creo el producto";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            // Devuelve el resultado deseado (aquí deberías devolver responseApi o ListaCategoria)
            return Ok(responseApi);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateProducto(ProductoDTO producto, int id)
        {
            var responseApi = new ResponseApi<int>();

            try
            {
                var dbProducto = await _dbContexto.Productos.FirstOrDefaultAsync(x => x.IdProducto == id);

                if (dbProducto != null)
                {
                    dbProducto.Nombre = producto.Nombre;
                    dbProducto.Precio = (int)producto.Precio;
                    dbProducto.Stock = (int)producto.Stock;
                    dbProducto.IdCategoria = producto.IdCategoria;
                    dbProducto.Descripcion = producto.Descripcion;
                    dbProducto.FechaCreacion = producto.FechaCreacion;

                    _dbContexto.Productos.Update(dbProducto);
                    await _dbContexto.SaveChangesAsync();


                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbProducto.IdProducto;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No se encontro el producto";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            // Devuelve el resultado deseado (aquí deberías devolver responseApi o ListaCategoria)
            return Ok(responseApi);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var responseApi = new ResponseApi<int>();

            try
            {
                var dbProducto = await _dbContexto.Productos.FirstOrDefaultAsync(x => x.IdProducto == id);

                if (dbProducto != null)
                {
                    _dbContexto.Productos.Remove(dbProducto);
                    await _dbContexto.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = id; // Asignar el id como Valor
                    responseApi.Mensaje = "Producto eliminado";
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No se encontró el producto";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }
    }
}
