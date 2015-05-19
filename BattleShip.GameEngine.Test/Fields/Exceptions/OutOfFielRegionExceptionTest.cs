using System;
using BattleShip.GameEngine.Fields.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleShip.GameEngine.Test.Fields.Exceptions
{
    [TestClass]
    public class OutOfFielRegionExceptionTest
    {
        [TestMethod]
        [ExpectedException(typeof(OutOfFielRegionException))]
        public void msgOK()
        {
            Assert.IsTrue(new OutOfFielRegionException("").Message == "out of field region");
            throw new OutOfFielRegionException("TestMethod");
        }


    }
}
