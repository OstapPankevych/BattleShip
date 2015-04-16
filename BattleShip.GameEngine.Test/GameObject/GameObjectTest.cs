using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


using BattleShip.GameEngine.GameObject;
using BattleShip.GameEngine.Location;

namespace BattleShip.GameEngineTest.GameObject
{
    [TestClass]
    public class GameObjectTest
    {


        [TestMethod]
        public void GetLifeStatus()
        {

            Position p = new Position(0, 1);
            Position p1 = new Position(1, 3);

            Position[] arr = new Position[2];
            arr[0] = p;
            arr[1] = p1;


            ObjectLocation ob = new ObjectLocation(arr);
            Assert.IsTrue(ob.IsLife);

        }

        [TestMethod]
        public void GetCountPart()
        {
            Position p = new Position(0, 1);
            Position p1 = new Position(1, 3);


            ObjectLocation ob = new ObjectLocation(p, p1);
            Assert.IsTrue(ob.GetCountParts() == 2);
        }

        [TestMethod]
        public void ChangeLifeToDead()
        {
            Position p = new Position(0, 1);
            Position p1 = new Position(1, 3);


            ObjectLocation ob = new ObjectLocation(p, p1);

            ob.ChangeLifeToDead(p);

            Assert.IsTrue(ob.GetPositionsLifeParts().Length == 1);
        }
    }
}
