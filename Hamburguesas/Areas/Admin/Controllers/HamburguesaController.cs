using Hamburguesas.Areas.Admin.Models;
using Hamburguesas.Models.Entities;
using Hamburguesas.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hamburguesas.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HamburguesaController : Controller
    {
        ClasificacionRepository _clasificacionRepos { get; }
        MenuRepository _menuRepos { get; }
        public HamburguesaController(ClasificacionRepository repos , MenuRepository repos2) 
        {
            _clasificacionRepos = repos;
            _menuRepos = repos2;
        }
        public IActionResult Index()
        {
            AdminIndexViewModel vm = new()
            {
                Clasificaciones = _clasificacionRepos.GetClasificaciones().Select(x=> new ClasificacionModel
                {
                    Nombre = x.Nombre,
                    Hamburguesas = x.Menu.Select(y=> new HamburguesaModel
                    {
                        Nombre = y.Nombre,
                        Precio = (decimal)y.Precio,
                        PrecioDescuento = y.PrecioPromocion!= null?(decimal)y.PrecioPromocion:0,
                        Descripcion = y.Descripción,
                        Id = y.Id


                    })
                })
            };
            return View(vm);
        }
        public IActionResult Agregar()
        {
           
            AdminAgregarHamburguesaViewModel vm = new()
            {
                Clasificaciones = _clasificacionRepos.GetAll().OrderBy(x => x.Nombre).Select(x => new ClasifModel
                {
                    Id = x.Id,
                    Nombre = x.Nombre
                })
            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult Agregar(AdminAgregarHamburguesaViewModel vm)
        {
            ModelState.Clear();
            //validar
            if (string.IsNullOrWhiteSpace(vm.Hamburguesa.Nombre))
                ModelState.AddModelError("", "Debes agregar un nombre");
            if (string.IsNullOrWhiteSpace(vm.Hamburguesa.Descripción))
                ModelState.AddModelError("", "Debes agregar una descripcion");
            if (vm.Hamburguesa.Precio <= 0)
                ModelState.AddModelError("", "El valor de precio es invalido");
            if(vm.Archivo != null)
            {
                if (vm.Archivo.ContentType != "image/png")
                    ModelState.AddModelError("", "Solo se permiten imagenes de tipo PNG");            
            }
            //imagen
            if (ModelState.IsValid)
            {
                //insert de la hamburguesa
                _menuRepos.Insert(vm.Hamburguesa);
                if (vm.Archivo == null)
                {
                    System.IO.File.Copy("wwwroot/images/burger.png", $"wwwroot/hamburguesas/{vm.Hamburguesa.Id}.png");
                }
                else
                {
                    System.IO.FileStream fs = System.IO.File.Create($"wwwroot/hamburguesas/{vm.Hamburguesa.Id}.png");
                    vm.Archivo.CopyTo(fs);
                    fs.Close();
                }
                return RedirectToAction("Index");
            }
            AdminAgregarHamburguesaViewModel vim = new()
            {
                Hamburguesa = vm.Hamburguesa,
                Archivo = vm.Archivo,
                Clasificaciones = _clasificacionRepos.GetAll().OrderBy(x => x.Nombre).Select(x => new ClasifModel
                {
                    Id = x.Id,
                    Nombre = x.Nombre
                })
            };
            return View(vim);
        }
        public IActionResult Editar(int Id)
        {
            var entidad = _menuRepos.Get(Id);
            if(entidad != null)
            {
                AdminAgregarHamburguesaViewModel vm = new()
                {
                    Hamburguesa = entidad,
                    Clasificaciones = _clasificacionRepos.GetAll().OrderBy(x => x.Nombre).Select(x => new ClasifModel
                    {
                        Id = x.Id,
                        Nombre = x.Nombre
                    }),

                };
                return View(vm);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Editar(AdminAgregarHamburguesaViewModel vm)
        {
            ModelState.Clear();
            if (string.IsNullOrWhiteSpace(vm.Hamburguesa.Nombre))
                ModelState.AddModelError("", "Debes agregar un nombre");
            if (string.IsNullOrWhiteSpace(vm.Hamburguesa.Descripción))
                ModelState.AddModelError("", "Debes agregar una descripcion");
            if (vm.Hamburguesa.Precio <= 0)
                ModelState.AddModelError("", "El valor de precio es invalido");
            if (vm.Archivo != null)
            {
                if (vm.Archivo.ContentType != "image/png")
                    ModelState.AddModelError("", "La imagen debe ser PNG , maldita sea");
            }

            if (ModelState.IsValid)
            {
                var e = _menuRepos.Get(vm.Hamburguesa.Id);
                if (e == null)
                    return RedirectToAction("Index");

                //Editar valores y llamar al repositorio para actualizarlo
                e.Nombre = vm.Hamburguesa.Nombre;
                e.Precio = vm.Hamburguesa.Precio;
                e.Descripción = vm.Hamburguesa.Descripción;
                e.IdClasificacion = vm.Hamburguesa.IdClasificacion;
                _menuRepos.Update(e);


                //Editar foto
                if(vm.Archivo != null)
                {
                    System.IO.FileStream fs = System.IO.File.Create($"wwwroot/hamburguesas/{vm.Hamburguesa.Id}.png");
                    vm.Archivo.CopyTo(fs);
                    fs.Close();
                }
                return RedirectToAction("Index");
            }
            vm.Clasificaciones = _clasificacionRepos.GetAll().OrderBy(x => x.Nombre).Select(x => new ClasifModel
            {
                Id = x.Id,
                Nombre = x.Nombre
            });

            return View(vm);
        }

        [HttpGet]
        public IActionResult Eliminar(int Id)
        {
            var h = _menuRepos.Get(Id);
            if(h != null)
            {
                return View(h);
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Eliminar(Menu m)
        {
            var h = _menuRepos.Get(m.Id);
            if(h != null)
            {
                var ruta = $"wwwroot/hamburguesas/{h.Id}.png";
                _menuRepos.Delete(h);
                if(System.IO.File.Exists(ruta))
                {
                    System.IO.File.Delete(ruta);
                }
            }

            return RedirectToAction("Index");
        }
    }
}
