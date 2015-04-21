using BattleShip.GameEngine.Field;
using BattleShip.GameEngine.Location;

namespace BattleShip.GameEngine.Game.Players.Computer
{
    public class Computer : BasePlayer
    {
        #region Private

        private Brain.Brain _brain;// = new Brain(new Play(), new SetProtect(), new SetShip());

        #endregion Private

        #region BasePlayer realization

        public Position GetPositionForAttack(FakeField manFakeField)
        {
            return _brain.GetPositionForAttackAndSetGun(this.gameMode, manFakeField);
        }

        #endregion BasePlayer realization

        #region Constructors

        public Computer(Brain.Brain brain, Game.GameMode.GameMode gameMode)
            : base("Computer", gameMode)
        {
            _brain = brain;

            this._brain.SetShips(this.gameMode);
            this._brain.SetProtects(this.gameMode);
        }

        #endregion Constructors
    }
}