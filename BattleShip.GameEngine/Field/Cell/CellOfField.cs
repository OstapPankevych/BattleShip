using BattleShip.GameEngine.Arsenal.Gun;
using BattleShip.GameEngine.Arsenal.Protection;
using BattleShip.GameEngine.Field.Cell.AttackResult;
using BattleShip.GameEngine.Field.Cell.StatusCell;
using BattleShip.GameEngine.GameEventArgs;
using BattleShip.GameEngine.Location;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BattleShip.GameEngine.Field.Cell
{
    public sealed class CellOfField : IEnumerable<ProtectBase>
    {
        // клітинка поля може бути захищена декількома захистами(записуються їхні імена)
        private readonly List<ProtectBase> _protectionObjectList = new List<ProtectBase>();

        // клвтинка поля може містити обєкт поля(захист чи кораблик, і т.д)
        private GameObject.GameObject _gameObject;

        public CellOfField(Position position)
        {
            WasAttacked = false;
            Location = position;
            _gameObject = new EmptyCell(position);
        }

        public Position Location { get; private set; }

        public bool WasAttacked { get; private set; }

        // отримати список захистів, які є на клітинці
        public IEnumerator<ProtectBase> GetEnumerator()
        {
            foreach (var x in _protectionObjectList)
                yield return x;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void AddGameObject(GameObject.GameObject gameObject, bool sign)
        {
            _gameObject = gameObject;

            // якщо це захист, тоді встановити захист
            if (gameObject is ProtectBase)
                SetProtect((ProtectBase)gameObject);
            // підписати об'єкт на руйнацію під час руйнації клітинки
            if (sign)
                DeadHandler += gameObject.OnHitMeHandler;
        }

        public void AddStatus(StatusCell.StatusCell status)
        {
            _gameObject = status;
            DeadHandler += status.OnHitMeHandler;
        }

        // встановити захист
        public void SetProtect(ProtectBase protect)
        {
            _protectionObjectList.Add(protect);
        }

        public void OnRemoveProtection(GameObject.GameObject sender, ProtectEventArgs e)
        {
            foreach (var protect in _protectionObjectList)
            {
                if (protect.GetType() == e.Type)
                    _protectionObjectList.Remove(protect);
            }
        }

        // івент знищення клітинки
        public event Action<GameObject.GameObject, GameEvenArgs> DeadHandler = delegate { };

        // пальнути в цю клітинку
        public Type Shot(Gun gun)
        {
            // провірити клітинку на захист від зброї
            Type gunType = gun.GetTypeOfCurrentCun();

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

            return _gameObject.GetType();
        }

        // опрацювання вмирання клітинки
        public void OnDeadHandler()
        {
            // сказати всім, хто на неї підписаний, що її зачепили
            DeadHandler(_gameObject, new GameEvenArgs(Location));
        }

        public void OnHitMeHandler(GameObject.GameObject sender, GameEvenArgs e)
        {
            WasAttacked = true;
            OnDeadHandler();
        }

        // подивитись на цю клітинку
        public Type Show()
        {
            // якщо ще не була атаковано - то показати як пусту(невідому)
            if (!WasAttacked)
                return typeof(EmptyCell);

            return _gameObject.GetType();
        }

        public Type GetStatusCell()
        {
            return _gameObject.GetType();
        }

        // повернути цю клітинку, коли в неї вже стріляли, інакше - повернути фейк
        public CellOfField GetMyFake()
        {
            if (WasAttacked)
                return this;

            CellOfField fake = new CellOfField(this.Location);
            fake.AddGameObject(new EmptyCell(this.Location), false);
            return fake;
        }
    }
}