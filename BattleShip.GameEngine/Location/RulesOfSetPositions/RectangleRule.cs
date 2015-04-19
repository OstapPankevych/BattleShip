namespace BattleShip.GameEngine.Location.RulesOfSetPositions
{
    public class RectangleRule : BaseRule<Position>
    {
        public RectangleRule(Position beginPosition, Position endPosition)
        {
            InitPositions(beginPosition, endPosition);
        }

        protected override void InitPositions(params Position[] positions)
        {
            var beginPosition = positions[0];
            var endPosition = positions[1];
            var minLine = (beginPosition.Line <= endPosition.Line) ? beginPosition.Line : endPosition.Line;
            var maxLine = (beginPosition.Line <= endPosition.Line) ? endPosition.Line : beginPosition.Line;

            var minColumn = (beginPosition.Column <= endPosition.Column) ? beginPosition.Column : endPosition.Column;
            var maxColumn = (beginPosition.Column <= endPosition.Column) ? endPosition.Column : beginPosition.Column;

            beginPosition = new Position(minLine, minColumn);
            endPosition = new Position(maxLine, maxColumn);

            _positions = new Position[(maxLine - minLine + 1) * (maxColumn - minColumn + 1)];

            for (var i = 0; i < (maxLine - minLine + 1); i++)
                for (var j = 0; j < (maxColumn - minColumn + 1); j++)
                    _positions[i * (maxColumn - minColumn + 1) + j] = new Position((byte)(minLine + i),
                        (byte)(minColumn + j));
        }
    }
}