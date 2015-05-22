using BattleShip.GameEngine.Location;
using System;

namespace BattleShip.GameEngine.Arsenal.Flot.Corectible
{
    public class Ractangle : ICorrectible
    {
        #region Static methods

        // Перевіряє чи регіон з positions[] є сприйнятний для кораблика з countStorey
        public static bool ChackShipRegion(byte countStorey, params Position[] positions)
        {

            Position begin = positions[0];
            Position end = positions[positions.Length - 1];

            // Для тривіального випадку - поверути true
            if ((begin == end) & (countStorey == 1))
            {
                return true;
            }

            // Дял решти випадків - зробити перевірку коректності
            if (IsCorrectLine(begin, end) ^ IsCorrectColumn(begin, end))
            {
                if (IsCorrectLine(begin, end) & IsCorrectCountStorey(begin.Column, end.Column, countStorey))
                {
                    return true;   
                }
                
                if (IsCorrectColumn(begin, end) & IsCorrectCountStorey(begin.Line, end.Line, countStorey))
                {
                    return true;
                }
            }

            return false;
        }

        // Дати всі координати кораблика з countStorey для початкової і кінцевої позиції
        public static Position[] GetRectangleRegion(byte countStorey, Position begin, Position end)
        {
            if (!ChackShipRegion(countStorey, begin, end))
            {
                return null;
            }
            else
            {
                if (countStorey == 1)
                {
                    return new Position[] { begin };
                }

                if (countStorey == 2)
                {
                    return new Position[] { begin, end };
                }
                
                Position[] midlePos = new Position[countStorey - 2];

                //не обов'язково вказувати ім'я класу для ф-й з цього ж класу
                if (Ractangle.IsCorrectColumn(begin, end))
                {
                    byte minLine = (begin.Line < end.Line) ? begin.Line : end.Line;
                    for (byte i = 0; i < countStorey - 2; i++)
                    {
                        midlePos[i] = new Position((byte)(minLine + (i + 1)), begin.Column);
                    }
                }

                //не обов'язково вказувати ім'я класу для ф-й з цього ж класу
                else if (Ractangle.IsCorrectLine(begin, end))
                {
                    byte minColumn = (begin.Column < end.Column) ? begin.Column : end.Column;
                    for (byte i = 0; i < countStorey - 2; i++)
                    {
                        midlePos[i] = new Position(begin.Line, (byte)(minColumn + (i + 1)));
                    }                  
                }

                Position[] res = new Position[midlePos.Length + 2];
                res[0] = begin;
                res[res.Length - 1] = end;

                for (int i = 1; i < res.Length - 1; i++)
                {
                    res[i] = midlePos[i - 1];
                }
                return res;

                
            }
        }

        // Чи точки begin і end є на одній горизонтальній лінії на полі
        public static bool IsCorrectLine(Position begin, Position end)
        {
            if (begin.Line == end.Line)
            {
                return true;
            }

            return false;
        }

        // Чи точки begin і end є на одній вертикальній лінії на полі
        public static bool IsCorrectColumn(Position begin, Position end)
        {
            if (begin.Column == end.Column)
            {
                return true;
            }

            return false;
        }

        // Чи є правильна кількість поверхів
        public static bool IsCorrectCountStorey(byte begin, byte end, byte countStorey)
        {
            if (Math.Abs(begin - end) + 1 == countStorey)
            {
                
                return true;
            }

            return false;
        }

        #endregion Static methods


        // аналог ChackShipRegion() тільки не статична
        public bool IsTrueShipRegion(byte countStorey, params Position[] positions)
        {
            return ChackShipRegion(countStorey, positions);
        }
    }
}