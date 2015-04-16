using System;


using BattleShip.GameEngine.Location;

using BattleShip.GameEngine.Arsenal.Protection;

namespace BattleShip.GameEngine.Arsenal.Gun.Destroyable
{

    public class PlaneDestroy : GunBase, IDestroyable
    {
        public Position[] Destroy(Position point, byte size)
        {
            Position[] points = new Position[size];
            for (byte i = 0; i < size; i++)
                points[i] = new Position(point.Line, i);
            return points;
        }

        public PlaneDestroy()
        {
            fearList.Add(typeof(PVOProtected));
        }
    }

}