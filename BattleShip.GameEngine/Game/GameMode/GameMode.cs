using BattleShip.GameEngine.Arsenal.Flot;
using BattleShip.GameEngine.Arsenal.Gun;
using BattleShip.GameEngine.Arsenal.Gun.Destroyable;
using BattleShip.GameEngine.Arsenal.Protection;
using BattleShip.GameEngine.Field;
using System.Collections.Generic;

namespace BattleShip.GameEngine.Game.GameMode
{
    public abstract class GameMode
    {
        #region Player's Arsenal

        protected Field.Field currentField = new Field.Field(10);

        protected Field.FakeField currentFakeField;

        protected List<ProtectBase> protectList = new List<ProtectBase>();

        protected List<ShipBase> shipList = new List<ShipBase>();

        protected List<IDestroyable> gunTypesList = new List<IDestroyable>();

        protected Gun gun = new Gun();

        #endregion Player's Arsenal

        #region Properties

        public Gun CurrentGun
        {
            get { return gun; }
        }

        public List<IDestroyable> GunTypesList
        {
            get { return gunTypesList; }
        }

        public List<ShipBase> ShipList
        {
            get { return shipList; }
        }

        public List<ProtectBase> ProtectList
        {
            get { return protectList; }
        }

        public FakeField CurrentFakeField
        {
            get { return currentFakeField; }
        }

        public Field.Field CurrentField
        {
            get { return currentField; }
        }

        #endregion Properties

        #region Methods

        #region Abstract Methods

        public abstract bool AddShip(ShipBase ship);

        public abstract bool AddProtect(ProtectBase protect);

        #endregion Abstract Methods

        public void SetCurrentGun(IDestroyable gunType)
        {
            // встоновити зброю якщо вона є в наявності у player
            if (gunTypesList.Contains(gunType))
                gun.ChangeCurrentGun(gunType);
            else
            {
                gun.ChangeCurrentGun(new GunDestroy());
            }
        }

        #endregion Methods
    }
}