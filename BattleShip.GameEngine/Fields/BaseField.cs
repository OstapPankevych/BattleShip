using BattleShip.GameEngine.Fields.Cells;
using BattleShip.GameEngine.Fields.Exceptions;
using BattleShip.GameEngine.Location;
using System.Collections;
using System.Collections.Generic;
using BattleShip.GameEngine.Fields.Cells.StatusCell;


namespace BattleShip.GameEngine.Fields
{
    public abstract class BaseField : IEnumerable<Cell>
    {
        #region Constructors

        protected BaseField(byte size)
        {
            Size = size;
            InitCells();
        }

        #endregion


        #region Private

        private byte _size;

        #endregion


        #region Protected

        protected Cell[][] _cells;

        #endregion


        #region Public

        public const byte MaxFieldSize = 20;

        #endregion Public


        #region Properties

        public byte Size
        {
            get { return _size; }
            private set
            {
                _size = value;
            }
        }

        #region this[]

        public Cell this[byte line, byte column]
        {
            get
            {
                return _cells[line][column];
            }
        }

        public Cell this[Position pos]
        {
            get { return this[pos.Line, pos.Column]; }
        }

        #endregion this[]

        #endregion Properties


        #region Private methods

        private void InitCells()
        {
            _cells = new Cell[Size][];
            for (byte i = 0; i < Size; i++)
            {
                _cells[i] = new Cell[Size];
                for (byte j = 0; j < Size; j++)
                {
                    _cells[i][j] = new Cell(new Position(i, j));
                    _cells[i][j].AddGameObject(new EmptyCell(new Position(i, j)), false);
                }
            }
        }

        #endregion Private methods


        #region Public methods

        public bool WasCellAttack(Position position)
        {
            return this[position].WasAttacked;
        }

        public static Position GetPositionForNumber(int number, byte fieldSize)
        {
            if (number > fieldSize * fieldSize)
            {
                throw new OutOfFielRegionException("Field:: GetPositionForNumber()");
            }
            else
            {
                return new Position((byte)(number / fieldSize), (byte)(number % fieldSize));
            }
        }

        public bool IsFieldRegion(byte line, byte column)
        {
            return IsFieldRegion(line, column, Size);
        }

        public static bool IsFieldRegion(byte line, byte column, byte fieldSize)
        {
            if ((line < fieldSize) & (column < fieldSize))
            {
                return true;
            }

            return false;
        }

        #endregion Public methods
        

        #region IEnumerable<CellOfField>

        public IEnumerator<Cell> GetEnumerator()
        {
            for (byte i = 0; i < Size; i++)
            {
                for (byte j = 0; j < Size; j++)
                {
                    yield return _cells[i][j];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion IEnumerable<CellOfField>
    }
}