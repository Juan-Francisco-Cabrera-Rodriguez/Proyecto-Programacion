using RLNET;
using RogueSharp;
using Proyecto_Juan_Francisco_Cabrera_Rodriguez.Interfaces;

namespace Proyecto_Juan_Francisco_Cabrera_Rodriguez.Core
{
   public class Escalera : IDibuja
   {
      public RLColor Color
      {
         get; set;
      }
      public char Simbolo
      {
         get; set;
      }
      public int X
      {
         get; set;
      }
      public int Y
      {
         get; set;
      }
      public bool IsUp
      {
         get; set;
      }

      public void Dibujar( RLConsole console, IMap map )
      {
         if ( !map.GetCell( X, Y ).IsExplored )
         {
            return;
         }

         Simbolo = IsUp ? '<' : '>';

         if ( map.IsInFov( X, Y ) )
         {
                Color = Core.Color.Jugador;
         }
         else
         {
                Color = Core.Color.Piso;
         }

         console.Set( X, Y, Color, null, Simbolo );
      }
   }
}
