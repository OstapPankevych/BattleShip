using System;
using BattleShip.ConsoleUI.Draw;
using BattleShip.ConsoleUI.Draw.DrawFields;
using BattleShip.GameEngine.Arsenal.Gun.Destroyable;
using BattleShip.GameEngine.Fields;
using BattleShip.GameEngine.Game.Referee;
using BattleShip.GameEngine.Location;



namespace BattleShip.ConsoleUI.ConsoleCore
{
    public class GameProcessHandler
    {
        private ClassicReferee _referee;

        private void ShowFields()
        {
            Console.Clear();
            DrawRegion.Draw(_referee.GetFieldOfPlayer1(), _referee.GetFieldOfPlayer2(), _referee.GetCurrentPlayerName(),
            BeginGame.classicGame);
            Console.Write('\n');
        }

        public GameProcessHandler(ClassicReferee referee)
        {
            this._referee = referee;
        }

        public Position GetPositionFunc(byte fieldSize)
        {
            ShowFields();

            Console.SetCursorPosition(0, fieldSize + 20);

            Console.Write("EnterPosition\n");
            return Get.GetPosition(fieldSize);
        }

        public Position[] GetPositionsShip(byte fieldSize)
        {
            Console.SetCursorPosition(0, fieldSize + 20);

            Position[] posArr = new Position[2];
            Console.Write("Enter begin position of ship\n");
            posArr[0] = Get.GetPosition(fieldSize);
            Console.Write("Enter end position of ship\n");
            posArr[1] = Get.GetPosition(fieldSize);

            return posArr;
        }

        public void WillPuttingShipsInfo()
        {
            ShowFields(); 

            Console.Write("You mast set ships on your game fields\n");
            Console.Write("Press any key to start...\n");
            Console.ReadLine();
        }

        public void WillPuttingProtectsInfo()
        {
            ShowFields();

            Console.Write("You mast set protect on your game fields\n");
            Console.Write("Press any key to start...\n");
            Console.ReadLine();
        }

        public Position[] IsSettingShipsNowPlayerFunc(Field field, string playerName)
        {
            Console.Clear();
            DrawField.Draw(2, 1, field, BeginGame.classicGame);
            Console.Write('\n');

            Console.SetCursorPosition(0, field.Size + 15);
            Console.Write(playerName + ":\n");

            Console.WriteLine("-->Please set ships\n");

            return GetPositionsShip(field.Size);
        }

        public Position IsSettingProtectsNowPlayerFunc(Field field, string playerName)
        {
            Console.Clear();
            DrawField.Draw(2, 1, field, BeginGame.classicGame);
            Console.Write('\n');

            Console.SetCursorPosition(0, field.Size + 15);
            Console.Write(playerName + ":\n");

            Console.WriteLine("-->Please set protects\n");
            Console.ReadLine();

            Console.Write("EnterPosition\n");
            return Get.GetPosition(field.Size);
        }

        public void SettedAllShipsSuccesfulyInfo()
        {
            ShowFields();

            Console.Write("All ships was setted on your game field\n");
            Console.Write("Press any key to continue...\n");
            Console.ReadLine();
            Console.WriteLine('\a');
        }

        public void SettedAllProtectsSuccesfulyInfo()
        {
            ShowFields();

            Console.Write("All protects was setted on your game field\n");
            Console.Write("Press any key to continue...\n");
            Console.ReadLine();
            Console.WriteLine('\a');
        }

        public void SettedSomeShipSuccesfulyInfo()
        {
            ShowFields();
            Console.WriteLine('\a');
        }

        public void SettedSomeProtectSuccesfulyInfo()
        {
            ShowFields();
            Console.WriteLine('\a');
        }

        public IDestroyable IsChoisingGunTypeFunc()
        {
            ShowFields();

            do
            {
                Console.Write("Please enter your gun to Shot\n");
                Console.Write('\n');

                int choice;
                if (_referee.CurrentPlayerGunList().Count == 0)
                {
                    Console.WriteLine('\a');
                    return new GunDestroy();
                }
                else
                {
                    int number = 0;
                    foreach (var gunType in _referee.CurrentPlayerGunList())
                    {
                        Console.Write(number + "   ");
                        Console.Write(gunType.GetType().Name);
                        number++;
                        Console.Write('\n');
                    }

                    Console.Write('\n');
                    Console.Write("your choice : (press 'enter' to miss this step)\n");
                    Console.Write("-->");
                    string res = Console.ReadLine();

                    Int32.TryParse(res, out choice);
                    if (res == "")
                    {
                        return new GunDestroy();
                    }

                    if (choice >= 0 & choice < _referee.CurrentPlayerGunList().Count)
                    {
                        Console.WriteLine('\a');
                        return _referee.CurrentPlayerGunList()[choice];
                    }

                }
            } while (true);
        }

        public void GameEndedInfo()
        {
            Console.WriteLine('\a');
            Console.WriteLine("****************************");
            Console.WriteLine("*********            *******");
            Console.WriteLine("********              ******");
            Console.WriteLine("**       Game ended       **");
            Console.WriteLine("*                          *");
            Console.WriteLine("Player {0}   WIN!!!", _referee.GetCurrentPlayerName());
        }

        public void WasShotActionInfo()
        {
            ShowFields();
        }

        public bool SetRandomAllGameObjectsOnField(string playerName)
        {
            ShowFields();

            do
            {
                Console.WriteLine("You can set all of your game objects arsenal automatically...");
                Console.WriteLine("->  Do it?   {yes(y) / no(n)}");
                string str = Console.ReadLine();
                if (str == "YES" || str == "Y" || str == "y" || str == "yes")
                {
                    Console.WriteLine('\a');
                    return true;
                }
                if (str == "NO" || str == "N" || str == "n" || str == "no")
                {
                    Console.WriteLine('\a');
                    return false;
                }
            } while (true);
        }

        public void GameWasStartedInfo()
        {
            for (int i = 0; i < 3; i++)
            {
                Console.Clear();
                Console.SetCursorPosition(20, 20);
                Console.WriteLine("Starting game...\a");
                System.Threading.Thread.Sleep(500 - i*150);
                Console.Clear();
                System.Threading.Thread.Sleep(500 - i*150);
            }
        }

        public void MakeShotInfo()
        {
            ShowFields();

            Console.WriteLine("**********  make Shot ***************");
            System.Threading.Thread.Sleep(200);
            Console.WriteLine('\a');
        }
    }
}
