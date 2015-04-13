using System;


using BattleShip.GameEngine.Location;



namespace BattleShip.GameEngine.Field.Cell.StatusCell
{
    class StatusCell : GameObject.GameObject
    {
        bool _isLife = true;

        Position _position;

        public StatusCell(Position position)
            : base(0)
        {
            _position = position;
        }

        public override bool IsLife
        {
            get
            {
                return _isLife;
            }
        }

        public event Action<Position> DeadHandler;

        public override void OnHitMeHandler(Location.Position position)
        {
            if (position == _position)
            {
                if (_isLife)
                {
                    OnDeadHandler();
                    if (DeadHandler != null)
                        DeadHandler(position);
                }
                return;
            }
            return;
        }

        public override void OnDeadHandler()
        {
            _isLife = false;
        }
    }
}