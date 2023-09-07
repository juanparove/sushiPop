namespace SushiPop.Models
{
    public class Cliente : Usuario
    {
        public int NumeroCliente { get; set; }

        /*
         * Relaciones
         */
        public ICollection<Reserva> Reservas { get; set; }
        public ICollection<Carrito> Carritos { get; set; }
    }
}
