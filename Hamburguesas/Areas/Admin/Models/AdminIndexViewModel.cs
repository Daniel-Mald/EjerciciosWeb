namespace Hamburguesas.Areas.Admin.Models
{
    public class AdminIndexViewModel
    {
        public IEnumerable<ClasificacionModel> Clasificaciones { get; set; } = null!;
    }
    public class HamburguesaModel
    {
        public int Id { get; set; }
        public decimal Precio { get; set; }
        public decimal PrecioDescuento { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
    }
    public class ClasificacionModel
    {
        public IEnumerable<HamburguesaModel> Hamburguesas { get; set; } = null!;
        public string Nombre { get; set; } = null!;
    }
}
