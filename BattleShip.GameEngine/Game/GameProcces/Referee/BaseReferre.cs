using BattleShip.GameEngine.Arsenal.Gun;
using BattleShip.GameEngine.Game.GameProcces.ClassicGameReferee;
using BattleShip.GameEngine.Game.Players;
using BattleShip.GameEngine.Location;
using System;

namespace BattleShip.GameEngine.Game.GameProcces.Referee
{
    public abstract class BaseReferre : IReferee
    {
        #region Private And Protected Members

        private IPlayer _player1;
        private IPlayer _player2;

        protected IPlayer cyrrentPlayer;
        protected IPlayer notCyrrentPlayer;

        #endregion Private And Protected Members

        #region Events

        public abstract event Action<string, string> MadeGameStep;

        #endregion Events

        #region Constructors

        public BaseReferre(IPlayer player1, IPlayer player2)
        {
            _player1 = player1;
            _player2 = player2;

            // рендомом вибрати першого ігорока, який робить хід
            Random rnd = new Random();
            switch (rnd.Next(2))
            {
                case 0:
                    {
                        cyrrentPlayer = player1;
                        notCyrrentPlayer = player2;
                        break;
                    }
                case 1:
                    {
                        cyrrentPlayer = player2;
                        notCyrrentPlayer = player1;
                        break;
                    }
            }
        }

        #endregion Constructors

        #region Public Methods

        public Field.Field GetFieldOfPlayer1()
        {
            return _player1.CurreField;
        }

        public Field.FakeField GetFakeFieldOfPlayer2()
        {
            return _player2.CurrentFakeField;
        }

        #endregion Public Methods

        #region Abstract Methods

        public abstract void StartGame(Gun gun, Position position);

        #endregion Abstract Methods
    }
}