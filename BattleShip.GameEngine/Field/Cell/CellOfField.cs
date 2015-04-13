using System;
using System.Collections.Generic;

using BattleShip.GameEngine.Location;
using BattleShip.GameEngine.GameObject;
using BattleShip.GameEngine.Arsenal.Protection;
using BattleShip.GameEngine.Field.Cell.StatusCell;

namespace BattleShip.GameEngine.Field.Cell
{
    sealed class CellOfField
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
        List<Type> _protectionNameList = new List<Type>();

        public CellOfField(Position position)
        {
            _position = position;
            _gameObject = new EmptyCell(position);
        }

        // івент влучання в клітинку
        public event Action<Position> ShotHandler;

        public bool AddObject(GameObject.GameObject gameObject)
        {
            if ((_gameObject is AroundShip || _gameObject is EmptyCell) & gameObject is ProtectedBase)
            {
                _gameObject = gameObject;

                // підписати вхідний обєкт на івент влучання
                ShotHandler = _gameObject.OnHitMeHandler;

                return true;
            }

            return false;
        }

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