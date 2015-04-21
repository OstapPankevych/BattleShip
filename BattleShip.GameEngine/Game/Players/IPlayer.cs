using BattleShip.GameEngine.Arsenal.Gun;
using BattleShip.GameEngine.Field;
using BattleShip.GameEngine.Location;
using System;
using System.Collections.Generic;

namespace BattleShip.GameEngine.Game.Players
{
    public interface IPlayer
    {
        #region Properties

        string Name { get; }

        FakeField CurrentFakeField { get; }

        Field.Field CurreField { get; }

        Gun CurrentGun { get; }

        #endregion Properties

        #region Methods

        List<Type> AttackMe(Gun gun, Position position);

        #endregion Methods
    }
}