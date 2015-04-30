using System;
//не використовуються
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.ConsoleUI.Draw.DrawFields.DrawCell.DrawType
{
    class DrawThreeSoreyShip : IDrawableCell
    {
        public void Draw(bool wasAttacked)
        {
            if (wasAttacked)
            {
                Console.BackgroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.Write("#");
            Console.ResetColor(); 
        }
    }
}
