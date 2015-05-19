using System;
using BattleShip.GameEngine.Arsenal.Gun;
using BattleShip.GameEngine.Fields.Cells.StatusCell;
using BattleShip.GameEngine.Fields;
using BattleShip.GameEngine.GameEvent;
using BattleShip.GameEngine.GameEventArgs;
using BattleShip.GameEngine.Location;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleShip.GameEngine.Test.Fields
{
    [TestClass]
    public class FakeFieldTest
    {
        [TestMethod]
        public void Consytuctor()
        {
            Field f = new Field(1);
            FakeField field = new FakeField(f);
            Assert.IsTrue(field.Size == 1);

            Func<bool> act = () =>
            {
                field[0, 0].OnDestroyMe(new GameEvenArgs(new Position(0, 0)));
                return true;
            };

            Assert.IsTrue(act());
            
            Position pos = new Position(0, 0);

            field[0, 0].Shot(new Gun(), ref pos);
        }
    }
}
