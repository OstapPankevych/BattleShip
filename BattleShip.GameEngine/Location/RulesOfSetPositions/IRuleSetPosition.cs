namespace BattleShip.GameEngine.Location.RulesOfSetPositions
{
    public interface IRuleSetPosition
    {
        Position[] GetRegionForCurrentRule();
    }
}