using BattleShip.GameEngine.GameEventArgs;
using BattleShip.GameEngine.Location;
using BattleShip.GameEngine.ObjectOfGame;
using System;

namespace BattleShip.GameEngine.Fields.Cells.StatusOfCells
{
    public abstract class StatusCell : GameObject
    {
        #region Private

        private readonly Position _position;
        private bool _isLife = true;

        private void OnDeadHandler()
        {
            this._isLife = false;
        }

        #endregion Private

        #region Public

        public StatusCell(Position position)
            : base(0)
        {
            this._position = position;
        }

        #region Override

        public override bool IsLife
        {
            get { return this._isLife; }
        }

        public override event Action<GameObject, GameEvenArgs> DeadHandler = delegate { };

        public override event Action<GameObject, GameEvenArgs> HitMeHandler = delegate { };

        public override void OnHitMeHandler(GameObject gameObject, GameEvenArgs e)
        {
            if (e.Location == _position)
            {
                if (this._isLife)
                {
                    OnDeadHandler();
<<<<<<< HEAD
                    //if (DeadHandler != null) DeadHandler(this, new GameEvenArgs(e.Location));
=======
>>>>>>> adcb4d49f57b1a9c51a12f9f9099df7db01d1a0d
                    DeadHandler(this, new GameEvenArgs(e.Location));
                }
            }
        }

        #endregion Override

        #endregion Public
    }
}