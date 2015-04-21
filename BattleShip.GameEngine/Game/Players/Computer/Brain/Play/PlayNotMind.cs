using BattleShip.GameEngine.Arsenal.Gun.Destroyable;
using BattleShip.GameEngine.Location;
using System;
using System.Collections.Generic;

namespace BattleShip.GameEngine.Game.Players.Computer.Brain.Play
{
    public class PlayNotMind : IPlayable
    {
        public Location.Position GetPositionForAttackAndSetGun(GameMode.GameMode myMode, Field.FakeField fakeManField)
        {
            myMode.SetCurrentGun(GunChoise(myMode.GunTypesList));
            return AnnalizeField(fakeManField);
        }

        // вибрати зброю
        private IDestroyable GunChoise(List<IDestroyable> gunList)
        {
            Random rnd = new Random();
            return gunList[rnd.Next(gunList.Count)];
        }

        // проаналізувати фейкове поле player's і вибрати точку куди стріляти
        private Position AnnalizeField(Field.FakeField manFakeField)
        {
            Random rnd = new Random();

            // знайти пусту
            int cellNumber = 0;
            do
            {
                cellNumber = rnd.Next(manFakeField.Size * manFakeField.Size);
            } while (manFakeField[cellNumber].WasAttacked == true);

            // повернути її позицію
            return manFakeField[cellNumber].Location;
        }
    }
}