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

            referre.WillPuttingShipsInfo += game.WillPuttingShipsInfo;
            referre.WillPuttingProtectsInfo += game.WillPuttingProtectsInfo;
            referre.WasShotActionInfo += game.WasShotActionInfo;
            referre.SettedSomeShipSuccesfulyInfo += game.SettedSomeShipSuccesfulyInfo;
            referre.SettedSomeProtectSuccesfulyInfo += game.SettedSomeProtectSuccesfulyInfo;
            referre.SettedAllShipsSuccesfulyInfo += game.SettedAllShipsSuccesfulyInfo;
            referre.SettedAllProtectsSuccesfulyInfo += game.SettedAllProtectsSuccesfulyInfo;
            referre.SetRandomAllGameObjectsOnField += game.SetRandomAllGameObjectsOnField;
            referre.MakeShotInfo += game.MakeShotInfo;
            referre.IsChoisingGunTypeFunc += game.IsChoisingGunTypeFunc;
            referre.GetPositionFunc += game.GetPositionFunc;
            referre.GameWasEndedIndo += game.GameEndedInfo;
            referre.GameWasStartedInfo += game.GameWasStartedInfo;
            referre.IsSettingShipsNowPlayerFunc += game.IsSettingShipsNowPlayerFunc;
            referre.IsSettingProtectsNowPlayerFunc += game.IsSettingProtectsNowPlayerFunc;

            referre.StartGame();

            Console.ReadLine();
        }
    }
}