using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mostrar_Perfil.Models;

namespace Mostrar_Perfil.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
          
            return View();
        }
        public ActionResult MiPerfil()
        {
            MiPerfilModel vm = new();
            return View(vm);
        }

        
    }
}
