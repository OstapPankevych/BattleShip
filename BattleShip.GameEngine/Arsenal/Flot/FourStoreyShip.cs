using BattleShip.GameEngine.Location;

namespace BattleShip.GameEngine.Arsenal.Flot
{
    public class FourStoreyShip : ShipRectangleBase
    {
        public FourStoreyShip(byte id, Position begin, Position average1, Position average2, Position end)
            : base(id, begin, average1, average2, end)
        {
            _storeyCount = 4;
        }
    }
}