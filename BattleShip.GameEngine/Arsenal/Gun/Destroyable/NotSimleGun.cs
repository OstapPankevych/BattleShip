using BattleShip.GameEngine.Location;
using System;
using System.Collections.Generic;

namespace BattleShip.GameEngine.Arsenal.Gun.Destroyable
{
    public abstract class NotSimpleGun : IDestroyable
    {
        protected List<Type> FearList = new List<Type>();

        public abstract Position[] Destroy(Position point, byte size);
    }
}