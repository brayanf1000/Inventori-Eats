using System.ComponentModel.DataAnnotations;

namespace InventoriEats.Shared
{
    public class ProductoDTO
    {
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int Stock { get; set; }

        [Required]
        [Range(1,int.MaxValue, ErrorMessage = "El campo {0} es requerido")]
        public int? IdCategoria { get; set; }

        public string NombreCategoria { get; set; }

        public string? Descripcion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public CategoriaDTO? Categoria { get; set; }
    }
}
