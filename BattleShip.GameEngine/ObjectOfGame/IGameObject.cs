using BattleShip.GameEngine.GameEventArgs;


namespace BattleShip.GameEngine.ObjectOfGame
{
    public interface IGameObject
    {
        bool IsLife { get; }

        void KillMe(GameEvenArgs e);
    }
}
