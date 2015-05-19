using BattleShip.GameEngine.Location;
using System;

namespace BattleShip.GameEngine.GameEventArgs
{
    public class GameEvenArgs : EventArgs
    {
        public GameEvenArgs(Position position)
        {
            Location = position;
        }

        public Position Location { get; private set; }
    }
}