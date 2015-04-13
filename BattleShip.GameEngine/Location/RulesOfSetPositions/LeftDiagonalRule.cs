using System;


namespace BattleShip.GameEngine.Location.RulesOfSetPositions
{
    class LeftDiagonalRule : BaseRule<Position>
    {
        public LeftDiagonalRule(Position point, byte countCells)
        {
            countCells++;
            Position endPosition = new Position((byte)(point.Line + countCells - 1), (byte)(point.Column + countCells - 1));

            InitPositions(point, endPosition);
        }

        protected override void InitPositions(params Position[] point)
        {
            _positions = new Position[(point[1].Line - point[0].Line) + 1];
            for (int i = 0; i < _positions.Length; i++)
                _positions[i] = new Position((byte)(point[0].Line + i), (byte)(point[0].Column + i));
        }
    }
}