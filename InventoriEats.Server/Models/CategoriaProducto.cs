using System;
using System.Collections.Generic;

namespace InventoriEats.Server.Models;

public partial class CategoriaProducto
{
    public int IdCategoria { get; set; }

    public string NombreCategoria { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; } = new List<Producto>();
}
