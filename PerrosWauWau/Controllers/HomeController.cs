using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using PerrosWauWau.Models.Entities;
using PerrosWauWau.Models.ViewModels;

namespace PerrosWauWau.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(string? Id)
        {
            //if(Id != null && Id.Length > 1)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            PerrosContext context = new PerrosContext();
            var letras = context.Razas.Select(x => x.Nombre.FirstOrDefault()).Distinct().OrderBy(x=>x);
            //IEnumerable<char> d= letras;
            IEnumerable<PerritoViewModel> perros;
            if(Id != null)
            {
                  perros = context.Razas.Where(x => x.Nombre.FirstOrDefault() == Id.FirstOrDefault()).Select(x => new PerritoViewModel
                {
                    Nombre = x.Nombre,
                    Id = (int)x.Id
                });
            }
            else
            {
                 perros = context.Razas.OrderBy(x=>x.Nombre).Select(x => new PerritoViewModel
                {
                    Nombre = x.Nombre,
                    Id = (int)x.Id
                });
            }


            IndexViewModel vm = new()
            {
                Perritos = perros,
                Letras = letras,
                //Letra = 
            };
            
            
            return View(vm);
        }

        public IActionResult PerrosPorPais()
        {
            PerrosContext context = new();
            var datos = context.Paises.Include(x => x.Razas).OrderBy(x=>x.Nombre).Select(x => new PerrosPorPaisViewModel
            {
                Nombre = x.Nombre??"Sin nombre",
                Perros = x.Razas.Select(perro => new PerritoViewModel
                {
                    Id = (int)perro.Id,
                    Nombre = perro.Nombre

                })
            }) ;
            return View(datos);
        }

        [Route("/raza/{Id}")]
        public IActionResult Detalles(string Id)
        {
            Id = Id.Replace("-", " ");
            PerrosContext context = new();
            Random r = new();
            var info = context.Razas.Include(x => x.Caracteristicasfisicas).Include(x => x.Estadisticasraza)
                .Where(x => x.Nombre == Id).Select(x => new DetallesViewModel
                {
                    Id = (int)x.Id,
                    Nombre = x.Nombre,
                    Descripcion = x.Descripcion,
                    otrosNombres = x.OtrosNombres ?? "No tiene",
                    paisDeOrigen = x.IdPaisNavigation.Nombre ?? "Sin pais",
                    Peso = $"{x.PesoMin}Kg. - {x.PesoMax}Kg.",
                    Altura = $"{x.AlturaMin}cm. - {x.AlturaMax}cm.",
                    esperanzaDeVida = (int)x.EsperanzaVida,
                    nivelDeEnergia = x.Estadisticasraza != null ? (int)x.Estadisticasraza.NivelEnergia : 0,
                    facilidadDeEntrenamiento = x.Estadisticasraza != null ? (int)x.Estadisticasraza.FacilidadEntrenamiento : 0,
                    ejercicioObligatorio = x.Estadisticasraza != null ? (int)x.Estadisticasraza.EjercicioObligatorio : 0,
                    amistadConDesconocidos = x.Estadisticasraza != null ? (int)x.Estadisticasraza.AmistadDesconocidos : 0,
                    amistadConPerros = x.Estadisticasraza != null ? (int)x.Estadisticasraza.AmistadPerros : 0,
                    necesidadDeCepillado = x.Estadisticasraza != null ? (int)x.Estadisticasraza.NecesidadCepillado : 0,
                    MovimientoDefinitivo = Movs.Poder(r.Next(0, 40)),
                    Ataque = r.Next(0,11),
                    Defensa = r.Next(0,11),
                    Patas = x.Caracteristicasfisicas != null ? x.Caracteristicasfisicas.Patas ?? "Sin patas" : "Sin patas",
                    Cola = x.Caracteristicasfisicas != null ? x.Caracteristicasfisicas.Cola ?? "Sin Cola" : "Sin cola",
                    Hocico = x.Caracteristicasfisicas != null ? x.Caracteristicasfisicas.Hocico ?? "Sin hocico" : "Sin hocico",
                    Pelo = x.Caracteristicasfisicas != null ? x.Caracteristicasfisicas.Pelo ?? "Esta pelon" : "Esta pelon",
                    Color = x.Caracteristicasfisicas != null ? x.Caracteristicasfisicas.Patas ?? "Sin color" : "Sin color",

                    //otrosPerros = perrosrandom.Take(4)
                }).FirstOrDefault() ;
            
            if(info == null)
            {
                return RedirectToAction("Index");
            }
            info.otrosPerros = context.Razas.OrderBy(x => EF.Functions.Random()).Take(4).Select(x => new PerritoViewModel
            {
                Id = (int)x.Id,
                Nombre = x.Nombre ?? "Sin nombre"
            });
            return View(info);
        }

}//Where(x => x.Id == r.Next(0, context.Razas.Count())
}
