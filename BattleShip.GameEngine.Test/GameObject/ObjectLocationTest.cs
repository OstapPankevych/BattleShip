using BattleShip.GameEngine.GameObject;
using BattleShip.GameEngine.Location;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleShip.GameEngineTest.GameObject
{
    [TestClass]
    public class ObjectLocationTest
    {
        [TestMethod]
        public void InitCountParts()
        {
            var point = new Position(4, 6);
            var point2 = new Position(5, 8);
            var ob = new ObjectLocation(point, point2);

            Assert.IsTrue(ob.GetCountParts() == 2);
        }

        [TestMethod]
        public void ChangeLifeToDaed()
        {
            var point = new Position(4, 6);
            var point2 = new Position(5, 8);
            var point3 = new Position(5, 8);
            var ob = new ObjectLocation(point, point2, point3);

            ob.ChangeLifeToDead(point2);

            Assert.IsTrue(ob.ChangeLifeToDead(point2) & ob.GetPositionDeadParts()[0] == point2 &
                          ob.GetCountLifeParts() == 2);
        }

        [TestMethod]
        public void GetCountLifeParts()
        {
            var point = new Position(4, 6);
            var point2 = new Position(5, 8);
            var point3 = new Position(5, 8);
            var ob = new ObjectLocation(point, point2, point3);

            Assert.IsTrue(ob.GetCountLifeParts() == 3 & ob.GetCountParts() == 3);
        }

        [TestMethod]
        public void GetCountLifePartsAfterOneShot()
        {
            var point = new Position(4, 6);
            var point2 = new Position(5, 8);
            var point3 = new Position(5, 8);
            var ob = new ObjectLocation(point, point2, point3);

            ob.ChangeLifeToDead(point2);

            Assert.IsTrue(ob.GetCountLifeParts() == 2);
        }

        [TestMethod]
        public void GetCountDeadParts()
        {
            var point = new Position(4, 6);
            var point2 = new Position(5, 8);
            var point3 = new Position(5, 8);

            var ob = new ObjectLocation(point, point2, point3);

            ob.ChangeLifeToDead(point2);

            Assert.IsTrue(ob.GetCountLifeParts() != ob.GetCountParts());
        }

        [TestMethod]
        public void GetPositionLifeParts()
        {
            var point = new Position(4, 6);
            var point2 = new Position(5, 8);
            var ob = new ObjectLocation(point, point2);

            ob.ChangeLifeToDead(point2);

            Assert.IsTrue(ob.GetPositionsLifeParts()[0] == point);
        }
    }
}