using System;
using BattleShip.GameEngine.Arsenal.Flot.Corectible;
using BattleShip.GameEngine.Location;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleShip.GameEngine.Test.Arsenal.Flot.Corectible
{
    [TestClass]
    public class RactangleTest
    {
        [TestMethod]
        public void IsCorrectLineChackForTrue()
        {
            Position begin = new Position(3, 5);
            Position end = new Position(3, 7);
            Assert.IsTrue(Ractangle.IsCorrectLine(begin, end));
        }

        [TestMethod]
        public void IsCorrectLineChackForFalse()
        {
            Position begin = new Position(4, 5);
            Position end = new Position(3, 7);
            Assert.IsFalse(Ractangle.IsCorrectLine(begin, end));
        }

        [TestMethod]
        public void IsCorrectColumnChackForTrue()
        {
            Position begin = new Position(0, 4);
            Position end = new Position(1, 4);
            Assert.IsTrue(Ractangle.IsCorrectColumn(begin, end));
        }

        [TestMethod]
        public void IsCorrectColumnChackForFalse()
        {
            Position begin = new Position(0, 2);
            Position end = new Position(1, 4);
            Assert.IsFalse(Ractangle.IsCorrectColumn(begin, end));
        }

        [TestMethod]
        public void IsCorrectCountStoreyForTrue()
        {
            byte begin = 1;
            byte end = 4;
            byte count = 4;
            Assert.IsTrue(Ractangle.IsCorrectCountStorey(begin, end, count));
        }

        [TestMethod]
        public void IsCorrectCountStoreyForFalse()
        {
            byte begin = 1;
            byte end = 4;
            byte count = 2;
            Assert.IsFalse(Ractangle.IsCorrectCountStorey(begin, end, count));
        }

        [TestMethod]
        public void ChackShipRegionOneStoreyForTrue()
        {
            byte count = 1;
            Position[] pos = new[] {new Position(1, 1), new Position(1, 1)};
            Assert.IsTrue(Ractangle.ChackShipRegion(count, pos));
        }

        [TestMethod]
        public void ChackShipRegionOneStoreyForFalse()
        {
            byte count = 1;
            Position[] pos = new[] { new Position(1, 2), new Position(1, 1) };
            Assert.IsFalse(Ractangle.ChackShipRegion(count, pos));
        }

        [TestMethod]
        public void ChackShipRegionForCorrectLineNotSimpleShipForTrue()
        {
            byte count = 3;
            Position[] pos = new[] { new Position(1, 2), new Position(1, 3), new Position(1, 4),  };
            Assert.IsTrue(Ractangle.ChackShipRegion(count, pos));
        }

        [TestMethod]
        public void ChackShipRegionForCorrectColumnNotCorrectCountStorey()
        {
            byte count = 6;
            Position[] pos = new[] { new Position(1, 4), new Position(2, 4), new Position(3, 4), };
            Assert.IsFalse(Ractangle.ChackShipRegion(count, pos));
        }

        [TestMethod]
        public void ChackShipRegionForCorrectLineNotNotCorrectCountStorey()
        {
            byte count = 6;
            Position[] pos = new[] { new Position(1, 2), new Position(1, 3), new Position(1, 4), };
            Assert.IsFalse(Ractangle.ChackShipRegion(count, pos));
        }

        [TestMethod]
        public void GetRectangleRegionForNULL()
        {
            byte count = 3;
            Position begin = new Position(1, 2);
            Position end = new Position(5, 6);
            Assert.IsTrue(Ractangle.GetRectangleRegion(count, begin, end) == null);
        }

        [TestMethod]
        public void GetRectangleRegionForOneStorey()
        {
            byte count = 1;
            Position begin = new Position(1, 2);
            Position end = new Position(1, 2);
            Assert.IsTrue(Ractangle.GetRectangleRegion(count, begin, end).Length == 1 & Ractangle.GetRectangleRegion(count, begin, end)[0] == begin & Ractangle.GetRectangleRegion(count, begin, end)[0] == end);
        }

        [TestMethod]
        public void GetRectangleRegionForTwoStorey()
        {
            byte count = 2;
            Position begin = new Position(1, 2);
            Position end = new Position(2, 2);
            Assert.IsTrue(Ractangle.GetRectangleRegion(count, begin, end).Length == 2);
            Position[] region = Ractangle.GetRectangleRegion(count, begin, end);
            Assert.IsTrue(region[0] == begin & region[1] == end);
        }

        [TestMethod]
        public void GetRectangleRegionForThreeStoreyCorrectColumn()
        {
            byte count = 3;
            Position begin = new Position(1, 2);
            Position end = new Position(3, 2);
            Assert.IsTrue(Ractangle.GetRectangleRegion(count, begin, end).Length == 3);
            Position[] region = Ractangle.GetRectangleRegion(count, begin, end);
            Assert.IsTrue(region[0] == begin & region[1] == new Position(2, 2) & region[2] == end);

            begin = new Position(3, 2);
            end = new Position(1, 2);
            Assert.IsTrue(Ractangle.GetRectangleRegion(count, begin, end).Length == 3);
            region = Ractangle.GetRectangleRegion(count, begin, end);
            Assert.IsTrue(region[0] == begin & region[1] == new Position(2, 2) & region[2] == end);
        }

        [TestMethod]
        public void GetRectangleRegionForThreeStoreyForCorrectLine()
        {
            byte count = 3;
            Position begin = new Position(3, 1);
            Position end = new Position(3, 3);

            Assert.IsTrue(Ractangle.GetRectangleRegion(count, begin, end).Length == 3);
            Position[] region = Ractangle.GetRectangleRegion(count, begin, end);
            Assert.IsTrue(region[0] == begin & region[1] == new Position(3, 2) & region[2] == end);

            begin = new Position(3, 3);
            end = new Position(3, 1);
            Assert.IsTrue(Ractangle.GetRectangleRegion(count, begin, end).Length == 3);
            region = Ractangle.GetRectangleRegion(count, begin, end);
            Assert.IsTrue(region[0] == begin & region[1] == new Position(3, 2) & region[2] == end);
        }

        [TestMethod]
        public void GetRectangleRegionForFourStorey()
        {
            byte count = 4;
            Position begin = new Position(3, 1);
            Position end = new Position(3, 4);

            Assert.IsTrue(Ractangle.GetRectangleRegion(count, begin, end).Length == 4);
            Position[] region = Ractangle.GetRectangleRegion(count, begin, end);
            Assert.IsTrue(region[0] == begin & region[1] == new Position(3, 2) &
                region[2] == new Position(3, 3) & region[3] == end);
        }

        [TestMethod]
        public void IsTrueShipRegionForTrue()
        {
            Ractangle ract = new Ractangle();
            byte count = 3;
            Position[] pos = new[] { new Position(1, 2), new Position(1, 3), new Position(1, 4), };
            Assert.IsTrue(ract.IsTrueShipRegion(count, pos));

        }

    }
}
