using System;
using BattleShip.GameEngine.GameEventArgs;


namespace BattleShip.GameEngine.ObjectOfGame
{
    public interface IFullGameObject : IGameObject
    {
        event Action<GameEvenArgs> DeadHandler;

        event Action<GameEvenArgs> HitHandler;
        
        void DestroyMe(GameEvenArgs e);
    }
}
