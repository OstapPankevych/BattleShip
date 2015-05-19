using BattleShip.GameEngine.Arsenal.Flot;
using BattleShip.GameEngine.Arsenal.Gun;
using BattleShip.GameEngine.Arsenal.Protection;
using BattleShip.GameEngine.Location;
using System;
using System.Collections;
using System.Collections.Generic;
using BattleShip.GameEngine.Arsenal.Gun.Destroyable;
using BattleShip.GameEngine.Fields.Cells;
using BattleShip.GameEngine.Fields.Cells.StatusCell;
using BattleShip.GameEngine.ObjectOfGame;


namespace BattleShip.GameEngine.Fields
{
    public class Field : BaseField
    {
        #region Constructor

        public Field(byte size)
            : base(size)
        { }

        #endregion Constructor


        #region Public Methods

        public bool AddRectangleShip(ShipBase ship)
        {
            if (ChackRegion(ship))
            {
                // поставити кораблик і підписатись на дії з клітинкою
                foreach (var x in ship)
                {
                    this[x].AddGameObject(ship, true);
                }

                SetRegionAround(ship);

                return true;
            }

            return false;
        }

        public bool AddProtected(ProtectBase protect)
        {
            if (ChackRegion(protect))
            {
                // поставити обєкт захисту
                foreach (var x in protect)
                {
                    // додати об'єкт захисту на клітинки
                    this[x].AddGameObject(protect, true);
                }

                SetRegionAround(protect);

                SetProtectOnAllProtectionCells(protect);

                return true;
            }

            return false;
        }

        public List<Type> Shot(Gun gun, Position pos)
        {
            var attackResults = new List<Type>();

            foreach (var x in gun.Shot(pos, Size))
            {
                var result = this[x].Shot(gun, ref pos);

                if (result == typeof(ProtectedCell))
                {
                    // видалити захист(зробити використаним)
                    this[pos].OnDestroyMe(null);

                    return attackResults;
                }

                attackResults.Add(result);
            }

            return attackResults;
        }

        #endregion Public Methods


        #region Private methods

        private bool ChackRegion(IGameObject gameObject)
        {
            foreach (Position x in (IEnumerable<Position>)gameObject)
            {
                if (this[x].GetTypeOfCellObject() != typeof(EmptyCell))
                {
                    return false;
                }
            }

            return true;
        }

        private void SetRegionAround(IGameObject gameObject)
        {
            // Зробити поля кругом кораблика і підписати їх знищення при знищенні кораблика
            foreach (var x in (IEnumerable<Position>)gameObject)
            {
                for (var i = -1; i < 2; i++)
                {
                    for (var j = -1; j < 2; j++)
                    {
                        var pos = new Position((byte)(x.Line + i), (byte)(x.Column + j));
                        if (IsFieldRegion(pos.Line, pos.Column))
                        {
                            this[pos].AddGameObject(new AroundShip(pos), true);
                            if (gameObject is ShipBase)
                            {
                                ((ShipBase)gameObject).DeadHandler += this[pos].OnDestroyMe;
                            }
                            else if (gameObject is ProtectBase)
                            {
                                ((ProtectBase) gameObject).DeadHandler += this[pos].OnDestroyMe;
                            }
                        }
                    }
                }
            }
        }

        private void SetProtectOnAllProtectionCells(ProtectBase protect)
        {
            // поставити захисти на всі клітинки, які захищає цей об'єкт
            foreach (var x in protect.GetProtectedPositions())
            {
                // встановити захист на клітинку
                this[x].SecureCell(protect);

                protect.ProtectedHandler += this[x].OnCancelSecureHandler;
            }
        }

        #endregion Private methods
    }
}