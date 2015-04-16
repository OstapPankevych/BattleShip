using System;
using System.Collections.Generic;


using BattleShip.GameEngine.Location;
using BattleShip.GameEngine.Field.Cell;
using BattleShip.GameEngine.Field.Cell.StatusCell;
using BattleShip.GameEngine.Arsenal.Flot;
using BattleShip.GameEngine.Location.RulesOfSetPositions;
using BattleShip.GameEngine.Exceptions;
using BattleShip.GameEngine.Arsenal.Protection;
using BattleShip.GameEngine.Arsenal.Gun;

namespace BattleShip.GameEngine.Field
{
    public class Field : IEnumerable<CellOfField>
    {
        byte _size;

        public byte Size
        {
            get
            {
                return _size;
            }

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

        public Field(byte size)
        {
            Size = size;
            InitCells();
        }

        public void InitCells()
        {
            _cells = new CellOfField[_size * _size];
            for (int i = 0; i < _cells.Length; i++)
                _cells[i] = new CellOfField(GetPosition(i));
        }

        // отримати номер клітинки з _cells за Position
        int GetNumberCell(Position position)
        {
            for (int i = 0; i < _cells.Length; i++)
                if (_cells[i].Location == position)
                    return i;

            throw new OutOfFielRegionException(this.ToString());
        }

        // отримати Position з _cells за порядковим номером
        Position GetPosition(int number)
        {
            if (number > Size * Size)
                throw new ArgumentOutOfRangeException("Field.GetPosition()");

            return new Position((byte)(number/Size), (byte)(number % Size));
        }

        private CellOfField[] _cells;

        public bool IsFielRegion(int Line, int Column)
        {
            if ((Line < _size & Line >= 0) & (Column < _size & Column >= 0))
                return true;
            return false;
        }

        public bool IsCellEmpty(Position position)
        {
            CellOfField cell = _cells[GetNumberCell(position)];
            // Поставити тільки в тому випадку, якщо клітинка є пуста
            if (cell.Show() != typeof(EmptyCell))
                return false;    
            return true;
        }

        public bool AddRectangleShip(ShipRectangleBase ship)
        {
            foreach (Position x in ship)
                if (!IsCellEmpty(x))
                    return false;

            // поставити кораблик і підписатись на дії з клітинкою
            foreach (Position x in ship)
            {
                CellOfField cell = _cells[GetNumberCell(x)];
                cell.AddShip(ship);
                // Підписати
                cell.DeadHandler += ship.OnHitMeHandler;
            }

            // Зробити поля кругом кораблика і підписати їх знищення на знищення кораблика
            foreach (Position x in ship)
            {
                CellOfField cell;
                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        Position pos = new Position((byte)(x.Line + i), (byte)(x.Column + j));
                        if (IsFielRegion(pos.Line, pos.Column))
                        {
                            cell = _cells[GetNumberCell(pos)];
                            if (cell.GetStatusCell() != typeof(EmptyCell))
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
            foreach (Position x in protect)
                if (!IsCellEmpty(x))
                    return false;

            // поставити обєкт захисту
            foreach (Position x in protect)
            {
                CellOfField cell = _cells[GetNumberCell(x)];

                // додати об'єкт захисту на клітинки
                cell.AddProtect(protect);
            }

            // поставити захисти на всі клітинки, які захищає цей об'єкт
            foreach (Position x in protect.GetProtectedPositions())
            {
                CellOfField cell = _cells[GetNumberCell(x)];

                // встановити захист на клітинку
                cell.SetProtect(protect);

                // підписати protect на видалення захисту для кожної клітинки, при його знищенні
                protect.ProtectedHandler += cell.OnRemoveProtection;
            }
            
            return true;
        }

        public IEnumerator<CellOfField> GetEnumerator()
        {
            foreach (var x in _cells)
                yield return x;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (IEnumerator<CellOfField>)GetEnumerator();
        }

        public List<Type> Shot(Gun gun, params Position[] positions)
        {
            List<Type> attackResults = new List<Type>();

            foreach (Position x in positions)
            {
                Type result = _cells[GetNumberCell(x)].Shot(gun);

                if (result == typeof(Cell.AttackResult.ProtectedCell))
                    return attackResults;

                attackResults.Add(result);
            }

            return attackResults;
        }
    }
}