using System;
using System.Collections.Generic; //не використовується
using System.Linq; //не використовується
using System.Text; //не використовується
using System.Threading.Tasks; //не використовується
using BattleShip.GameEngine.ObjectOfGame;

//простір імен не відповідає розміщенню файлу
namespace BattleShip.GameEngine.GameEventArgs
{
    public delegate void GameEventHandler<in TEventArgs>(IGameObject sender, TEventArgs e)
        where TEventArgs : EventArgs;
}
