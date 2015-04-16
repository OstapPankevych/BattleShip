using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


using BattleShip.GameEngine.GameObject;
using BattleShip.GameEngine.Location;



namespace BattleShip.GameEngineTest.GameObject
{
    [TestClass]
    public class PositionAndStatusTest
    {
        [TestMethod]
        public void ChackLocation()
        {
            Position point = new Position(5, 7);
            PositionAndStatus positionAndStatus = new PositionAndStatus(point);

            Assert.IsTrue(positionAndStatus.Location == point);
        }

        [TestMethod]
        public void InitLifeStatus()
        {
            Position point = new Position(5, 7);
            PositionAndStatus positionAndStatus = new PositionAndStatus(point);

            Assert.IsTrue(positionAndStatus.IsLife == true);
        }

        [TestMethod]
        public void ChangeLifeToDead()
        {
            Position point = new Position(5, 7);
            PositionAndStatus positionAndStatus = new PositionAndStatus(point);
            positionAndStatus.ChangeLifeToDead();
            Assert.IsTrue(positionAndStatus.IsLife == false);
        }
    }
}
