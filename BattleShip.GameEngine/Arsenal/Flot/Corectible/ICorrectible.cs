using BattleShip.GameEngine.Location;

namespace BattleShip.GameEngine.Arsenal.Flot.Corectible
{
    public interface ICorrectible
    {
        bool IsTrueShipRegion(byte countStorey, params Position[] positions);
    }
}