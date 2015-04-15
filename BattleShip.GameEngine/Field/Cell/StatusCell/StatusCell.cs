using System;


using BattleShip.GameEngine.Location;
using BattleShip.GameEngine.GameEventArgs;


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

        public override event Action<GameObject.GameObject, GameEvenArgs> DeadHandler;
        public override event Action<GameObject.GameObject, GameEvenArgs> HitMeHandler;

        void OnDeadHandler()
        {
            _isLife = false;
        }
        public override void OnHitMeHandler(GameObject.GameObject gameObject, GameEventArgs.GameEvenArgs e)
        {
            if (e.Location == _position)
            {
                if (_isLife)
                {
                    OnDeadHandler();
                    if (DeadHandler != null)
                        DeadHandler(this, new GameEvenArgs(e.Location));
                }
                return;
            }
            return;
        }

        
    }
}