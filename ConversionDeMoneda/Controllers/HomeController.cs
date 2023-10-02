using ConversionDeMoneda.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ConversionDeMoneda.Controllers
{
    public class HomeController : Controller
    {
        //[HttpPost]
        public IActionResult Index(ConversionViewModel vm)
        {
            
            if (vm.transaccion == "PesosADolares")
            {
                vm.cantidadDestino = (vm.cantidadOrigen / 18).ToString("c");
                vm.tipoMonedaOrigen = "pesos";
                vm.tipoMonedaDestino = "dolares";
            }
            else
            {
                vm.cantidadDestino = (vm.cantidadOrigen * 18).ToString("c");
                vm.tipoMonedaOrigen = "dolares";
                vm.tipoMonedaDestino = "pesos";
            }
                return View(vm);
        }
        
    }
}
