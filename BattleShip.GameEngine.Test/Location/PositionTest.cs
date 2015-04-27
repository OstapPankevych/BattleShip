using BattleShip.GameEngine.Location;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleShip.GameEngine.Test.Location
{
    [TestClass]
    public class PositionTest
    {
        [TestMethod]
        public void InitBytes()
        {
            var pos = new Position(3, 5);

            Assert.IsTrue(pos.Line == 3 & pos.Column == 5);
        }

        [TestMethod]
        public void InitAnotherPosition()
        {
            var pos1 = new Position(3, 5);

            var pos2 = new Position(pos1);

            Assert.IsTrue(pos2.Line == pos1.Line & pos2.Column == pos1.Column);
        }

        [TestMethod]
        public void InitTrueForStruct()
        {
            var pos1 = new Position(3, 5);

            var pos2 = new Position(pos1);

            Assert.AreNotSame(pos1, pos2);
        }

        [TestMethod]
        public void AreNotEqualOperator()
        {
            var pos1 = new Position(3, 5);

            var pos2 = new Position(pos1.Line, 0);

            Assert.AreNotEqual(pos1, pos2);

            Assert.IsTrue(pos1 != pos2);
        }

        [TestMethod]
        public void AreEqual()
        {
            var pos1 = new Position(3, 5);

            var pos2 = new Position(3, 5);

            Assert.IsTrue(pos1 == pos2);
            Assert.IsTrue(pos1.Equals(pos2));
            Assert.AreEqual(pos1, pos2);
        }

    }
}