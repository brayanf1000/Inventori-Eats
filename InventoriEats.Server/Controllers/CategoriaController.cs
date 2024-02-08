using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using InventoriEats.Server.Models;
using InventoriEats.Shared;
using Microsoft.EntityFrameworkCore;

namespace InventoriEats.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly InventoriEatsContext _dbContexto;

        public CategoriaController(InventoriEatsContext dbContext)
        {
            _dbContexto = dbContext;
        }

        [HttpGet]
        [Route("ListaCategoria")]
        public async Task<IActionResult> ListaCategoria()
        {
            var responseApi = new ResponseApi<List<CategoriaDTO>>();
            var ListaCategoriaDTO = new List<CategoriaDTO>();

            try
            {
                foreach (var item in await _dbContexto.CategoriaProductos.ToListAsync())
                {
                    // Realiza las operaciones necesarias con cada elemento
                    ListaCategoriaDTO.Add(new CategoriaDTO 
                    {
                        IdCategoria = item.IdCategoria,
                        NombreCategoria = item.NombreCategoria
                    });
                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = ListaCategoriaDTO;

            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            // Devuelve el resultado deseado (aquí deberías devolver responseApi o ListaCategoria)
            return Ok(responseApi);
        }
    }
}
