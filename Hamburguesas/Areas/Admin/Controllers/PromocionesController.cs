using Hamburguesas.Areas.Admin.Models;
using Hamburguesas.Models.Entities;
using Hamburguesas.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Hamburguesas.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PromocionesController : Controller
    {
        MenuRepository _menuRepos { get; }
        public PromocionesController(MenuRepository repos)
        {
            _menuRepos = repos;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Agregar(int id)
        {
            var hamburguesa = _menuRepos.Get(id);
            if (hamburguesa != null)
            {
                AdminAgregarPromocionViewModel vm = new()
                {
                    Id = hamburguesa.Id,
                    Nombre = hamburguesa.Nombre,
                    Precio = (decimal)hamburguesa.Precio,
                    PrecioDescuento = hamburguesa.PrecioPromocion == null || hamburguesa.PrecioPromocion <= 0 ? 0:(decimal)hamburguesa.PrecioPromocion
                };

                return View(vm);
            }
            return RedirectToAction("Index", "Hamburguesa", new { area = "Admin" });
        }
        [HttpPost]
        public IActionResult Agregar(AdminAgregarPromocionViewModel h)
        {
            if(h.PrecioDescuento <= 0)
            {
                ModelState.AddModelError("", "El precio con descuento debe ser mayor a 0");
            }
            if (ModelState.IsValid)
            {
                var hamburguesita = _menuRepos.Get(h.Id);
                if (hamburguesita != null)
                {
                    hamburguesita.PrecioPromocion = (double)h.PrecioDescuento;
                    _menuRepos.Update(hamburguesita);
                }
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            return View(h);
            
        }
        public IActionResult Eliminar(int id)
        {
            var hamburguesa = _menuRepos.Get(id);
            if (hamburguesa != null)
            {
                AdminAgregarPromocionViewModel vm = new()
                {
                    Id = hamburguesa.Id,
                    Nombre = hamburguesa.Nombre,
                    Precio = (decimal)hamburguesa.Precio,
                    PrecioDescuento = hamburguesa.PrecioPromocion == null || hamburguesa.PrecioPromocion <= 0 ? 0 : (decimal)hamburguesa.PrecioPromocion
                };

                return View(vm);
            }
            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }
        [HttpPost]
        public IActionResult Eliminar(AdminAgregarPromocionViewModel h)
        {
            var ham = _menuRepos.Get(h.Id);
            if(ham != null)
            {
                ham.PrecioPromocion = null;
                _menuRepos.Update(ham);
            }
            return RedirectToAction("Index", "Hamburguesa", new { area = "Admin" });
        }
    }
}
