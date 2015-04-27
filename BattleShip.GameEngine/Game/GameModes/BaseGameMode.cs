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
    public abstract class BaseGameMode : IGameMode
    {
        private byte _currentCountShipsOnField = 0;
        private byte _currentCountProtectsOnField = 0;

        protected Fields.Field currentField;
        protected FakeField currentfakeField;
        protected List<IDestroyable> gunList = new List<IDestroyable>();

        public BaseGameMode(Fields.Field field)
        {
            this.currentField = field;
            this.currentfakeField = new FakeField(field);
        }

        public Fields.Field CurrentField
        {
            get { return currentField; }
        }

        public FakeField CurrentFakeField
        {
            get { return currentfakeField; }
        }

        public IList<IDestroyable> GunTypeList
        {
            get { return gunList.AsReadOnly(); }
        }

        public List<Type> AttackField(Gun gun, Position position)
        {
            return currentField.Shot(gun, position);
        }

        public void RemoveGunFromList(Gun gun)
        {
            // Чи міститься такий вид озброєння в арсеналі зброї
            bool contain = false;
            foreach (var gunTypy in gunList)
            {
                if (gun.GetTypeOfCurrentCun() == gunTypy.GetType())
                {
                    contain = true;
                    gunList.Remove(gunTypy);
                    break;
                }
            }

            // якщо не міститься, тоді встановити звичайну зброю
            if (!contain)
            {
                gun.ChangeCurrentGun(new GunDestroy());
            }
        }

        public bool WasInitAllComponent
        {
            get
            {
                if ((CurrentCountProtectsOnField == CountMaxProtectsOnField)
                    & (CurrentCountShipsOnField == CountMaxShipsOnField))
                {
                    return true;
                }

                return false;
            }
        }

        public new virtual byte CurrentCountShipsOnField
        {
            get { return _currentCountShipsOnField; }
            protected set { _currentCountShipsOnField = value; }
        }

        public new virtual byte CurrentCountProtectsOnField
        {
            get { return _currentCountProtectsOnField; }
            protected set { _currentCountProtectsOnField = value; }
        }

        public abstract bool IsLife { get; }

        public abstract Brain BrainForComputer { get; }

        public abstract byte CountMaxShipsOnField { get; }

        public abstract byte CountMaxProtectsOnField { get; }

        public abstract bool AddProtect(ProtectBase protect);

        public abstract bool AddShip(ShipBase ship);
    }
}