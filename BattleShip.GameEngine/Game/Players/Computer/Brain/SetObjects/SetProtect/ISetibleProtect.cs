using BattleShip.GameEngine.Arsenal.Protection;
using System;

namespace BattleShip.GameEngine.Game.Players.Computer.Brain.SetObjects.SetProtect
{
    public interface ISetibleProtect
    {
        // передається делегат на функцію, яка розставляє захисти (зроблено з міркувань безпеки)
        void SetProtects(Func<ProtectBase, bool> SetPtotectFunc, byte fieldSize);
    }
}