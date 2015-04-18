namespace BattleShip.GameEngine.Location.RulesOfSetPositions
{
    internal abstract class BaseRule<T> : IRuleSetPosition where T : struct
    {
        protected Position[] _positions;

        public Position[] GetRegionForCurrentRule()
        {
            return _positions;
        }

        protected abstract void InitPositions(params T[] inputData);
    }
}