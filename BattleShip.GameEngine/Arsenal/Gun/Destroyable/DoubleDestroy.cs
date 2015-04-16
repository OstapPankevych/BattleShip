using System;


using BattleShip.GameEngine.Location;

namespace BattleShip.GameEngine.Arsenal.Gun.Destroyable
{

    public class DoubleDestroy : IDestroyable
    {
        public Position[] Destroy(Position point, byte size)
        {
            Position[] positions = ((point.Line + 1) < size)
                ? new Position[2] { new Position(point.Line, point.Column), new Position((byte)(point.Line + 1), point.Column) }
                : new Position[1] { point };

            return positions;
        }
    }

}
