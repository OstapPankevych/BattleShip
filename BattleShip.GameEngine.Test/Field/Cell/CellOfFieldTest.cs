using System;
using System.Reflection;
using BattleShip.GameEngine.Arsenal.Flot;
using BattleShip.GameEngine.Arsenal.Gun;
using BattleShip.GameEngine.Arsenal.Gun.Destroyable;
using BattleShip.GameEngine.Arsenal.Protection;
using BattleShip.GameEngine.Field.Cell;
using BattleShip.GameEngine.Field.Cell.AttackResult;
using BattleShip.GameEngine.Field.Cell.StatusCell;
using BattleShip.GameEngine.Location;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleShip.GameEngineTest.Field.Cell
{
    [TestClass]
    public class CellOfFieldTest
    {
        [TestMethod]
        public void Init()
        {
            var pos = new Position(3, 5);
            var cell = new CellOfField(pos);

            Assert.IsTrue(cell.Location == pos);

            Assert.IsTrue(cell.Show() == typeof (EmptyCell));

            Assert.IsTrue(cell.GetStatusCell() == typeof (EmptyCell));
        }

        [TestMethod]
        public void IsWasAttackActually()
        {
            var pos = new Position(3, 5);
            var cell = new CellOfField(pos);

            Assert.IsTrue(cell.WasAttacked == false);

            var gun = new Gun();

            cell.Shot(gun);

            Assert.IsTrue(cell.WasAttacked);
        }

        [TestMethod]
        public void AddObject()
        {
            var pos = new Position(3, 5);
            var cell = new CellOfField(pos);

            var ship = new OneStoreyShip(0, pos);

            cell.AddGameObject(ship, true);

            Assert.IsTrue(cell.Show() == typeof (EmptyCell));

            Assert.IsTrue(cell.GetStatusCell() == typeof (OneStoreyShip));

            var gun = new Gun();

            cell.Shot(gun);

            Assert.IsTrue(cell.Show() == typeof (OneStoreyShip));

            Assert.IsTrue(cell.GetStatusCell() == typeof (OneStoreyShip));
        }

        [TestMethod]
        public void Shot()
        {
            var pos = new Position(3, 5);
            var cell = new CellOfField(pos);

            ShipRectangleBase ship = new OneStoreyShip(0, pos);

            cell.AddGameObject(ship, true);

            var gun = new Gun();

            var result = cell.Shot(gun);

            Assert.IsTrue(result == typeof (OneStoreyShip));

            Assert.IsTrue(cell.Show() == typeof (OneStoreyShip));
            Assert.IsTrue(cell.GetStatusCell() == typeof (OneStoreyShip));
        }

        [TestMethod]
        public void ShotWithProtectSimpleGun()
        {
            var pos = new Position(3, 5);
            var cell = new CellOfField(pos);

            var ship = new OneStoreyShip(0, pos);

            var pvo = new PVOProtected(0, pos, 10);

            cell.AddGameObject(pvo, true);

            var gun = new Gun();

            Assert.IsTrue(cell.Show() == typeof (EmptyCell));

            Assert.IsTrue(cell.GetStatusCell() == typeof (PVOProtected));

            var result = cell.Shot(gun);

            Assert.IsTrue(result == typeof (PVOProtected));

            Assert.IsTrue(cell.Show() == typeof (PVOProtected));
            Assert.IsTrue(cell.GetStatusCell() == typeof (PVOProtected));
        }

        [TestMethod]
        public void AddStatus()
        {
            var pos = new Position(3, 5);
            var cell = new CellOfField(pos);

            cell.AddStatus(new AroundShip(pos));

            Assert.IsTrue(cell.Show() == typeof (EmptyCell));
            Assert.IsTrue(cell.GetStatusCell() == typeof (AroundShip));

            var gun = new Gun();
            var t = cell.Shot(gun);

            Assert.IsTrue(t == typeof (AroundShip));

            Assert.IsTrue(cell.GetStatusCell() == typeof (AroundShip));
        }

        [TestMethod]
        public void ShotWithProtectNotSimleGun()
        {
            var pos = new Position(3, 5);
            var cell = new CellOfField(pos);

            var pvo = new PVOProtected(0, pos, 10);


            cell.SetProtect(pvo);


            var gun = new Gun();


            gun.ChangeCurrentGun(new PlaneDestroy());

            var result = cell.Shot(gun);

            Assert.IsTrue(cell.WasAttacked == false);

            Assert.IsTrue(result == typeof (ProtectedCell));
        }

        [TestMethod]
        public void ShotWithProtectSimleGun()
        {
            var pos = new Position(3, 5);
            var cell = new CellOfField(pos);

            var pvo = new PVOProtected(0, pos, 10);


            cell.AddGameObject(pvo, false);


            var gun = new Gun();

            gun.ChangeCurrentGun(new GunDestroy());

            var result = cell.Shot(gun);

            Assert.IsTrue(cell.WasAttacked == true);

            Assert.IsTrue(result == typeof(PVOProtected));
        }
    }
}