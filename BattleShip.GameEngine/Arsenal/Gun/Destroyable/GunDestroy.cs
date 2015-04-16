using System;

using BattleShip.GameEngine.Location;

namespace BattleShip.GameEngine.Arsenal.Gun.Destroyable
{

    public class GunDestroy : IDestroyable
    {
        public Position[] Destroy(Position point, byte size)
        {
            return new Position[1] { point };
        }
    }

}