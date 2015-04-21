using BattleShip.GameEngine.Location;
using System;

namespace BattleShip.GameEngine.Arsenal.Flot.Corectible
{
    public class Ractangle : ICorrectible
    {
        public static bool ChackShipRegion(byte countStorey, params Position[] positions)
        {
            Position begin = positions[0];
            Position end = positions[positions.Length - 1];

            // тривіально для одноповерхового
            if (begin == end & countStorey == 1)
                return true;

            // для решти робити перевірки коректності
            if (IsCorrectLine(begin, end) ^ IsCorrectColumn(begin, end))
            {
                if (IsCorrectLine(begin, end))
                {
                    if (IsCorrectCountStorey(begin.Column, end.Column, countStorey))
                        return true;
                }
                else if (IsCorrectColumn(begin, end))
                {
                    if (IsCorrectCountStorey(begin.Line, end.Line, countStorey))
                        return true;
                }
            }

            return false;
        }

        public static bool IsCorrectLine(Position begin, Position end)
        {
            if (begin.Line == end.Line)
                return true;
            return false;
        }

        public static bool IsCorrectColumn(Position begin, Position end)
        {
            if (begin.Column == end.Column)
                return true;
            return false;
        }

        public static bool IsCorrectCountStorey(byte begin, byte end, byte countStorey)
        {
            if (Math.Abs(begin - end) + 1 == countStorey)
                return true;
            return false;
        }

        public bool IsTrueShipRegion(byte countStorey, params Position[] positions)
        {
            return ChackShipRegion(countStorey, positions);
        }
    }
}