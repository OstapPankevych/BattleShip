using System;
using System.Collections;
using System.Collections.Generic;
using BattleShip.GameEngine.Arsenal.Flot;
using BattleShip.GameEngine.Arsenal.Gun;
using BattleShip.GameEngine.Arsenal.Protection;
using BattleShip.GameEngine.Exceptions;
using BattleShip.GameEngine.Field.Cell;
using BattleShip.GameEngine.Field.Cell.AttackResult;
using BattleShip.GameEngine.Field.Cell.StatusCell;
using BattleShip.GameEngine.Location;

namespace BattleShip.GameEngine.Field
{
    public class Field : IEnumerable<CellOfField>
    {
        private CellOfField[] _cells;
        private byte _size;

        public Field(byte size)
        {
            Size = size;
            InitCells();
        }

        public byte Size
        {
            get { return _size; }

            private set
            {
                if (value < 10)
                {
                    _size = 10;
                }
                else
                {
                    _size = value;
                }
            }
        }

        public IEnumerator<CellOfField> GetEnumerator()
        {
            foreach (var x in _cells)
                yield return x;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void InitCells()
        {
            _cells = new CellOfField[_size*_size];
            for (var i = 0; i < _cells.Length; i++)
                _cells[i] = new CellOfField(GetPosition(i));
        }

        // отримати номер клітинки з _cells за Position
        private int GetNumberCell(Position position)
        {
            for (var i = 0; i < _cells.Length; i++)
                if (_cells[i].Location == position)
                    return i;

            throw new OutOfFielRegionException("Field.GetNumberCell()");
        }

        // отримати Position з _cells за порядковим номером
        private Position GetPosition(int number)
        {
            if (number > Size*Size)
                throw new ArgumentOutOfRangeException("Field.GetPosition()");

            return new Position((byte) (number/Size), (byte) (number%Size));
        }

        public bool IsFielRegion(int Line, int Column)
        {
            if ((Line < _size & Line >= 0) & (Column < _size & Column >= 0))
                return true;
            return false;
        }

        public bool IsCellEmpty(Position position)
        {
            var cell = _cells[GetNumberCell(position)];
            // Поставити тільки в тому випадку, якщо клітинка є пуста
            if (cell.Show() != typeof (EmptyCell))
                return false;
            return true;
        }

        public bool AddRectangleShip(ShipRectangleBase ship)
        {
            foreach (var x in ship)
                if (!IsCellEmpty(x))
                    return false;

            // поставити кораблик і підписатись на дії з клітинкою
            foreach (var x in ship)
            {
                var cell = _cells[GetNumberCell(x)];
                cell.AddGameObject(ship, true);
            }

            // Зробити поля кругом кораблика і підписати їх знищення на знищення кораблика
            foreach (var x in ship)
            {
                CellOfField cell;
                for (var i = -1; i < 2; i++)
                {
                    for (var j = -1; j < 2; j++)
                    {
                        var pos = new Position((byte) (x.Line + i), (byte) (x.Column + j));
                        if (IsFielRegion(pos.Line, pos.Column))
                        {
                            cell = _cells[GetNumberCell(pos)];
                            if (cell.GetStatusCell() != typeof (EmptyCell))
                                cell.AddStatus(new AroundShip(pos));
                            // Підписати знищення кілтинки при знищенні кораблика
                            ship.DeadHandler += cell.OnHitMeHandler;
                        }
                    }
                }
            }

            return true;
        }

        public bool AddProtected(ProtectedBase protect)
        {
            foreach (var x in protect)
                if (!IsCellEmpty(x))
                    return false;

            // поставити обєкт захисту
            foreach (var x in protect)
            {
                var cell = _cells[GetNumberCell(x)];

                // додати об'єкт захисту на клітинки
                cell.AddGameObject(protect, true);
            }

            // поставити захисти на всі клітинки, які захищає цей об'єкт
            foreach (var x in protect.GetProtectedPositions())
            {
                var cell = _cells[GetNumberCell(x)];

                // встановити захист на клітинку
                cell.SetProtect(protect);

                // підписати protect на видалення захисту для кожної клітинки, при його знищенні
                protect.ProtectedHandler += cell.OnRemoveProtection;
            }

            return true;
        }

        public List<Type> Shot(Gun gun, params Position[] positions)
        {
            var attackResults = new List<Type>();

            foreach (var x in positions)
            {
                var result = _cells[GetNumberCell(x)].Shot(gun);

                if (result == typeof (ProtectedCell))
                    return attackResults;

                attackResults.Add(result);
            }

            return attackResults;
        }
    }
}