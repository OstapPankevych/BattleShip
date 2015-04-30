using System;
using BattleShip.GameEngine.Fields;
using BattleShip.ConsoleUI.Draw.DrawFields;

namespace BattleShip.ConsoleUI.Draw
{
    static class DrawRegion 
    {
        public static void Draw(BaseField player1Field, BaseField player2Field, string currentPlayerName, bool classic)
        {
            Console.WriteLine("                     BattleShip Game         ");
            Console.WriteLine();
            Console.WriteLine();
            Console.SetCursorPosition(5, 2);
            Console.Write("Field of {0}\n", "Player1");
            Console.SetCursorPosition(33, 2);
            Console.Write("Field of {0}\n", "Player2");
            DrawField.Draw(4, 0, player1Field, classic);
            DrawField.Draw(4, 32, player2Field, classic);
            Console.SetCursorPosition(0, player1Field.Size + 15);
            Console.WriteLine();
            Console.WriteLine(currentPlayerName + ":");
        }
    }
}
