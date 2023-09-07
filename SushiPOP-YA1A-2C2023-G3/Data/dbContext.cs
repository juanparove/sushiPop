using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SushiPop.Models;

    public class dbContext : DbContext
    {
        public dbContext (DbContextOptions<dbContext> options)
            : base(options)
        {
        }

        public DbSet<SushiPop.Models.Carrito> Carrito { get; set; } = default!;

        public DbSet<SushiPop.Models.CarritoItem>? CarritoItem { get; set; }

        public DbSet<SushiPop.Models.Categoria>? Categoria { get; set; }

        public DbSet<SushiPop.Models.Cliente>? Cliente { get; set; }

        public DbSet<SushiPop.Models.Contacto>? Contacto { get; set; }

        public DbSet<SushiPop.Models.Descuento>? Descuento { get; set; }

        public DbSet<SushiPop.Models.Empleado>? Empleado { get; set; }

        public DbSet<SushiPop.Models.Pedido>? Pedido { get; set; }

        public DbSet<SushiPop.Models.Producto>? Producto { get; set; }

        public DbSet<SushiPop.Models.Reclamo>? Reclamo { get; set; }

        public DbSet<SushiPop.Models.Reserva>? Reserva { get; set; }
    }
