using Hamburguesas.Models.Entities;
using Hamburguesas.Models.ViewModels;
using Hamburguesas.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Hamburguesas.Controllers
{
    public class HomeController : Controller
    {
        ClasificacionRepository _clasificacionRepos { get; }
        MenuRepository _menuRepos { get; }
        public HomeController(ClasificacionRepository repos, MenuRepository repos2)
        {
            _clasificacionRepos = repos;
            _menuRepos = repos2;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Promociones(string id)
        {
            var promociones = _menuRepos.GetPromociones().Select(x => new
            {
                Nombre = x.Nombre
            });
            if(promociones != null && promociones.Any())
            {
                var p = promociones.ToArray();
                if(id  == null)
                {
                    id = p[0].Nombre;
                }
                id = id.Replace("-", " ");
                var datos = _menuRepos.GetHamburguesaPorNombre(id);
                
                int y = p!= null ? Array.FindIndex(p , x=>x.Nombre ==  id) : 0;
                PromocionesViewModel vm = new PromocionesViewModel()
                {
                    Nombre = id,
                    Id = datos != null ? datos.Id : 0,
                    PrecioNormal = datos != null ? (decimal)datos.Precio : 0,
                    PrecioDescuento = (datos != null && datos.PrecioPromocion != null) ? (decimal)datos.PrecioPromocion : 0,
                    Descripcion = datos != null ? datos.Descripción : "",
                    NombreAnterior = y > 0 ? p[y - 1].Nombre : p[p.Length - 1].Nombre,
                    NombreSiguiente = y < p.Length - 1 ? p[y + 1].Nombre : p[0].Nombre


                };
                return View(vm);

            }
            else
            {
                return RedirectToAction("Index");
            }
            


        }
        public IActionResult Menu(string id)
        {

            if (id == null)
            {
                MenuViewModel vm = LlenarMenu();
                return View(vm);
            }
            else
            {
                id = id.Replace("-", " ");
                var hamburguesaSeleccioneada = _menuRepos.GetHamburguesaPorNombre(id);
                if (hamburguesaSeleccioneada != null)
                {
                    MenuViewModel vm = LlenarMenu();
                    vm.Id = hamburguesaSeleccioneada.Id;
                    vm.Descripcion = hamburguesaSeleccioneada.Descripción;
                    return View(vm);
                }
                else
                {
                    return RedirectToAction("Index");
                }

            }

        }
        private MenuViewModel LlenarMenu()
        {
            MenuViewModel vm = new()
            {
                Clasificaciones = _clasificacionRepos.GetClasificaciones().Select(x => new ClasificacionModel
                {
                    Nombre = x.Nombre,
                    Hamburguesas = x.Menu.Select(x => new HamburguesaModel
                    {
                        Id = x.Id,
                        Nombre = x.Nombre,
                        Precio = (decimal)x.Precio
                    }).OrderBy(x => x.Precio)
                })


            };
            return vm;
        }
    }
}
