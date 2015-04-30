using BattleShip.GameEngine.Location;
using System;
using BattleShip.GameEngine.Arsenal.Flot.Corectible;

namespace BattleShip.GameEngine.Arsenal.Flot.Correctible
{
    public class Ractangle : ICorrectible
    {
        // Перевіряє чи регіон з positions[] є сприйнятний для кораблика з countStorey
        public static bool CheckShipRegion(byte countStorey, params Position[] positions)
        {
            Position begin = positions[0];
            Position end = positions[positions.Length - 1];

            // тривіально для одноповерхового
            if (begin == end & countStorey == 1)
            {
                return true;
            }
            else
            {
                // для решти робити перевірки коректності
                if (IsCorrectLine(begin, end) ^ IsCorrectColumn(begin, end))
                {
                    if (IsCorrectLine(begin, end))
                    {
                        if (IsCorrectCountStorey(begin.Column, end.Column, countStorey))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }

                    if (IsCorrectColumn(begin, end))
                    {
                        if (IsCorrectCountStorey(begin.Line, end.Line, countStorey))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                return false;
            }
        }

        // Дати всі координати кораблика з countStorey для початкової і кінцевої позиції
        public static Position[] GetRectangleRegion(byte countStorey, Position begin, Position end)
        {
            if (!CheckShipRegion(countStorey, begin, end))
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

                // ініціалізація неможливим значенням, щоб коли наступний пошук буде неправильним - вилетів ShipException()
                Position midlePosition1 = new Position((byte)(begin.Line + 1), (byte)(begin.Column + 1));
                Position midlePosition2 = new Position((byte)(begin.Line + 2), (byte)(begin.Column + 2));

                if (Ractangle.IsCorrectColumn(begin, end))
                {
                    if (begin.Line < end.Line)
                    {
                        //можна не використовувати приведення типу (див. зауваження в класі Position) 
                        midlePosition1 = new Position((byte)(begin.Line + 1), begin.Column);
                        midlePosition2 = new Position((byte)(begin.Line + 2), begin.Column);
                    }
                    else if (end.Line < begin.Line)
                    {
                        midlePosition1 = new Position((byte)(end.Line + 1), begin.Column);
                        midlePosition2 = new Position((byte)(end.Line + 2), begin.Column);
                    }
                }
                else if (Ractangle.IsCorrectLine(begin, end))
                {
                    if (begin.Column < end.Column)
                    {
                        midlePosition1 = new Position(begin.Line, (byte)(begin.Column + 1));
                        midlePosition2 = new Position(begin.Line, (byte)(begin.Column + 2));
                    }
                    else if (end.Column < begin.Column)
                    {
                        midlePosition1 = new Position(begin.Line, (byte)(end.Column + 1));
                        midlePosition2 = new Position(begin.Line, (byte)(end.Column + 2));
                    }
                }

                if (countStorey == 3)
                {
                    return new Position[] { begin, midlePosition1, end };
                }
                if (countStorey == 4)
                {
                    return new Position[] { begin, midlePosition1, midlePosition2, end };
                }

                return null;
            }
        }

        // Чи точки begin і end є на одній горизонтальній лінії на полі
        public static bool IsCorrectLine(Position begin, Position end)
        {
            //для економії місця можна використати теренарний оператор
            if (begin.Line == end.Line)
            {
                return true;
            }

            return false;
        }

        // Чи точки begin і end є на одній вертикальній лінії на полі
        public static bool IsCorrectColumn(Position begin, Position end)
        {
            //для економії місця можна використати теренарний оператор
            if (begin.Column == end.Column)
            {
                return true;
            }

            return false;
        }

        // Чи є правильна кількість поверхів
        public static bool IsCorrectCountStorey(byte begin, byte end, byte countStorey)
        {
            //для економії місця можна використати теренарний оператор
            if (Math.Abs(begin - end) + 1 == countStorey)
            {
                return true;
            }

            return false;
        }

        // аналог ChackShipRegion() тільки не статична
        public bool IsTrueShipRegion(byte countStorey, params Position[] positions)
        {
            return CheckShipRegion(countStorey, positions);
        }
    }
}