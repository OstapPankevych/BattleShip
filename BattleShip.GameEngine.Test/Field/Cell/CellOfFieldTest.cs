using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using BattleShip.GameEngine.Location;
using BattleShip.GameEngine.Field.Cell;
using BattleShip.GameEngine.Field.Cell.StatusCell;
using BattleShip.GameEngine.Arsenal.Flot;
using BattleShip.GameEngine.Arsenal.Gun;
using BattleShip.GameEngine.Arsenal.Protection;
using BattleShip.GameEngine.Arsenal.Gun.Destroyable;
using BattleShip.GameEngine.Field.Cell.AttackResult;



namespace BattleShip.GameEngineTest.Field.Cell
{
    [TestClass]
    public class CellOfFieldTest
    {
        [TestMethod]
        public void Init()
        {
            Position pos = new Position(3, 5);
            GameEngine.Field.Cell.CellOfField cell = new CellOfField(pos);

            Assert.IsTrue(cell.Location == pos);

            Assert.IsTrue(cell.Show() == typeof(EmptyCell));

            Assert.IsTrue(cell.GetStatusCell() == typeof(EmptyCell));
        }

        [TestMethod]
        public void AddObject()
        {
            Position pos = new Position(3, 5);
            GameEngine.Field.Cell.CellOfField cell = new CellOfField(pos);

            OneStoreyShip ship = new OneStoreyShip(0, pos);

            cell.AddShip(ship);

            Assert.IsTrue(cell.Show() == typeof(EmptyCell));

            Assert.IsTrue(cell.GetStatusCell() == typeof(OneStoreyShip));

            Gun gun = new Gun();

            cell.Shot(gun);

            //Assert.IsTrue(cell.Show() == typeof(OneStoreyShip));

            Assert.IsTrue(cell.GetStatusCell() == typeof(OneStoreyShip));
        }

        [TestMethod]
        public void Shot()
        {
            Position pos = new Position(3, 5);
            GameEngine.Field.Cell.CellOfField cell = new CellOfField(pos);

            ShipRectangleBase ship = new OneStoreyShip(0, pos);

            cell.AddShip(ship);

            Gun gun = new Gun();

            Type result = cell.Shot(gun);

            Assert.IsTrue(result == typeof(OneStoreyShip));

            Assert.IsTrue(cell.Show() == typeof(OneStoreyShip));
            Assert.IsTrue(cell.GetStatusCell() == typeof(OneStoreyShip));
        }

        [TestMethod]
        public void ShotWithProtectSimpleGun()
        {
            Position pos = new Position(3, 5);
            GameEngine.Field.Cell.CellOfField cell = new CellOfField(pos);

            OneStoreyShip ship = new OneStoreyShip(0, pos);

            PVOProtected pvo = new PVOProtected(0, pos, 10);

            cell.AddProtect(pvo);

            Gun gun = new Gun();

            Assert.IsTrue(cell.Show() == typeof(EmptyCell));

            Assert.IsTrue(cell.GetStatusCell() == typeof(PVOProtected));

            Type result = cell.Shot(gun);

            Assert.IsTrue(result == typeof(PVOProtected));

            Assert.IsTrue(cell.Show() == typeof(PVOProtected));
            Assert.IsTrue(cell.GetStatusCell() == typeof(PVOProtected));
        }

        [TestMethod]
        public void ShotWithProtectNotSimleGun()
        {
            Position pos = new Position(3, 5);
            GameEngine.Field.Cell.CellOfField cell = new CellOfField(pos);

            OneStoreyShip ship = new OneStoreyShip(0, pos);

            PVOProtected pvo = new PVOProtected(0, pos, 10);

            cell.AddProtect(pvo);

            Gun gun = new Gun();

            gun.ChangeCurrentGun(new PlaneDestroy());

            Assert.IsTrue(cell.Show() == typeof(EmptyCell));

            Assert.IsTrue(cell.GetStatusCell() == typeof(PVOProtected));

            Type result = cell.Shot(gun);

            Assert.IsTrue(result == typeof(ProtectedCell));

            //Assert.IsTrue(cell.Show() == typeof(PVOProtected));
            //Assert.IsTrue(cell.GetStatusCell() == typeof(PVOProtected));
        }
    }
}
