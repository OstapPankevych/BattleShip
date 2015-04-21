using BattleShip.GameEngine.Arsenal.Flot.Corectible;
using BattleShip.GameEngine.Arsenal.Flot.RectangleShips;
using BattleShip.GameEngine.Game.GameMode;
using BattleShip.GameEngine.Game.Players.Computer.Brain.SetObjects.SetRectangleBase;
using BattleShip.GameEngine.Location;
using System;
using System.Collections.Generic;
using System.Threading;
using BattleShip.GameEngine.Game.GameMode.ClassicGameMode;

namespace BattleShip.GameEngine.Game.Players.Computer.Brain.SetObjects.SetRectangleShip
{
    public class SetShip : ISetibleShip
    {
        public void SetShips(GameMode.GameMode myMode)
        {
            if (myMode is ClassicGameMode)
            {
                // FourStoreyShip
                for (byte i = 0; i < 1; i++)
                {
                    if (!SetRectangleShip(4, i, myMode))
                        i--;
                }

                // ThreeStoreyShip
                for (byte i = 0; i < 2; i++)
                {
                    if (!SetRectangleShip(3, i, myMode))
                        i--;
                }

                // TwoStoreyShip
                for (byte i = 0; i < 3; i++)
                {
                    if (!SetRectangleShip(2, i, myMode))
                        i--;
                }

                // OneStoreyShip
                for (byte i = 0; i < 4; i++)
                {
                    if (!SetRectangleShip(1, i, myMode))
                        i--;
                }
            }
        }

        private bool SetRectangleShip(byte countStoreyShip, byte idShip, GameMode.GameMode myMode)
        {
            Random rnd = new Random();

            Position begin;

            #region try setting ship on field

            do
            {
                begin = myMode.CurrentField[(byte)rnd.Next(myMode.CurrentField.Size * myMode.CurrentField.Size)].Location;

                List<Position> positionsList = GetArountPositions(begin, countStoreyShip, myMode.CurrentField.Size);

                // взяти всі можливі точки кругом для цього виду кораблика
                while (positionsList.Count != 0)
                {
                    int index = rnd.Next(positionsList.Count);

                    Position x = positionsList[index];
                    positionsList.RemoveAt(index);

                    #region chack ship region and try set ship : when setted - exit from while(true)

                    if (Ractangle.ChackShipRegion(countStoreyShip, begin, x))
                    {
                        // спробувати поставити кораблик
                        switch (countStoreyShip)
                        {
                            case 1:
                                {
                                    if (myMode.AddShip(new OneStoreyRectangleShip(idShip, begin)))
                                    {
                                        return true;
                                    }
                                    break;
                                }
                            case 2:
                                {
                                    if (myMode.AddShip(new TwoStoreyRectangleShip(idShip, begin, x)))
                                    {
                                        return true;
                                    }
                                    break;
                                }
                            case 3:
                                {
                                    #region PartsOfShipInitialization

                                    // ініціалізація неможливим значенням, щоб коли наступний пошук буде неправильним - вилетів ShipException()
                                    Position midlePosition = new Position((byte)(begin.Line + 1), (byte)(begin.Column + 1));

                                    if (Ractangle.IsCorrectColumn(begin, x))
                                    {
                                        if (begin.Line < x.Line)
                                        {
                                            midlePosition = new Position((byte)(begin.Line + 1), begin.Column);
                                        }
                                        else if (x.Line < begin.Line)
                                        {
                                            midlePosition = new Position((byte)(x.Line + 1), begin.Column);
                                        }
                                    }
                                    else if (Ractangle.IsCorrectLine(begin, x))
                                    {
                                        if (begin.Column < x.Column)
                                        {
                                            midlePosition = new Position(begin.Line, (byte)(begin.Column + 1));
                                        }
                                        else if (x.Column < begin.Column)
                                        {
                                            midlePosition = new Position(begin.Line, (byte)(x.Column + 1));
                                        }
                                    }
                                    else
                                    {
                                        return false;
                                    }

                                    #endregion PartsOfShipInitialization

                                    if (myMode.AddShip(new ThreeStoreyRectangleShip(idShip, begin, midlePosition, x)))
                                    {
                                        return true;
                                    }
                                    break;
                                }
                            case 4:
                                {
                                    #region PartsOfShipInitialization

                                    // ініціалізація неможливим значенням, щоб коли наступний пошук буде неправильним - вилетів ShipException()
                                    Position midlePosition1 = new Position((byte)(begin.Line + 1), (byte)(begin.Column + 1));
                                    Position midlePosition2 = new Position((byte)(begin.Line + 2), (byte)(begin.Column + 2));
                                    

                                    if (Ractangle.IsCorrectColumn(begin, x))
                                    {
                                        if (begin.Line < x.Line)
                                        {
                                            midlePosition1 = new Position((byte)(begin.Line + 1), begin.Column);
                                            midlePosition2 = new Position((byte)(begin.Line + 2), begin.Column);
                                        }
                                        else if (x.Line < begin.Line)
                                        {
                                            midlePosition1 = new Position((byte)(x.Line + 1), begin.Column);
                                            midlePosition2 = new Position((byte)(x.Line + 2), begin.Column);
                                        }
                                    }
                                    else if (Ractangle.IsCorrectLine(begin, x))
                                    {
                                        if (begin.Column < x.Column)
                                        {
                                            midlePosition1 = new Position(begin.Line, (byte)(begin.Column + 1));
                                            midlePosition2 = new Position(begin.Line, (byte)(begin.Column + 2));
                                        }
                                        else if (x.Column < begin.Column)
                                        {
                                            midlePosition1 = new Position(begin.Line, (byte)(x.Column + 1));
                                            midlePosition2 = new Position(begin.Line, (byte)(x.Column + 2));
                                        }
                                    }
                                    else
                                    {
                                        return false;
                                    }

                                    #endregion PartsOfShipInitialization

                                    if (myMode.AddShip(new FourStoreyRectangleShip(idShip, begin, midlePosition1, midlePosition2, x)))
                                    {
                                        return true;
                                    }
                                    break;
                                }
                        }
                    }
                    else
                    {
                        return false;
                    }

                    #endregion chack ship region and try set ship : when setted - exit from while(true)
                }
            } while (true);

            #endregion try setting ship on field
        }

        private List<Position> GetArountPositions(Position begin, byte countStorey, byte fieldSize)
        {
            List<Position> positions = new List<Position>();

            int Column;
            int Line;

            //////////////////////
            Line = begin.Line + (countStorey - 1);
            Column = begin.Column;

            if (Line > 0 || Column > 0)
                if (Field.Field.IsFielRegion(Line, Column, fieldSize))
                    positions.Add(new Position((byte)Line, (byte)Column));

            ///////////////////////
            Line = begin.Line - (countStorey - 1);

            if (Line > 0 || Column > 0)
                if (Field.Field.IsFielRegion(Line, Column, fieldSize))
                    positions.Add(new Position((byte)Line, (byte)Column));

            //////////////////////
            Line = begin.Line;
            Column = begin.Column + (countStorey - 1);

            if (Line > 0 || Column > 0)
                if (Field.Field.IsFielRegion(Line, Column, fieldSize))
                    positions.Add(new Position((byte)Line, (byte)Column));

            //////////////////////
            Column = begin.Column - (countStorey - 1);

            if (Line > 0 || Column > 0)
                if (Field.Field.IsFielRegion(Line, Column, fieldSize))
                    positions.Add(new Position((byte)Line, (byte)Column));

            return positions;
        }
    }
}