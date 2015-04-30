using BattleShip.GameEngine.Arsenal.Flot;
using BattleShip.GameEngine.Arsenal.Flot.RectangleShips;
using BattleShip.GameEngine.Arsenal.Protection;
using BattleShip.GameEngine.Game.Players.Computer.Brain;
using BattleShip.GameEngine.Game.Players.Computer.Brain.Play;
using BattleShip.GameEngine.Game.Players.Computer.Brain.SetObjects.SetProtect;
using BattleShip.GameEngine.Game.Players.Computer.Brain.SetObjects.SetRectangleShip;
using System.Collections.Generic;

namespace BattleShip.GameEngine.Game.GameModes.ClassicGameModes
{
    public class ClassicGameMode : BaseGameMode
    {
        private ClassicGameMode(Fields.Field field)
            : base(field)
        { }

<<<<<<< HEAD
        //питання: як тестив приватні методи? чи можна їх зробити публічними?
=======
>>>>>>> adcb4d49f57b1a9c51a12f9f9099df7db01d1a0d
        private bool AddShip<T>(List<T> shipList, T ship) where T : ShipBase
        {
            if (shipList.Count < shipList.Capacity)
            {
                if (currentField.AddRectangleShip(ship))
                {
                    shipList.Add(ship);
                    CurrentCountShipsOnField++;

                    return true;
                }
            }

            return false;
        }

        private bool LifeStatusForShip<T>(List<T> shipList) where T : ShipBase
        {
            foreach (var ship in shipList)
            {
                if (ship.IsLife)
                {
                    return true;
                }
            }

            return false;
        }

        protected List<OneStoreyRectangleShip> oneStoreyShipList = new List<OneStoreyRectangleShip>(4);
        protected List<TwoStoreyRectangleShip> twoStoreyShipList = new List<TwoStoreyRectangleShip>(3);
        protected List<ThreeStoreyRectangleShip> threeStoreyShipList = new List<ThreeStoreyRectangleShip>(2);
        protected List<FourStoreyRectangleShip> fourStoreyShipList = new List<FourStoreyRectangleShip>(1);

        protected ClassicGameMode(byte fieldSize)
            : this(new Fields.Field(fieldSize))
        { }

        public ClassicGameMode()
            : this(new Fields.Field(10))
        { }

        public override bool AddShip(ShipBase ship)
        {
            if (!WasInitAllComponent)
            {
                if (ship is OneStoreyRectangleShip)
                {
                    return AddShip<OneStoreyRectangleShip>(oneStoreyShipList, (OneStoreyRectangleShip)ship);
                }
                else if (ship is TwoStoreyRectangleShip)
                {
                    return AddShip<TwoStoreyRectangleShip>(twoStoreyShipList, (TwoStoreyRectangleShip)ship);
                }
                else if (ship is ThreeStoreyRectangleShip)
                {
                    return AddShip<ThreeStoreyRectangleShip>(threeStoreyShipList, (ThreeStoreyRectangleShip)ship);
                }
                else if (ship is FourStoreyRectangleShip)
                {
                    return AddShip<FourStoreyRectangleShip>(fourStoreyShipList, (FourStoreyRectangleShip)ship);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // мозок для компютера для цього типу гри
        public override Brain BrainForComputer
        {
            get { return new Brain(new PlayNotMind(), new NotSetProtect(), new SetShip()); }
        }

        public override byte CountMaxShipsOnField
        {
            get { return 10; }
        }

        public override byte CountMaxProtectsOnField
        {
            get { return 0; }
        }

        public override bool IsLife
        {
            get
            {
<<<<<<< HEAD
                //return LifeStatusForShip(oneStoreyShipList) || LifeStatusForShip(twoStoreyShipList) ||
                //          LifeStatusForShip(threeStoreyShipList) || LifeStatusForShip(fourStoreyShipList);
=======
>>>>>>> adcb4d49f57b1a9c51a12f9f9099df7db01d1a0d
                if (LifeStatusForShip(oneStoreyShipList) || LifeStatusForShip(twoStoreyShipList) ||
                    LifeStatusForShip(threeStoreyShipList) || LifeStatusForShip(fourStoreyShipList))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public override bool AddProtect(ProtectBase protect)
        {
            return false;
        }
    }
}