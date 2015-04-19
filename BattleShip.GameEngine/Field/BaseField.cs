using BattleShip.GameEngine.Field.Cell;
using BattleShip.GameEngine.Field.Cell.StatusCell;
using BattleShip.GameEngine.Field.Exceptions;
using BattleShip.GameEngine.Location;
using System.Collections;
using System.Collections.Generic;

namespace BattleShip.GameEngine.Field
{
    public abstract class BaseField : IEnumerable<CellOfField>
    {
        #region Protected Members

        protected CellOfField[] _cells;

        protected byte _size;

        #endregion Protected Members

        #region Private Members

        private void InitCells()
        {
            _cells = new CellOfField[Size * Size];

            for (int i = 0; i < _cells.Length; i++)
            {
                _cells[i] = new CellOfField(new Position((byte)(i / Size), (byte)(i % Size)));
            }
        }

        #endregion Private Members

        #region Public Methods

        #region this[]

        public CellOfField this[int pos]
        {
            get
            {
                if (pos > Size * Size)
                    throw new OutOfFielRegionException("Get : BaseField.this[Position]");

                return this[new Position((byte)(pos / Size), (byte)(pos % Size))];
            }

            private set
            {
                if (pos < 0)
                    throw new OutOfFielRegionException("Set : BaseField.this[Position]");

                _cells[pos] = value;
            }
        }

        public CellOfField this[Position pos]
        {
            get
            {
                for (var i = 0; i < _cells.Length; i++)
                    if (_cells[i].Location == pos)
                        return _cells[i];

                throw new OutOfFielRegionException("BaseField.this[byte]");
            }
        }

        #endregion this[]

        #region Properties

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

        #endregion Properties

        #region Methods

        public BaseField(byte size)
        {
            Size = size;
            InitCells();
        }

        public static bool IsFielRegion(int Line, int Column, byte size)
        {
            if ((Line < size & Line >= 0) & (Column < size & Column >= 0))
                return true;
            return false;
        }

        public bool IsCellEmpty(Position position)
        {
            if (this[position].Show() == typeof(EmptyCell))
                return true;
            return false;
        }

        public bool WasCellAttack(Position position)
        {
            return this[position].WasAttacked;
        }

        #endregion Methods

        #endregion Public Methods

        #region IEnumerable<CellOfField>

        public IEnumerator<CellOfField> GetEnumerator()
        {
            foreach (CellOfField cell in _cells)
            {
                yield return cell;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion IEnumerable<CellOfField>
    }
}