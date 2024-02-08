using InventoriEats.Shared;
using System.Net.Http.Json;

namespace InventoriEats.Client.Services
{
    public class ProductServices : IProductoService
    {
        private readonly HttpClient _http;

        public ProductServices(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ProductoDTO>> ProductAll()
        {
            var result = await _http.GetFromJsonAsync<ResponseApi<List<ProductoDTO>>>("api/Producto/ListaProductos");

            if (result!.EsCorrecto)
            {
                return result.Valor!;
            }
            else
            {
                throw new Exception(result.Mensaje);
            }
        }

        public async Task<ProductoDTO> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseApi<ProductoDTO>>($"api/Producto/Producto/{id}");

            if (result!.EsCorrecto)
            {
                return result.Valor!;
            }
            else
            {
                throw new Exception(result.Mensaje);
            }
        }

        public async Task<int> SaveProduct(ProductoDTO product)
        {
            var result = await _http.PostAsJsonAsync($"api/Producto/Create", product);
            var response = await result.Content.ReadFromJsonAsync<ResponseApi<int>>();

            if (response!.EsCorrecto)
            {
                return response.Valor!;
            }
            else
            {
                throw new Exception(response.Mensaje);
            }
        }

        public async Task<int> EditProduct(ProductoDTO product)
        {
            var result = await _http.PutAsJsonAsync($"api/Producto/update?id={product.IdProducto}", product);
            var response = await result.Content.ReadFromJsonAsync<ResponseApi<int>>();

            if (response!.EsCorrecto)
            {
                return response.Valor!;
            }
            else
            {
                throw new Exception(response.Mensaje);
            }
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var result = await _http.DeleteAsync($"api/Producto/Delete/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseApi<int>>();

            if (response!.EsCorrecto)
            {
                return response.EsCorrecto!;
            }
            else
            {
                throw new Exception(response.Mensaje);
            }
        }



    }
}
