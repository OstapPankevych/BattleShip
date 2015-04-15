using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.GameEngine.GameEventArgs
{
    class ProtectEventArgs : EventArgs
    {
        public Type Type;
        public ProtectEventArgs(Type type)
        {
            this.Type = type;
        }
    }
}
