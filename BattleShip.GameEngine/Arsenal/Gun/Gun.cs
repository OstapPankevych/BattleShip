using BattleShip.GameEngine.Arsenal.Gun.Destroyable;
using BattleShip.GameEngine.Location;
using System;

namespace BattleShip.GameEngine.Arsenal.Gun
{
    public class Gun
    {
        public Gun()
        {
            this._destroyable = new GunDestroy();
        }


        #region Private

        private IDestroyable _destroyable;

        #endregion Private


        #region Public methods

        // повернути Type встановленої зброї
        public Type GetTypeOfCurrentCun()
        {
            // повернути тип, на який зараз!!!! вказує інтерфейс
            return this._destroyable.GetType();
        }

        public IDestroyable GetDestroyable()
        {
            return _destroyable;
        }

        public Position[] Shot(Position point, byte size)
        {
            return this._destroyable.Destroy(point, size);
        }

        public void ChangeCurrentGun(IDestroyable gun)
        {
            this._destroyable = gun;
        }

        #endregion Public methods
    }
}