using BattleShip.GameEngine.Location;

namespace BattleShip.GameEngine.Arsenal.Gun.Destroyable
{
    public interface IDestroyable
    {
        Position[] Destroy(Position point, byte size);
    }
}