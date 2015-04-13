using System;



namespace BattleShip.GameEngine.Location
{
    struct Position
    {
        byte _line;

        byte _column;

        public byte Line
        {
            get
            {
                return _line;
            }
        }

        public byte Column
        {
            get
            {
                return _column;
            }
        }

        public Position(byte line, byte column)
        {
            this._line = line;
            this._column = column;
        }

        public Position(Position position)
        {
            this._line = position.Line;
            this._column = position.Column;
        }

        public static bool operator ==(Position position1, Position position2)
        {
            if (position1.Line == position2.Line & position1.Column == position2.Column)
                return true;
            return false;
        }

        public static bool operator !=(Position position1, Position position2)
        {
            if (position1 == position2)
                return false;
            return true;
        }
    }
}