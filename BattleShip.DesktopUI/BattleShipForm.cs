using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BattleShip.DesktopUI.InfoPanel;
using BattleShip.DesktopUI.Core;
using BattleShip.GameEngine.Arsenal.Gun.Destroyable;
using BattleShip.GameEngine.Game.GameModes.ClassicGameModes;
using BattleShip.GameEngine.Game.Referee;
using BattleShip.GameEngine.Location;
using System.Threading.Tasks;
using BattleShip.DesktopUI.Field;
using BattleShip.GameEngine.Arsenal.Gun;

namespace BattleShip.DesktopUI
{
    public partial class BattleShipForm : Form
    {
        public BattleShipForm()
        {
            InitializeComponent();
        }


        #region Private 

        private readonly FontFamily _fm = new FontFamily("Segoe Print");
        private readonly StringFormat _sf = new StringFormat(StringFormatFlags.LineLimit);

        private SimpleMenuText _title;
        private SimpleMenuText _menuChoiseTitle;

        private SimpleMenuText _helpMsg;

        private ActiveMenuText _choice1;
        private ActiveMenuText _choice2;
        private ActiveMenuText _help;
        private ActiveMenuText _startGame;
        private ActiveMenuText _ok;

        private const byte _sizeOneCellPxls = 32;

        private GameSettings _settingsGame = new GameSettings();

        private ActiveMenuText _next;
        private ActiveMenuText _back;

        private ClassicReferee _referee;

        #endregion Private


        #region Private Methods

        private void BattleShipForm_Load(object sender, EventArgs e)
        {
            ucPanel.Init(_sizeOneCellPxls);

            ucField2.Location = new Point(ucField1.Width, 0);

            ucField1.InitNewField("", new GameEngine.Fields.Field(12), _sizeOneCellPxls);
            ucField2.InitNewField("", new GameEngine.Fields.Field(12), _sizeOneCellPxls);

            ucField2.Location = new Point(ucField1.Width, 0);

            Width = ucField2.Width + ucField2.Location.X + 20;
            Height = ucField1.Height + _sizeOneCellPxls * 5;
            StartGame();
        }

        private void InitRefereree()
        {
            ucPanel.DeleteAllData();

            _referee = new ClassicReferee(_settingsGame.GameMode, _settingsGame.PlayerVSPlayer());

            _referee.PrePuttingShipHandler += SetShipMsg;
            _referee.PrePuttingProtectHandler += SetProtectMsg;
            _referee.WasShotActionHandler += WasShotActionOnHandler;
            _referee.SomeShipSuccesfulySettedHandler += SomeShipSuccesfulySettedOnHandler;
            _referee.SomeProtectSuccesfulySettedHandler += SomeProtectSuccesfulySettedOnHandler;
            _referee.AllShipsSuccesfulySettedHandler += AllShipsSuccesfulySettedOnHandler;
            _referee.AllProtectsSuccesfulySettedHandler += AllProtectsSuccesfulySettedOnHandler;
            _referee.SetRandomAllGameObjectsOnFieldQuertyHandler += SetRandomAllGameObjectsOnFieldQuertyOnHandler;
            _referee.MakeShotHandler += MakeShotOnHandler;
            _referee.ChoisingGunTypeHandler += ChoisingGunTypeOnHandler;
            _referee.GetPositionHandler += GetPositionOnHandler;
            _referee.GameWasEndedHandler += GameWasEndedOnHandler;
            _referee.GameWasStartedHandler += GameWasStartedOnHandler;
            _referee.PuttingShipHandler += PuttingShipOnHandler;
            _referee.PuttingProtectHandler += PuttingProtectOnHandler;
            _referee.ComputerSettedAllGameObjectsSuccesfuly += ComputerSettedAllGameObjectsSuccesfulyOnHandler;
            _referee.PlayerSettedAllGameObjectsSuccessfuly += ComputerSettedAllGameObjectsSuccesfulyOnHandler;
        }

        private void StartGame()
        {
            _settingsGame.SetGameMode(new ClassicGameMode());
            _settingsGame.SetPlayerVS(PlayerVS.Player);

            // ініціалізувіти текст для меню
            InitTextForMenu();

            // заховати ігрові поля
            ucField1.Visible = false;
            ucField2.Visible = false;

            ucPanel.ClearAllText();
            ucPanel.DeleteAllData();

            ucField1.Enabled = true;
            ucField2.Enabled = true;

            // показати меню1
            ShowMenu1();
        }

        private void OnStartGame()
        {
            _referee = new ClassicReferee(_settingsGame.GameMode, _settingsGame.PlayerVSPlayer());
            InitRefereree();
            (new Task(() => { _referee.StartGame(); })).Start();
        }

        private Point GetFormPositionFromCellPosition(byte lineCell, byte columnCell)
        {
            return new Point(columnCell * _sizeOneCellPxls, lineCell * _sizeOneCellPxls);
        }

        #endregion Private Methods


        #region Menu

        private void InitTextForMenu()
        {
            _title = new SimpleMenuText("BattleShip", Color.BlueViolet, new Font(_fm, 60), GetFormPositionFromCellPosition(0, 7));

            _help = new ActiveMenuText("{ help }", Color.DarkOrange, new Font(_fm, 21), GetFormPositionFromCellPosition(4, 22), new Pen(Color.White));
        }

        private void ShowMenu1()
        {
            ucField1.Visible = false;
            ucField2.Visible = false;
            ucField1.Enabled = true;
            ucField2.Enabled = true;

            ucPanel.ClearAllText();
            ucPanel.DeleteAllData();

            ucPanel.Refresh();

            PrintMenu("please choice your game mode:", "Classic Game", "Extension Game");

            // "next" print
            _next = new ActiveMenuText("next >", Color.Blue, new Font(_fm, 36), GetFormPositionFromCellPosition(13, 19), new Pen(Color.WhiteSmoke));
            _next.ChangeCurrentActiveStatus += OnMenu2StartGameClick;
            ucPanel.WriteActiveText(_next);

            _help.ChangeCurrentActiveStatus += OnHelpMenu1Ckick;

            _choice1.ChangeCurrentActiveStatus += OnChangeCurrentActiveStatusClassicGame;
            _choice2.ChangeCurrentActiveStatus += OnChangeCurrentActiveStatusExtensionGame;

            _ok.ChangeCurrentActiveStatus += ShowMenu1;

            //instruction text
            Color color = Color.Red;
            Font font = new Font(_fm, 14);

            SimpleMenuText instructionText = new SimpleMenuText("Instruction for choice:", color, font, GetFormPositionFromCellPosition(13, 1));
            ucPanel.WriteText(instructionText);

            instructionText = new SimpleMenuText(" 1) move mouse cursor over the text ", color, font, GetFormPositionFromCellPosition(14, 1));
            ucPanel.WriteText(instructionText);

            instructionText = new SimpleMenuText(" 2) you will see green rim around this text", color, font, GetFormPositionFromCellPosition(15, 1));
            ucPanel.WriteText(instructionText);

            instructionText = new SimpleMenuText(" 3) click mouse button for choice", color, font, GetFormPositionFromCellPosition(16, 1));
            ucPanel.WriteText(instructionText);
        }

        private void ShowMenu2()
        {
            PrintMenu("please choice Player/Multiplayer", "Player VS Player", "Player VS Computer");

            // "next" print
            _next = new ActiveMenuText("start >", Color.Blue, new Font(_fm, 36), GetFormPositionFromCellPosition(13, 18), new Pen(Color.WhiteSmoke));
            _next.ChangeCurrentActiveStatus += OnStartGame;
            ucPanel.WriteActiveText(_next);

            // "back" print
            _back = new ActiveMenuText("< back", Color.Blue, new Font(_fm, 30), GetFormPositionFromCellPosition(14, 2), new Pen(Color.WhiteSmoke));
            _back.ChangeCurrentActiveStatus += OnMenu1NextClick;
            ucPanel.WriteActiveText(_back);

            _help.ChangeCurrentActiveStatus += OnHelpMenu2Click;

            _choice1.ChangeCurrentActiveStatus += OnChangeCurrentActiveStatusPlayerVsPlayer;
            _choice2.ChangeCurrentActiveStatus += OnChangeCurrentActiveStatusPlayerVsComputer;

            _ok.ChangeCurrentActiveStatus += ShowMenu2;
        }

        private void OnMenu2StartGameClick()
        {
            ShowMenu2();
        }

        private void OnMenu1NextClick()
        {
            ShowMenu1();
        }

        private void OnHelpMenu1Ckick()
        {
            PrintHelpPanelTitles();

            AddTextToHelpMessage("'Classic Game' - it's classic game batlle ship.", 7, 1);
            AddTextToHelpMessage("'Extension Game' - it's game with 'Classic Game' ships,", 9, 1);
            AddTextToHelpMessage(" three type of gun:", 10, 9);
            AddTextToHelpMessage("- plane destroy { count = 1 }", 11, 13);
            AddTextToHelpMessage("- double destroy { count = 2 }", 12, 13);
            AddTextToHelpMessage("- simple gun { count =  ∞ }", 13, 13);
            AddTextToHelpMessage(" and one protection of 'plane destroy'", 14, 9);
            AddTextToHelpMessage(" gun type.", 15, 9);

            if (_help.IsSetted)
            {
                _help.ChangeSettedStatus();
            }
        }

        private void PrintHelpPanelTitles()
        {
            // очистити панель
            ucPanel.ClearAllText();
            ucPanel.DeleteAllData();

            // заголовок гри
            ucPanel.WriteText(_title);
            ucPanel.WriteText(new SimpleMenuText("Info:", Color.Red, new Font(_fm, 40), GetFormPositionFromCellPosition(4, 4))); 
            ucPanel.WriteText(new SimpleMenuText("{ press 'ok!' to continue }", Color.Crimson, new Font(_fm, 16), GetFormPositionFromCellPosition(5, 10)));

            ucPanel.WriteActiveText(_ok);
        }

        private void AddTextToHelpMessage(string msg, byte line, byte column)
        {
            ucPanel.WriteText(new SimpleMenuText(msg, Color.Black, new Font(_fm, 20), GetFormPositionFromCellPosition(line, column))); 
        }

        private void OnHelpMenu2Click()
        {
            PrintHelpPanelTitles();

            AddTextToHelpMessage("'Player VS Player' - it's game mode where two people can", 7, 1);
            AddTextToHelpMessage("play with each other.", 8, 10);
            AddTextToHelpMessage("'Player VS Computer' - it's game mode where one person", 10, 1);
            AddTextToHelpMessage(" can play with computer.", 11, 11);

            if (_help.IsSetted)
            {
                _help.ChangeSettedStatus();
            }
        }

        private void PrintMenu(string task, string choice1, string choice2)
        {
            _menuChoiseTitle = new SimpleMenuText(task, Color.Red, new Font(_fm, 26), GetFormPositionFromCellPosition(4, 2));

            Pen pen = new Pen(Color.OrangeRed, 3);

            _choice1 = new ActiveMenuText(choice1, Color.Black, new Font(_fm, 30), GetFormPositionFromCellPosition(7, 5), pen, true);
            _choice2 = new ActiveMenuText(choice2, Color.Black, new Font(_fm, 30), GetFormPositionFromCellPosition(10, 5), pen, false);

            _ok = new ActiveMenuText("ok!", Color.OrangeRed, new Font(_fm, 30), GetFormPositionFromCellPosition(4, 20), pen);

            // очистити панель
            ucPanel.ClearAllText();
            ucPanel.DeleteAllData();

            // заголовок гри
            ucPanel.WriteText(_title);

            // help
            ucPanel.WriteActiveText(_help);

            // info про вибір
            ucPanel.WriteText(_menuChoiseTitle);

            // варіанти гри
            ucPanel.WriteActiveText(_choice1);
            ucPanel.WriteActiveText(_choice2);
        }


        #region Game Settings

        private void SetGameMode()
        {
            if (IsSettedChoice1())
            {
                _settingsGame.SetGameMode(new ClassicGameMode());
            }
            else
            {
                _settingsGame.SetGameMode(new ExtensionClassicGameMode());
            }
        }

        private void SetPlayerVs()
        {
            if (IsSettedChoice1())
            {
                _settingsGame.SetPlayerVS(PlayerVS.Player);
            }
            else
            {
                _settingsGame.SetPlayerVS(PlayerVS.Computer);
            }
        }

        private bool IsSettedChoice1()
        {
            if (_choice1.IsSetted & !(_choice2.IsSetted))
            {
                return true;
            }
            else if (!(_choice1.IsSetted) & _choice2.IsSetted)
            {
                return false;
            }
            else
            {
                throw new InvalidOperationException("both of choice can't be true of false");
            }
        }

        private void OnChangeCurrentActiveStatusExtensionGame()
        {
            _choice1.OnChangeCurrentActiveStatus();

            SetGameMode();
        }

        private void OnChangeCurrentActiveStatusClassicGame()
        {
            _choice2.OnChangeCurrentActiveStatus();

            SetGameMode();
        }

        private void OnChangeCurrentActiveStatusPlayerVsPlayer()
        {
            _choice2.OnChangeCurrentActiveStatus();

            SetPlayerVs();
        }

        private void OnChangeCurrentActiveStatusPlayerVsComputer()
        {
            _choice1.OnChangeCurrentActiveStatus();

            SetPlayerVs();
        }

        #endregion Game Settings


        #endregion Menu


        #region OnRefereeHandlers 

        private bool SetRandomAllGameObjectsOnFieldQuertyOnHandler(string playerName)
        {
            SimpleMenuText playerNameTest = new SimpleMenuText(playerName + " :", Color.OrangeRed, new Font(_fm, 40), GetFormPositionFromCellPosition(4, 1));
            SimpleMenuText text1 = new SimpleMenuText("> you can set all of your game objects", Color.Black, new Font(_fm, 26), GetFormPositionFromCellPosition(7, 2));
            SimpleMenuText text2 = new SimpleMenuText("   automaticaly, do it?", Color.Black, new Font(_fm, 26), GetFormPositionFromCellPosition(9, 2));
            ActiveMenuText yesText = new ActiveMenuText("YES", Color.Green, new Font(_fm, 60), GetFormPositionFromCellPosition(11, 5), new Pen(Color.White));
            ActiveMenuText noText = new ActiveMenuText("NO", Color.Green, new Font(_fm, 60), GetFormPositionFromCellPosition(11, 16), new Pen(Color.White));

            yesText.ChangeCurrentActiveStatus += OnYesClick;
            noText.ChangeCurrentActiveStatus += OnNoClick;

            Invoke(new Action(() =>
            {
                ucField1.Visible = false;

                ucPanel.ClearAllText();
                ucPanel.DeleteAllData();

                ucPanel.WriteText(_title);
                ucPanel.WriteText(playerNameTest);
                ucPanel.WriteText(text1);
                ucPanel.WriteText(text2);
                ucPanel.WriteActiveText(yesText);
                ucPanel.WriteActiveText(noText);
                ucPanel.Refresh();
            }));

            _isBlockPuttingGameObjects = true;

            while (_isBlockPuttingGameObjects) ;

            return _yes;
        }

        private bool _yes = false;
        private void OnYesClick()
        {
            _isBlockPuttingGameObjects = false;
            _yes = true;
        }

        private void OnNoClick()
        {
            _isBlockPuttingGameObjects = false;
            _yes = false;
        }

        private void WasShotActionOnHandler()
        {
            Invoke(new Action(() =>
            {
                ucField1.RefreshField();
                ucField2.RefreshField();

                ucPanel.Refresh();
            }));
        }

        private void SomeShipSuccesfulySettedOnHandler(byte countStorey)
        {
            ucField1.ShipSettedSuccessfuly(countStorey);
        }

        private void SomeProtectSuccesfulySettedOnHandler()
        {
            
        }

        private void AllShipsSuccesfulySettedOnHandler()
        {
            PrintSuccessfulySettedSomeTypeOfGameObjects("-> all ships setted successfuly");
        }

        private void AllProtectsSuccesfulySettedOnHandler()
        {
            PrintSuccessfulySettedSomeTypeOfGameObjects("-> all protects setted successfuly");
        }

        private void MakeShotOnHandler()
        {
            ucField1.CurrentGun.ChangeCurrentGun(new GunDestroy());
            ucField2.CurrentGun.ChangeCurrentGun(new GunDestroy());
        }

        private IDestroyable ChoisingGunTypeOnHandler()
        {
            if (_referee.CurrentPlayerGunList().Count == 0)
            {
                return new GunDestroy();
            }
            else
            {
                if (_referee.IsPlayer1Current())
                {
                    return ucField2.CurrentGun.GetDestroyable();
                }
                else
                {
                    return ucField1.CurrentGun.GetDestroyable();
                }
            }
        }


        private Position GetPositionOnHandler(byte fieldSize)
        {
            if (_referee.IsPlayer1Current())
            {
                Invoke(new Action(() =>
                {
                    ucField1.Enabled = false;
                    ucField2.Enabled = true;
                }));
            }
            else
            {
                Invoke(new Action(() =>
                {
                    ucField2.Enabled = false;
                    ucField1.Enabled = true;
                }));
            }

            if (_settingsGame.GameMode.GetType().IsAssignableFrom(typeof(ClassicGameMode)))
            {
                Invoke(new Action(() =>
                {
                    PrintHelpMessage(_referee.GetCurrentPlayerName(), "-> choice position for shot", true);

                    WasShotActionOnHandler();
                }));
            }
            else
            {
                Invoke(new Action(() =>
                {
                    ucPanel.ClearAllText();
                    ucPanel.DeleteAllData();

                    Color color = Color.Red;
                    Font font = new Font(_fm, 22);

                    byte line = 15;
                    byte column = 1;

                    SimpleMenuText playerNameText = new SimpleMenuText(_referee.GetCurrentPlayerName() + " : ", color, font, GetFormPositionFromCellPosition(15, 1));

                    color = Color.Black;
                    font = new Font(_fm, 20);

                    line++;

                    _helpMsg = new SimpleMenuText("your guns: ", color, font, GetFormPositionFromCellPosition(++line, column));

                    ucPanel.WriteText(playerNameText);
                    ucPanel.WriteText(_helpMsg);

                    color = Color.Blue;
                    font = new Font(_fm, 14);

                    line = 16;
                    column += 6;

                    Action<IDestroyable> GetGunField = (IDestroyable gun) =>
                    {
                        if (_referee.IsPlayer1Current())
                        {
                            ucField2.CurrentGun.ChangeCurrentGun(gun);
                        }
                        else
                        {
                            ucField1.CurrentGun.ChangeCurrentGun(gun);
                        }

                        _referee.SetCurrentGun(gun);
                    };

                    GetGunField(new GunDestroy());

                    ActiveMenuText simpleGun = new ActiveMenuText("simpleGun", color, font, GetFormPositionFromCellPosition(line, column), new Pen(color, 2), true);

                    ActiveMenuText planeDestroy = new ActiveMenuText("planeDestroy", color, font, GetFormPositionFromCellPosition(line, (byte)(column + 6)), new Pen(color, 2));
                    ActiveMenuText doubleDestroy = new ActiveMenuText("doubleDestroy", color, font, GetFormPositionFromCellPosition(line, (byte)(column + 2 * 6)), new Pen(color, 2));

                    simpleGun.ChangeCurrentActiveStatus += () =>
                    {
                        if (doubleDestroy.IsSetted)
                        {
                            doubleDestroy.OnChangeCurrentActiveStatus();
                        }

                        if (planeDestroy.IsSetted)
                        {
                            planeDestroy.OnChangeCurrentActiveStatus();
                        }

                        if (!simpleGun.IsSetted)
                        {
                            simpleGun.OnChangeCurrentActiveStatus();
                        }
                        GetGunField(new GunDestroy());
                    };

                    ucPanel.WriteActiveText(simpleGun);
                    ucPanel.WriteText(new SimpleMenuText("{count = ∞}", color, font, GetFormPositionFromCellPosition((byte)(line + 1), column)));

                    byte countPlaneDestroy = 0;
                    byte countDoubleDestroy = 0;

                    for (int i = 0; i < _referee.CurrentPlayerGunList().Count; i++)
                    {
                        if (_referee.CurrentPlayerGunList()[i] is PlaneDestroy)
                        {
                            countPlaneDestroy++;
                        }
                        else if (_referee.CurrentPlayerGunList()[i] is DoubleDestroy)
                        {
                            countDoubleDestroy++;
                        }
                    }

                    column += 6;

                    if (countPlaneDestroy != 0)
                    {
                        ucPanel.WriteActiveText(planeDestroy);

                        planeDestroy.ChangeCurrentActiveStatus += () =>
                        {
                            if (simpleGun.IsSetted)
                            {
                                simpleGun.OnChangeCurrentActiveStatus();
                            }

                            if (doubleDestroy.IsSetted)
                            {
                                doubleDestroy.OnChangeCurrentActiveStatus();
                            }

                            if (!planeDestroy.IsSetted)
                            {
                                planeDestroy.OnChangeCurrentActiveStatus();
                            }

                            GetGunField(new PlaneDestroy());
                        };
                    }
                    else
                    {
                        ucPanel.WriteText(planeDestroy);
                    }
                    ucPanel.WriteText(new SimpleMenuText("{count = " + countPlaneDestroy + "}", color, font, GetFormPositionFromCellPosition((byte)(line + 1), column)));

                    column += 6;

                    doubleDestroy = new ActiveMenuText("doubleDestroy", color, font, GetFormPositionFromCellPosition(line, column), new Pen(color, 2));
                    if (countDoubleDestroy != 0)
                    {
                        ucPanel.WriteActiveText(doubleDestroy);

                        doubleDestroy.ChangeCurrentActiveStatus += () =>
                        {
                            if (simpleGun.IsSetted)
                            {
                                simpleGun.OnChangeCurrentActiveStatus();
                            }
                            if (planeDestroy.IsSetted)
                            {
                                planeDestroy.OnChangeCurrentActiveStatus();
                            }

                            if (!doubleDestroy.IsSetted)
                            {
                                doubleDestroy.OnChangeCurrentActiveStatus();
                            }

                            GetGunField(new DoubleDestroy());
                        };
                    }
                    else
                    {
                        ucPanel.WriteText(doubleDestroy);
                    }
                    ucPanel.WriteText(new SimpleMenuText("{count = " + countDoubleDestroy + "}", color, font, GetFormPositionFromCellPosition((byte)(line + 1), column)));

                    WasShotActionOnHandler();
                }));
            }


            if (_referee.IsPlayer1Current())
            {
                while (!ucField2.StatusReadyToRead) ;

                return ucField2.GetPositions()[0];                
            }
            else
            {
                while (!ucField1.StatusReadyToRead) ;

                return ucField1.GetPositions()[0];
            }
        }

        private void GameWasEndedOnHandler()
        {
            ActiveMenuText newGame = new ActiveMenuText("NewGame", Color.Green, new Font(_fm, 20), GetFormPositionFromCellPosition((byte)((Height - 4 * _sizeOneCellPxls) / _sizeOneCellPxls), 20), new Pen(Color.Beige));
            newGame.ChangeCurrentActiveStatus += StartGame;

            Invoke(new Action(() =>
            {
                ucPanel.ClearAllText();
                ucPanel.DeleteAllData();

                ucField1.InitNewField(_referee.GetPlayer1Name(), _referee.GetFieldOfPlayer1(), _sizeOneCellPxls);
                ucField2.InitNewField(_referee.GetPlayer2Name(), _referee.GetFieldOfPlayer2(), _sizeOneCellPxls);

                ucField1.RefreshField();
                ucField2.RefreshField();

                ucField1.Enabled = false;
                ucField2.Enabled = false;

                PrintHelpMessage(_referee.GetCurrentPlayerName() + "  WIN ! ", "click 'NewGame' for new game");

                ucPanel.WriteActiveText(newGame);

                ucPanel.Refresh();
            }));            
        }

        private void GameWasStartedOnHandler()
        {
            Invoke(new Action(() =>
                {
                    ucPanel.ClearAllText();
                    ucPanel.DeleteAllData();

                    ucField1.ResetStatusReadyToRead();
                    ucField2.ResetStatusReadyToRead();

                    ucField1.InitNewField(_referee.GetPlayer1Name(), _referee.GetFieldOfPlayer1(), _sizeOneCellPxls);
                    ucField2.InitNewField(_referee.GetPlayer2Name(), _referee.GetFieldOfPlayer2(), _sizeOneCellPxls);

                    ucField2.Visible = true;

                    ucField2.GameProccesStep = StepGameProcces.Play;
                    ucField1.GameProccesStep = StepGameProcces.Play;
                }));
        }

        private void SetShipMsg()
        {
            Invoke(new Action(() =>
            {
                ucPanel.ClearAllText();
                ucPanel.DeleteAllData();

                PrintHelpMessage(_referee.GetCurrentPlayerName(), "-> please set your ships on field");

                PrintSetShipsInstruction();

                _availableShipsDictionary.Clear();

                // доступні кораблики
                _availableShipsDictionary.Add(1, 4);
                _availableShipsDictionary.Add(2, 3);
                _availableShipsDictionary.Add(3, 2);
                _availableShipsDictionary.Add(4, 1);
            }));
        }

        private void SetProtectMsg()
        {
            Invoke(new Action(() =>
            {
                ucPanel.ClearAllText();
                ucPanel.DeleteAllData();   

                PrintSetProtectInstruction();
            }));
        }

        private Dictionary<byte, byte> _availableShipsDictionary = new Dictionary<byte, byte>();

        private Position[] PuttingShipOnHandler(BattleShip.GameEngine.Fields.Field field, string playerName)
        {
            Invoke(new Action(() =>
            {
                ucField1.InitNewField(playerName, field);

                ucField1.ResetStatusReadyToRead();

                ucField1.GameProccesStep = StepGameProcces.SettingShip;

                ucField1.AvailableShipsDictionary = _availableShipsDictionary;

                ucField1.Visible = true;
            }));

            while (!ucField1.StatusReadyToRead) ;

            return ucField1.GetPositions();
        }

        private Position PuttingProtectOnHandler(BattleShip.GameEngine.Fields.Field field, string playerName)
        {
            Invoke(new Action(() =>
            {
                ucField1.InitNewField(playerName, field);

                ucField1.ResetStatusReadyToRead();

                ucField1.GameProccesStep = StepGameProcces.SettingProtect;
            }));

            while (!ucField1.StatusReadyToRead) ;

            return ucField1.GetPositions()[0];
        }

        private void ComputerSettedAllGameObjectsSuccesfulyOnHandler()
        {
            PrintSuccessfulySettedSomeTypeOfGameObjects("-> all of your game objects setted successfuly");
        }


        #region Helpers for _refereeHandlers

        private void PrintHelpMessage(string playerName, string messageText, bool clearAllText = false)
        {
            Color color = Color.Red;
            Font font = new Font(_fm, 26);

            byte fieldSize = _referee.GetFieldOfPlayer1().Size;
            SimpleMenuText playerNameText = new SimpleMenuText(playerName + " : ", color, font, GetFormPositionFromCellPosition(15, 1));

            color = Color.Black;
            font = new Font(_fm, 20);

            _helpMsg = new SimpleMenuText(messageText, color, font, GetFormPositionFromCellPosition(17, 2));
            Invoke(new Action(() =>
            {
                if (clearAllText)
                {
                    ucPanel.DeleteAllData();
                    ucPanel.ClearAllText();
                }
                ucPanel.WriteText(_helpMsg);
                ucPanel.WriteText(playerNameText);
            }));
        }

        // після натискання ОК - продовжити роботу програми
        bool _isBlockPuttingGameObjects = false;
        private void OnOkClick()
        {
            _isBlockPuttingGameObjects = false;
        }

        private void PrintSuccessfulySettedSomeTypeOfGameObjects(string message)
        {
            Invoke(new Action(() =>
            {
                ucPanel.ClearAllText();
                ucPanel.DeleteAllData();
            }));

            PrintHelpMessage(_referee.GetCurrentPlayerName(), message);

            PrintSuccessfulActiveMessage();

            Invoke(new Action(() =>
            {
                ucField1.InitNewField(_referee.GetCurrentPlayerName(), _referee.GetFieldOfCurrentPlayer());
                ucField1.Visible = true;

                ucField1.OffCursorOnField();
            }));

            // почекати, доки користувач не натиснить ОК
            _isBlockPuttingGameObjects = true;

            Invoke(new Action(() => { ucPanel.Refresh(); }));

            while (_isBlockPuttingGameObjects) ;
        }

        private void PrintSuccessfulActiveMessage()
        {
            Color color = Color.Red;
            Font font = new Font(_fm, 24);

            _ok = new ActiveMenuText("ok!", Color.Chartreuse, new Font(_fm, 50), new Point(_sizeOneCellPxls * 18, 6 * _sizeOneCellPxls), new Pen(Color.White));

            _ok.ChangeCurrentActiveStatus += OnOkClick;

            Invoke(new Action(() => { ucPanel.WriteActiveText(_ok); }));

            _helpMsg = new SimpleMenuText("click \"ok!\" for continue", color, font, new Point(_sizeOneCellPxls * 15, 4 * _sizeOneCellPxls));
            Invoke(new Action(() => { ucPanel.WriteText(_helpMsg); }));
        }

        private void PrintSetShipsInstruction()
        {
            byte line = 1;
            byte column = 14;

            SimpleMenuText title = new SimpleMenuText("instruction for ships set:", Color.Red, new Font(_fm, 20), GetFormPositionFromCellPosition(line, column));
            ucPanel.WriteText(title);

            Font font = new Font(_fm, 14);

            Color color = Color.CornflowerBlue;

            Action<string, byte, byte> PrintMsgInfo = (string msg, byte l, byte c) =>
            {
                SimpleMenuText text = new SimpleMenuText(msg, color, font,
                    GetFormPositionFromCellPosition(l, c));

                ucPanel.WriteText(text);
            };

            line++;

            PrintMsgInfo(" 1) Press left mouse button for choice", ++line, ++column);
            PrintMsgInfo("    begin position of ship;", ++line, column);

            PrintMsgInfo(" 2) Move cursor horizontal or vertical", ++line, column);
            PrintMsgInfo("    for choice second position of ship;", ++line, column);

            PrintMsgInfo(" 3) Hang of left mouse button on the", ++line, column);
            PrintMsgInfo("    second position  for create the ship,", ++line, column);
            PrintMsgInfo("    or click right mouse button for cancel", ++line, column);
            PrintMsgInfo("    create this ship;", ++line, column);

            ++line;

            color = Color.Green;

            ucPanel.DrawCellOfShip(++line, column, Color.LightGreen);
            ucPanel.DrawCellOfShip(line, (byte)(column + 1), Color.LightGreen);
            PrintMsgInfo(" - available type of ship", line, (byte)(column + 2));

            line++;

            color = Color.IndianRed;

            ucPanel.DrawCellOfShip(++line, column, Color.Red);
            ucPanel.DrawCellOfShip(line, (byte)(column + 1), Color.Red);
            PrintMsgInfo(" - don't available type of ship", line, (byte)(column + 2));

            ucPanel.Refresh();
        }

        private void PrintSetProtectInstruction()
        {
            ucField1.Visible = true;

            ucPanel.ClearAllText();
            ucPanel.DeleteAllData();

            PrintHelpMessage(_referee.GetCurrentPlayerName(), "-> please set your protect on field");

            byte line = 1;
            byte column = 14;

            SimpleMenuText title = new SimpleMenuText("instruction for protect set:", Color.Red, new Font(_fm, 20), GetFormPositionFromCellPosition(line, column));
            ucPanel.WriteText(title);

            Font font = new Font(_fm, 14);

            Color color = Color.CornflowerBlue;

            Action<string, byte, byte> PrintMsgInfo = (string msg, byte l, byte c) =>
            {
                SimpleMenuText text = new SimpleMenuText(msg, color, font,
                    GetFormPositionFromCellPosition(l, c));

                ucPanel.WriteText(text);
            };

            line++;

            PrintMsgInfo(" 1) Move mouse cursor on field region;", ++line, ++column);

            PrintMsgInfo(" 2) You will see a highlighted cell to", ++line, column);
            PrintMsgInfo("    be reserved;", ++line, column);

            PrintMsgInfo(" 3) Click mouse button for set protect;", ++line, column);

            line++;

            title = new SimpleMenuText("for more info about protect click 'help' ", Color.Blue, new Font(_fm, 16), GetFormPositionFromCellPosition((byte)(line + 3), (byte)(column - 1)));
            ucPanel.WriteText(title);

            _help = new ActiveMenuText("help", Color.DarkGoldenrod, new Font(_fm, 36), GetFormPositionFromCellPosition((byte)(line + 4), (byte)(column + 3)), new Pen(Color.White), false);

            _help.ChangeCurrentActiveStatus += OnHelpExtensionGameInfo;

            ucPanel.WriteActiveText(_help);

            _ok = new ActiveMenuText("ok!", Color.OrangeRed, new Font(_fm, 30), GetFormPositionFromCellPosition(4, 20), new Pen(Color.White));

            _ok.ChangeCurrentActiveStatus += PrintSetProtectInstruction;

            ucPanel.Refresh();
        }

        private void OnHelpExtensionGameInfo()
        {
            ucPanel.ClearAllText();
            ucPanel.DeleteAllData();

            ucField1.Visible = false;
            ucField2.Visible = false;

            if (_help.IsSetted)
            {
                _help.ChangeSettedStatus();
            }

            PrintHelpPanelTitles();

            byte line = 8;
            const byte column = 1;
            
            AddTextToHelpMessage("'double destroy' - destroy two cell", line, column);
            AddTextToHelpMessage("'plane destroy' - destroy one horizontal line", ++line, column);
            AddTextToHelpMessage("'simple destroy' - destroy one cell", ++line, column);

            line++;

            AddTextToHelpMessage("'protect' - protects against 'plane destroy' one horizontal line;", ++line, column);

            ucPanel.Refresh();
        }

        #endregion Helpres for _refereeHandlers

        #endregion OnRefereeHandlers
    }
}
