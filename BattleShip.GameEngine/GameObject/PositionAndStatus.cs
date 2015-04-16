using System;


using BattleShip.GameEngine.Location;



namespace BattleShip.GameEngine.GameObject
{
    public struct PositionAndStatus
    {
        Position _position;
        bool _life;

        public bool IsLife
        {
            get
            {
                return _life;
            }
        }

        public Position Location
        {
            get
            {
                return _position;
            }
        }

        public PositionAndStatus(Position position)
        {
            _life = true;
            this._position = position;
        }

        public void ChangeLifeToDead()
        {
            _life = false;
        }
    }
}