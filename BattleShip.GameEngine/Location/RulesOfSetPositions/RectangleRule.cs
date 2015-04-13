using System;


namespace BattleShip.GameEngine.Location.RulesOfSetPositions
{
    class RectangleRule : BaseRule<Position>
    {
        public RectangleRule(Position beginPosition, Position endPosition)
        {
            InitPositions(beginPosition, endPosition);
        }

        protected override void InitPositions(Position[] positions)
        {
            Position beginPosition = positions[0];
            Position endPosition = positions[1];
            byte minLine = (beginPosition.Line <= endPosition.Line) ? beginPosition.Line : endPosition.Line;
            byte maxLine = (beginPosition.Line <= endPosition.Line) ? endPosition.Line : beginPosition.Line;

            byte minColumn = (beginPosition.Column <= endPosition.Column) ? beginPosition.Column : endPosition.Column;
            byte maxColumn = (beginPosition.Column <= endPosition.Column) ? endPosition.Column : beginPosition.Column;

            beginPosition = new Position(minLine, minColumn);
            endPosition = new Position(maxLine, maxColumn);

            _positions = new Position[(maxLine - minLine + 1) * (maxColumn - minColumn + 1)];

            for (int i = 0; i < (maxLine - minLine + 1); i++)
                for (int j = 0; j < (maxColumn - minColumn + 1); j++)
                    _positions[i * (maxColumn - minColumn + 1) + j] = new Position((byte)(minLine + i), (byte)(minColumn + j));
        }
    }
}