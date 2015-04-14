using System;

using BattleShip.GameEngine.Location;
using BattleShip.GameEngine.Field.Cell;
using BattleShip.GameEngine.Field.Cell.StatusCell;
using BattleShip.GameEngine.Arsenal.Flot;
using BattleShip.GameEngine.Location.RulesOfSetPositions;
using BattleShip.GameEngine.Exceptions;

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



        public bool AddRectangleShip(ShipRectangleBase ship)
        {
            foreach (Position x in ship)
            {
                if (_cells[GetNumberCell(x)] is EmptyCell)
                {
                    _cells[GetNumberCell(x)].AddObject(ship);
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        private void SetAroundShipCell(ShipRectangleBase ship)
        {
            IRuleSetPosition rule;



        }
    }
}