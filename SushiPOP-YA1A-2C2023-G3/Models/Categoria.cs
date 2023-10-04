﻿using SushiPOP_YA1A_2C2023_G3.Models;
using System.ComponentModel.DataAnnotations;

namespace SushiPop.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [MaxLength(100, ErrorMessage = ErrorViewModel.CaracteresMaximos)]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        
        /*
         * Relaciones
         */        
        public ICollection<Producto> Productos { get; set; }
    }
}
