using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BattleShip.GameEngine.Arsenal.Gun;
using BattleShip.GameEngine.Arsenal.Gun.Destroyable;
using BattleShip.GameEngine.Location;

namespace BattleShip.GameEngine.Test.Arsenal.Gun
{
    [TestClass]
    public class GunTest
    {
        [TestMethod]
        public void GetTypeOfCurrentCun()
        {
            GameEngine.Arsenal.Gun.Gun gun = new GameEngine.Arsenal.Gun.Gun();

            Assert.IsTrue(gun.GetTypeOfCurrentCun() == typeof(GunDestroy));
        }

        [TestMethod]
        public void GetDestroyable()
        {
            GameEngine.Arsenal.Gun.Gun gun = new GameEngine.Arsenal.Gun.Gun();

            IDestroyable d = new GunDestroy();

            Assert.IsTrue(gun.GetDestroyable().GetType() == d.GetType());
        }

        [TestMethod]
        public void Shot()
        {
            GameEngine.Arsenal.Gun.Gun gun = new GameEngine.Arsenal.Gun.Gun();

            Assert.IsTrue(gun.Shot(new Position(5, 5), 5).Length == 1);
        }
    }
}
