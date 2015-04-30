using System;
//не використовуються
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.ConsoleUI.Draw.DrawFields.DrawCell.DrawType
{
    class DrawPVOProtect : IDrawableCell
    {
        public void Draw(bool wasAttacked)
        {
            if (wasAttacked)
            {
                Console.BackgroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
            }

            Console.Write("X");
            Console.ResetColor(); 
        }
    }
}
