using System;


using BattleShip.GameEngine.Location;

namespace BattleShip.GameEngine.GameEventArgs
{
    class GameEvenArgs : EventArgs
    {
        private Position _position;

        public Position Location
        {
            get
            {
                return _position;
            }
        }

        public GameEvenArgs(Position position)
        {
            _position = position;
        }
    }
}
