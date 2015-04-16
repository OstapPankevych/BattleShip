using System;

using BattleShip.GameEngine.Field;

using BattleShip.GameEngine.Arsenal.Flot;

using BattleShip.GameEngine.Location;

namespace BattleShip.ConsoleUI
{
    class Program
    {

        public static void Hit(GameEngine.GameObject.GameObject o, GameEngine.GameEventArgs.GameEvenArgs e) 
        {
            Console.WriteLine("popali");
        }


        static void Main(string[] args)
        {

            Position p1 = new Position(0, 0);
            Position p2 = new Position(0, 1);
            Position p3 = new Position(0, 2);

            ThreeStoreyShip ship = new ThreeStoreyShip(0, p1, p2, p3);

            Field field = new Field(10);

            //Console.WriteLine("Line = {0}, Column = {1}", field.GetPosition(10).Line, field.GetPosition(10).Column);
            //Console.WriteLine(field.Size);
            Console.WriteLine(field.AddRectangleShip(ship));

            ship.HitMeHandler += Hit;

            //field.

            //Console.WriteLine(p1==p2);

            Console.ReadLine();
        }
    }
}
