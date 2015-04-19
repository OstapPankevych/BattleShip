using BattleShip.GameEngine.Arsenal.Flot;
using BattleShip.GameEngine.Arsenal.Gun;
using BattleShip.GameEngine.Arsenal.Gun.Destroyable;
using BattleShip.GameEngine.Game.Players;
using BattleShip.GameEngine.Game.Players.Computer;
using BattleShip.GameEngine.Game.Players.Man;
using BattleShip.GameEngine.Location;
using System;
using System.Collections.Generic;

namespace BattleShip.GameEngine.Game.GameProcces.Referee.ClassicGameReferee
{
    public class ClassicReferee : BaseReferre
    {
        #region Private

        private Man _player1;
        private Computer _player2;

        #endregion Private

        #region Events

        public event Action ManCurrentHandler; // навмисьне не роблю заглушки, щоб в Console не забути підписатись

        public override event Action<string, string> MadeGameStep = delegate { };

        #endregion Events

        #region Constructors

        public ClassicReferee(Man player1, Computer player2)
            : base(player1, player2)
        {
            this._player1 = player1;
            this._player2 = player2;
        }

        #endregion Constructors

        #region Public Methods

        public override void StartGame(Gun gun, Position position)
        {
            SetCurrentPlayer(gun, notCyrrentPlayer.AttackMe(gun, position));

            // запустити івент(повідомити підписників про зроблений хід)
            MadeGameStep(cyrrentPlayer.Name, notCyrrentPlayer.Name);
            GetDinamicAttackPosition();
        }

        #endregion Public Methods

        #region Private Methods

        private void SetCurrentPlayer(Gun gun, List<Type> attackResult)
        {
            if (gun.GetTypeOfCurrentCun() != typeof(GunDestroy))
            {
                SwapCurrentPlayer();
                return;
            }
            else
            {
                foreach (Type type in attackResult)
                {
                    if (type.BaseType == typeof(ShipBase))
                        return;
                }
                SwapCurrentPlayer();
            }
        }

        protected void SwapCurrentPlayer()
        {
            IPlayer temp = cyrrentPlayer;
            cyrrentPlayer = notCyrrentPlayer;
            notCyrrentPlayer = temp;
        }

        private void GetDinamicAttackPosition()
        {
            if (cyrrentPlayer is Man)
            {
                ManCurrentHandler();
                StartGame(cyrrentPlayer.CurrentGun, _player1.CurrentPositionForAttack);
            }
            else
            {
                StartGame(cyrrentPlayer.CurrentGun, _player2.GetPositionForAttack(notCyrrentPlayer.CurrentFakeField));
            }
        }

        #endregion Private Methods
    }
}