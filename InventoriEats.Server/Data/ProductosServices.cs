using InventoriEats.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ReportWebAssembly.Server.Data
{
    public class ProductosServices : Controller
    {
        private readonly InventoriEatsContext _dbContexto;

        public ProductosServices(InventoriEatsContext dbContext)
        {
            _dbContexto = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<DataTable> GetProductosInfo()
        {
            var productos = await _dbContexto.Productos.Include(x => x.IdCategoriaNavigation).ToListAsync();

            var dt = new DataTable();
            dt.Columns.Add("Nombre");
            dt.Columns.Add("Stock");
            dt.Columns.Add("NombreCategoria");
            dt.Columns.Add("Descripcion");
            dt.Columns.Add("FechaCreacion", typeof(DateTime));

            foreach (var item in productos)
            {
                var dr = dt.NewRow();
                dr["Nombre"] = item.Nombre;
                dr["Stock"] = item.Stock;
                dr["NombreCategoria"] = item.IdCategoria;
                dr["Descripcion"] = item.Descripcion;
                dr["FechaCreacion"] = item.FechaCreacion;
                dt.Rows.Add(dr);
            }

            return dt;
        }
    }
}
