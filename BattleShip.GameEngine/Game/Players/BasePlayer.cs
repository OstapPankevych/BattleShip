using BattleShip.GameEngine.Arsenal.Flot;
using BattleShip.GameEngine.Arsenal.Gun;
using BattleShip.GameEngine.Arsenal.Gun.Destroyable;
using BattleShip.GameEngine.Arsenal.Protection;
using BattleShip.GameEngine.Location;
using System;
using System.Collections.Generic;

namespace BattleShip.GameEngine.Game.Players
{
    public abstract class BasePlayer : IPlayer
    {
        private string _name;

        protected readonly Func<ShipBase, bool> SetShipsFunc;
        protected readonly Func<ProtectBase, bool> SetProtectFunc;
        protected readonly byte fieldSize;

        /*
         * Review GY: не рекомендую використовувати такий підхід при роботі з делегатами, так як він ускладнює розуміння загальної логіки.
         * Якщо уникнути даної ситуації неможливо, це означає, що система спроектована не зовсім правильно.
         */
        public BasePlayer(string name, Func<ShipBase, bool> SetShipsFunc,
            Func<ProtectBase, bool> SetProtectFunc,
            byte fieldSize)
        {
            this.SetShipsFunc = SetShipsFunc;
            this.SetProtectFunc = SetProtectFunc;

            this.fieldSize = fieldSize;

            this._name = name;
        }

        public string Name
        {
            get { return this._name; }
        }

        public abstract void BeginSetShips();

        public abstract void BeginSetProtect();

        public abstract Position GetPositionForAttack(Gun gun, IList<IDestroyable> gunList);
    }
}