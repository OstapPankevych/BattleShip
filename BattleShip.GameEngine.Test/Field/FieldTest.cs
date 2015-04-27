using BattleShip.GameEngine.Arsenal.Flot;
using BattleShip.GameEngine.Arsenal.Gun;
using BattleShip.GameEngine.Arsenal.Gun.Destroyable;
using BattleShip.GameEngine.Arsenal.Protection;
using BattleShip.GameEngine.Location;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BattleShip.GameEngineTest.Field
{
    [TestClass]
    public class FieldTest
    {
        #region PublicMethods

        #region Initialization

        [TestMethod]
        public void InitCorrectData()
        {
            var field = new GameEngine.Field.Field(11);

            Assert.IsTrue(field.Size == 11);
        }

        [TestMethod]
        public void InitIncorrectData()
        {
            var field = new GameEngine.Field.Field(5);

            Assert.IsTrue(field.Size == 10);
        }

        [TestMethod]
        public void InitCellsOfFieldData()
        {
            var field = new GameEngine.Field.Field(5);

            foreach (var x in field)
            {
                Assert.IsNotNull(x);
            }
        }

        [TestMethod]
        public void InitEmptyAllCells()
        {
            var field = new GameEngine.Field.Field(5);

            foreach (var x in field)
            {
                Assert.IsTrue(field.IsCellEmpty(x.Location));
            }
        }

        #endregion Initialization

        #region IsFielRegion : bool

        [TestMethod]
        public void IsFieldRegion()
        {
            var field = new GameEngine.Field.Field(10);

            Assert.IsTrue(field.IsFielRegion(9, 9));
        }

        [TestMethod]
        public void IsNotFieldRegion()
        {
            var field = new GameEngine.Field.Field(10);

            Assert.IsFalse(field.IsFielRegion(10, 10) & field.IsFielRegion(0, -1));
        }

        #endregion IsFielRegion : bool

        #region IsCellEmpty : Position

        [TestMethod]
        public void IsCellEmptyWhenEmpty()
        {
            var field = new BattleShip.GameEngine.Field.Field(10);

            Position pos = new Position(4, 7);

            var ship = new BattleShip.GameEngine.Arsenal.Flot.OneStoreyRectangleShip(0, pos);

            Assert.IsTrue(field.IsCellEmpty(pos));
        }

        [TestMethod]
        public void IsNotCellEmptyWhenNotEmpty()
        {
            var field = new BattleShip.GameEngine.Field.Field(10);

            Position pos = new Position(4, 7);

            var ship = new BattleShip.GameEngine.Arsenal.Flot.OneStoreyRectangleShip(0, pos);

            field.AddRectangleShip(ship);

            Assert.IsTrue(field.IsCellEmpty(pos));

            Gun gun = new Gun();

            field.Shot(gun, pos);

            Assert.IsFalse(field.IsCellEmpty(pos));
        }

        #endregion IsCellEmpty : Position

        #region AddRectangleShip : bool

        [TestMethod]
        public void AddShipAndChackRegionAround()
        {
            Position pos1 = new Position(4, 5);
            Position pos2 = new Position(4, 6);

            var field = new BattleShip.GameEngine.Field.Field(10);

            var ship = new TwoStoreyRectangleShip(0, pos1, pos2);

            Assert.IsTrue(field.AddRectangleShip(ship));

            Gun gun = new Gun();

            Position[] positions = new[]
            {
                new Position(3, 4),
                new Position(3, 5),
                new Position(3, 6),
                new Position(3, 7),
                new Position(4, 4),
                new Position(4, 7),
                new Position(5, 4),
                new Position(5, 5),
                new Position(5, 6),
                new Position(5, 7)
            };
            foreach (Position position in positions)
            {
                Assert.IsTrue(field.Shot(gun, position)[0] == typeof(AroundShip));
            }

            Assert.IsTrue(field.Shot(gun, pos1)[0].BaseType == typeof(ShipBase));
            Assert.IsTrue(field.Shot(gun, pos2)[0].BaseType == typeof(ShipBase));
        }

        [TestMethod]
        public void TryAddShipAboutAnotherShipOrProtect()
        {
            Position pos1 = new Position(4, 5);
            Position pos2 = new Position(4, 6);

            var field = new BattleShip.GameEngine.Field.Field(10);

            var ship = new TwoStoreyRectangleShip(0, pos1, pos2);

            Assert.IsTrue(field.AddRectangleShip(ship));

            Position[] positions = new[]
            {
                new Position(3, 4),
                new Position(3, 5),
                new Position(3, 6),
                new Position(3, 7),
                new Position(4, 4),
                new Position(4, 7),
                new Position(5, 4),
                new Position(5, 5),
                new Position(5, 6),
                new Position(5, 7)
            };
            foreach (Position position in positions)
            {
                Assert.IsFalse(field.AddRectangleShip(new OneStoreyRectangleShip(0, position)));
            }

            foreach (Position position in positions)
            {
                Assert.IsTrue(field.AddProtected(new PVOProtect(0, position, 10)));
            }
        }

        [TestMethod]
        public void TryAddShipOnNotEmptyRegion()
        {
            Position pos1 = new Position(4, 5);
            Position pos2 = new Position(4, 6);

            var field = new BattleShip.GameEngine.Field.Field(10);

            var pvo = new PVOProtect(0, pos1, 10);
            Assert.IsTrue(field.AddProtected(pvo));

            var ship = new TwoStoreyRectangleShip(0, pos1, pos2);

            Assert.IsFalse(field.AddRectangleShip(ship));
        }

        #endregion AddRectangleShip : bool

        #region Shot : List<Type>

        [TestMethod]
        public void ShotSimpleGun()
        {
            Position pos1 = new Position(4, 5);
            Position pos2 = new Position(5, 5);

            var field = new BattleShip.GameEngine.Field.Field(10);

            var ship = new TwoStoreyRectangleShip(0, pos1, pos2);

            Assert.IsTrue(field.AddRectangleShip(ship));

            // клітинка біля кораблика
            Position aroundShip = new Position(4, 6);
            // як пуста - невідома
            Assert.IsTrue(field.IsCellEmpty(aroundShip));
            // не атакована
            Assert.IsFalse(field.WasCellAttack(aroundShip));

            Gun gun = new Gun();
            gun.ChangeCurrentGun(new DoubleDestroy());

            List<System.Type> listResult = field.Shot(gun, pos1);
            Assert.IsNotNull(listResult);
            Assert.IsTrue(listResult.Count == 2);

            foreach (Type type in listResult)
            {
                Assert.IsTrue(type == typeof(TwoStoreyRectangleShip));
            }

            // перевірка івента знищення клітинок навколо кораблика
            Assert.IsFalse(field.IsCellEmpty(aroundShip));
            Assert.IsTrue(field.WasCellAttack(aroundShip));
        }

        [TestMethod]
        public void ShotNotSimpleGunToLineWithProtect()
        {
            Position pos1 = new Position(4, 5);
            Position pos2 = new Position(4, 6);

            var field = new BattleShip.GameEngine.Field.Field(10);

            var ship = new TwoStoreyRectangleShip(0, pos1, pos2);

            Assert.IsTrue(field.AddRectangleShip(ship));

            // клітинка біля кораблика
            Position aroundShip = new Position(4, 6);
            // як пуста - невідома
            Assert.IsTrue(field.IsCellEmpty(aroundShip));
            // не атакована
            Assert.IsFalse(field.WasCellAttack(aroundShip));

            Assert.IsTrue(field.AddProtected(new PVOProtect(0, new Position(4, 9), 10)));

            Gun gun = new Gun();
            gun.ChangeCurrentGun(new PlaneDestroy());

            List<System.Type> listResult = field.Shot(gun, new Position(4, 0));
            Assert.IsNotNull(listResult);
            Assert.IsTrue(listResult.Count == 0);

            // як пуста - невідома
            Assert.IsTrue(field.IsCellEmpty(aroundShip));
            // не атакована
            Assert.IsFalse(field.WasCellAttack(aroundShip));
        }

        #endregion Shot : List<Type>

        #endregion PublicMethods
    }
}