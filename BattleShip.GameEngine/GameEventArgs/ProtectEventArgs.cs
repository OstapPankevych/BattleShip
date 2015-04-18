using System;

namespace BattleShip.GameEngine.GameEventArgs
{
    public class ProtectEventArgs : EventArgs
    {
        public Type Type;

        public ProtectEventArgs(Type type)
        {
            Type = type;
        }
    }
}