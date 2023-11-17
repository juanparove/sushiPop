using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SushiPop.Models;
using SushiPOP_YA1A_2C2023_G3.Models;

namespace SushiPOP_YA1A_2C2023_G3.Controllers
{
    public class PedidosController : Controller
    {
        private readonly DbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private const int EnvioEstandar = 80;
        private const int NumPedidoInicial = 30000;

        public PedidosController(DbContext context)
        {
            _context = context;
        }

        // GET: Pedidos
        public async Task<IActionResult> Index()
        {
            var DbContext = _context.Pedido.Include(p => p.Carrito);
            return View(await DbContext.ToListAsync());
        }

        // GET: Pedidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pedido == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido
                .Include(p => p.Carrito)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // GET: Pedidos/Create
        [Authorize(Roles ="CLIENTE")] //Solo usuarios de tipo cliente pueden hacer pedidos (RN40, RN47)
        public IActionResult Create()
        {
            ViewData["CarritoId"] = new SelectList(_context.Carrito, "Id", "Id");
            return View();
        }

        // POST: Pedidos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NroPedido,FechaCompra,Subtotal,GastoEnvio,Total,Estado,CarritoId")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarritoId"] = new SelectList(_context.Carrito, "Id", "Id", pedido.CarritoId);
            return View(pedido);
        }

        // GET: Pedidos/Edit/5
        [Authorize(Roles ="EMPLEADO")] //Los usuarios empleados pueden editar el estado de los pedidos de forma vertical incremental (RN54)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pedido == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            ViewData["CarritoId"] = new SelectList(_context.Carrito, "Id", "Id", pedido.CarritoId);
            return View(pedido);
        }

        // POST: Pedidos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NroPedido,FechaCompra,Subtotal,GastoEnvio,Total,Estado,CarritoId")] Pedido pedido)
        {
            if (id != pedido.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarritoId"] = new SelectList(_context.Carrito, "Id", "Id", pedido.CarritoId);
            return View(pedido);
        }

        // GET: Pedidos/Delete/5
        [Authorize(Roles = "CLIENTE")]//Los usuarios clientes pueden cancelar el pedido solo si está en estado 1 (RN55)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pedido == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido
                .Include(p => p.Carrito)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pedido == null)
            {
                return Problem("Entity set 'dbContext.Pedido'  is null.");
            }
            var pedido = await _context.Pedido.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedido.Remove(pedido);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(int id)
        {
          return (_context.Pedido?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> GenerarPedido(int carritoId)
        {
            var user = await _userManager.GetUserAsync(User);
            var cliente = await _context.Cliente.Where(c => c.Email == user.Email).FirstOrDefaultAsync();
            //El cliente no puede crear un pedido nuevo si tiene uno en estado Sin confirmar.
            foreach (var c in cliente.Carritos)
            {
                if (c.Pedido.Estado == 1)
                {
                    return NotFound();
                }

            }
            // traer el carrito
            var carrito = await _context.Carrito.Where(c => c.Id == carritoId).FirstOrDefaultAsync();
            //generar subtotal
            decimal subTotal = 0;
            foreach (var item in carrito.CarritosItems)
            {
                subTotal += item.PrecioUnitarioConDescuento * item.Cantidad;
            }
            //verificar descuentos
            var descuento = _context.Descuento.Where(d => d.Dia == DateTime.Now.Day).FirstOrDefault();
            decimal cantDescuento = 0;
            foreach (var item in carrito.CarritosItems)
            {
                if (item.ProductoId == descuento.ProductoId)
                {
                    cantDescuento = item.PrecioUnitarioConDescuento * item.Cantidad * descuento.Porcentaje / 100;
                }
            }
            //costo envio Si el cliente tiene 10 pedidos en estado Entregado en los últimos 30 días, el costo de envío es gratis.
            decimal costoEnvio = 80;
            DateTime fechaActual = DateTime.Now;
            DateTime fechaHace30Dias = fechaActual.AddDays(-30);
            var listaPedidos = await _context.Pedido
                                        .Include(p => p.Carrito)
                                .Where(p => p.Carrito.ClienteId == cliente.Id
                                   &&
                                   p.FechaCompra.Date >= fechaHace30Dias.Date
                                   &&
                                   p.Estado == 5)
                                .ToListAsync();

            if (listaPedidos.Count >= 10)
            {
                costoEnvio = 0;
            }
            //FALTA CHEQUEAR CLIMA

            string respuesta = string.Empty;

            using (HttpClient client = new HttpClient()) 
            {
                string url = "http://api.weatherapi.com/v1/search.json?key=11f624ca55034b1cb98194510231711&q=Buenos_Aires";
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode) 
                {   
                    respuesta = await response.Content.ReadAsStringAsync();
                }

            }
            var clima = JsonConvert.DeserializeObject<ClimaVm>(respuesta);
            if (clima.temperatura < 5 || clima.clima.Equals("Rain")) 
            {
                costoEnvio = costoEnvio * (decimal) 1.5;
            }

                //calculo del total
                var total = subTotal - cantDescuento + costoEnvio;
            //generar num pedido
            int numPedido;
            listaPedidos = await _context.Pedido.OrderByDescending(p => p.NroPedido).ToListAsync();
            //list = list.OrderByDescending(x => x.AVC).ToList();
            if (listaPedidos.Count ==0)
            {
                numPedido = NumPedidoInicial;
            }
            else
            {
             numPedido = listaPedidos.First().NroPedido + 5;
            }
            // estado 1 y fecha de hoy
            var pedido = new Pedido()
            {
                NroPedido = numPedido,
                FechaCompra = fechaActual,  
                Subtotal = subTotal,
                GastoEnvio = costoEnvio,
                Total = total,
                Estado = 1,
            };
            _context.Add(pedido);
            await _context.SaveChangesAsync();
            //cambiar estado carrito
            carrito.Procesado = true;

            return View();
        }
        
    }
}
