using BattleShip.GameEngine.Arsenal.Flot;
using BattleShip.GameEngine.Arsenal.Flot.RectangleShips;
using BattleShip.GameEngine.Game.Players.Computer.Brain.SetObjects.SetRectangleBase;
using BattleShip.GameEngine.Location;
using System;
using System.Collections.Generic;
using BattleShip.GameEngine.Arsenal.Flot.Correctible;

namespace BattleShip.GameEngine.Game.Players.Computer.Brain.SetObjects.SetRectangleShip
{
    public class SetShip : ISetibleShip
    {
        public void SetShips(Func<ShipBase, bool> SetShipsFunc, byte fieldSize)
        {
            // FourStoreyShip
            for (byte i = 0; i < 1; i++)
            {
                if (!SetRectangleShip(4, i, SetShipsFunc, fieldSize))
                {
                    i--;
                }
            }

            // ThreeStoreyShip
            for (byte i = 0; i < 2; i++)
            {
                if (!SetRectangleShip(3, i, SetShipsFunc, fieldSize))
                {
                    i--;
                }
            }

            // TwoStoreyShip
            for (byte i = 0; i < 3; i++)
            {
                if (!SetRectangleShip(2, i, SetShipsFunc, fieldSize))
                {
                    i--;
                }
            }

            // OneStoreyShip
            for (byte i = 0; i < 4; i++)
            {
                if (!SetRectangleShip(1, i, SetShipsFunc, fieldSize))
                {
                    i--;
                }
            }
        }

        private bool SetRectangleShip(byte countStoreyShip, byte idShip, Func<ShipBase, bool> SetShipsFunc, byte fieldSize)
        {
            Random rnd = new Random();

            Position begin;

            do
            {
                //Field доцільно замінити на BaseField
                begin = Fields.Field.GetPositionForNumber(rnd.Next(fieldSize * fieldSize), fieldSize);

                List<Position> positionsList = GetArountPositions(begin, countStoreyShip, fieldSize);

                // взяти всі можливі точки кругом для цього виду кораблика
                while (positionsList.Count != 0)
                {
                    int index = rnd.Next(positionsList.Count);

                    Position x = positionsList[index];
                    positionsList.RemoveAt(index);

                    // взяти позиції для кораблика
                    Position[] positions = Ractangle.GetRectangleRegion(countStoreyShip, begin, x);

                    if (positions == null)
                    {
                        return false;
                    }
                    
                        // спробувати поставити кораблик
                        switch (countStoreyShip)
                        {
                            case 1:
                                {
                                    if (SetShipsFunc(new OneStoreyRectangleShip(idShip, positions[0])))
                                    {
                                        return true;
                                    }
                                    break;
                                }
                            case 2:
                                {
                                    if (SetShipsFunc(new TwoStoreyRectangleShip(idShip, positions[0], positions[1])))
                                    {
                                        return true;
                                    }
                                    break;
                                }
                            case 3:
                                {
                                    if (SetShipsFunc(new ThreeStoreyRectangleShip(idShip, positions[0], positions[1], positions[2])))
                                    {
                                        return true;
                                    }
                                    break;
                                }
                            case 4:
                                {
                                    if (SetShipsFunc(new FourStoreyRectangleShip(idShip, positions[0], positions[1], positions[2], positions[3])))
                                    {
                                        return true;
                                    }
                                    break;
                                }
                        }
                    }
            } while (true);
        }

        private List<Position> GetArountPositions(Position begin, byte countStorey, byte fieldSize)
        {
            List<Position> positions = new List<Position>();

            //значення можна призначити одразу при оголошенні
            int Column;
            int Line;

            //////////////////////
            Line = begin.Line + (countStorey - 1);
            Column = begin.Column;

            if (Line > 0 || Column > 0)
            {
                //Field доцільно замінити на BaseField
                if (Fields.Field.IsFielRegion(Line, Column, fieldSize))
                {
                    positions.Add(new Position((byte) Line, (byte) Column));
                }
            }

            ///////////////////////
            Line = begin.Line - (countStorey - 1);

            if (Line > 0 || Column > 0)
            {
                if (Fields.Field.IsFielRegion(Line, Column, fieldSize))
                {
                    positions.Add(new Position((byte) Line, (byte) Column));
                }
            }

            //////////////////////
            Line = begin.Line;
            Column = begin.Column + (countStorey - 1);

            if (Line > 0 || Column > 0)
            {
                //Field доцільно замінити на BaseField
                if (Fields.Field.IsFielRegion(Line, Column, fieldSize))
                {
                    positions.Add(new Position((byte) Line, (byte) Column));
                }
            }

            //////////////////////
            Column = begin.Column - (countStorey - 1);

            if (Line > 0 || Column > 0)
            {
                //Field доцільно замінити на BaseField
                if (Fields.Field.IsFielRegion(Line, Column, fieldSize))
                {
                    positions.Add(new Position((byte) Line, (byte) Column));
                }
            }

            return positions;
        }
    }
}