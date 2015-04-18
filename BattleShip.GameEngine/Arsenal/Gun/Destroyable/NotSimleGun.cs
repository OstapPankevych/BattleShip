using System;
using System.Collections.Generic;
using BattleShip.GameEngine.Location;

namespace BattleShip.GameEngine.Arsenal.Gun.Destroyable
{
    public abstract class NotSimpleGun : IDestroyable
    {
        protected List<Type> FearList = new List<Type>();

        public abstract Position[] Destroy(Position point, byte size);

    }
}