using System;
using System.Linq;
using BattleShip.ConsoleUI.Draw.DrawFields.DrawCell.DrawType;
using BattleShip.GameEngine.Arsenal.Flot.RectangleShips;
using BattleShip.GameEngine.Arsenal.Protection;
using BattleShip.GameEngine.Fields.Cells;
using BattleShip.GameEngine.Fields.Cells.StatusCell;

namespace BattleShip.ConsoleUI.Draw.DrawFields.DrawCell
{
    class DrawCellWithAllElements
    {
        private IDrawableCell _drawableCell;

        public void Draw(Cell cell, bool drawAllElements = false)
        {
            Type typeCell = cell.GetTypeOfCellObject();

            bool wasAttacked = cell.WasAttacked;

            if (typeCell == typeof (EmptyCell))
            {
                _drawableCell = new DrawEmptyCell(cell.IsProtected);
            }
            else if (typeCell == typeof(OneStoreyRectangleShip))
            {
                _drawableCell = new DrawOneStoreyShip();
            }
            else if (typeCell == typeof(TwoStoreyRectangleShip))
            {
                _drawableCell = new DrawTwoStoreyShip();
            }
            else if (typeCell == typeof(ThreeStoreyRectangleShip))
            {
                _drawableCell = new DrawThreeSoreyShip();
            }
            else if (typeCell == typeof(FourStoreyRectangleShip))
            {
                _drawableCell = new DrawFourStoreyShip();
            }
            else if (typeCell == typeof(Pvo))
            {
                _drawableCell = new DrawPVOProtect();
            }
            else if (typeCell == typeof (AroundShip))
            {
                if (drawAllElements)
                    _drawableCell = new DrawAroundShip();
                else
                    _drawableCell = new DrawEmptyCell(cell.IsProtected);
            }

            // намалювати
            _drawableCell.Draw(wasAttacked);
        }
    }
}
