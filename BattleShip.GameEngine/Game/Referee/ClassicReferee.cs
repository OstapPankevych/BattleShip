using BattleShip.GameEngine.Arsenal.Flot;
using BattleShip.GameEngine.Arsenal.Flot.Corectible;
using BattleShip.GameEngine.Arsenal.Flot.RectangleShips;
using BattleShip.GameEngine.Arsenal.Gun;
using BattleShip.GameEngine.Arsenal.Gun.Destroyable;
using BattleShip.GameEngine.Arsenal.Protection;
using BattleShip.GameEngine.Fields;
using BattleShip.GameEngine.Game.GameModes;
using BattleShip.GameEngine.Game.GameModes.ClassicGameModes;
using BattleShip.GameEngine.Game.Players;
using BattleShip.GameEngine.Game.Players.Computer;
using BattleShip.GameEngine.Location;
using System;
using System.Collections.Generic;

namespace BattleShip.GameEngine.Game.Referee
{
    public class ClassicReferee
    {
        private class PlayerAndGameMode
        {
            // чи є об'єктом, який зараз робить дію
            public bool IsCurrent = false;

            public readonly IGameMode GameMode;
            public readonly IPlayer Player;

            private PlayerAndGameMode(Type gameModeType)
            {
                GameMode = (IGameMode)Activator.CreateInstance(gameModeType);
            }

            public bool IsLife
            {
                get { return GameMode.IsLife; }
            }

            // Ініціалізується ігрок як людина.
            public PlayerAndGameMode(Type gameModeType, string name,
                Action<Man> StartSetShips,
                Action<Man> StartSetProtects,
                Func<Gun, IList<IDestroyable>, Position> GetPositionForAttack)
                : this(gameModeType)
            {
                this.Player = new Man(name, this.GameMode.CurrentFakeField.Size,
                    StartSetShips,
                    StartSetProtects,
                    GetPositionForAttack);
            }

            // Ініціалізується ігрок як компютер
            public PlayerAndGameMode(Type gameModeType, FakeField fakefieldAnotherPlayer)
                : this(gameModeType)
            {
                this.Player = new Computer(this.GameMode.BrainForComputer,
                    this.GameMode.AddShip,
                    this.GameMode.AddProtect,
                    fakefieldAnotherPlayer,
                    this.GameMode.CurrentField.Size);
            }
        }

        private readonly PlayerAndGameMode _playerAndGameMode1;
        private readonly PlayerAndGameMode _playerAndGameMode2;

        #region Public methods

        public string GetCurrentPlayerName()
        {
            if (_playerAndGameMode1.IsCurrent)
            {
                return _playerAndGameMode1.Player.Name;
            }
            else
            {
                return _playerAndGameMode2.Player.Name;
            }
        }

        public BaseField GetPlayer1Field()
        {
            if (_playerAndGameMode2.Player is Man)
            {
                return _playerAndGameMode1.GameMode.CurrentField;
            }
            else
            {
                return _playerAndGameMode1.GameMode.CurrentFakeField;
            }
        }

        public BaseField GetPlayer2Field()
        {
            return _playerAndGameMode2.GameMode.CurrentFakeField;
        }

        public IList<IDestroyable> GetPlayer1GunList()
        {
            return _playerAndGameMode1.GameMode.GunTypeList;
        }

        public IList<IDestroyable> GetPlayer2GunTypeList()
        {
            return _playerAndGameMode2.GameMode.GunTypeList;
        }

        public IList<IDestroyable> Player1GunList()
        {
            return _playerAndGameMode1.GameMode.GunTypeList;
        }

        public IList<IDestroyable> Player2GunList()
        {
            return _playerAndGameMode2.GameMode.GunTypeList;
        }

        public IList<IDestroyable> CurrentPlayerGunList()
        {
            if (_playerAndGameMode1.IsCurrent)
            {
                return _playerAndGameMode1.GameMode.GunTypeList;
            }
            else
            {
                return _playerAndGameMode2.GameMode.GunTypeList;
            }
        }

        public BaseField GetFieldOfPlayer1()
        {
            // коли гра типу Player VS Player - показувати фейкове поле
            if (this._playerAndGameMode2.Player is Man)
            {
                return this._playerAndGameMode1.GameMode.CurrentFakeField;
            }
            else
            {
                return this._playerAndGameMode1.GameMode.CurrentField;
            }
        }

        public BaseField GetFieldOfPlayer2()
        {
            return this._playerAndGameMode2.GameMode.CurrentFakeField;
        }

        #endregion public methods

        #region Events

        // івент отримання позиції(точки) від зовнішнього світу
        public event Func<byte, Position> GetPositionFunc;

        // івент про те, що зараз буде процес розтавлення кораблика
        public event Action WillPuttingShipsInfo;

        // івент про те, що зараз встановлення корабликів
        public event Func<Fields.Field, string, Position[]> IsSettingShipsNowPlayerFunc;

        // івент про те, що зараз встановленн захистів
        public event Func<Fields.Field, string, Position> IsSettingProtectsNowPlayerFunc;

        // івент про те, що зараз буде процес розставлення захисту
        public event Action WillPuttingProtectsInfo;

        // івент про те, що всі корабликі розставлені
        public event Action SettedAllShipsSuccesfulyInfo;

        // івент про те, зо всі захисти розтавленні
        public event Action SettedAllProtectsSuccesfulyInfo;

        // івент про те, що успішно поставився кораблик
        public event Action SettedSomeShipSuccesfulyInfo;

        // івент про те, що успішно поставився захист
        public event Action SettedSomeProtectSuccesfulyInfo;

        // івент про те, що потрібно вибрати тип зброї
        public event Func<IDestroyable> IsChoisingGunTypeFunc;

        // івент про те, що відбувся постріл
        public event Action WasShotActionInfo;

        // івент отримання запиту чи розставляти кораблики і захисти для користувача рендомом
        public event Func<string, bool> SetRandomAllGameObjectsOnField;

        // івент про початок гри
        public event Action GameWasStartedInfo;

        // івент про закінчення гри
        public event Action GameWasEndedIndo;

        // івент про те, що потрібно зробити постріл гравцю
        public event Action MakeShotInfo;

        #endregion Events

        #region Putting process object on field

        private void StartSetShipsForMan(Man player)
        {
            // Ідентифікувати гравця:
            PlayerAndGameMode playerAndGameMode = (ReferenceEquals(_playerAndGameMode1.Player, player))
                ? _playerAndGameMode1
                : _playerAndGameMode2;

            PuttingShipsProcess(playerAndGameMode);
        }

        public void StartSetProtectsForMan(Man player)
        {
            // Ідентифікувати гравця:
            PlayerAndGameMode playerAndGameMode = (ReferenceEquals(_playerAndGameMode1.Player, player))
                ? _playerAndGameMode1
                : _playerAndGameMode2;

            PuttingProtectsProcess(playerAndGameMode);
        }

        private void PuttingShipsProcess(PlayerAndGameMode playerAndGameMode)
        {
            Func<byte, Position, Position, ShipBase> GetShip = (countStorey, begin, end) =>
            {
                Position[] shipRegion = Ractangle.GetRectangleRegion(countStorey, begin, end);

                if (shipRegion.Length == 1)
                {
                    return new OneStoreyRectangleShip(playerAndGameMode.GameMode.CurrentCountShipsOnField,
                        shipRegion[0]);
                }
                if (shipRegion.Length == 2)
                {
                    return new TwoStoreyRectangleShip(playerAndGameMode.GameMode.CurrentCountShipsOnField,
                        shipRegion[0], shipRegion[1]);
                }
                if (shipRegion.Length == 3)
                {
                    return new ThreeStoreyRectangleShip(playerAndGameMode.GameMode.CurrentCountShipsOnField,
                        shipRegion[0], shipRegion[1], shipRegion[2]);
                }
                if (shipRegion.Length == 4)
                {
                    return new FourStoreyRectangleShip(playerAndGameMode.GameMode.CurrentCountShipsOnField,
                        shipRegion[0], shipRegion[1], shipRegion[2], shipRegion[3]);
                }

                return null;
            };

            // запустити івент про те, що зараз буде йти процес встановлення корабликів на поле.
            WillPuttingShipsInfo();

            while (playerAndGameMode.GameMode.CurrentCountShipsOnField <
                   playerAndGameMode.GameMode.CountMaxShipsOnField)
            {
                Position[] positions =
                    IsSettingShipsNowPlayerFunc(playerAndGameMode.GameMode.CurrentField, playerAndGameMode.Player.Name);

                // створення кораблика
                Position begin = positions[0];
                Position end = positions[positions.Length - 1];

                byte countStorey = 0;
                if (Ractangle.IsCorrectColumn(begin, end))
                {
                    countStorey = (byte)(Math.Abs(begin.Line - end.Line) + 1);
                }
                else
                {
                    if (Ractangle.IsCorrectLine(begin, end))
                    {
                        countStorey = (byte)(Math.Abs(begin.Column - end.Column) + 1);
                    }
                }

                if (countStorey > 4)
                {
                    continue;
                }

                if (Ractangle.ChackShipRegion(countStorey, begin, end))
                {
                    if (playerAndGameMode.GameMode.AddShip(GetShip(countStorey, begin, end)))
                    {
                        // запустити івент про успішне додавання кораблика на поле
                        SettedSomeShipSuccesfulyInfo();
                    }
                }
            }

            // Запустити івент про завершення додавань всіх корабликів на поле
            SettedAllShipsSuccesfulyInfo();
        }

        private void PuttingProtectsProcess(PlayerAndGameMode playerAndGameMode)
        {
            // запустити івент про те, що зараз буде процес встановлення захистів на поле.
            WillPuttingProtectsInfo();

            while (playerAndGameMode.GameMode.CurrentCountProtectsOnField <
                   playerAndGameMode.GameMode.CountMaxProtectsOnField)
            {
                Position position =
                    IsSettingProtectsNowPlayerFunc(playerAndGameMode.GameMode.CurrentField, playerAndGameMode.Player.Name);

                if (playerAndGameMode.GameMode.AddProtect(
                    new PVOProtect(playerAndGameMode.GameMode.CurrentCountProtectsOnField,
                            position, playerAndGameMode.GameMode.CurrentField.Size)))
                {
                    // запустити івент про успішне додавання захисту на поле
                    SettedSomeProtectSuccesfulyInfo();
                }
            }

            // Запустити івент про завершення додавань всіх захистів
            SettedAllProtectsSuccesfulyInfo();
        }

        #endregion Putting process object on field

        #region BeginStartGame

        private void InitGameObjectsPlayer1()
        {
            // Дати запит про розставляння корабликів і захистів рендомом
            if (SetRandomAllGameObjectsOnField(_playerAndGameMode1.Player.Name))
            {
                // створити Computer, який розставить всі об'єкти
                Computer cmp = new Computer(_playerAndGameMode1.GameMode.BrainForComputer,
                    _playerAndGameMode1.GameMode.AddShip,
                    _playerAndGameMode1.GameMode.AddProtect,
                    null,
                    _playerAndGameMode1.GameMode.CurrentField.Size);

                // почати встановлення обєктів
                cmp.BeginSetShips();
                cmp.BeginSetProtect();
            }
            else
            {
                this._playerAndGameMode1.Player.BeginSetShips();
                // не встановлювати захисти коли класична гра
                if (!(_playerAndGameMode1.GameMode is ClassicGameMode))
                {
                    this._playerAndGameMode1.Player.BeginSetProtect();
                }
            }
        }

        private void InitGameObjectsPlayer2()
        {
            // Player2 - або комп, або людина. тому робимо перевірку хто він
            if (_playerAndGameMode2.Player is Man)
            {
                // Дати запит про розставляння корабликів і захистів рендомом
                if (SetRandomAllGameObjectsOnField(_playerAndGameMode2.Player.Name))
                {
                    // створити Computer, який розставить всі об'єкти
                    Computer cmp = new Computer(_playerAndGameMode2.GameMode.BrainForComputer,
                        _playerAndGameMode2.GameMode.AddShip,
                        _playerAndGameMode2.GameMode.AddProtect,
                        null,
                        _playerAndGameMode2.GameMode.CurrentField.Size);

                    // почати встановлення обєктів
                    cmp.BeginSetShips();
                    cmp.BeginSetProtect();
                    return;
                }
            }

            this._playerAndGameMode2.Player.BeginSetShips();
            // не встановлювати захисти, коли класична гра
            //if (!(_playerAndGameMode2.GameMode is ClassicGameMode))
            {
                this._playerAndGameMode2.Player.BeginSetProtect();
            }
        }

        public void StartGame()
        {
            InitGameObjectsPlayer1();
            InitGameObjectsPlayer2();

            if (_playerAndGameMode1.GameMode.WasInitAllComponent & _playerAndGameMode2.GameMode.WasInitAllComponent)
            {
                BeginStartGame();
            }
            else
            {
                throw new ApplicationException("Can't start game because not all game objects was putting on field");
            }
        }

        private void BeginStartGame()
        {
            Gun gun = new Gun();

            Position pos;
            List<Type> resultShotList = new List<Type>();

            // повідомити зовнішній світ про початок гри
            GameWasStartedInfo();

            while (_playerAndGameMode1.IsLife & _playerAndGameMode2.IsLife)
            {
                // визначити чий хід
                if (_playerAndGameMode1.IsCurrent)
                {
                    // взяти координату
                    pos = _playerAndGameMode1.Player.GetPositionForAttack(gun, _playerAndGameMode1.GameMode.GunTypeList);

                    // коли не класична гра - взяти тип озброєння
                    if (!(_playerAndGameMode1.GameMode is ClassicGameMode))
                    {
                        gun.ChangeCurrentGun(IsChoisingGunTypeFunc());
                    }

                    //зробити постріл
                    resultShotList = _playerAndGameMode2.GameMode.AttackField(gun, pos);

                    // видалити зброю для даного Player2 - того, хто стріляв
                    _playerAndGameMode1.GameMode.RemoveGunFromList(gun);
                }
                else
                {
                    pos = _playerAndGameMode2.Player.GetPositionForAttack(gun, _playerAndGameMode2.GameMode.GunTypeList);

                    // якщо людина тоді дати запит про отримання типу зброї;
                    if (_playerAndGameMode2.Player is Man)
                    {
                        // коли не класична гра - взяти тип озброєння
                        if (!(_playerAndGameMode1.GameMode is ClassicGameMode))
                        {
                            gun.ChangeCurrentGun(IsChoisingGunTypeFunc());
                        }
                    }

                    //зробити постріл
                    resultShotList = _playerAndGameMode1.GameMode.AttackField(gun, pos);

                    // видалити зброю для даного Player1 - того, хто стріляв
                    _playerAndGameMode2.GameMode.RemoveGunFromList(gun);
                }
                // повідомити про здійснений постріл
                MakeShotInfo();

                // опрацювання рузультату пострілу
                Machining(resultShotList);
            }
            GameWasEndedIndo();
        }

        private Position GetPositionAttackForMan(Gun gun, IList<IDestroyable> gunList)
        {
            gun.ChangeCurrentGun(IsChoisingGunTypeFunc());

            return GetPositionFunc(_playerAndGameMode1.GameMode.CurrentField.Size);
        }

        private void Machining(List<Type> resultShotList)
        {
            foreach (var type in resultShotList)
            {
                if (type.IsSubclassOf(typeof(ShipBase)))
                {
                    return;
                }
            }
            SwapCurrentPlayer();
        }

        private void SwapCurrentPlayer()
        {
            if (_playerAndGameMode1.IsCurrent)
            {
                _playerAndGameMode1.IsCurrent = false;
                _playerAndGameMode2.IsCurrent = true;
            }
            else if (_playerAndGameMode2.IsCurrent)
            {
                _playerAndGameMode1.IsCurrent = true;
                _playerAndGameMode2.IsCurrent = false;
            }
        }

        #endregion BeginStartGame

        #region constructor

        public ClassicReferee(IGameMode gameMode, bool playerVSplayer)
        {
            // Створення певного режиму для кожного гравця
            Type gameModeType = gameMode.GetType();

            this._playerAndGameMode1 = new PlayerAndGameMode(gameModeType, "Player1",
                StartSetShipsForMan,
                StartSetProtectsForMan,
                GetPositionAttackForMan);

            this._playerAndGameMode1.IsCurrent = true;

            if (playerVSplayer)
            {
                this._playerAndGameMode2 = new PlayerAndGameMode(gameModeType, "Player2",
                    StartSetShipsForMan,
                    StartSetProtectsForMan,
                    GetPositionAttackForMan);
            }
            else
            {
                this._playerAndGameMode2 = new PlayerAndGameMode(gameModeType,
                    this._playerAndGameMode1.GameMode.CurrentFakeField);
            }
        }

        #endregion constructor
    }
}