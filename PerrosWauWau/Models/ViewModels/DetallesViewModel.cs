namespace PerrosWauWau.Models.ViewModels
{
    public class DetallesViewModel
    {
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int Id { get; set; }
        public string otrosNombres { get; set; } = null!;
        public string paisDeOrigen { get; set; } = null!;
        public string Peso { get; set; } = null!;
        public string Altura { get; set; } = null!;
        public int esperanzaDeVida{ get; set; }
        public int nivelDeEnergia { get; set; }
        public int facilidadDeEntrenamiento { get; set; }
        public int ejercicioObligatorio { get; set; }
        public int amistadConDesconocidos { get; set; }
        public int amistadConPerros { get; set; }
        public int necesidadDeCepillado { get; set; }
        public string Patas { get; set; } = null!;
        public string Cola { get; set; } = null!;
        public string Hocico { get; set; } = null!;
        public string Pelo { get; set; } = null!;
        public string Color { get; set; } = null!;
        public IEnumerable<PerritoViewModel> otrosPerros { get; set; } = null!;

        public int Ataque { get; set; } = 10;
        public int Defensa { get; set; } = 6;
        public string MovimientoDefinitivo { get; set; } = "Super ladrido";



    }
}
