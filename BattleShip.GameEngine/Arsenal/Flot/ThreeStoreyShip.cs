using BattleShip.GameEngine.Location;

namespace BattleShip.GameEngine.Arsenal.Flot
{
    public class ThreeStoreyShip : ShipRectangleBase
    {
        public ThreeStoreyShip(byte id, Position begin, Position average, Position end)
            : base(id, begin, average, end)
        {
            _storeyCount = 3;
        }
    }
}