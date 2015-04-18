namespace BattleShip.GameEngine.Location.RulesOfSetPositions
{
    internal interface IRuleSetPosition
    {
        Position[] GetRegionForCurrentRule();
    }
}