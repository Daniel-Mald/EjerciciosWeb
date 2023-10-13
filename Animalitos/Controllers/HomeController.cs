using Animalitos.Models.Entities;
using Animalitos.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Animalitos.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            AnimalesContext context = new AnimalesContext();
            
            var clases = context.Clase.OrderBy(x => x.Nombre);
          
            return View(clases);
        }
        public IActionResult Especie(string Id)
        {
            AnimalesContext context = new();
            Especies modelo = context.Especies.Include(x=>x.IdClaseNavigation)
                .FirstOrDefault(x => x.Especie == Id) ?? new Especies() ;
            return View(modelo);
        }//hola que tal
        public IActionResult Especies(string Id)
        {
            AnimalesContext context = new AnimalesContext();
            var clases = context.Clase.Where(x => x.Nombre.ToLower() == Id.ToLower()).FirstOrDefault();
            var espe = context.Especies.Where(x=>x.IdClase == clases.Id).Select(x => new EspeciesSoloId { Id = x.Id ,Nombre = x.Especie});
            EspeciesViewModel vm = new EspeciesViewModel
            {
                Nombre = Id,
                especiesFotos = espe,
                Id = clases.Id
            };
            return View(vm);
        }
    }
}
