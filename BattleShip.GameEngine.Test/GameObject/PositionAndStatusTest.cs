using BattleShip.GameEngine.Location;
using BattleShip.GameEngine.ObjectOfGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleShip.GameEngineTest.GameObject
{
    [TestClass]
    public class PositionAndStatusTest
    {
        [TestMethod]
        public void ChackLocation()
        {
            var point = new Position(5, 7);
            var positionAndStatus = new PositionAndStatus(point);

            Assert.IsTrue(positionAndStatus.Location == point);
        }

        [TestMethod]
        public void InitLifeStatus()
        {
            var point = new Position(5, 7);
            var positionAndStatus = new PositionAndStatus(point);

            Assert.IsTrue(positionAndStatus.IsLife);
        }

        [TestMethod]
        public void ChangeLifeToDead()
        {
            var point = new Position(5, 7);
            var positionAndStatus = new PositionAndStatus(point);
            positionAndStatus.ChangeLifeToDead();
            Assert.IsTrue(positionAndStatus.IsLife == false);
        }
    }
}