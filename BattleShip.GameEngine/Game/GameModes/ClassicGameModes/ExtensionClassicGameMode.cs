using BattleShip.GameEngine.Arsenal.Gun.Destroyable;
using BattleShip.GameEngine.Arsenal.Protection;
using BattleShip.GameEngine.Game.Players.Computer.Brain;
using BattleShip.GameEngine.Game.Players.Computer.Brain.Play;
using BattleShip.GameEngine.Game.Players.Computer.Brain.SetObjects.SetProtect;
using BattleShip.GameEngine.Game.Players.Computer.Brain.SetObjects.SetRectangleShip;

namespace BattleShip.GameEngine.Game.GameModes.ClassicGameModes
{
    public class ExtensionClassicGameMode : ClassicGameMode
    {
        private void AddGunTypesToList()
        {
            base.gunList.Add(new PlaneDestroy());
            base.gunList.Add(new DoubleDestroy());
            base.gunList.Add(new DoubleDestroy());
        }

        protected PVOProtect pvo;

        protected ExtensionClassicGameMode(byte fieldSize)
            : base(fieldSize)
        {
            AddGunTypesToList();
        }

        public ExtensionClassicGameMode()
            : base(12)
        {
            AddGunTypesToList();
        }

        public override bool AddProtect(ProtectBase protect)
        {
            //Вкладені if
            //if (!(protect is PVOProtect)) return false;
            if (protect is PVOProtect)
            {
                //if (!currentField.AddProtected(protect)) return false;
                if (currentField.AddProtected(protect))
                {
                    pvo = (PVOProtect)protect;

                    CurrentCountProtectsOnField++;

                    return true;
                }
            }

            return false;
        }

        public override Brain BrainForComputer
        {
            get { return new Brain(new PlayNotMind(), new SetProtect(), new SetShip()); }
        }

        public override byte CountMaxShipsOnField
        {
            get { return 10; }
        }

        public override byte CountMaxProtectsOnField
        {
            get { return 1; }
        }
    }
}