using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SushiPop.Models;

namespace SushiPOP_YA1A_2C2023_G3.Controllers
{
    public class CarritosController : Controller
    {
        private readonly DbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CarritosController(DbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Carritos
        public async Task<IActionResult> Index()
        {
            var dbContext = _context.Carrito.Include(c => c.Cliente);
            return View(await dbContext.ToListAsync());
        }

        // GET: Carritos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Carrito == null)
            {
                return NotFound();
            }

            var carrito = await _context.Carrito
                .Include(c => c.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carrito == null)
            {
                return NotFound();
            }

            return View(carrito);
        }

        // GET: Carritos/Create
        [Authorize(Roles ="CLIENTE")]
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Set<Cliente>(), "Id", "Id");
            return View();
        }

        // POST: Carritos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Procesado,Cancelado,ClienteId")] Carrito carrito)
        {
            var user = await _userManager.GetUserAsync(User);
            var cliente = await _context.Cliente.Where(c => c.Email == user.Email).FirstOrDefaultAsync();


            if (ModelState.IsValid)
            {
                var pedido = await _context.Pedido
               .Include(p => p.Carrito)
               .Where(p => p.Carrito.ClienteId == cliente.Id
                    &&
                    p.Estado == 1)
                .FirstOrDefaultAsync();

                if (pedido != null)
                {
                    return NotFound();
                }


                var listaPedidos = await _context.Pedido
                                         .Include(p => p.Carrito)
                                 .Where(p => p.Carrito.ClienteId == cliente.Id
                                    &&
                                    p.FechaCompra.Date == DateTime.Now.Date)
                                 .ToListAsync();

                if (listaPedidos.Count > 3)
                {

                    return NotFound();
                }
                //                var carritoItems = _context.Carrito.Include(c => c.CarritosItems).Where(c => c.ClienteId == cliente.Id).FirstOrDefaultAsync
                 carrito = await _context.Carrito.Where(c => c.ClienteId == cliente.Id && c.Cancelado == false && c.Procesado == false).FirstOrDefaultAsync();

                if (carrito == null) 
                {
                    carrito = new Carrito()
                    {
                        ClienteId = cliente.Id,
                        Procesado = false,
                        Cancelado = false,
                        CarritosItems = new List<CarritoItem>()
                    };
                }

                _context.Add(carrito);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Set<Cliente>(), "Id", "Id", carrito.ClienteId);
            return View(carrito);
        }

        // GET: Carritos/Edit/5
        [Authorize(Roles ="CLIENTE")] //El carrito únicamente puede ser editado por el usuario cliente propietario (RN43)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Carrito == null)
            {
                return NotFound();
            }

            var carrito = await _context.Carrito.FindAsync(id);
            if (carrito == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Set<Cliente>(), "Id", "Id", carrito.ClienteId);
            return View(carrito);
        }

        // POST: Carritos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Procesado,Cancelado,ClienteId")] Carrito carrito)
        {
            if (id != carrito.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carrito);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarritoExists(carrito.Id))
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
            ViewData["ClienteId"] = new SelectList(_context.Set<Cliente>(), "Id", "Id", carrito.ClienteId);
            return View(carrito);
        }

        // GET: Carritos/Delete/5
        [Authorize(Roles ="CLIENTE")] //Solo los usuarios cliente propietarios de ese carrito pueden cancelar el carrito (RN44)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Carrito == null)
            {
                return NotFound();
            }

            var carrito = await _context.Carrito
                .Include(c => c.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carrito == null)
            {
                return NotFound();
            }

            return View(carrito);
        }

        // POST: Carritos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Carrito == null)
            {
                return Problem("Entity set 'dbContext.Carrito'  is null.");
            }
            var carrito = await _context.Carrito.FindAsync(id);
            if (carrito != null)
            {
                _context.Carrito.Remove(carrito);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarritoExists(int id)
        {
          return (_context.Carrito?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [Authorize(Roles = "CLIENTE")]
        public async Task<IActionResult> AgregarAlCarrito(int productoId)
        {
            var user = await _userManager.GetUserAsync(User);
            var cliente = await _context.Cliente.Where(c => c.Email == user.Email).FirstOrDefaultAsync();

            // Validamos stock
            var producto = await _context.Producto.FindAsync(productoId);
            if (producto == null)
            {
                return NotFound();

            }

            // Validamos pedido activo
            var pedido = await _context.Pedido
               .Include(p => p.Carrito)
               .Where(p => p.Carrito.ClienteId == cliente.Id
                    &&
                    p.Estado == 1)
                .FirstOrDefaultAsync();
            if (pedido != null)
            {
                return NotFound();
            }

            // Validamos cantidad de pedidos hechos en el dia
            var listaPedidos = await _context.Pedido
                                         .Include(p => p.Carrito)
                                 .Where(p => p.Carrito.ClienteId == cliente.Id
                                    &&
                                    p.FechaCompra.Date == DateTime.Now.Date)
                                 .ToListAsync();
            if (listaPedidos.Count == 3)
            {
                return NotFound();
            }

            // Validamos existencia de carrito
            var carrito = await _context.Carrito.Where(c => c.ClienteId == cliente.Id && c.Cancelado == false && c.Procesado == false).FirstOrDefaultAsync();
            if (carrito == null)
            {
                carrito = new Carrito()
                {
                    ClienteId = cliente.Id,
                    Procesado = false,
                    Cancelado = false,
                    CarritosItems = new List<CarritoItem>()
                };
                _context.Add(carrito);
                await _context.SaveChangesAsync();
            }

            // Validamos si el item ya existe en el carrito
            var item = carrito.CarritosItems.Where(i => i.ProductoId == productoId).FirstOrDefault();
            if(item == null)
            {
                item = new CarritoItem();
                item.CarritoId = carrito.Id;
                item.ProductoId = productoId;
                item.PrecioUnitarioConDescuento = producto.Precio;
                item.Cantidad = 1;

                carrito.CarritosItems.Add(item);
                _context.Update(carrito);
                _context.Add(item);
                await _context.SaveChangesAsync();
            } else
            {
                item.Cantidad += 1;

                _context.Update(item);
                await _context.SaveChangesAsync();
            }

            producto.Stock -= 1;
            _context.Update(producto);
            await _context.SaveChangesAsync();

            return View(Index);
        }
    }
}
