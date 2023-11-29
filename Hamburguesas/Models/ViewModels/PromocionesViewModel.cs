namespace Hamburguesas.Models.ViewModels
{
    public class PromocionesViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public decimal PrecioNormal { get; set; }
        public decimal PrecioDescuento { get; set; }
        public string NombreAnterior { get; set; } = null!;
        public string NombreSiguiente { get; set; } = null!;
    }
}
