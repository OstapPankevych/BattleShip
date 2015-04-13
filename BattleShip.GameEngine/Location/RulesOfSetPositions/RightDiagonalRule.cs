using System;


namespace BattleShip.GameEngine.Location.RulesOfSetPositions
{
    class RightDiagonalRule : BaseRule<Position>
    {
        public RightDiagonalRule(Position point, byte countCells)
        {
            Position endPosition;
            if (point.Line - countCells < 0)
                endPosition = new Position(0, (byte)(point.Column + countCells));
            else
                endPosition = new Position((byte)(point.Line - countCells), (byte)(point.Column + countCells));

            InitPositions(point, endPosition);
        }

        protected override void InitPositions(params Position[] point)
        {
            _positions = new Position[(point[0].Line - point[1].Line) + 1];
            for (int i = 0; i < _positions.Length; i++)
                _positions[i] = new Position((byte)(point[0].Line - i), (byte)(point[0].Column + i));
        }
    }
}