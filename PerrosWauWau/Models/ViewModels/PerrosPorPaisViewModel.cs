namespace PerrosWauWau.Models.ViewModels
{
    public class PerrosPorPaisViewModel
    {
        public string Nombre { get; set; } = null!;
        public IEnumerable<PerritoViewModel> Perros { get; set; } = null!;
    }
}
