using System;
<<<<<<< HEAD
//не використовуються
=======
>>>>>>> adcb4d49f57b1a9c51a12f9f9099df7db01d1a0d
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.ConsoleUI.Draw.DrawFields.DrawCell.DrawType
{
    class DrawAroundShip : IDrawableCell
    {
        public void Draw(bool wasAttacked)
        {
            if (wasAttacked)
            {
                Console.BackgroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
            }

            Console.Write("#");
            Console.ResetColor(); 
        }
    }
}
