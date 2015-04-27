using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.GameEngine.Location;

namespace BattleShip.ConsoleUI.ConsoleCore
{
    public static class Get
    {
        public static Position GetPosition(byte fieldSize)
        {
            int Line = 0;
            int Column = 0;

            string variable;

            int x = 0;
            for (int i = 0; i < 2; i++)
            {
                do
                {
                    if (i == 0)
                    {
                        Console.Write("Line = ");
                    }
                    else
                    {
                        Console.Write("Column = ");
                    }

                    variable = Console.ReadLine();

                    if (Int32.TryParse(variable, out x))
                    {
                        if ((x >= fieldSize || x < 0))
                        {
                            Console.WriteLine("Out of field diapazon");
                            continue;
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Bad input variable!!!");
                        continue;
                    }
                } while (true);

                if (i == 0)
                {
                    Column = x;
                }
                else
                {
                    Line = x;
                }
            }

            return new Position((byte)Column, (byte)Line);
        }

        public static bool QuertyGetRandom()
        {
            Console.WriteLine("(y/n)");
            string result;

            bool retry = false;
            do
            {
                result = Console.ReadLine();
                if (result == "y")
                {
                    return true;
                }
                else if (result == "n")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Bad input result");
                    retry = true;
                }
            } while (retry);
            return false;
        }
    }
}
