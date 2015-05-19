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
using BattleShip.GameEngine.Game.Players.Man;


namespace BattleShip.GameEngine.Game.Referee
{
    public class PlayerAndGameMode
    {
        // чи є об'єктом, який зараз робить дію
        public bool IsCurrent { get; set; }

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
            Action<ManPlayer> StartSetShips,
            Action<ManPlayer> StartSetProtects,
            Func<Gun, IList<IDestroyable>, Position> GetPositionForAttack)
            : this(gameModeType)
        {
            this.Player = new ManPlayer(name, this.GameMode.CurrentFakeField.Size,
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

    public class ClassicReferee
    {
        #region Private 

        private readonly PlayerAndGameMode _playerAndGameMode1;
        private readonly PlayerAndGameMode _playerAndGameMode2;

        private readonly Gun _gun = new Gun();

        private bool _gameWasEnded = false;

        #endregion Private


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

        public void StartSetProtectsForMan(ManPlayer player)
        {
            // Ідентифікувати гравця:
            PlayerAndGameMode playerAndGameMode = (ReferenceEquals(_playerAndGameMode1.Player, player))
                ? _playerAndGameMode1
                : _playerAndGameMode2;

            PuttingProtectsProcess(playerAndGameMode);
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

        public void StartGame()
        {
            InitGameObjectsPlayer1();

            SwapCurrentPlayer();
            InitGameObjectsPlayer2();

            SwapCurrentPlayer();

            if (_playerAndGameMode1.GameMode.WasInitAllComponent & _playerAndGameMode2.GameMode.WasInitAllComponent)
            {
                BeginStartGame();
            }
            else
            {
                throw new ApplicationException("Can't start game because not all game objects was putting on field");
            }
        }

        public BaseField GetFieldOfPlayer1()
        {
            // коли гра типу Player VS Player - показувати фейкове поле
            if (this._playerAndGameMode2.Player is ManPlayer & !_gameWasEnded)
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
            //return _playerAndGameMode2.GameMode.CurrentField;
            if (!_gameWasEnded)
            {
                return this._playerAndGameMode2.GameMode.CurrentFakeField;
            }
            else
            {
                return _playerAndGameMode2.GameMode.CurrentField;
            }
        }

        public BaseField GetFieldOfCurrentPlayer()
        {
            if (_playerAndGameMode1.IsCurrent)
            {
                return _playerAndGameMode1.GameMode.CurrentField;
            }
            else
            {
                return _playerAndGameMode2.GameMode.CurrentField;
            }
        }

        public bool IsPlayer1Current()
        {
            if (_playerAndGameMode1.IsCurrent)
            {
                return true;
            }

            return false;
        }

        public string GetPlayer1Name()
        {
            return _playerAndGameMode1.Player.Name;
        }

        public string GetPlayer2Name()
        {
            return _playerAndGameMode2.Player.Name;
        }

        #endregion public methods


        #region Events

        // івент отримання позиції(точки) від зовнішнього світу
        public event Func<byte, Position> GetPositionHandler;

        // івент про те, що зараз буде процес розтавлення кораблика
        public event Action PrePuttingShipHandler;

        // івент про те, що зараз буде процес розставлення захисту
        public event Action PrePuttingProtectHandler;

        // івент про те, що зараз встановлення корабликів
        public event Func<Fields.Field, string, Position[]> PuttingShipHandler;

        // івент про те, що зараз встановленн захистів
        public event Func<Fields.Field, string, Position> PuttingProtectHandler;

        // івент про успішне встановлення всіх ігрових об'єктів людиною
        public event Action PlayerSettedAllGameObjectsSuccessfuly = delegate { }; 

        // івент про те, що всі корабликі розставлені
        public event Action AllShipsSuccesfulySettedHandler;

        // івент про те, зо всі захисти розтавленні
        public event Action AllProtectsSuccesfulySettedHandler;

        // івент про те, що успішно поставився кораблик
        public event Action<byte> SomeShipSuccesfulySettedHandler;

        // івент про те, що успішно поставився захист
        public event Action SomeProtectSuccesfulySettedHandler;

        // івент про те, що потрібно вибрати тип зброї
        public event Func<IDestroyable> ChoisingGunTypeHandler;

        // івент про те, що відбувся постріл
        public event Action WasShotActionHandler;

        // івент отримання запиту чи розставляти кораблики і захисти для користувача рендомом
        public event Func<string, bool> SetRandomAllGameObjectsOnFieldQuertyHandler;

        // івент про початок гри
        public event Action GameWasStartedHandler;

        // івент про закінчення гри
        public event Action GameWasEndedHandler;

        // івент про те, що потрібно зробити постріл гравцю
        public event Action MakeShotHandler;

        public event Action ComputerSettedAllGameObjectsSuccesfuly = delegate { }; 

        #endregion Events


        #region Putting process object on field

        private void StartSetShipsForMan(ManPlayer player)
        {
            // Ідентифікувати гравця:
            PlayerAndGameMode playerAndGameMode = (ReferenceEquals(_playerAndGameMode1.Player, player))
                ? _playerAndGameMode1
                : _playerAndGameMode2;

            PuttingShipsProcess(playerAndGameMode);
        }
   
        private ShipBase FabricShips(PlayerAndGameMode playerAndGameMode, byte countStorey, Position begin, Position end)
        {
            Position[] shipRegion = Ractangle.GetRectangleRegion(countStorey, begin, end);

            switch (shipRegion.Length)
            {
                case 1:
                {
                    return new OneStoreyRectangleShip(playerAndGameMode.GameMode.CurrentCountShipsOnField,
                        shipRegion[0]);
                }
                case 2:
                {
                    return new TwoStoreyRectangleShip(playerAndGameMode.GameMode.CurrentCountShipsOnField,
                        shipRegion[0], shipRegion[1]);
                }
                case 3:
                {
                    return new ThreeStoreyRectangleShip(playerAndGameMode.GameMode.CurrentCountShipsOnField,
                        shipRegion[0], shipRegion[1], shipRegion[2]);
                }
                case 4:
                {
                    return new FourStoreyRectangleShip(playerAndGameMode.GameMode.CurrentCountShipsOnField,
                        shipRegion[0], shipRegion[1], shipRegion[2], shipRegion[3]);
                }
                default:
                {
                    return null;
                }

            }
        }

        private void PuttingShipsProcess(PlayerAndGameMode playerAndGameMode)
        {
            // запустити івент про те, що зараз буде йти процес встановлення корабликів на поле.
            PrePuttingShipHandler();

            while (playerAndGameMode.GameMode.CurrentCountShipsOnField <
                   playerAndGameMode.GameMode.CountMaxShipsOnField)
            {
                Position[] positions =
                    PuttingShipHandler(playerAndGameMode.GameMode.CurrentField, playerAndGameMode.Player.Name);

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
                    if (playerAndGameMode.GameMode.AddShip(FabricShips(playerAndGameMode, countStorey, begin, end)))
                    {
                        // запустити івент про успішне додавання кораблика на поле
                        SomeShipSuccesfulySettedHandler(countStorey);
                    }
                }
            }

            // Запустити івент про завершення додавань всіх корабликів на поле
            AllShipsSuccesfulySettedHandler();
        }

        private void PuttingProtectsProcess(PlayerAndGameMode playerAndGameMode)
        {
            // запустити івент про те, що зараз буде процес встановлення захистів на поле.
            PrePuttingProtectHandler();

            while (playerAndGameMode.GameMode.CurrentCountProtectsOnField <
                   playerAndGameMode.GameMode.CountMaxProtectsOnField)
            {
                Position position =
                    PuttingProtectHandler(playerAndGameMode.GameMode.CurrentField, playerAndGameMode.Player.Name);

                if (playerAndGameMode.GameMode.AddProtect(
                    new Pvo(playerAndGameMode.GameMode.CurrentCountProtectsOnField,
                            position, playerAndGameMode.GameMode.CurrentField.Size)))
                {
                    // запустити івент про успішне додавання захисту на поле
                    SomeProtectSuccesfulySettedHandler();
                }
            }

            // Запустити івент про завершення додавань всіх захистів
            AllProtectsSuccesfulySettedHandler();
        }

        #endregion Putting process object on field


        #region BeginStartGame

        private void InitGameObjectsPlayer1()
        {
            // Дати запит про розставляння корабликів і захистів рендомом
            if (SetRandomAllGameObjectsOnFieldQuertyHandler(_playerAndGameMode1.Player.Name))
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

                ComputerSettedAllGameObjectsSuccesfuly();
            }
            else
            {
                _playerAndGameMode1.Player.BeginSetShips();
                
                // встановити захисти для некласичної гри
                if (_playerAndGameMode1.GameMode is ExtensionClassicGameMode)
                {
                    _playerAndGameMode1.Player.BeginSetProtect();
                }

                PlayerSettedAllGameObjectsSuccessfuly();
            }
        }

        private void InitGameObjectsPlayer2()
        {
            // Player2 - або комп, або людина. тому робимо перевірку хто він
            if (_playerAndGameMode2.Player is ManPlayer)
            {
                // Дати запит про розставляння корабликів і захистів рендомом
                if (SetRandomAllGameObjectsOnFieldQuertyHandler(_playerAndGameMode2.Player.Name))
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

                    ComputerSettedAllGameObjectsSuccesfuly();

                    return;
                }
            }

            _playerAndGameMode2.Player.BeginSetShips();

            // встановити захисти для некласичної гри
            if (_playerAndGameMode2.GameMode is ExtensionClassicGameMode)
            {
                _playerAndGameMode2.Player.BeginSetProtect();
            }

            if (_playerAndGameMode2.Player is ManPlayer)
            {
                PlayerSettedAllGameObjectsSuccessfuly();
            }
        }

        public void SetCurrentGun(IDestroyable gunType)
        {
            _gun.ChangeCurrentGun(gunType);
        }
        private void BeginStartGame()
        {
            

            Position pos;
            List<Type> resultShotList = new List<Type>();

            // повідомити зовнішній світ про початок гри
            GameWasStartedHandler();

            while (_playerAndGameMode1.IsLife & _playerAndGameMode2.IsLife)
            {
                // визначити чий хід
                if (_playerAndGameMode1.IsCurrent)
                {
                    // взяти координату
                    pos = _playerAndGameMode1.Player.GetPositionForAttack(_gun, _playerAndGameMode1.GameMode.GunTypeList);

                    // коли не класична гра - взяти тип озброєння
                    if (!(_playerAndGameMode1.GameMode is ClassicGameMode))
                    {
                        _gun.ChangeCurrentGun(ChoisingGunTypeHandler());
                    }

                    //зробити постріл
                    resultShotList = _playerAndGameMode2.GameMode.AttackField(_gun, pos);

                    // видалити зброю для даного Player2 - того, хто стріляв
                    _playerAndGameMode1.GameMode.RemoveGunFromList(_gun);
                }
                else
                {
                    pos = _playerAndGameMode2.Player.GetPositionForAttack(_gun, _playerAndGameMode2.GameMode.GunTypeList);

                    // якщо людина тоді дати запит про отримання типу зброї;
                    if (_playerAndGameMode2.Player is ManPlayer)
                    {
                        // коли не класична гра - взяти тип озброєння
                        if (!(_playerAndGameMode1.GameMode is ClassicGameMode))
                        {
                            _gun.ChangeCurrentGun(ChoisingGunTypeHandler());
                        }
                    }

                    //зробити постріл
                    resultShotList = _playerAndGameMode1.GameMode.AttackField(_gun, pos);

                    // видалити зброю для даного Player1 - того, хто стріляв
                    _playerAndGameMode2.GameMode.RemoveGunFromList(_gun);
                }
                // повідомити про здійснений постріл
                MakeShotHandler();

                // опрацювання рузультату пострілу
                Machining(resultShotList);
            }

            _gameWasEnded = true; 

            GameWasEndedHandler();
        }

        private Position GetPositionAttackForMan(Gun gun, IList<IDestroyable> gunList)
        {
            gun.ChangeCurrentGun(ChoisingGunTypeHandler());

            return GetPositionHandler(_playerAndGameMode1.GameMode.CurrentField.Size);
        }

        private void Machining(List<Type> resultShotList)
        {
            foreach (var type in resultShotList)
            {
                // стріляна клітинка - повертається NULL
                if (type == null)
                {
                    if (resultShotList.Count == 1)
                    {
                        return;
                    }
                    continue;
                }
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

            this._playerAndGameMode1 = new PlayerAndGameMode(gameModeType, "Player 1",
                StartSetShipsForMan,
                StartSetProtectsForMan,
                GetPositionAttackForMan);

            this._playerAndGameMode1.IsCurrent = true;

            if (playerVSplayer)
            {
                this._playerAndGameMode2 = new PlayerAndGameMode(gameModeType, "Player 2",
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