using BattleShip.GameEngine.Location;
using BattleShip.GameEngine.ObjectOfGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;
<<<<<<< HEAD
//невірний namespace
=======

>>>>>>> adcb4d49f57b1a9c51a12f9f9099df7db01d1a0d
namespace BattleShip.GameEngineTest.GameObject
{
    [TestClass]
    public class GameObjectTest
    {
        [TestMethod]
        public void GetLifeStatus()
        {
            var p = new Position(0, 1);
            var p1 = new Position(1, 3);

            var arr = new Position[2];
            arr[0] = p;
            arr[1] = p1;

            var ob = new ObjectLocation(arr);
            Assert.IsTrue(ob.IsLife);
        }

        [TestMethod]
        public void GetCountPart()
        {
            var p = new Position(0, 1);
            var p1 = new Position(1, 3);

            var ob = new ObjectLocation(p, p1);
            Assert.IsTrue(ob.GetCountParts() == 2);
        }

        [TestMethod]
        public void ChangeLifeToDead()
        {
            var p = new Position(0, 1);
            var p1 = new Position(1, 3);

            var ob = new ObjectLocation(p, p1);

            ob.ChangeLifeToDead(p);

            Assert.IsTrue(ob.GetPositionsLifeParts().Length == 1);
        }
    }
}