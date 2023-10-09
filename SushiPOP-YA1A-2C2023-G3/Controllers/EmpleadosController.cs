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
    public class EmpleadosController : Controller
    {
        private readonly DbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public EmpleadosController(DbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager; 
        }

        // GET: Empleados
        [Authorize(Roles = "ADMIN")] //Solo el administrador puede ver la lista de clientes (RN59)
        public async Task<IActionResult> Index()
        {
              return _context.Empleado != null ? 
                          View(await _context.Empleado.ToListAsync()) :
                          Problem("Entity set 'dbContext.Empleado'  is null.");
        }

        // GET: Empleados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Empleado == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleado
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // GET: Empleados/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Empleados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Legajo,Id,Nombre,Apellido,Direccion,Telefono,FechaNacimiento,FechaAlta,Activo,Email")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {

                //Crea el usuario
                IdentityUser user = new IdentityUser();
                user.Email = empleado.Email;
                user.UserName = empleado.Email;
                var result = await _userManager.CreateAsync(user, "Password1!");


                //Asignacion fechad de alta
                empleado.FechaAlta = DateTime.Now;

                //Asignacion estado
                empleado.Activo = true;


                if (result.Succeeded)
                {
                    //Asignacion de rol
                    await _userManager.AddToRoleAsync(user, "EMPLEADO");

                    //Crea empleado
                    _context.Add(empleado);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }




            }
            return View(empleado);
        }

        // GET: Empleados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Empleado == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleado.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }
            return View(empleado);
        }

        // POST: Empleados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Legajo,Id,Nombre,Apellido,Direccion,Telefono,FechaNacimiento,FechaAlta,Activo,Email")] Empleado empleado)
        {
            if (id != empleado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empleado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadoExists(empleado.Id))
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
            return View(empleado);
        }

        // GET: Empleados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Empleado == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleado
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Empleado == null)
            {
                return Problem("Entity set 'dbContext.Empleado'  is null.");
            }
            var empleado = await _context.Empleado.FindAsync(id);
            if (empleado != null)
            {
                _context.Empleado.Remove(empleado);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadoExists(int id)
        {
          return (_context.Empleado?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
