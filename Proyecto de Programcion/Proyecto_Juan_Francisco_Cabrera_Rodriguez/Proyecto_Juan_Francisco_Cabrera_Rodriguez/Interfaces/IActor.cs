namespace Proyecto_Juan_Francisco_Cabrera_Rodriguez.Interfaces
{
   public interface IActor
   {
      int Ataque { get; set; }
      int Oportunidad_de_ataque { get; set; }
      int Conciencia { get; set; }
      int Defensa { get; set; }
      int Oportunidad_de_defensa { get; set; }
      int Oro { get; set; }
      int Salud { get; set; }
      int maxima_salud { get; set; }
      string Nombre { get; set; }
      int Velocidad { get; set; }
   }
}
