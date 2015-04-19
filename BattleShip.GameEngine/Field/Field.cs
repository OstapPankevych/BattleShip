using BattleShip.GameEngine.Arsenal.Flot;
using BattleShip.GameEngine.Arsenal.Gun;
using BattleShip.GameEngine.Arsenal.Protection;
using BattleShip.GameEngine.Field.Cell.AttackResult;
using BattleShip.GameEngine.Field.Cell.StatusCell;
using BattleShip.GameEngine.Location;
using System;
using System.Collections.Generic;

namespace BattleShip.GameEngine.Field
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
            foreach (Position x in ship)
            {
                if (this[x].GetStatusCell() != typeof(EmptyCell))
                {
                    return false;
                }
            }

            // поставити кораблик і підписатись на дії з клітинкою
            foreach (var x in ship)
            {
                this[x].AddGameObject(ship, true);
            }

            // Зробити поля кругом кораблика і підписати їх знищення на знищення кораблика
            foreach (var x in ship)
            {
                for (var i = -1; i < 2; i++)
                {
                    for (var j = -1; j < 2; j++)
                    {
                        var pos = new Position((byte)(x.Line + i), (byte)(x.Column + j));
                        if (IsFielRegion(pos.Line, pos.Column, Size))
                        {
                            if (this[pos].GetStatusCell() == typeof(EmptyCell))
                                this[pos].AddStatus(new AroundShip(pos));
                            // Підписати знищення кілтинки при знищенні кораблика
                            ship.DeadHandler += this[pos].OnHitMeHandler;
                        }
                    }
                }
            }

            return true;
        }

        public bool AddProtected(ProtectBase protect)
        {
            foreach (var x in protect)
                if (!IsCellEmpty(x))
                    return false;

            // поставити обєкт захисту
            foreach (var x in protect)
            {
                // додати об'єкт захисту на клітинки
                this[x].AddGameObject(protect, true);
            }

            // поставити захисти на всі клітинки, які захищає цей об'єкт
            foreach (var x in protect.GetProtectedPositions())
            {
                // встановити захист на клітинку
                this[x].SetProtect(protect);

                // підписати protect на видалення захисту для кожної клітинки, при його знищенні
                protect.ProtectedHandler += this[x].OnRemoveProtection;
            }

            return true;
        }

        public List<Type> Shot(Gun gun, Position pos)
        {
            var attackResults = new List<Type>();

            foreach (var x in gun.Shot(pos, Size))
            {
                var result = this[x].Shot(gun);

                if (result == typeof(ProtectedCell))
                    return attackResults;

                attackResults.Add(result);
            }

            return attackResults;
        }

        #endregion Public Methods
    }
}