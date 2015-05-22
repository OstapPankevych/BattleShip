using System; //не використовується
using System.Collections.Generic;
using System.Linq; //не використовується
using System.Text; //не використовується
using System.Threading.Tasks; //не використовується

using BattleShip.GameEngine.Location;
using BattleShip.GameEngine.Fields;

namespace BattleShip.GameEngine.Game.Players.Computer.Brain.SetObjects
{
    public static class PositionAroundInputPosition
    {
        public static List<Position> GetArountPositions(Position begin, byte countStorey, byte fieldSize)
        {
            List<Position> positions = new List<Position>();

            //////////////////////
            byte line = (byte)(begin.Line + (countStorey - 1));
            byte column = begin.Column;

            if (line > 0 || column > 0)
            {
                if (BaseField.IsFieldRegion(line, column, fieldSize))
                {
                    positions.Add(new Position(line, column));
                }
            }

            ///////////////////////
            line = (byte)(begin.Line - (countStorey - 1));

            if (line > 0 || column > 0)
            {
                //приведення типу є зайвим line i column є byte
                if (BaseField.IsFieldRegion((byte)line, (byte)column, fieldSize))
                {
                    positions.Add(new Position((byte)line, (byte)column));
                }
            }

            //////////////////////
            line = begin.Line;
            column = (byte)(begin.Column + (countStorey - 1));

            if (line > 0 || column > 0)
            {
                //приведення типу є зайвим line i column є byte
                if (BaseField.IsFieldRegion((byte)line, (byte)column, fieldSize))
                {
                    positions.Add(new Position((byte)line, (byte)column));
                }
            }

            //////////////////////
            column = (byte)(begin.Column - (countStorey - 1));

            if (line > 0 || column > 0)
            {
                //приведення типу є зайвим line i column є byte
                if (BaseField.IsFieldRegion((byte)line, (byte)column, fieldSize))
                {
                    positions.Add(new Position((byte)line, (byte)column));
                }
            }

            return positions;
        }
    }
}
