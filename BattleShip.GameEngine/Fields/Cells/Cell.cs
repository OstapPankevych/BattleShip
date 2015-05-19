using BattleShip.GameEngine.Arsenal.Gun;
using BattleShip.GameEngine.Arsenal.Protection;
using BattleShip.GameEngine.GameEventArgs;
using BattleShip.GameEngine.Location;
using BattleShip.GameEngine.ObjectOfGame;
using System;
using System.Collections;
using System.Collections.Generic;
using BattleShip.GameEngine.Arsenal.Flot;
using BattleShip.GameEngine.Fields.Cells.StatusCell;
using BattleShip.GameEngine.GameEvent;


namespace BattleShip.GameEngine.Fields.Cells
{
    public sealed class Cell : IEnumerable<ProtectBase>
    {
        #region Constructors

        public Cell(Position position)
        {
            WasAttacked = false;

            Location = position;

            _gameObject = new EmptyCell(position);
        }

        #endregion Constructors


        #region Private

        // клітинка поля може бути захищена декількома захистами(записуються їхні імена)
        private readonly List<ProtectBase> _protectionObjectList = new List<ProtectBase>();

        // статус клітинки
        private IGameObject _gameObject;

        #endregion Private


        #region Properties

        // чи ця клітинка знаходиться під захистом
        public bool IsProtected
        {
            get
            {
                if (_protectionObjectList.Count == 0)
                {
                    return false;
                }

                return true;
            }
            
        }

        public Position Location { get; private set; }

        public bool WasAttacked { get; private set; }

        #endregion Properties


        #region Events

        // івент знищення клітинки
        public event Action<GameEvenArgs> DeadHandler;

        #endregion Events


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


        #region OnHandler

        // опрацювання вмирання клітинки
        private void OnKillMe()
        {
            WasAttacked = true;

            // сказати всім, хто на неї підписаний, що її зачепили
            if (DeadHandler != null)
            {
                DeadHandler(new GameEvenArgs(Location));
            }
        }

        public void OnDestroyMe(GameEvenArgs e)
        {
            if (!WasAttacked)
            {
                OnKillMe();
            }
        }

        public void OnCancelSecureHandler(ProtectEventArgs e)
        {
            for (int i = 0; i < _protectionObjectList.Count; i++)
            {
                if (_protectionObjectList[i].GetType() == e.Type)
                {
                    _protectionObjectList.Remove(_protectionObjectList[i]);
                }
            }
            
        }

        #endregion


        #region Public methods

        public bool AddGameObject(IGameObject gameObject, bool sign)
        {
            if (gameObject is ShipBase)
            {
                return SetShip((ShipBase) gameObject, sign);
            }

            if (gameObject is ProtectBase)
            {
                return SetProtect((ProtectBase) gameObject, sign);
            }

            if (gameObject is AroundShip)
            {
                SetAroundShip((AroundShip) gameObject);
                return false;
            }
            
            return false;
        }

        // встановити захист
        public void SecureCell(ProtectBase protect)
        {
            _protectionObjectList.Add(protect);
        }

        // пальнути в цю клітинку
        public Type Shot(Gun gun, ref Position pos)
        {
            // Якщо клітинка була стріляна - повертати null
            if (WasAttacked)
            {
                return null;
            }
            else
            {
                if (ChackProtect(gun, ref pos))
                {
                    return typeof (ProtectedCell);
                }

                OnDestroyMe(null);

                return GetTypeOfCellObject();
            }   
        }

        // подивитись на цю клітинку
        public Type Show()
        {
            // якщо ще не була атаковано - то показати як пусту(невідому)
            if (!WasAttacked)
            {
                return typeof (EmptyCell);
            }

            return GetTypeOfCellObject();
        }

        // дати значення, яке  зберігає клітинка
        public Type GetTypeOfCellObject()
        {
            return _gameObject.GetType();
        }

        #endregion Public methods


        #region Private methods

        // чи захищена від gun
        private bool ChackProtect(Gun gun, ref Position pos)
        {
            if (IsProtected)
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
                            pos = x.Positions[0];
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool SetShip(ShipBase ship, bool sign)
        {
            if (_gameObject.GetType() == typeof(EmptyCell))
            {
                _gameObject = ship;

                if (sign)
                {
                    DeadHandler += ship.DestroyMe;
                }

                return true;
            }

            return false;
        }

        private bool SetProtect(ProtectBase protect, bool sign)
        {
            if (!(_gameObject.GetType() is ShipBase))
            {
                _gameObject = protect;

                if (sign)
                {
                    DeadHandler += protect.KillMe;
                }

                return true;
            }

            return false;
        }

        private void SetAroundShip(AroundShip aroundShip)
        {
            if (_gameObject.GetType() == typeof (EmptyCell))
            {
                _gameObject = aroundShip;
            }
        }

        #endregion Private methods
    }
}