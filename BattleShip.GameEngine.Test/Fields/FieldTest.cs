using System;
using BattleShip.GameEngine.Arsenal.Flot.RectangleShips;
using BattleShip.GameEngine.Arsenal.Protection;
using BattleShip.GameEngine.Fields;
using BattleShip.GameEngine.Fields.Cells.StatusCell;
using BattleShip.GameEngine.Fields.Exceptions;
using BattleShip.GameEngine.Location;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleShip.GameEngine.Test.Fields
{
    [TestClass]
    public class FieldTest
    {
        [TestMethod]
        public void Constructor()
        {
            Field field = new Field(10);
            Assert.IsTrue(field.Size == 10);
            for (byte  i = 0; i < 10; i++)
            {
                for (byte j = 0; j < 10; j++)
                {
                    Assert.IsTrue(field[i, j].WasAttacked == false);
                    Assert.IsTrue(field[i, j].IsProtected == false);
                    Assert.IsTrue(field[i, j].GetTypeOfCellObject() == typeof(EmptyCell));
                    Assert.IsTrue(field[new Position(i, j)].GetTypeOfCellObject() == typeof(EmptyCell));
                }
            }

            foreach (var x in field)
            {
                Assert.IsTrue(x.GetTypeOfCellObject() == typeof(EmptyCell));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(OutOfFielRegionException))]
        public void ChackRegion()
        {
            Position pos = BaseField.GetPositionForNumber(150, 10);
        }

        [TestMethod]
        public void ChackRegion1()
        {
            Field field = new Field(10);

            Assert.IsFalse(field.IsFieldRegion(11, 11));
        }

        [TestMethod]
        public void AddShip()
        {
            Field field = new Field(10);

            Assert.IsTrue(field.AddRectangleShip(new OneStoreyRectangleShip(0, new Position(5, 5))));
            Assert.IsFalse(field.AddRectangleShip(new OneStoreyRectangleShip(0, new Position(5, 5))));
        }

        [TestMethod]
        public void AddProtect()
        {
            Field field = new Field(10);

            Assert.IsTrue(field.AddProtected(new Pvo(0, new Position(5, 5), 10)));
            Assert.IsFalse(field.AddProtected(new Pvo(0, new Position(5, 5), 10)));
        }

        [TestMethod]
        public void DestroyProtect()
        {
            Pvo p = new Pvo(0, new Position(0, 0), 2);
            p.KillMe(null);
        }
    }
}
