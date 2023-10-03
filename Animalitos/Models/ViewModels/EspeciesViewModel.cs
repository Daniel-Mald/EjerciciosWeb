using Animalitos.Models.Entities;

namespace Animalitos.Models.ViewModels
{
    public class EspeciesViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public IEnumerable<EspeciesSoloId> especiesFotos { get; set; } = null!;
    }
    public class EspeciesSoloId
    {
        public int Id { get; set; }
        public string Nombre { get; set;} = null!;
    }
}
