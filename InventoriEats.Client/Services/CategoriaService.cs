using InventoriEats.Shared;
using System.Net.Http.Json;

namespace InventoriEats.Client.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly HttpClient _http;

        public CategoriaService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<CategoriaDTO>> GetCategoriasAll()
        {
            var result = await _http.GetFromJsonAsync<ResponseApi<List<CategoriaDTO>>>("api/Categoria/ListaCategoria");

            if (result!.EsCorrecto)
            {
                return result.Valor!;
            }
            else
            {
                throw new Exception(result.Mensaje);
            }
        }
    }
}

