using BattleShip.GameEngine.Game.GameModes;

namespace BattleShip.DesktopUI.Core
{
    internal enum PlayerVS
    {
        Player,
        Computer
    };

    struct GameSettings
    {
        public IGameMode GameMode { get; private set; }
        public PlayerVS PlayerVs { get; private set; }

        public bool PlayerVSPlayer()
        {
            if (PlayerVs == PlayerVS.Player)
            {
                return true;
            }

            return false;
        }
        public void SetPlayerVS(PlayerVS playerVS)
        {
            PlayerVs = playerVS;
        }

        public void SetGameMode(IGameMode gameMode)
        {
            GameMode = gameMode;
        }
    }
}
