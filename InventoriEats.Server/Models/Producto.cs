using System;
using System.Collections.Generic;

namespace InventoriEats.Server.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal Precio { get; set; }

    public int Stock { get; set; }

    public int? IdCategoria { get; set; }

    public string? Descripcion { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual CategoriaProducto? IdCategoriaNavigation { get; set; }
}
