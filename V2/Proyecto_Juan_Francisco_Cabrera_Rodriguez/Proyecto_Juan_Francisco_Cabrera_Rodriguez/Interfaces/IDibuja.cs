using RLNET;
using RogueSharp;

namespace Proyecto_Juan_Francisco_Cabrera_Rodriguez.Interfaces
{
   public interface IDibuja
   {
      RLColor Color { get; set; }
      char Simbolo { get; set; }
      int X { get; set; }
      int Y { get; set; }

      void Dibujar( RLConsole console, IMap map );
   }
}
