using InventoriEats.Shared;

namespace InventoriEats.Client.Services
{
    public interface IProductoService
    {
        Task<List<ProductoDTO>> ProductAll();
        Task<ProductoDTO> Buscar(int id);
        Task<int> SaveProduct(ProductoDTO product);
        Task<int> EditProduct(ProductoDTO product);
        Task<bool> DeleteProduct(int id);
    }
}
