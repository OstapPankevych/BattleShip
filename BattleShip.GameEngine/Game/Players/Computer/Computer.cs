using BattleShip.GameEngine.Arsenal.Flot;
using BattleShip.GameEngine.Arsenal.Gun;
using BattleShip.GameEngine.Arsenal.Gun.Destroyable;
using BattleShip.GameEngine.Arsenal.Protection;
using BattleShip.GameEngine.Fields;
using BattleShip.GameEngine.Location;
using System;
using System.Collections.Generic;

namespace BattleShip.GameEngine.Game.Players.Computer
{
    public class Computer : BasePlayer
    {
        private Brain.Brain _brain;// = new Brain(new Play(), new SetProtect(), new SetShip());

        private readonly FakeField _anotherPlayerFakeField;

        public override Position GetPositionForAttack(Gun gun, IList<IDestroyable> gunList)
        {
            return this._brain.GetPositionForAttackAndSetGun(_anotherPlayerFakeField, gun, gunList);
        }

        public Computer(Brain.Brain brain,
            Func<ShipBase, bool> SetShipsFunc,
            Func<ProtectBase, bool> SetPtotectFunc,
            FakeField anotherPlayerFakeField,
            byte fieldSize)
            : base("Computer", SetShipsFunc, SetPtotectFunc, fieldSize)
        {
            this._brain = brain;

            this._anotherPlayerFakeField = anotherPlayerFakeField;
        }

        public override void BeginSetShips()
        {
            this._brain.SetRectangleShips(base.SetShipsFunc, base.fieldSize);
        }

        public override void BeginSetProtect()
        {
            this._brain.SetPVOProtect(base.SetProtectFunc, base.fieldSize);
        }
    }
}