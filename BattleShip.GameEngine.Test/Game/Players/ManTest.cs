using System;
using System.Collections.Generic;
using BattleShip.GameEngine.Arsenal.Gun;
using BattleShip.GameEngine.Arsenal.Gun.Destroyable;
using BattleShip.GameEngine.Game.Players.Man;
using BattleShip.GameEngine.Location;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleShip.GameEngine.Test.Game.Players
{
    [TestClass]
    public class ManTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Action<ManPlayer> StartSetShipsFromReferriOnHandler = (ManPlayer man) =>
            {
                Assert.IsTrue(true);
            };

            Action<ManPlayer> StartSetProtectsFromReferriOnHandler = (ManPlayer man) =>
            {
                Assert.IsTrue(true);
            };

            Func<Gun, IList<IDestroyable>, Position> GetPositionForAttackFromReferriOnHandler = (a, b) =>
            {
                Assert.IsTrue(true);
                return new Position(5, 5);
            };

            const byte fieldSize = 10;

            ManPlayer Man = new ManPlayer("Test", fieldSize,
                StartSetShipsFromReferriOnHandler,
                StartSetProtectsFromReferriOnHandler,
                GetPositionForAttackFromReferriOnHandler);

        
            Man.BeginSetProtect();
            Man.BeginSetShips();

            Assert.IsTrue(Man.GetPositionForAttack(new Gun(), null) == new Position(5, 5));
        }
    }
}
