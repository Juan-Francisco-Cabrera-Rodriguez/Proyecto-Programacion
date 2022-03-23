using System;
using RLNET;
using RogueSharp.Random;
using Proyecto_Juan_Francisco_Cabrera_Rodriguez.Core;
#pragma warning disable CS0234 // El tipo o el nombre del espacio de nombres 'Systems' no existe en el espacio de nombres 'Proyecto_Juan_Francisco_Cabrera_Rodriguez' (¿falta alguna referencia de ensamblado?)
#pragma warning disable CS0234 // El tipo o el nombre del espacio de nombres 'Systems' no existe en el espacio de nombres 'Proyecto_Juan_Francisco_Cabrera_Rodriguez' (¿falta alguna referencia de ensamblado?)
using Proyecto_Juan_Francisco_Cabrera_Rodriguez.Systems;
#pragma warning restore CS0234 // El tipo o el nombre del espacio de nombres 'Systems' no existe en el espacio de nombres 'Proyecto_Juan_Francisco_Cabrera_Rodriguez' (¿falta alguna referencia de ensamblado?)
#pragma warning restore CS0234 // El tipo o el nombre del espacio de nombres 'Systems' no existe en el espacio de nombres 'Proyecto_Juan_Francisco_Cabrera_Rodriguez' (¿falta alguna referencia de ensamblado?)

namespace Proyecto_Juan_Francisco_Cabrera_Rodriguez
{
   public static class Juego
   {
      private static readonly int _screenWidth = 100;
      private static readonly int _screenHeight = 70;
      private static RLRootConsole _rootConsole;

      private static readonly int _mapWidth = 80;
      private static readonly int _mapHeight = 48;
      private static RLConsole _mapConsole;

      private static readonly int _messageWidth = 80;
      private static readonly int _messageHeight = 11;
      private static RLConsole _messageConsole;

      private static readonly int _statWidth = 20;
      private static readonly int _statHeight = 70;
      private static RLConsole _statConsole;

      private static readonly int _inventoryWidth = 80;
      private static readonly int _inventoryHeight = 11;
      private static RLConsole _inventoryConsole;

      private static int _mapLevel = 1;
      private static bool _renderRequired = true;

#pragma warning disable CS0246 // El nombre del tipo o del espacio de nombres 'Jugador' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
#pragma warning disable CS0246 // El nombre del tipo o del espacio de nombres 'Jugador' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
      public static Jugador Jugador { get; set; }
#pragma warning restore CS0246 // El nombre del tipo o del espacio de nombres 'Jugador' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
#pragma warning restore CS0246 // El nombre del tipo o del espacio de nombres 'Jugador' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
      public static MapaMazmorra MapaMazmorra { get; private set; }
#pragma warning disable CS0246 // El nombre del tipo o del espacio de nombres 'Registro_de_mensajes' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
#pragma warning disable CS0246 // El nombre del tipo o del espacio de nombres 'Registro_de_mensajes' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
      public static Registro_de_mensajes MessageLog { get; private set; }
#pragma warning restore CS0246 // El nombre del tipo o del espacio de nombres 'Registro_de_mensajes' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
#pragma warning restore CS0246 // El nombre del tipo o del espacio de nombres 'Registro_de_mensajes' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
#pragma warning disable CS0246 // El nombre del tipo o del espacio de nombres 'Sistema_de_comando' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
#pragma warning disable CS0246 // El nombre del tipo o del espacio de nombres 'Sistema_de_comando' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
      public static Sistema_de_comando CommandSystem { get; private set; }
#pragma warning restore CS0246 // El nombre del tipo o del espacio de nombres 'Sistema_de_comando' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
#pragma warning restore CS0246 // El nombre del tipo o del espacio de nombres 'Sistema_de_comando' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
#pragma warning disable CS0246 // El nombre del tipo o del espacio de nombres 'Sistema_de_programacion' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
#pragma warning disable CS0246 // El nombre del tipo o del espacio de nombres 'Sistema_de_programacion' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
      public static Sistema_de_programacion sistema_programacion { get; private set; }
#pragma warning restore CS0246 // El nombre del tipo o del espacio de nombres 'Sistema_de_programacion' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
#pragma warning restore CS0246 // El nombre del tipo o del espacio de nombres 'Sistema_de_programacion' no se encontró (¿falta una directiva using o una referencia de ensamblado?)

      public static IRandom Random { get; private set; }

      public static void Main()
      {
         int seed = (int) DateTime.UtcNow.Ticks;
         Random = new DotNetRandom( seed );

         string fontFileName = "terminal8x8.png";

         string consoleTitle = $"Proyecto_Juan_Francisco_Cabrera_Rodriguez - Nivel {_mapLevel} - Seed {seed}";

         MessageLog = new Registro_de_mensajes();
         MessageLog.Add("Estas en el Nivel 1");
         MessageLog.Add( $"Mapa aleatorio generado '{seed}'" );

         _rootConsole = new RLRootConsole( fontFileName, _screenWidth, _screenHeight, 8, 8, 1f, consoleTitle );

         _mapConsole = new RLConsole( _mapWidth, _mapHeight );
         _messageConsole = new RLConsole( _messageWidth, _messageHeight );
         _statConsole = new RLConsole( _statWidth, _statHeight );
         _inventoryConsole = new RLConsole( _inventoryWidth, _inventoryHeight );

         sistema_programacion = new Sistema_de_programacion();

         Generador_de_mapas mapGenerator = new Generador_de_mapas( _mapWidth, _mapHeight, 20, 13, 7, _mapLevel );
         MapaMazmorra = mapGenerator.Crear_mapa();
         MapaMazmorra.Actualizar_campo_vision_jugador();

         CommandSystem = new Sistema_de_comando();

         _rootConsole.Update += OnRootConsoleUpdate;

         _rootConsole.Render += OnRootConsoleRender;

         _inventoryConsole.SetBackColor( 0, 0, _inventoryWidth, _inventoryHeight, Muestras.DbMetal);
         _inventoryConsole.Print( 1, 1, "Inventario", Color.Titulo_texto );

         _rootConsole.Run();
      }

      private static void OnRootConsoleUpdate( object sender, UpdateEventArgs e )
      {
         bool didPlayerAct = false;
         RLKeyPress keyPress = _rootConsole.Keyboard.GetKeyPress();

         if ( CommandSystem.Final_Turno )
         {
            if ( keyPress != null )
            {
               if ( keyPress.Key == RLKey.Up )
               {
                  didPlayerAct = CommandSystem.Mover_jugador( Direccion.Arriba );
               }
               else if ( keyPress.Key == RLKey.Down )
               {
                  didPlayerAct = CommandSystem.Mover_jugador( Direccion.Abajo );
               }
               else if ( keyPress.Key == RLKey.Left )
               {
                  didPlayerAct = CommandSystem.Mover_jugador( Direccion.Izquierda );
               }
               else if ( keyPress.Key == RLKey.Right )
               {
                  didPlayerAct = CommandSystem.Mover_jugador( Direccion.Derecha );
               }
               else if ( keyPress.Key == RLKey.Escape )
               {
                  _rootConsole.Close();
               }
               else if ( keyPress.Key == RLKey.Period )
               {
                  if ( MapaMazmorra.Puede_bajar_al_siguiente_nivel() )
                  {
                     Generador_de_mapas mapGenerator = new Generador_de_mapas( _mapWidth, _mapHeight, 20, 13, 7, ++_mapLevel );
                     MapaMazmorra = mapGenerator.Crear_mapa();
                     MessageLog = new Registro_de_mensajes();
                     CommandSystem = new Sistema_de_comando();
                     _rootConsole.Title = $"Proyecto_Juan_Francisco_Cabrera_Rodriguez - Nivel {_mapLevel}";
                     didPlayerAct = true;
                  }
               }
            }

            if ( didPlayerAct )
            {
               _renderRequired = true;
               CommandSystem.Final_Turno_Jugador();
            }
         }
         else
         {
            CommandSystem.Activar_Monstruos();
            _renderRequired = true;
         }
      }

      private static void OnRootConsoleRender( object sender, UpdateEventArgs e )
      {
         if ( _renderRequired )
         {
            _mapConsole.Clear();
            _statConsole.Clear();
            _messageConsole.Clear();

            MapaMazmorra.Draw( _mapConsole, _statConsole );
            Jugador.Dibujar( _mapConsole, MapaMazmorra );
            Jugador.DrawStats( _statConsole );  
            MessageLog.Dibujar( _messageConsole );

            RLConsole.Blit( _mapConsole, 0, 0, _mapWidth, _mapHeight, _rootConsole, 0, _inventoryHeight );
            RLConsole.Blit( _messageConsole, 0, 0, _messageWidth, _messageHeight, _rootConsole, 0, _screenHeight - _messageHeight );
            RLConsole.Blit( _statConsole, 0, 0, _statWidth, _statHeight, _rootConsole, _mapWidth, 0 );
            RLConsole.Blit( _inventoryConsole, 0, 0, _inventoryWidth, _inventoryHeight, _rootConsole, 0, 0 );

            _rootConsole.Draw();

            _renderRequired = false;
         }
      }
   }
}
