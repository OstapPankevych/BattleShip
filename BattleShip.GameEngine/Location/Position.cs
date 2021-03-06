﻿namespace BattleShip.GameEngine.Location
{
    public struct Position
    {
        private readonly byte _column;
        private readonly byte _line;

        public Position(byte line, byte column)
        {
            _line = line;
            _column = column;
        }

        public Position(Position position)
            : this(position.Line, position.Column)
        { }

        public byte Line
        {
            get { return _line; }
        }

        public byte Column
        {
            get { return _column; }
        }

        public static bool operator ==(Position position1, Position position2)
        {
            return position1.Equals(position2);
        }

        public static bool operator !=(Position position1, Position position2)
        {
            return !position1.Equals(position2);
        }

        public override bool Equals(object obj)
        {
            Position? pos = obj as Position?;
            if (pos != null)
            {
                if (Line == pos.Value.Line & Column == pos.Value.Column)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Line.GetHashCode() ^ Column.GetHashCode();
        }
    }
}