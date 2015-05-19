using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.ConsoleUI.Draw.DrawFields.DrawCell.DrawType
{
    class DrawEmptyCell : IDrawableCell
    {
        public DrawEmptyCell(bool sequre)
        {
            Sequre = sequre;
        }

        public bool Sequre { get; private set; }

        public void Draw(bool wasAttacked)
        {
            if (wasAttacked)
            {
                Console.BackgroundColor = ConsoleColor.Red;
            }
            else
            {
                if (Sequre)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
            } 
            Console.Write("*");
            Console.ResetColor(); 
        }
    }
}
