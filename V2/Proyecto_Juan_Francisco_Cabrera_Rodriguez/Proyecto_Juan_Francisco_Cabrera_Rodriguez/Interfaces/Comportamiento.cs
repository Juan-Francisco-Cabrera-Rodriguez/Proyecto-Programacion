using Proyecto_Juan_Francisco_Cabrera_Rodriguez.Core;
using Proyecto_Juan_Francisco_Cabrera_Rodriguez.Systems;

namespace Proyecto_Juan_Francisco_Cabrera_Rodriguez.Interfaces
{
   public interface Comportamiento
   {
      bool Act( Monstruo monster, Sistema_de_comando commandSystem );
   }
}