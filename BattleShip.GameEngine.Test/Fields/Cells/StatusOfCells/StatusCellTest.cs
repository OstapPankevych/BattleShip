//не використовується
using System;
using BattleShip.GameEngine.Fields.Cells.StatusOfCells;
//не використовується
using BattleShip.GameEngine.GameEventArgs;
using BattleShip.GameEngine.Location;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleShip.GameEngine.Test.Fields.Cells.StatusOfCells
{
    [TestClass]
    public class StatusCellTest
    {
        [TestMethod]
        public void IsLife()
        {
            FakeClass fake = new FakeClass(new Position(1, 2));
            Assert.IsTrue(fake.IsLife);
        }

        [TestMethod]
        public void HitMeHandlerNotNull()
        {
            FakeClass fake = new FakeClass(new Position(1, 2));
        }

        class FakeClass : StatusCell
        {
            public FakeClass(Position position) : base(position)
            { }
        }
    }
}
