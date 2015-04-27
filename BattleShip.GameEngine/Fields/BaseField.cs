using BattleShip.GameEngine.Fields.Cells;
using BattleShip.GameEngine.Fields.Cells.StatusOfCells;
using BattleShip.GameEngine.Fields.Exceptions;
using BattleShip.GameEngine.Location;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BattleShip.GameEngine.Fields
{
    public abstract class BaseField : IEnumerable<Cell>
    {
        #region Private

        private Cell[] _cells;

        private byte _size;

        private void InitCells()
        {
            this._cells = new Cell[Size * Size];

            for (int i = 0; i < _cells.Length; i++)
            {
                this._cells[i] = new Cell(new Position((byte)(i / Size), (byte)(i % Size)));
            }
        }

        #endregion Private

        #region Public

        #region this[]

        public Cell this[int pos]
        {
            get
            {
                if ((pos < 0) || (pos > Size * Size))
                {
                    throw new OutOfFielRegionException("Get : BaseField.this[Position]");
                }

                return this[new Position((byte)(pos / Size), (byte)(pos % Size))];
            }

            private set
            {
                if ((pos < 0) || (pos > Size * Size))
                {
                    throw new OutOfFielRegionException("Set : BaseField.this[Position]");
                }

                this._cells[pos] = value;
            }
        }

        public Cell this[Position pos]
        {
            get
            {
                for (var i = 0; i < this._cells.Length; i++)
                {
                    if (this._cells[i].Location == pos)
                    {
                        return this._cells[i];
                    }
                }

                throw new OutOfFielRegionException("BaseField.this[byte]");
            }
        }

        #endregion this[]



        #region Static

        public static Position GetPositionForNumber(int number, byte fieldSize)
        {
            if (number < 0 || number > fieldSize * fieldSize)
            {
                throw new ApplicationException("Error region for this number");
            }
            else
            {
                return new Position((byte)(number / fieldSize), (byte)(number % fieldSize));
            }
        }

        #endregion Static

        #region Properties

        public byte Size
        {
            get { return this._size; }

            private set
            {
                if (value < 10)
                {
                    this._size = 10;
                }
                else
                {
                    this._size = value;
                }
            }
        }

        #endregion Properties

        #region Methods

        public BaseField(byte size)
        {
            this.Size = size;
            InitCells();
        }

        public static bool IsFielRegion(int line, int column, byte size)
        {
            if ((line < size & line >= 0) & (column < size & column >= 0))
            {
                return true;
            }

            return false;
        }

        public bool IsCellEmpty(Position position)
        {
            if (this[position].Show() == typeof(EmptyCell))
            {
                return true;
            }

            return false;
        }

        public bool WasCellAttack(Position position)
        {
            return this[position].WasAttacked;
        }

        #endregion Methods

        #endregion Public

        #region IEnumerable<CellOfField>

        public IEnumerator<Cell> GetEnumerator()
        {
            foreach (Cell cell in this._cells)
            {
                yield return cell;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion IEnumerable<CellOfField>
    }
}