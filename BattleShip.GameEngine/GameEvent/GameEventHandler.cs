using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.GameEngine.ObjectOfGame;

namespace BattleShip.GameEngine.GameEventArgs
{
    public delegate void GameEventHandler<in TEventArgs>(IGameObject sender, TEventArgs e)
        where TEventArgs : EventArgs;
}
