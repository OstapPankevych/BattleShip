using BattleShip.GameEngine.Arsenal.Gun;
using BattleShip.GameEngine.Arsenal.Gun.Destroyable;
using BattleShip.GameEngine.Location;
using System.Collections.Generic;

namespace BattleShip.GameEngine.Game.Players
{
    public interface IPlayer
    {
        string Name { get; }

        void BeginSetShips();

        void BeginSetProtect();

        Position GetPositionForAttack(Gun gun, IList<IDestroyable> gunList);
    }
}