using System;

//простір імен не відповідає розміщенню файлу
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