using System;



namespace BattleShip.GameEngine.Location.RulesOfSetPositions
{
    interface IRuleSetPosition
    {
        Position[] GetRegionForCurrentRule();
    }
}

