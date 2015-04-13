using System;


using BattleShip.GameEngine.Location;



namespace BattleShip.GameEngine.GameObject
{
    struct PositionAndStatus
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
            this._position = position;
            _life = true;
        }

        public void ChangeLifeToDead()
        {
            _life = false;
        }
    }
}