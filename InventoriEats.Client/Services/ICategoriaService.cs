using InventoriEats.Shared;

namespace InventoriEats.Client.Services
{
    public interface ICategoriaService
    {
        Task<List<CategoriaDTO>> GetCategoriasAll();
    }
}
