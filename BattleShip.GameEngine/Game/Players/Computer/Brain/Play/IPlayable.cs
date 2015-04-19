using BattleShip.GameEngine.Location;

namespace BattleShip.GameEngine.Game.Players.Computer.Brain.Play
{
    public interface IPlayable
    {
        Position GetPositionForAttackAndSetGun(GameMode.GameMode myMode, Field.FakeField manFakeField);
    }
}