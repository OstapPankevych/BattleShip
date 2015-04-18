using System;
using System.CodeDom;
using BattleShip.GameEngine.Arsenal.Flot;
using BattleShip.GameEngine.Arsenal.Gun;
using BattleShip.GameEngine.Arsenal.Gun.Destroyable;
using BattleShip.GameEngine.Arsenal.Protection;
using BattleShip.GameEngine.Field;
using BattleShip.GameEngine.Field.Cell;
using BattleShip.GameEngine.GameEventArgs;
using BattleShip.GameEngine.GameObject;
using BattleShip.GameEngine.Location;
using BattleShip.GameEngine.Field.Cell.AttackResult;
namespace BattleShip.ConsoleUI
{
    internal class Program
    {
        public static void Hit(GameObject o, GameEvenArgs e)
        {
            Console.WriteLine("popali");
        }

        private static void Main(string[] args)
        {
            var p1 = new Position(0, 0);
            var p2 = new Position(0, 1);
            var p3 = new Position(0, 2);

            var ship = new ThreeStoreyShip(0, p1, p2, p3);

            var field = new Field(10);

            //Console.WriteLine("Line = {0}, Column = {1}", field.GetPosition(10).Line, field.GetPosition(10).Column);
            //Console.WriteLine(field.Size);
            Console.WriteLine(field.AddRectangleShip(ship));

            ship.HitMeHandler += Hit;





            Gun gun = new Gun();
            gun.ChangeCurrentGun(new PlaneDestroy());

            Console.WriteLine(gun.GetTypeOfCurrentCun().ToString());

            Position pos = new Position(4, 7);
            CellOfField cell = new CellOfField(pos);
            cell.SetProtect(new PVOProtected(0, pos, 10));
            Type t = cell.Shot(gun);

            if (t == typeof(ProtectedCell))
                Console.WriteLine(t.ToString());


            Console.ReadLine();
        }
    }
}