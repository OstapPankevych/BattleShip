using System;
using System.Collections.Generic;

using BattleShip.GameEngine.Location;
using BattleShip.GameEngine.GameObject;
using BattleShip.GameEngine.Arsenal.Protection;
using BattleShip.GameEngine.Field.Cell.StatusCell;

namespace BattleShip.GameEngine.Field.Cell
{
    sealed class CellOfField : IEnumerable<Type>
    {
        Position _position;

        bool _wasAttacked = false;

        public Position GetMyLocation()
        {
            return this._position;
        }

        public bool WasAttacked
        {
            get
            {
                return this._wasAttacked;
            }
        }

        // клвтинка поля може містити обєкт поля(захист чи кораблик, і т.д)
        private GameObject.GameObject _gameObject;

        // клітинка поля може бути захищена декількома захистами(записуються їхні імена)
        List<Type> _protectionTypeList = new List<Type>();

        public CellOfField(Position position)
        {
            _position = position;
            _gameObject = new EmptyCell(position);
        }

        public void AddProtected(Type typeProtected)
        {
            _protectionTypeList.Add(typeProtected);
        }

        public bool RemoveProtection(Type typeProtected)
        {
            return _protectionTypeList.Remove(typeProtected);
        }

        // отримати список захистів, які є на клітинці
        public IEnumerator<Type> GetEnumerator()
        {
            return _protectionTypeList.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (IEnumerator<Type>)GetEnumerator();
        }


        public void AddObject(GameObject.GameObject gameObject)
        {
            _gameObject = gameObject;
            ShotHandler += gameObject.OnHitMeHandler;
        }

        // івент влучання в клітинку
        public event Action<Position> ShotHandler;

        // пальнути в цю клітинку
        public Type Shot()
        {
            // якщо ще не була атаковано - то показати як пусту(невідому)
            if (!_wasAttacked)
                return typeof(EmptyCell);

            this._wasAttacked = true;

            // сказати всім, хто на неї підписаний, що її зачепили
            if (ShotHandler != null)
                ShotHandler(_position);

            return _gameObject.GetType();
        }

        // подивитись на цю клітинку
        public Type Show()
        {
            // якщо ще не була атаковано - то показати як пусту(невідому)
            if (!_wasAttacked)
                return typeof(EmptyCell);

            return _gameObject.GetType();
        }

       
    }
}