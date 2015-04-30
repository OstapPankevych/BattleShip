using System;

namespace BattleShip.GameEngine.Game.Players.Computer.Brain.SetObjects.SetProtect
{
    internal class NotSetProtect : ISetibleProtect
    {
        public void SetProtects(Func<Arsenal.Protection.ProtectBase, bool> SetPtotectFunc, byte fieldSize)
        { }
    }
}