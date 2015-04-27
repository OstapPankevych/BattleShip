using BattleShip.GameEngine.Arsenal.Flot;
using BattleShip.GameEngine.Arsenal.Gun;
using BattleShip.GameEngine.Arsenal.Gun.Destroyable;
using BattleShip.GameEngine.Arsenal.Protection;
using BattleShip.GameEngine.Fields;
using BattleShip.GameEngine.Game.Players.Computer.Brain.Play;
using BattleShip.GameEngine.Game.Players.Computer.Brain.SetObjects.SetProtect;
using BattleShip.GameEngine.Game.Players.Computer.Brain.SetObjects.SetRectangleBase;
using BattleShip.GameEngine.Location;
using System;
using System.Collections.Generic;

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

        public void SetIPlayable(IPlayable play)
        {
            _play = play;
        }

        public void SetISetibleProtect(ISetibleProtect setProtect)
        {
            _setProject = setProtect;
        }

        public void SetISetibleShip(ISetibleShip setShip)
        {
            _setShip = setShip;
        }

        public Position GetPositionForAttackAndSetGun(FakeField myFakeField, Gun gun, IList<IDestroyable> gunList)
        {
            return _play.GetPositionForAttackAndSetGun(myFakeField, gun, gunList);
        }

        public void SetRectangleShips(Func<ShipBase, bool> SetShipsFunc, byte fieldSize)
        {
            _setShip.SetShips(SetShipsFunc, fieldSize);
        }

        public void SetPVOProtect(Func<ProtectBase, bool> SetPtotectFunc, byte fieldSize)
        {
            _setProject.SetProtects(SetPtotectFunc, fieldSize);
        }
    }
}