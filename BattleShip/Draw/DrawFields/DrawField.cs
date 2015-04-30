using System;
using BattleShip.ConsoleUI.Draw.DrawFields.DrawCell;
using BattleShip.ConsoleUI.Draw.DrawFields.DrawCell.DrawType;
using BattleShip.GameEngine.Fields;
using BattleShip.GameEngine.Location;

namespace BattleShip.ConsoleUI.Draw.DrawFields
{
    public static class DrawField
    {
        public static void Draw(int line, int column, BaseField field, bool classic)
        {
            DrawCellWithAllElements drawCell = new DrawCellWithAllElements();

            // намалювати верхні цифри
            Console.SetCursorPosition(column + 2, line);
            for (int l = 0; l < field.Size; l++)
            {
                Console.Write(l + " ");
            }

            // намалювати всі рядки
            for (int l = 0; l < field.Size; l++)
            {
                Console.SetCursorPosition(column, line + 1 + l);
                Console.Write(l);
                if (l < 10)
                {
                    Console.Write(" ");
                }
                
                // намалювати кожен елемент рядка
                for (int c = 0; c < field.Size; c++)
                {
                    if (c > 9)
                    {
                        Console.Write(" ");
                    }
                    drawCell.Draw(field[new Position((byte) l, (byte) (c))]);
                    Console.Write(" ");
                }
            }

            Console.Write('\n');
            Console.Write('\n');
            Console.Write('\n');
            Console.WriteLine("Arsenal:");
            IDrawableCell drInfo = new DrawOneStoreyShip();
            Console.Write("->One storey ship :");
            drInfo.Draw(false);
            Console.Write('\n');

            drInfo = new DrawTwoStoreyShip();
            Console.Write("->Two storey ship :");
            drInfo.Draw(false);
            Console.Write('\n');

            drInfo = new DrawThreeSoreyShip();
            Console.Write("->Three storey ship :");
            drInfo.Draw(false);
            Console.Write('\n');

            drInfo = new DrawFourStoreyShip();
            Console.Write("->Four storey ship :");
            drInfo.Draw(false);
            Console.Write('\n');
            if (!classic)
            {
                drInfo = new DrawPVOProtect();
                Console.Write("->PVO protect :");
                drInfo.Draw(false);
                Console.Write('\n');
            }
            drInfo = new DrawEmptyCell();
            Console.Write("->empty cell on field :");
            drInfo.Draw(false);
            Console.Write('\n');

            Console.WriteLine();
        }
    }
    
}
