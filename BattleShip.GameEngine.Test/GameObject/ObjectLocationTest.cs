using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using BattleShip.GameEngine.Location;
using BattleShip.GameEngine.GameObject;

namespace BattleShip.GameEngineTest.GameObject
{
    [TestClass]
    public class ObjectLocationTest
    {
        [TestMethod]
        public void InitCountParts()
        {
            Position point = new Position(4, 6);
            Position point2 = new Position(5, 8);
            ObjectLocation ob = new ObjectLocation(point, point2);

            Assert.IsTrue(ob.GetCountParts() == 2);
        }

        [TestMethod]
        public void ChangeLifeToDaed()
        {
            Position point = new Position(4, 6);
            Position point2 = new Position(5, 8);
            Position point3 = new Position(5, 8);
            ObjectLocation ob = new ObjectLocation(point, point2, point3);

            ob.ChangeLifeToDead(point2);

            Assert.IsTrue(ob.ChangeLifeToDead(point2) & ob.GetPositionDeadParts()[0] == point2 & ob.GetCountLifeParts() == 2);
        }

        [TestMethod]
        public void GetCountLifeParts()
        {
            Position point = new Position(4, 6);
            Position point2 = new Position(5, 8);
            Position point3 = new Position(5, 8);
            ObjectLocation ob = new ObjectLocation(point, point2, point3);

            Assert.IsTrue(ob.GetCountLifeParts() == 3 & ob.GetCountParts() == 3);
        }

        [TestMethod]
        public void GetCountLifePartsAfterOneShot()
        {
            Position point = new Position(4, 6);
            Position point2 = new Position(5, 8);
            Position point3 = new Position(5, 8);
            ObjectLocation ob = new ObjectLocation(point, point2, point3);

            ob.ChangeLifeToDead(point2);

            Assert.IsTrue(ob.GetCountLifeParts() == 2);
        }

        [TestMethod]
        public void GetCountDeadParts()
        {
            Position point = new Position(4, 6);
            Position point2 = new Position(5, 8);
            Position point3 = new Position(5, 8);

            ObjectLocation ob = new ObjectLocation(point, point2, point3);

            ob.ChangeLifeToDead(point2);

            Assert.IsTrue(ob.GetCountLifeParts() != ob.GetCountParts());
        }

        [TestMethod]
        public void GetPositionLifeParts()
        {
            Position point = new Position(4, 6);
            Position point2 = new Position(5, 8);
            ObjectLocation ob = new ObjectLocation(point, point2);

            ob.ChangeLifeToDead(point2);

            Assert.IsTrue(ob.GetPositionsLifeParts()[0] == point);
        }


    }
}
