﻿namespace SushiPop.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public int NroPedido { get; set; }
        public DateTime FechaCompra { get; set; }
        public decimal Subtotal { get; set; }
        public decimal GastoEnvio { get; set; }
        public decimal Total { get; set; }
        public int Estado { get; set; }

        /*
         * Relaciones
         */
        public virtual Reclamo Reclamo { get; set; }
        public int CarritoId { get; set; }
        public Carrito Carrito { get; set; }
    }
}
