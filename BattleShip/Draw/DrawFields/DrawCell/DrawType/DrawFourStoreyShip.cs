using System;

namespace BattleShip.ConsoleUI.Draw.DrawFields.DrawCell.DrawType
{
    class DrawFourStoreyShip : IDrawableCell
    {
        public void Draw(bool wasAttacked)
        {
            if (wasAttacked)
            {
                Console.BackgroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
            }

            Console.Write("#");
            Console.ResetColor(); 
        }
    }
}
