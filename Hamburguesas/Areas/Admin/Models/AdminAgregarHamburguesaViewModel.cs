using Hamburguesas.Models.Entities;
using System.Reflection.Metadata.Ecma335;

namespace Hamburguesas.Areas.Admin.Models
{
    public class AdminAgregarHamburguesaViewModel
    {
        public Menu Hamburguesa { get; set; } = new();
        public IFormFile? Archivo { get; set; }

        public IEnumerable<ClasifModel> Clasificaciones { get; set; } = null!;
    }
    public class ClasifModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
