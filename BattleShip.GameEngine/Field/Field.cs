using System;

using BattleShip.GameEngine.Location;
using BattleShip.GameEngine.Field.Cell;
using BattleShip.GameEngine.Field.Cell.StatusCell;
using BattleShip.GameEngine.Arsenal.Flot;
using BattleShip.GameEngine.Location.RulesOfSetPositions;
using BattleShip.GameEngine.Exceptions;
using BattleShip.GameEngine.Arsenal.Protection;


namespace BattleShip.GameEngine.Field
{
    class Field
    {
        byte _size;

        public int Size
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
        private int GetNumberCell(Position position)
        {
            for (int i = 0; i < _cells.Length; i++)
                if (_cells[i].GetMyLocation() == position)
                    return i;

            throw new OutOfFielRegionException(this.ToString());
        }

        // отримати Position з _cells за порядковим номером
        private Position GetPosition(int number)
        {
            if (number > Size * Size)
                throw new ArgumentOutOfRangeException(this.ToString());

            return new Position((byte)(number/Size), (byte)(number % Size));
        }

        private CellOfField[] _cells;

        public bool IsFielRegion(int Line, int Column)
        {
            if ((Line <= _size & Line >= 0) & (Column <= _size & Column >= 0))
                return true;
            return false;
        }

        public bool IsCellEmpty(Position position)
        {
            CellOfField cell = _cells[GetNumberCell(position)];
            // Поставити тільки в тому випадку, якщо клітинка є пуста
            if (cell.GetStatusCell() != typeof(EmptyCell))
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
                cell.AddObject(ship);
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
                            cell.AddObject(new AroundShip(pos));
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

            // поставити захист
            foreach (Position x in protect)
            {
                CellOfField cell = _cells[GetNumberCell(x)];
                cell.AddObject(protect);
                // Підписати
                cell.DeadHandler += protect.OnHitMeHandler;
            }

            Position[] pos = protect.GetProtectedPositions();
            // підписати решту клітинок на захист
            for (int i = 0; i < pos.Length; i++ )
            {
                CellOfField cell = _cells[GetNumberCell(pos[i])];
                cell.AddProtected(protect.GetType());
                protect.RemoveProtectHandler += cell.
            }
            return true;
        }
    }
}