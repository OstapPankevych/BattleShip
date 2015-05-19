using System;
using BattleShip.GameEngine.Location;

namespace BattleShip.GameEngine.GameEvent
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