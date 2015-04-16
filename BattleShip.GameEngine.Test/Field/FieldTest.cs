using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;

using BattleShip.GameEngine.Field;
using BattleShip.GameEngine.Field.Cell;
using BattleShip.GameEngine.Arsenal.Protection;
using BattleShip.GameEngine.Location;
using BattleShip.GameEngine.Arsenal.Gun;
using BattleShip.GameEngine.Arsenal.Gun.Destroyable;
using BattleShip.GameEngine.Field.Cell.AttackResult;

namespace BattleShip.GameEngineTest.Field
{
    [TestClass]
    public class FieldTest
    {
        [TestMethod]
        public void InitCorrectData()
        {
            GameEngine.Field.Field field = new GameEngine.Field.Field(11);

            Assert.IsTrue(field.Size == 11);
        }

        [TestMethod]
        public void InitIncorrectData()
        {
            GameEngine.Field.Field field = new GameEngine.Field.Field(5);

            Assert.IsTrue(field.Size == 10);
        }

        [TestMethod]
        public void InitCellsOfFieldData()
        {
            GameEngine.Field.Field field = new GameEngine.Field.Field(5);

            foreach (CellOfField x in field)
            {
                Assert.IsNotNull(x);
            }
        }

        [TestMethod]
        public void InitEmptyAllCells()
        {
            GameEngine.Field.Field field = new GameEngine.Field.Field(5);

            foreach (CellOfField x in field)
            {
                Assert.IsTrue(field.IsCellEmpty(x.Location));
            }
        }

        [TestMethod]
        public void IsFieldRegion()
        {
            GameEngine.Field.Field field = new GameEngine.Field.Field(10);

            Assert.IsTrue(field.IsFielRegion(9, 9));
        }

        [TestMethod]
        public void IsNotFieldRegion()
        {
            GameEngine.Field.Field field = new GameEngine.Field.Field(10);

            Assert.IsFalse(field.IsFielRegion(10, 10) & field.IsFielRegion(0, -1));
        }

        [TestMethod]
        public void IsCellBeginImplicit()
        {
            GameEngine.Field.Field field = new GameEngine.Field.Field(10);

            Position pos = new Position(5, 7);

            PVOProtected pvo = new PVOProtected(0, pos, field.Size);

            Assert.IsTrue(field.AddProtected(pvo));

            Assert.IsTrue(field.IsCellEmpty(pos));
        }

        [TestMethod]
        public void IsCellNotEmptyAfterShot()
        {
            GameEngine.Field.Field field = new GameEngine.Field.Field(10);

            Position pos = new Position(5, 7);

            PVOProtected pvo = new PVOProtected(0, pos, field.Size);

            Assert.IsTrue(field.AddProtected(pvo));

            Assert.IsTrue(field.IsCellEmpty(pos));

            Gun gun = new Gun();
            gun.ChangeCurrentGun(new GunDestroy());

            List<Type> resultsList = field.Shot(gun, pos);
            Assert.IsTrue(resultsList.Count == 1);

            Assert.IsTrue(typeof(GameEngine.Field.Cell.StatusCell.EmptyCell) == resultsList[0]);
        }

    }
}
