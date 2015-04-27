using BattleShip.GameEngine.Location;

namespace BattleShip.GameEngine.Fields.Cells.StatusOfCells
{
    // клас клітинки навколо кораблика
    public class AroundShip : StatusCell
    {
        public AroundShip(Position position)
            : base(position)
        { }
    }
}