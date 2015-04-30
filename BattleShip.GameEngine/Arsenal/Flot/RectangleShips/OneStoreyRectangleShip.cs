using BattleShip.GameEngine.Arsenal.Flot.Correctible;
using BattleShip.GameEngine.Location;

namespace BattleShip.GameEngine.Arsenal.Flot.RectangleShips
{
    public class OneStoreyRectangleShip : ShipBase
    {
        public OneStoreyRectangleShip(byte id, Position position)
            : base(new Ractangle(), id, position)
        { }
    }
}