using BattleShip.GameEngine.Arsenal.Flot.Corectible;
using BattleShip.GameEngine.Location;

namespace BattleShip.GameEngine.Arsenal.Flot.RectangleShips
{
    public class ThreeStoreyRectangleShip : ShipBase
    {
        public ThreeStoreyRectangleShip(byte id, Position begin, Position average, Position end)
            : base(new Ractangle(), id, begin, average, end)
        { }
    }
}