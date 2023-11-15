using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SushiPOP_YA1A_2C2023_G3.Models;
using System.Diagnostics;

namespace SushiPOP_YA1A_2C2023_G3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbContext _context;
        public HomeController(ILogger<HomeController> logger, DbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            int hoy = (int)DateTime.Now.DayOfWeek + 1;

            var descuento = await _context.Descuento.Include(d => d.Producto).Where(d => d.Dia == hoy && d.Activo == true).FirstOrDefaultAsync();

            var apertura = 0;
            var cierre = 0;
            if (hoy < 5)
            {
                apertura = 19;
                cierre = 23;
            }
            var descuentoYapertura = new DescuentoMasHorarioVm()
            {
                DiaDescuento = hoy,
                HorarioApertura = apertura,
                HorarioCierre = cierre,
                PorcentajeDescuento = -1,
                NombreProducto = ""
            };
            if (descuento == null)
            {
                
                return View("Index",descuentoYapertura);
            }
             descuentoYapertura = new DescuentoMasHorarioVm()
            {
                DiaDescuento = hoy,
                HorarioApertura = apertura,
                HorarioCierre = cierre,
                PorcentajeDescuento = descuento.DescuentoMaximo,
                NombreProducto = descuento.Producto.Nombre
                };    

            

                return View("Index", descuentoYapertura);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}