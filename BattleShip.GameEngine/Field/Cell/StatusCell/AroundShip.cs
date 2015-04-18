using BattleShip.GameEngine.Location;

namespace BattleShip.GameEngine.Field.Cell.StatusCell
{
    // клас клітинки навколо кораблика
    public class AroundShip : StatusCell
    {
        public AroundShip(Position position)
            : base(position)
        {
        }
    }
}