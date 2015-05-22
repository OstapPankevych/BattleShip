using BattleShip.GameEngine.GameEvent;
using BattleShip.GameEngine.GameEventArgs; //не використовується


namespace BattleShip.GameEngine.ObjectOfGame
{
    public interface IGameObject
    {
        bool IsLife { get; }

        void KillMe(GameEvenArgs e);
    }
}
