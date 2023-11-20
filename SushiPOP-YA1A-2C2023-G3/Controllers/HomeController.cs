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
            int hoy = (int)DateTime.Now.DayOfWeek+1;

            var descuento = await _context.Descuento.Include(d => d.Producto).Where(d => d.Dia == hoy && d.Activo == true).FirstOrDefaultAsync();
            var culture = new System.Globalization.CultureInfo("es-ES");
            var textohorarios = "hoy " + culture.DateTimeFormat.GetDayName(DateTime.Today.DayOfWeek) + " atendemos de 11 a 14 horas y de 19 a 23 horas por WhatsApp +541140044004 ";

            if (hoy < 5)
            {
                textohorarios = "hoy " + culture.DateTimeFormat.GetDayName(DateTime.Today.DayOfWeek) + " atendemos de 19 a 23 horas por WhatsApp +541140044004 ";
            }
            
            var descuentoYapertura = new DescuentoMasHorarioVm()
            {
                Horarios = textohorarios,
                DiaDescuento = culture.DateTimeFormat.GetDayName(DateTime.Today.DayOfWeek),
                PorcentajeDescuento = -1,
                NombreProducto = ""
            };
            if (descuento == null)
            {
                descuentoYapertura.textoDescuento = "Hoy es "+descuentoYapertura.DiaDescuento +" disfrutá del mejor sushi #EnCasa con amigos";
                return View("Index",descuentoYapertura);
            }
             descuentoYapertura = new DescuentoMasHorarioVm()
            {

                Horarios = textohorarios,
                DiaDescuento = culture.DateTimeFormat.GetDayName(DateTime.Today.DayOfWeek),
                PorcentajeDescuento = descuento.Porcentaje,
                NombreProducto = descuento.Producto.Nombre,
                };
            descuentoYapertura.textoDescuento = "Hoy " + descuentoYapertura.DiaDescuento + " ahorra un " + descuentoYapertura.PorcentajeDescuento + "% en " + descuentoYapertura.NombreProducto;




                return View("Index", descuentoYapertura);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}