using BattleShip.GameEngine.Game.Players.Computer.Brain.Play;
using BattleShip.GameEngine.Game.Players.Computer.Brain.SetObjects.SetProtect;
using BattleShip.GameEngine.Game.Players.Computer.Brain.SetObjects.SetRectangleBase;
using BattleShip.GameEngine.Location;

namespace BattleShip.GameEngine.Game.Players.Computer.Brain
{
    public class Brain
    {
        private IPlayable _play;
        private ISetibleProtect _setProject;
        private ISetibleShip _setShip;

        public Brain(IPlayable play, ISetibleProtect setProtect, ISetibleShip setShip)
        {
            _play = play;
            _setProject = setProtect;
            _setShip = setShip;
        }

        public Position GetPositionForAttackAndSetGun(GameMode.GameMode myMode, Field.FakeField fakeManField)
        {
            return _play.GetPositionForAttackAndSetGun(myMode, fakeManField);
        }

        public void SetShips(GameMode.GameMode mode)
        {
            _setShip.SetShips(mode);
        }

        public void SetProtects(GameMode.GameMode mode)
        {
            _setProject.SetProtects(mode);
        }
    }
}