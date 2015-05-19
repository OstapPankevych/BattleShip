using BattleShip.GameEngine.Arsenal.Protection;
using BattleShip.GameEngine.Location;

namespace BattleShip.GameEngine.Arsenal.Gun.Destroyable
{
    public class PlaneDestroy : NotSimpleGun
    {
        public PlaneDestroy()
        {
            base.FearList.Add(typeof(Pvo));
        }

        public override Position[] Destroy(Position point, byte size)
        {
            var points = new Position[size];
            for (byte i = 0; i < size; i++)
            {
                points[i] = new Position(point.Line, i);
            }

            return points;
        }
    }
}