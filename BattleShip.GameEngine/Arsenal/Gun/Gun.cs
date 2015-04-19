using BattleShip.GameEngine.Arsenal.Gun.Destroyable;
using BattleShip.GameEngine.Location;
using System;

namespace BattleShip.GameEngine.Arsenal.Gun
{
    public class Gun
    {
        protected IDestroyable destroyable;

        public Gun()
        {
            destroyable = new GunDestroy();
        }

        // повернути Type встановленої зброї
        public Type GetTypeOfCurrentCun()
        {
            // повернути тип, на який зараз!!!! вказує інтерфейс
            return destroyable.GetType();
        }

        public Position[] Shot(Position point, byte size)
        {
            return destroyable.Destroy(point, size);
        }

        public void ChangeCurrentGun(IDestroyable gun)
        {
            destroyable = gun;
        }
    }
}