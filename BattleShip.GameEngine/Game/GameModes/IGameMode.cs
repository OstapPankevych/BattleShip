using BattleShip.GameEngine.Arsenal.Flot;
using BattleShip.GameEngine.Arsenal.Gun;
using BattleShip.GameEngine.Arsenal.Gun.Destroyable;
using BattleShip.GameEngine.Arsenal.Protection;
using BattleShip.GameEngine.Fields;
using BattleShip.GameEngine.Game.Players.Computer.Brain;
using BattleShip.GameEngine.Location;
using System;
using System.Collections.Generic;

namespace BattleShip.GameEngine.Game.GameModes
{
    public interface IGameMode
    {
        List<Type> AttackField(Gun myGun, Position position);

        void RemoveGunFromList(Gun gun);

        bool AddProtect(ProtectBase protect);

        bool AddShip(ShipBase ship);

        Brain BrainForComputer { get; }

        // максимальна кількість корабликів на полі
        byte CountMaxShipsOnField { get; }

        // максимальна кількість захитсів на поле
        byte CountMaxProtectsOnField { get; }

        byte CurrentCountShipsOnField { get; }

        byte CurrentCountProtectsOnField { get; }

        bool IsLife { get; }

        IList<IDestroyable> GunTypeList { get; }

        bool WasInitAllComponent { get; }

        Fields.Field CurrentField { get; }

        FakeField CurrentFakeField { get; }
    }
}