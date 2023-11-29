namespace Hamburguesas.Areas.Admin.Models
{
    public class AdminAgregarPromocionViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal Precio { get; set; }
        public decimal PrecioDescuento { get; set; }
    }
}
