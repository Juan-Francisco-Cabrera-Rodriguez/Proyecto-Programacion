using Proyecto_Juan_Francisco_Cabrera_Rodriguez.Core;
using RogueSharp.DiceNotation;

namespace Proyecto_Juan_Francisco_Cabrera_Rodriguez.Monsters
{
    public class Trolls : Monstruo
    {
        public static Trolls Create(int level)
        {
            int health = Dice.Roll("2D5");
            return new Trolls
            {
                Ataque = Dice.Roll("1D3") + level / 3,
                Oportunidad_de_ataque = Dice.Roll("25D3"),
                Conciencia = 10,
                Color = Core.Color.Color_Trolls,
                Defensa = Dice.Roll("1D3") + level / 3,
                Oportunidad_de_defensa = Dice.Roll("10D4"),
                Oro = Dice.Roll("5D5"),
                Salud = health,
                maxima_salud = health,
                Nombre = "Trolls",
                Velocidad = 14,
                Simbolo = 'T'
            };
        }
    }
}
