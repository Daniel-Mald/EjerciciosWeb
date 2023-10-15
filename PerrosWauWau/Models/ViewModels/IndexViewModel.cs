using PerrosWauWau.Models.Entities;

namespace PerrosWauWau.Models.ViewModels
{
    public class IndexViewModel
    {
        //public char? Letra { get; set; } 
        public IEnumerable<char> Letras { get; set; } = null!;
        public IEnumerable<PerritoViewModel> Perritos { get; set; } = null!; 
    }
    public class PerritoViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
