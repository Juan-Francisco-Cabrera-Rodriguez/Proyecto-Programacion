using RLNET;
using RogueSharp;
using Proyecto_Juan_Francisco_Cabrera_Rodriguez.Interfaces;

namespace Proyecto_Juan_Francisco_Cabrera_Rodriguez.Core
{
   public class Actor : IActor, IDibuja, Iprogramable
   {
      // IActor
      private int _ataque;
      private int _Oportunidad_de_ataque;
      private int _conciencia;
      private int _defensa;
      private int _Oportunidad_de_defensa;
      private int _oro;
      private int _salud;
      private int _maxima_salud;
      private string _nombre;
      private int _velocidad;

      public int Ataque
      {
         get
         {
            return _ataque;
         }
         set
         {
            _ataque = value;
         }
      }

      public int Oportunidad_de_ataque
      {
         get
         {
            return _Oportunidad_de_ataque;
         }
         set
         {
            _Oportunidad_de_ataque = value;
         }
      }

      public int Conciencia
      {
         get
         {
            return _conciencia;
         }
         set
         {
            _conciencia = value;
         }
      }

      public int Defensa
      {
         get
         {
            return _defensa;
         }
         set
         {
            _defensa = value;
         }
      }

      public int Oportunidad_de_defensa
      {
         get
         {
            return _Oportunidad_de_defensa;
         }
         set
         {
            _Oportunidad_de_defensa = value;
         }
      }

      public int Oro
      {
         get
         {
            return _oro;
         }
         set
         {
            _oro = value;
         }
      }

      public int Salud
      {
         get
         {
            return _salud;
         }
         set
         {
            _salud = value;
         }
      }

      public int maxima_salud
      {
         get
         {
            return _maxima_salud;
         }
         set
         {
            _maxima_salud = value;
         }
      }

      public string Nombre
      {
         get
         {
            return _nombre;
         }
         set
         {
            _nombre = value;
         } 
      }

      public int Velocidad
      {
         get
         {
            return _velocidad;
         }
         set
         {
            _velocidad = value;
         }
      }

      // IDibuja
      public RLColor Color { get; set; }
      public char Simbolo { get; set; }
      public int X { get; set; }
      public int Y { get; set; }
      public void Dibujar( RLConsole console, IMap map )
      {

         if ( !map.GetCell( X, Y ).IsExplored )
         {
            return;
         }

         if ( map.IsInFov( X, Y ) )
         {
            console.Set(X, Y, Color, Core.Color.Piso_BackgroundFOV, Simbolo);
         }
         else
         {
            console.Set(X, Y, Core.Color.Piso, Core.Color.Piso_Background, '.' );
         }
      }

      // Iprogramable
      public int Time
      {
         get
         {
            return Velocidad;
         }
      }
   }
}
