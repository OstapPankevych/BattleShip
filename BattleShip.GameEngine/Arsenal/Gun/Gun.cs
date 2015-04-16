using System;

using BattleShip.GameEngine.Arsenal.Gun.Destroyable;

using BattleShip.GameEngine.Location;


namespace BattleShip.GameEngine.Arsenal.Gun
{
    public class Gun
    {
        protected IDestroyable destroyable;

        public Position[] Shot(Position point, byte size)
        {
            return destroyable.Destroy(point, size);
        }

        public Gun()
        {
            destroyable = new GunDestroy();
        }

        public void ChangeCurrentGun(IDestroyable gun)
        {
            destroyable = gun;
        }
    }
}