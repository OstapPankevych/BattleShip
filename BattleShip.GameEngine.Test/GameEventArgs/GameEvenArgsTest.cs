using System;
using BattleShip.GameEngine.GameEvent;
using BattleShip.GameEngine.GameEventArgs;
using BattleShip.GameEngine.Location;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleShip.GameEngine.Test.GameEventArgs
{
    [TestClass]
    public class GameEvenArgsTest
    {
        [TestMethod]
        public void Constr()
        {
            Assert.IsTrue((new GameEvenArgs(new Position(5, 5))).Location == new Position(5, 5));
        }
    }
}
