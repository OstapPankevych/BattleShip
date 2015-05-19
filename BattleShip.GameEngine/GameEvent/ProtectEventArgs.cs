using System;

namespace BattleShip.GameEngine.GameEventArgs
{
    public class ProtectEventArgs : EventArgs
    {
        public Type Type { get; private set; }

        public ProtectEventArgs(Type type)
        {
            Type = type;
        }
    }
}