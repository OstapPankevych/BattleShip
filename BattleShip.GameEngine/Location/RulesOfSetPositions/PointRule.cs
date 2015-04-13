using System;


namespace BattleShip.GameEngine.Location.RulesOfSetPositions
{
    class PointRule : BaseRule<Position>
    {
        public PointRule(Position point)
        {
            InitPositions(point);
        }

        protected override void InitPositions(params Position[] point)
        {
            _positions = point;
        }
    }
}