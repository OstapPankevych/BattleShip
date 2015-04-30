using BattleShip.GameEngine.Arsenal.Flot.Correctible;
using BattleShip.GameEngine.Location;

namespace BattleShip.GameEngine.Arsenal.Flot.RectangleShips
{
    public class FourStoreyRectangleShip : ShipBase
    {
        public FourStoreyRectangleShip(byte id, Position begin, Position average1, Position average2, Position end)
            : base(new Ractangle(), id, begin, average1, average2, end)
        { }
    }
}