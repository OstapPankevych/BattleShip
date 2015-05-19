using System;
using System.Collections.Generic;
using BattleShip.GameEngine.Arsenal.Gun;
using BattleShip.GameEngine.Arsenal.Gun.Destroyable;
using BattleShip.GameEngine.Location;

namespace BattleShip.GameEngine.Game.Players.Man
{
    public class ManPlayer : BasePlayer
    {
        public ManPlayer(string name,
            byte fieldSize,
            Action<ManPlayer> StartSetShipsFromReferriOnHandler,
            Action<ManPlayer> StartSetProtectsFromReferriOnHandler,
            Func<Gun, IList<IDestroyable>, Position> GetPositionForAttackFromReferriOnHandler)
            : base(name, null, null, fieldSize)
        {
            this.StartSetProtectsFromReferriHandler += StartSetProtectsFromReferriOnHandler;
            this.StartSetShipsFromReferriHandler += StartSetShipsFromReferriOnHandler;
            this.GetPositionForAttackFromReferriHandler += GetPositionForAttackFromReferriOnHandler;
        }

        public event Action<ManPlayer> StartSetShipsFromReferriHandler;

        public event Action<ManPlayer> StartSetProtectsFromReferriHandler;

        public event Func<Gun, IList<IDestroyable>, Position> GetPositionForAttackFromReferriHandler;

        public override void BeginSetShips()
        {
            StartSetShipsFromReferriHandler(this);
        }

        public override void BeginSetProtect()
        {
            StartSetProtectsFromReferriHandler(this);
        }

        public override Position GetPositionForAttack(Gun gun, IList<IDestroyable> gunList)
        {
            return GetPositionForAttackFromReferriHandler(gun, gunList);
        }
    }
}