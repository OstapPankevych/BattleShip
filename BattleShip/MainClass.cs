using System;
using BattleShip.ConsoleUI.ConsoleCore;
using BattleShip.GameEngine.Game.Referee;

namespace BattleShip.ConsoleUI
{
    public class MainClass
    {
        public static void Main(string[] args)
        {
            ClassicReferee referre;

            BeginGame.StartMenuEnterGameMode(out referre);

            // привязати всі функції callBack

            GameProcessHandler game = new GameProcessHandler(referre);

            referre.PrePuttingShipHandler += game.WillPuttingShipsInfo;
            referre.PrePuttingProtectHandler += game.WillPuttingProtectsInfo;
            referre.WasShotActionHandler += game.WasShotActionInfo;
            referre.SomeShipSuccesfulySettedHandler += game.SettedSomeShipSuccesfulyInfo;
            referre.SomeProtectSuccesfulySettedHandler += game.SettedSomeProtectSuccesfulyInfo;
            referre.AllShipsSuccesfulySettedHandler += game.SettedAllShipsSuccesfulyInfo;
            referre.AllProtectsSuccesfulySettedHandler += game.SettedAllProtectsSuccesfulyInfo;
            referre.SetRandomAllGameObjectsOnFieldQuertyHandler += game.SetRandomAllGameObjectsOnField;
            referre.MakeShotHandler += game.MakeShotInfo;
            referre.ChoisingGunTypeHandler += game.IsChoisingGunTypeFunc;
            referre.GetPositionHandler += game.GetPositionFunc;
            referre.GameWasEndedHandler += game.GameEndedInfo;
            referre.GameWasStartedHandler += game.GameWasStartedInfo;
            referre.PuttingShipHandler += game.IsSettingShipsNowPlayerFunc;
            referre.PuttingProtectHandler += game.IsSettingProtectsNowPlayerFunc;

            referre.StartGame();

            Console.ReadLine();
        }
    }
}