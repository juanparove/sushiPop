namespace SushiPop.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string Foto { get; set; }
        public int Stock { get; set; }
        public decimal Costo { get; set; }
        
        /*
         * Relaciones
         */
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        public ICollection<Descuento> Descuentos { get; set; }
        public ICollection<CarritoItem> CarritosItems { get; set; }
    }
}
