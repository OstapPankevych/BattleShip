using BattleShip.GameEngine.Arsenal.Flot;
using BattleShip.GameEngine.Arsenal.Flot.RectangleShips;
using BattleShip.GameEngine.Arsenal.Gun;
using BattleShip.GameEngine.Arsenal.Gun.Destroyable;
using BattleShip.GameEngine.Arsenal.Protection;
using BattleShip.GameEngine.Field;
using BattleShip.GameEngine.Location;
using System;
using System.Collections.Generic;

namespace BattleShip.GameEngine.Game.GameMode
{
    internal class ClassicGameMode : GameMode
    {
        public ClassicGameMode()
        {
            currentFakeField = new FakeField(CurrentField);

            OneStoreyShipList.Capacity = 4;
            TwoStoreyShipList.Capacity = 3;
            ThreeStoreyShipList.Capacity = 2;
            FourStoreyShipList.Capacity = 1;

            protectList.Capacity = 1;

            gunTypesList.Add(new DoubleDestroy());
            gunTypesList.Add(new DoubleDestroy());
            gunTypesList.Add(new PlaneDestroy());
        }

        #region Playar's arsenal for classic game

        public List<OneStoreyRectangleShip> OneStoreyShipList = new List<OneStoreyRectangleShip>();
        public List<TwoStoreyRectangleShip> TwoStoreyShipList = new List<TwoStoreyRectangleShip>();
        public List<ThreeStoreyRectangleShip> ThreeStoreyShipList = new List<ThreeStoreyRectangleShip>();
        public List<FourStoreyRectangleShip> FourStoreyShipList = new List<FourStoreyRectangleShip>();

        #endregion Playar's arsenal for classic game

        #region Methods

        public List<Type> AttackMe(Gun myGun, Position position)
        {
            List<Type> result = currentField.Shot(myGun, position);

            // видалити зброю з арсеналу Player's
            foreach (var gunType in gunTypesList)
            {
                if (gunType.GetType() == myGun.GetTypeOfCurrentCun())
                {
                    gunTypesList.Remove(gunType);
                }
            }

            return result;
        }

        #region Override Methods

        public override bool AddShip(ShipBase ship)
        {
            if (ship is OneStoreyRectangleShip)
            {
                if (OneStoreyShipList.Count < OneStoreyShipList.Capacity)
                {
                    if (currentField.AddRectangleShip(ship))
                    {
                        OneStoreyShipList.Add((OneStoreyRectangleShip)ship);
                        shipList.Add(ship);
                        return true;
                    }

                    return false;
                }
            }
            else if (ship is TwoStoreyRectangleShip)
            {
                if (TwoStoreyShipList.Count < TwoStoreyShipList.Capacity)
                {
                    if (currentField.AddRectangleShip(ship))
                    {
                        TwoStoreyShipList.Add((TwoStoreyRectangleShip)ship);
                        shipList.Add(ship);
                        return true;
                    }

                    return false;
                }
            }
            else if (ship is ThreeStoreyRectangleShip)
            {
                if (ThreeStoreyShipList.Count < ThreeStoreyShipList.Capacity)
                {
                    if (currentField.AddRectangleShip(ship))
                    {
                        ThreeStoreyShipList.Add((ThreeStoreyRectangleShip)ship);
                        shipList.Add(ship);
                        return true;
                    }

                    return false;
                }
            }
            else if (ship is FourStoreyRectangleShip)
            {
                if (FourStoreyShipList.Count < FourStoreyShipList.Capacity)
                {
                    if (currentField.AddRectangleShip(ship))
                    {
                        FourStoreyShipList.Add((FourStoreyRectangleShip)ship);
                        shipList.Add(ship);
                        return true;
                    }

                    return false;
                }
            }

            return false;
        }

        public override bool AddProtect(ProtectBase protect)
        {
            if (protect is PVOProtect)
            {
                if (protectList.Count < protectList.Capacity)
                {
                    if (CurrentField.AddProtected(protect))
                    {
                        protectList.Add(protect);
                        return true;
                    }
                }
            }

            return false;
        }

        #endregion Override Methods

        #endregion Methods
    }
}