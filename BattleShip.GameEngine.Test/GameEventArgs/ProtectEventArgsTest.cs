using System;
using BattleShip.GameEngine.Fields.Cells.StatusCell;
using BattleShip.GameEngine.GameEventArgs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleShip.GameEngine.Test.GameEventArgs
{
    [TestClass]
    public class ProtectEventArgsTest
    {
        [TestMethod]
        public void Constr()
        {
            Assert.IsTrue((new ProtectEventArgs(typeof(EmptyCell))).Type == typeof(EmptyCell));
        }
    }
}
