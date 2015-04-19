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

        public override void SetProtects()
        {
            this._brain.SetProtects(this.gameMode);
        }

        public override void SetRectangleShips()
        {
            this._brain.SetShips(this.gameMode);
        }

        #endregion BasePlayer realization

        #region Constructors

        public Computer(Brain.Brain brain, Game.GameMode.GameMode gameMode)
            : base("Computer", gameMode)
        {
            _brain = brain;
        }

        #endregion Constructors
    }
}