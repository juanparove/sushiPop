﻿namespace SushiPop.Models
{
    public class Contacto
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Mensaje { get; set; }
        public bool Leido { get; set; }
    }
}
