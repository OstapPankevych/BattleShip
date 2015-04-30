using System;
using BattleShip.GameEngine.Game.GameModes.ClassicGameModes;
using BattleShip.GameEngine.Game.Referee;


namespace BattleShip.ConsoleUI.ConsoleCore
{
    static class BeginGame
    {
        public static bool classicGame = true;
        public static void StartMenuEnterGameMode(out ClassicReferee cl)
        {
            Console.WriteLine("****************************************");
            Console.WriteLine("**********BattleShip Game***************");
            Console.WriteLine();
            Console.WriteLine("choice your game mode:");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("-> classic BattleShip [0]");
            Console.WriteLine();
            Console.WriteLine("-> extension classic BattleShip [1]");
            Console.WriteLine();
            do
            {
                Console.WriteLine("enter your choice {0/1}");
                string str = Console.ReadLine();
                int choice;
                Int32.TryParse(str, out choice);
                if (choice == 0)
                {
                    cl = new ClassicReferee(new ClassicGameMode(), StartMenuEnterTypeGame());
                    return;
                }
                if (choice == 1)
                {
                    cl = new ClassicReferee(new ExtensionClassicGameMode(), StartMenuEnterTypeGame());
                    classicGame = false;
                    return;
                }
            } while (true);
            
        }

        private static bool StartMenuEnterTypeGame()
        {
            Console.WriteLine("****************************************");
            Console.WriteLine("**********BattleShip Game***************");
            Console.WriteLine();
            Console.WriteLine("choice your simplePlayer or multiPlayer:");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("-> player VS player BattleShip [0]");
            Console.WriteLine();
            Console.WriteLine("-> player VS computer BattleShip [1]");
            Console.WriteLine();
            do
            {
                Console.WriteLine("enter your choice {0/1}");
                string str = Console.ReadLine();
                int choice;
                Int32.TryParse(str, out choice);
                if (choice == 0)
                {
                    return true;
                }
                if (choice == 1)
                {
                    return false;
                }
            } while (true);
        }
    }
}
