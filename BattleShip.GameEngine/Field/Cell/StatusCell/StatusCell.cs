using BattleShip.GameEngine.GameEventArgs;
using BattleShip.GameEngine.Location;
using System;

namespace BattleShip.GameEngine.Field.Cell.StatusCell
{
    public abstract class StatusCell : GameObject.GameObject
    {
        private readonly Position _position;
        private bool _isLife = true;

        public StatusCell(Position position)
            : base(0)
        {
            _position = position;
        }

        public override bool IsLife
        {
            get { return _isLife; }
        }

        public override event Action<GameObject.GameObject, GameEvenArgs> DeadHandler;

        public override event Action<GameObject.GameObject, GameEvenArgs> HitMeHandler;

        private void OnDeadHandler()
        {
            _isLife = false;
        }

        public override void OnHitMeHandler(GameObject.GameObject gameObject, GameEvenArgs e)
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
        }
    }
}