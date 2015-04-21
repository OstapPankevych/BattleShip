


using System;
using System.Linq.Expressions;
using BattleShip.ConsoleUI.Draw;
using BattleShip.ConsoleUI.Draw.DrawArsenal.DrawGunArsenal;
using BattleShip.ConsoleUI.Draw.DrawField;
using BattleShip.GameEngine.Arsenal.Flot;
using BattleShip.GameEngine.Arsenal.Flot.Corectible;
using BattleShip.GameEngine.Arsenal.Flot.RectangleShips;
using BattleShip.GameEngine.Arsenal.Gun.Destroyable;
using BattleShip.GameEngine.Arsenal.Protection;
using BattleShip.GameEngine.Field.Cell.StatusCell;
using BattleShip.GameEngine.Game.GameMode;
using BattleShip.GameEngine.Game.GameMode.ClassicGameMode;
using BattleShip.GameEngine.Game.Players.Computer;
using BattleShip.GameEngine.Game.Players.Computer.Brain;
using BattleShip.GameEngine.Game.Players.Computer.Brain.Play;
using BattleShip.GameEngine.Game.Players.Computer.Brain.SetObjects.SetProtect;
using BattleShip.GameEngine.Game.Players.Computer.Brain.SetObjects.SetRectangleShip;
using BattleShip.GameEngine.Game.Players.Man;
using BattleShip.GameEngine.Location;


namespace BattleShip.ConsoleUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Man ostap = new Man("Ostap", new ClassicGameMode());

            DrawPlayerRegion.Draw(ostap, 0, 0);

            SetFourStoreyShip(ostap);


            Console.Clear();

            DrawPlayerRegion.Draw(ostap, 0, 0);

            Console.WriteLine("Please wait while computer is setting his game objects....");

            Computer comp = new Computer(new Brain(new PlayNotMind(), new SetProtect(), new SetShip()), new ClassicGameMode());

            //Console.Clear();

            DrawPlayerRegion.Draw(comp, 0, 30);
            

            Console.ReadLine();
        }


        private static void SetFourStoreyShip(Man man)
        {
            Position begin;
            Position midle1 = new Position(0, 0);
            Position midle2 = new Position(0, 0);
            Position end;

            // встановлюємо 4-х палубний

            bool exit = false;
            do
            {
                Console.Clear();

                Console.WriteLine("Set positions for FourStoreyShip: ");

                
                Console.WriteLine("Begin position :");
                GetPositions(out begin, man.CurreField.Size);

                Console.WriteLine("End position :");
                GetPositions(out end, man.CurreField.Size);

                // перевірка чи такий діапазон сприятливий для такого кораблика
                if (!Ractangle.ChackShipRegion(4, begin, end))
                {
                    Console.WriteLine("Bad region for this ship type");
                    Console.WriteLine("Press any key for reenter region");
                    Console.ReadLine();
                    exit = false;
                    continue;
                }

                if (Ractangle.IsCorrectColumn(begin, end))
                {
                    if (begin.Line < end.Line)
                    {
                        midle1 = new Position((byte)(begin.Line + 1), begin.Column);
                        midle2 = new Position((byte)(begin.Line + 2), begin.Column);
                    }
                    else if (end.Line < begin.Line)
                    {
                        midle1 = new Position((byte)(end.Line + 1), begin.Column);
                        midle2 = new Position((byte)(end.Line + 2), begin.Column);
                    }
                }
                else if (Ractangle.IsCorrectLine(begin, end))
                {
                    if (begin.Column < end.Column)
                    {
                        midle1 = new Position(begin.Line, (byte)(begin.Column + 1));
                        midle2 = new Position(begin.Line, (byte)(begin.Column + 2));
                    }
                    else if (end.Column < begin.Column)
                    {
                        midle1 = new Position(begin.Line, (byte)(end.Column + 1));
                        midle2 = new Position(begin.Line, (byte)(end.Column + 2));
                    }
                }

                exit = man.CurreField.AddRectangleShip(new FourStoreyRectangleShip(0, begin, midle1, midle2, end));

            } while (!exit);
            
        }

        private static void SetThreeStoreyShip(Man man)
        {
            Position begin;
            Position midle1 = new Position(0, 0);
            Position end;

            for (int i = 0; i < 2; i++)
            {
                bool exit = false;
                do
                {
                    Console.Clear();

                    Console.WriteLine("Set positions for ThreeStoreyShip: count == {0}", i);

                    Console.WriteLine("Begin position :");
                    GetPositions(out begin, man.CurreField.Size);

                    Console.WriteLine("End position :");
                    GetPositions(out end, man.CurreField.Size);

                    // перевірка чи такий діапазон сприятливий для такого кораблика
                    if (!Ractangle.ChackShipRegion(4, begin, end))
                    {
                        Console.WriteLine("Bad region for this ship type");
                        Console.WriteLine("Press any key for reenter region");
                        Console.ReadLine();
                        exit = false;
                        continue;
                    }

                    if (Ractangle.IsCorrectColumn(begin, end))
                    {
                        if (begin.Line < end.Line)
                        {
                            midle1 = new Position((byte)(begin.Line + 1), begin.Column);
                        }
                        else if (end.Line < begin.Line)
                        {
                            midle1 = new Position((byte)(end.Line + 1), begin.Column);
                        }
                    }
                    else if (Ractangle.IsCorrectLine(begin, end))
                    {
                        if (begin.Column < end.Column)
                        {
                            midle1 = new Position(begin.Line, (byte)(begin.Column + 1));
                        }
                        else if (end.Column < begin.Column)
                        {
                            midle1 = new Position(begin.Line, (byte)(end.Column + 1));
                        }
                    }

                    exit = man.CurreField.AddRectangleShip(new ThreeStoreyRectangleShip(0, begin, midle1, end));

                } while (!exit);
            }
        }


        private static void SetTwoStoreyShip(Man man)
        {
            Position begin;
            Position end;

            for (int i = 0; i < 3; i++)
            {
                // встановлюємо 4-х палубний
                bool exit = false;
                do
                {
                    Console.Clear();

                    Console.WriteLine("Set positions for TwoStoreyShip: count == {0}", i);

                    Console.WriteLine("Begin position :");
                    GetPositions(out begin, man.CurreField.Size);

                    Console.WriteLine("End position :");
                    GetPositions(out end, man.CurreField.Size);

                    // перевірка чи такий діапазон сприятливий для такого кораблика
                    if (!Ractangle.ChackShipRegion(4, begin, end))
                    {
                        Console.WriteLine("Bad region for this ship type");
                        Console.WriteLine("Press any key for reenter region");
                        Console.ReadLine();
                        exit = false;
                        continue;
                    }

                    exit = man.CurreField.AddRectangleShip(new TwoStoreyRectangleShip(0, begin, end));

                } while (!exit);
            }
        }

        private static void SetOneStoreyShip(Man man)
        {
            Position begin;

            for (int i = 0; i < 3; i++)
            {
                // встановлюємо 4-х палубний
                do
                {
                    Console.Clear();

                    Console.WriteLine("Set positions for ThreeStoreyShip: ");

                    Console.WriteLine("Begin position :");
                    GetPositions(out begin, man.CurreField.Size);

                } while (!man.CurreField.AddRectangleShip(new OneStoreyRectangleShip((byte)i, begin)));
            }
        }

     

        private static void GetPositions(out Position pos, byte fieldSize)
        {
            int Line = 0;
            int Column = 0;

            string variable;

            Console.WriteLine("->Enter position (Line, Column)");

            do
            {
                Console.Write("Line = ");

                variable = Console.ReadLine();

                if (Int32.TryParse(variable, out Line)) 
                {
                    if ((Line >= fieldSize || Line < 0))
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

            do
            {
                Console.Write("Column = ");

                variable = Console.ReadLine();

                if (Int32.TryParse(variable, out Column))
                {
                    if ((Column >= fieldSize || Column < 0))
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

            pos= new Position((byte)Line, (byte)Column);
        }
    }

    
}