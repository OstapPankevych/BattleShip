using System;
using System.Collections;
using System.Diagnostics;
using BattleShip.GameEngine.Location;
using BattleShip.GameEngine.ObjectOfGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleShip.GameEngine.Test.GameObject
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
        [ExpectedException(typeof (NullReferenceException))]
        public void ExceptionInit()
        {
            ObjectLocation ob = new ObjectLocation(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ExceptionBadRegionLife()
        {
            ObjectLocation ob = new ObjectLocation(new Position(1, 2));

            ob.GetLifeStatus(new Position(4, 7));
        }

        [TestMethod]
        public void IsNotLife()
        {
            ObjectLocation ob = new ObjectLocation(new Position(1, 2));

            ob.ChangeLifeToDead(new Position(1, 2));

            Assert.IsFalse(ob.IsLife);
        }

        [TestMethod]
        public void IsLife()
        {
            ObjectLocation ob = new ObjectLocation(new Position(1, 2));

            Assert.IsTrue(ob.IsLife);
        }

        [TestMethod]
        public void GetLifeStatus()
        {
            Position[] posArr = new[] {new Position(1, 2), new Position(1, 4)};
            ObjectLocation ob = new ObjectLocation(posArr[0], posArr[1]);

            Assert.IsTrue(ob.GetLifeStatus(posArr[0]));
            Assert.IsTrue(ob.GetLifeStatus(posArr[1]));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ChangeLifeToDeadNotRealPosition()
        {
            ObjectLocation ob = new ObjectLocation(new Position(1, 3));

            ob.ChangeLifeToDead(new Position(0, 0));
        }

        [TestMethod]
        public void ForeachTest()
        {
            Position[] pos = new[] {new Position(0, 0), new Position(1, 2)};
            ObjectLocation ob = new ObjectLocation(pos);


            int i = 0;
            foreach (var x in ob)
            {
                Assert.IsTrue(x.Line == pos[i].Line && x.Column == pos[i].Column);
                i++;
            }
        }



        [TestMethod]
        public void ChangeLifeToDaed()
        {
            var point = new Position(4, 6);
            var point2 = new Position(5, 8);
            var point3 = new Position(5, 8);
            ObjectLocation ob = new ObjectLocation(point, point2, point3);

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
