using System;
using System.Collections.Generic;

using BattleShip.GameEngine.Location;
using BattleShip.GameEngine.GameObject;
using BattleShip.GameEngine.Arsenal.Protection;
using BattleShip.GameEngine.Field.Cell.StatusCell;
using BattleShip.GameEngine.GameEventArgs;


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

        public bool OnRemoveProtection(GameObject.GameObject sender, ProtectEventArgs e)
        {
            return _protectionTypeList.Remove(e.Type);
        }

        // івент зняття захисту, для всіх клітинок, які покриваються цією клітинкою
        public event Action<GameObject.GameObject, ProtectEventArgs> RemoveProtectHandler = delegate { };

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
        }

        // івент знищення клітинки
        public event Action<GameObject.GameObject, GameEvenArgs> DeadHandler = delegate { };

        // пальнути в цю клітинку
        public Type Shot()
        {
            // якщо ще не була атаковано - то показати як пусту(невідому)
            if (!_wasAttacked)
                return typeof(EmptyCell);

            OnHitMeHandler(_gameObject, new GameEvenArgs(this._position));

            return _gameObject.GetType();
        }

        // опрацювання вмирання клітинки
        public void OnDeadHandler()
        {
            // сказати всім, хто на неї підписаний, що її зачепили
            DeadHandler(_gameObject, new GameEventArgs.GameEvenArgs(this._position));

            if (_gameObject is ProtectedBase)
                ((ProtectedBase)_gameObject).OnRemoveProtect(_gameObject, new ProtectEventArgs(_gameObject.GetType()));
        }

        public void OnHitMeHandler(GameObject.GameObject sender, GameEvenArgs e)
        {
            this._wasAttacked = true;
            OnDeadHandler();
        }

        // подивитись на цю клітинку
        public Type Show()
        {
            // якщо ще не була атаковано - то показати як пусту(невідому)
            if (!_wasAttacked)
                return typeof(EmptyCell);

            return _gameObject.GetType();
        }

        public Type GetStatusCell()
        {
            return _gameObject.GetType();
        }
       
    }
}