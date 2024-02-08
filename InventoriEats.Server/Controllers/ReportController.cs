using AspNetCore.Reporting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using ReportWebAssembly.Server.Data;
using System.Data;
using InventoriEats.Server.Models;
using System.Text;

namespace ReportWebAssembly.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly InventoriEatsContext _dbContext;
        private readonly ProductosServices _productosService;

        public ReportController(IWebHostEnvironment webHostEnvironment, InventoriEatsContext dbContext)
        {
            this._webHostEnvironment = webHostEnvironment;
            this._dbContext = dbContext;
            this._productosService = new ProductosServices(_dbContext);
            System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        [HttpGet]
        [Route("GetReport")]
        public async Task<IActionResult> GetReport(int reportType)
        {
            try
            {
                var dt = await _productosService.GetProductosInfo();

                int extension = 1;
                var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\Report1.rdlc";

                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("param", "Blazor RDLC Report");
                LocalReport localReport = new LocalReport(path);
                localReport.AddDataSource("dsEmployee", dt);

                if (reportType == 1)
                {
                    var result = localReport.Execute(RenderType.Pdf, extension, parameters);
                    return File(result.MainStream, "application/pdf");
                }
                else
                {
                    var result = localReport.Execute(RenderType.Pdf, extension, parameters);
                    return File(result.MainStream, "application/msexcel");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
