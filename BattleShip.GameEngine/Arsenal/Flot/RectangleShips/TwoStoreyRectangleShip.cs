using BattleShip.GameEngine.Arsenal.Flot.Corectible;
using BattleShip.GameEngine.Location;

namespace BattleShip.GameEngine.Arsenal.Flot.RectangleShips
{
    public class TwoStoreyRectangleShip : ShipBase
    {
        public TwoStoreyRectangleShip(byte id, Position begin, Position end)
            : base(new Ractangle(), id, begin, end)
        {
        }
    }
}