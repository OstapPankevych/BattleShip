using BattleShip.GameEngine.Arsenal.Gun;
using BattleShip.GameEngine.Arsenal.Gun.Destroyable;
using BattleShip.GameEngine.Fields;
using BattleShip.GameEngine.Location;
using System.Collections.Generic;

namespace BattleShip.GameEngine.Game.Players.Computer.Brain.Play
{
    public interface IPlayable
    {
        Position GetPositionForAttackAndSetGun(FakeField myFakeField, Gun gun,
            IList<IDestroyable> gunList);
    }
}