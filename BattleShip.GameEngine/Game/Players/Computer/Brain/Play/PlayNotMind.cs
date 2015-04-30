using BattleShip.GameEngine.Arsenal.Gun;
using BattleShip.GameEngine.Arsenal.Gun.Destroyable;
using BattleShip.GameEngine.Fields;
using BattleShip.GameEngine.Location;
using System;
using System.Collections.Generic;

namespace BattleShip.GameEngine.Game.Players.Computer.Brain.Play
{
    public class PlayNotMind : IPlayable
    {
<<<<<<< HEAD
        //Location вже вказано в using BattleShip.GameEngine.Location;
=======
>>>>>>> adcb4d49f57b1a9c51a12f9f9099df7db01d1a0d
        public Location.Position GetPositionForAttackAndSetGun(FakeField myFakeField, Gun gun, IList<IDestroyable> gunList)
        {
            gun.ChangeCurrentGun(GunChoise(gunList));
            return AnnalizeFakeField(myFakeField);
        }

        // вибрати зброю
        private IDestroyable GunChoise(IList<IDestroyable> gunList)
        {
            if (gunList.Count == 0)
            {
                return new GunDestroy();
            }
            else
            {
                Random rnd = new Random();

                return gunList[rnd.Next(gunList.Count)];
            }
        }

        // проаналізувати фейкове поле player's і вибрати точку куди стріляти
        private Position AnnalizeFakeField(FakeField myFakeField)
        {
            Random rnd = new Random();

            // знайти пусту
            int cellNumber = 0;
            do
            {
                cellNumber = rnd.Next(myFakeField.Size * myFakeField.Size);
            } while (myFakeField[cellNumber].WasAttacked == true);

            // повернути її позицію
            return myFakeField[cellNumber].Location;
        }
    }
}