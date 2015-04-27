using System;
using BattleShip.GameEngine.Arsenal.Flot;
using BattleShip.GameEngine.Game.Players.Computer.Brain.SetObjects.SetRectangleShip;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleShip.GameEngine.Test.Game.Players.Computer.Brain.SetObject.SetRactangleShip
{
    [TestClass]
    public class SetShipTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            int a = 0;
            Func<ShipBase, bool> fakeFunc = (ShipBase ShipBase) =>
            {
                a++;
                return true;
            };

            SetShip setShip = new SetShip();
            setShip.SetShips(fakeFunc, 10);

            Assert.IsTrue(a == 10);
        }

        [TestMethod]
        public void SetShipFunc()
        {
            Func<ShipBase, bool> fakeFunc = (ShipBase ShipBase) =>
            {
                return true;
            };

            SetShip setShip = new SetShip();
            PrivateObject pr = new PrivateObject(setShip);

        }
    }
}
