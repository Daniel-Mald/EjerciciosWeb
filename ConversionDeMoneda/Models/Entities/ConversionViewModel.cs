using System.Reflection;

namespace ConversionDeMoneda.Models.Entities
{
    public class ConversionViewModel
    {
        public decimal cantidadOrigen { get; set; }
        public string tipoMonedaOrigen { get; set; } = "pesos";
        public string cantidadDestino { get; set; } = "";
        public string tipoMonedaDestino { get; set; } = "dolares";
        public string transaccion { get; set; } = "PesosADolares";

       
    }
}
