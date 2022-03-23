using RogueSharp.DiceNotation;
using Proyecto_Juan_Francisco_Cabrera_Rodriguez.Core;

namespace Proyecto_Juan_Francisco_Cabrera_Rodriguez.Monsters
{
#pragma warning disable CS0246 // El nombre del tipo o del espacio de nombres 'Monstruo' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
#pragma warning disable CS0246 // El nombre del tipo o del espacio de nombres 'Monstruo' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
   public class Nomo : Monstruo
#pragma warning restore CS0246 // El nombre del tipo o del espacio de nombres 'Monstruo' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
#pragma warning restore CS0246 // El nombre del tipo o del espacio de nombres 'Monstruo' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
   {
      public static Nomo Create( int level )
      {
         int health = Dice.Roll( "2D5" );
         return new Nomo
         {
            Ataque = Dice.Roll( "1D3" ) + level / 3,
            Oportunidad_de_ataque = Dice.Roll( "25D3" ),
            Conciencia = 10,
            Color = Core.Color.Color_Nomo,
            Defensa = Dice.Roll( "1D3" ) + level / 3,
            Oportunidad_de_defensa = Dice.Roll( "10D4" ),
            Oro = Dice.Roll( "5D5" ),
            Salud = health,
            maxima_salud = health,
            Nombre = "Nomo",
            Velocidad = 14,
            Simbolo = 'N'
         };
      }
   }
}
