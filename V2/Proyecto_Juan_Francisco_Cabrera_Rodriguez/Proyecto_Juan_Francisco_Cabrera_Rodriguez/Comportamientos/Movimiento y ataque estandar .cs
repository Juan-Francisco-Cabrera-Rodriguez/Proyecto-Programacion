using System.Linq;
using RogueSharp;
using Proyecto_Juan_Francisco_Cabrera_Rodriguez.Core;
using Proyecto_Juan_Francisco_Cabrera_Rodriguez.Interfaces;
using Proyecto_Juan_Francisco_Cabrera_Rodriguez.Systems;

namespace Proyecto_Juan_Francisco_Cabrera_Rodriguez.Behaviors
{
   public class Movimiento_y_ataque_estándar : Comportamiento
   {
      public bool Act( Monstruo monstruo, Sistema_de_comando sistema_de_comando )
      {
         MapaMazmorra mapamazmorra = Juego.MapaMazmorra;
         Jugador jugador = Juego.Jugador;
         FieldOfView monsterFov = new FieldOfView( mapamazmorra );

         if ( !monstruo.Turno_alertado.HasValue )
         {
            monsterFov.ComputeFov( monstruo.X, monstruo.Y, monstruo.Conciencia, true );
            if ( monsterFov.IsInFov( jugador.X, jugador.Y ) )
            {
               Juego.MessageLog.Add( $"{monstruo.Nombre} Esta ansioso por luchar {jugador.Nombre}" );
               monstruo.Turno_alertado = 1;
            }
         }
         
         if ( monstruo.Turno_alertado.HasValue )
         {
            mapamazmorra.Es_transitable( monstruo.X, monstruo.Y, true );
            mapamazmorra.Es_transitable( jugador.X, jugador.Y, true );

            PathFinder pathFinder = new PathFinder( mapamazmorra );
            Path path = null;

            try
            {
               path = pathFinder.ShortestPath( 
                  mapamazmorra.GetCell( monstruo.X, monstruo.Y ), 
                  mapamazmorra.GetCell( jugador.X, jugador.Y ) );
            }
            catch ( PathNotFoundException )
            {
               Juego.MessageLog.Add( $"{monstruo.Nombre} espera un turno " );
            }

            mapamazmorra.Es_transitable( monstruo.X, monstruo.Y, false );
            mapamazmorra.Es_transitable( jugador.X, jugador.Y, false );

            if ( path != null )
            {
               try
               {

                  sistema_de_comando.Mover_Monstruo( monstruo, path.Steps.First() );
               }
               catch ( NoMoreStepsException )
               {
                  Juego.MessageLog.Add( $"{monstruo.Nombre} gruñe de frustracion " );
               }
            }

            monstruo.Turno_alertado++;


            if ( monstruo.Turno_alertado > 15 )
            {
               monstruo.Turno_alertado = null;
            }
         }
         return true;
      }
   }
}
