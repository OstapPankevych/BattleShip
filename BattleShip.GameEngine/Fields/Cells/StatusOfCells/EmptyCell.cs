using BattleShip.GameEngine.Location;

namespace BattleShip.GameEngine.Fields.Cells.StatusOfCells
{
    // клас пустої клітинки
    public class EmptyCell : StatusCell
    {
        public EmptyCell(Position position)
            : base(position)
        { }
    }
}