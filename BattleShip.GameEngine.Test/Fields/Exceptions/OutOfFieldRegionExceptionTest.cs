//не використовується
using System;
using BattleShip.GameEngine.Fields.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleShip.GameEngine.Test.Fields.Exceptions
{
    [TestClass]
    public class OutOfFieldRegionExceptionTest
    {
        [TestMethod]
        [ExpectedException(typeof(OutOfFieldRegionException))]
        public void msgOK()
        {
            throw new OutOfFieldRegionException("TestMethod");
        }


    }
}
