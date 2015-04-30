using BattleShip.GameEngine.Arsenal.Gun;
using BattleShip.GameEngine.Arsenal.Protection;
using BattleShip.GameEngine.Field.Cells.AttackResult;
using BattleShip.GameEngine.Fields.Cells.StatusOfCells;
using BattleShip.GameEngine.GameEventArgs;
using BattleShip.GameEngine.Location;
using BattleShip.GameEngine.ObjectOfGame;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BattleShip.GameEngine.Fields.Cells
{
    public sealed class Cell : IEnumerable<ProtectBase>
    {
        #region Private

        // клітинка поля може бути захищена декількома захистами(записуються їхні імена)
        private readonly List<ProtectBase> _protectionObjectList = new List<ProtectBase>();

        // клвтинка поля може містити обєкт поля(захист чи кораблик, і т.д)
        private GameObject _gameObject;

        #endregion Private

        #region Public

        public Cell(Position position)
        {
            WasAttacked = false;
            Location = position;
            this._gameObject = new EmptyCell(position);
        }

        #region Properties

        public Position Location { get; private set; }

        public bool WasAttacked { get; private set; }

        #endregion Properties

        #region Methods

        #region IEnumerator<ProtectBase>

        // отримати список захистів, які є на клітинці
        public IEnumerator<ProtectBase> GetEnumerator()
        {
            foreach (var x in _protectionObjectList)
            {
                yield return x;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion IEnumerator<ProtectBase>

        public void AddGameObject(GameObject gameObject, bool sign)
        {
            this._gameObject = gameObject;

            // якщо це захист, тоді встановити захист
            if (gameObject is ProtectBase)
            {
                SetProtect((ProtectBase)gameObject);
            }

            // підписати об'єкт на руйнацію під час руйнації клітинки
            if (sign)
            {
                DeadHandler += gameObject.OnHitMeHandler;
            }
        }

        public void AddStatus(StatusCell status)
        {
            this._gameObject = status;
            DeadHandler += status.OnHitMeHandler;
        }

        // встановити захист
        public void SetProtect(ProtectBase protect)
        {
            this._protectionObjectList.Add(protect);
        }

        public void OnRemoveProtection(GameObject sender, ProtectEventArgs e)
        {
            for (int i = 0; i < _protectionObjectList.Count; i++)
            {
                foreach (var t in _protectionObjectList[i].GetProtectedType())
                {
                    if (t == e.Type)
                    {
                        _protectionObjectList.Remove(_protectionObjectList[i]);
                    }
                }
            }
        }

        // пальнути в цю клітинку
        public Type Shot(Gun gun)
        {
            // провірити клітинку на захист від зброї
            Type gunType = gun.GetTypeOfCurrentCun();

            // Перевірити чи клітинка захищена.
            foreach (ProtectBase x in _protectionObjectList)
            {
                Type[] gunTypes = x.GetProtectedType();
                for (int i = 0; i < gunTypes.Length; i++)
                {
                    if (gunTypes[i] == gunType)
                    {
                        return typeof(ProtectedCell);
                    }
                }
            }

            WasAttacked = true;

            OnHitMeHandler(_gameObject, new GameEvenArgs(Location));

            return this._gameObject.GetType();
        }

        // опрацювання вмирання клітинки
        public void OnDeadHandler()
        {
            WasAttacked = true;
            // сказати всім, хто на неї підписаний, що її зачепили
            DeadHandler(_gameObject, new GameEvenArgs(Location));
        }

        public void OnHitMeHandler(GameObject sender, GameEvenArgs e)
        {
            OnDeadHandler();
        }

        // подивитись на цю клітинку
        public Type Show()
        {
            // якщо ще не була атаковано - то показати як пусту(невідому)
            if (!WasAttacked)
            {
                return typeof(EmptyCell);
            }

            return this._gameObject.GetType();
        }

        public Type GetStatusCell()
        {
            return this._gameObject.GetType();
        }

        #region Events

        // івент знищення клітинки
        public event Action<GameObject, GameEvenArgs> DeadHandler = delegate { };

        #endregion Events

        #endregion Methods

        #endregion Public
    }
}