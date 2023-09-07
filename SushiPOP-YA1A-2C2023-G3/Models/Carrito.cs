namespace SushiPop.Models
{
    public class Carrito
    {
        public int Id { get; set; }
        public bool Procesado { get; set; }
        public bool Cancelado { get; set; }

        /*
         * Relaciones
         */
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public virtual Pedido Pedido { get; set; }
        public ICollection<CarritoItem> CarritosItems { get; set; }
    }
}
