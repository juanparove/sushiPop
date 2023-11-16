﻿using System.ComponentModel.DataAnnotations;

namespace SushiPop.Models
{
    public class Cliente : Usuario
    {
        [Display(Name = "Numero de cliente")]
        public int? NumeroCliente { get; set; }

        /*
         * Relaciones
         */
        public ICollection<Reserva>? Reservas { get; set; }
        public ICollection<Carrito>? Carritos { get; set; }
    }
}
 