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
            Position pos;
            do
            {
                cellNumber = rnd.Next(myFakeField.Size * myFakeField.Size);
                pos = BaseField.GetPositionForNumber(cellNumber, myFakeField.Size);
            } while (myFakeField[pos].WasAttacked == true); //"== true" не потрібно писати WasAttacked є bool

            // повернути її позицію
            return myFakeField[pos].Location;
        }
    }
}