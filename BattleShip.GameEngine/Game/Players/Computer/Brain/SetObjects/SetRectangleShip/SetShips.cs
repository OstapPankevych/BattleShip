using BattleShip.GameEngine.Arsenal.Flot;
using BattleShip.GameEngine.Arsenal.Flot.Corectible;
using BattleShip.GameEngine.Arsenal.Flot.RectangleShips;
using BattleShip.GameEngine.Game.Players.Computer.Brain.SetObjects.SetRectangleBase;
using BattleShip.GameEngine.Location;
using System;
using System.Collections.Generic;
using BattleShip.GameEngine.Fields;

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
                begin = BaseField.GetPositionForNumber(rnd.Next(fieldSize * fieldSize), fieldSize);

                List<Position> positionsList = PositionAroundInputPosition.GetArountPositions(begin, countStoreyShip, fieldSize);

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

        //public static  List<Position> GetArountPositions(Position begin, byte countStorey, byte fieldSize)
        //{
        //    List<Position> positions = new List<Position>();

        //    //////////////////////
        //    byte line = (byte)(begin.Line + (countStorey - 1));
        //    byte column = begin.Column;

        //    if (line > 0 || column > 0)
        //    {
        //        if (BaseField.IsFieldRegion(line, column, fieldSize))
        //        {
        //            positions.Add(new Position(line, column));
        //        }
        //    }

        //    ///////////////////////
        //    line = (byte)(begin.Line - (countStorey - 1));

        //    if (line > 0 || column > 0)
        //    {
        //        if (BaseField.IsFieldRegion((byte)line, (byte)column, fieldSize))
        //        {
        //            positions.Add(new Position((byte) line, (byte) column));
        //        }
        //    }

        //    //////////////////////
        //    line = begin.Line;
        //    column = (byte)(begin.Column + (countStorey - 1));

        //    if (line > 0 || column > 0)
        //    {
        //        if (BaseField.IsFieldRegion((byte)line, (byte)column, fieldSize))
        //        {
        //            positions.Add(new Position((byte) line, (byte) column));
        //        }
        //    }

        //    //////////////////////
        //    column = (byte)(begin.Column - (countStorey - 1));

        //    if (line > 0 || column > 0)
        //    {
        //        if (BaseField.IsFieldRegion((byte)line, (byte)column, fieldSize))
        //        {
        //            positions.Add(new Position((byte) line, (byte) column));
        //        }
        //    }

        //    return positions;
        //}
    }
}