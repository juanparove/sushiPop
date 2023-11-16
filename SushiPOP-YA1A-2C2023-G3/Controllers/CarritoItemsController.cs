using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SushiPop.Models;

namespace SushiPOP_YA1A_2C2023_G3.Controllers
{
    public class CarritoItemsController : Controller
    {
        private readonly DbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        

        public CarritoItemsController(DbContext context)
        {
            _context = context;
        }

        // GET: CarritoItems
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var cliente = await _context.Cliente.Where(c => c.Email == user.Email).FirstOrDefaultAsync();
            var carrito = await _context.Carrito.Where(c => c.ClienteId == cliente.Id).FirstOrDefaultAsync();
            var carritoItems = await _context.CarritoItem.Where(c => c.CarritoId == carrito.Id).ToListAsync();

            return View(carritoItems);
        }

        // GET: CarritoItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CarritoItem == null)
            {
                return NotFound();
            }

            var carritoItem = await _context.CarritoItem
                .Include(c => c.Carrito)
                .Include(c => c.Producto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carritoItem == null)
            {
                return NotFound();
            }

            return View(carritoItem);
        }

        // GET: CarritoItems/Create
        public IActionResult Create()
        {
            ViewData["CarritoId"] = new SelectList(_context.Carrito, "Id", "Id");
            ViewData["ProductoId"] = new SelectList(_context.Set<Producto>(), "Id", "Id");
            return View();
        }

        // POST: CarritoItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PrecioUnitarioConDescuento,Cantidad,CarritoId,ProductoId")] CarritoItem carritoItem)
        {
            var user = await _userManager.GetUserAsync(User);
            var cliente = await _context.Cliente.Where(c => c.Email == user.Email).FirstOrDefaultAsync();

      //      var producto = await _context.inProducto.Where(p => p.Id == carritoItem.ProductoId)
            if (ModelState.IsValid)
            {
                var producto = await _context.Producto.Include(p => p.Id).Where(p => p.Id == carritoItem.ProductoId).FirstOrDefaultAsync();

                if (producto == null) {
                    return NotFound();
                
                }
                var cantidadStock = producto.Stock;
                if (cantidadStock < carritoItem.Cantidad) 
                {
                    return NotFound();
                }

                //aca chequearia lo de carrito existente
                _context.Add(carritoItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarritoId"] = new SelectList(_context.Carrito, "Id", "Id", carritoItem.CarritoId);
            ViewData["ProductoId"] = new SelectList(_context.Set<Producto>(), "Id", "Id", carritoItem.ProductoId);
            return View(carritoItem);
        }

        // GET: CarritoItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CarritoItem == null)
            {
                return NotFound();
            }

            var carritoItem = await _context.CarritoItem.FindAsync(id);
            if (carritoItem == null)
            {
                return NotFound();
            }
            ViewData["CarritoId"] = new SelectList(_context.Carrito, "Id", "Id", carritoItem.CarritoId);
            ViewData["ProductoId"] = new SelectList(_context.Set<Producto>(), "Id", "Id", carritoItem.ProductoId);
            return View(carritoItem);
        }

        // POST: CarritoItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PrecioUnitarioConDescuento,Cantidad,CarritoId,ProductoId")] CarritoItem carritoItem)
        {
            if (id != carritoItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carritoItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarritoItemExists(carritoItem.Id))
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
            ViewData["CarritoId"] = new SelectList(_context.Carrito, "Id", "Id", carritoItem.CarritoId);
            ViewData["ProductoId"] = new SelectList(_context.Set<Producto>(), "Id", "Id", carritoItem.ProductoId);
            return View(carritoItem);
        }

        // GET: CarritoItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CarritoItem == null)
            {
                return NotFound();
            }

            var carritoItem = await _context.CarritoItem
                .Include(c => c.Carrito)
                .Include(c => c.Producto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carritoItem == null)
            {
                return NotFound();
            }

            return View(carritoItem);
        }

        // POST: CarritoItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CarritoItem == null)
            {
                return Problem("Entity set 'dbContext.CarritoItem'  is null.");
            }
            var carritoItem = await _context.CarritoItem.FindAsync(id);
            if (carritoItem != null)
            {
                _context.CarritoItem.Remove(carritoItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarritoItemExists(int id)
        {
          return (_context.CarritoItem?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
