using System;
using System.Drawing;
using BattleShip.DesktopUI.Field.DrawCells.DrawType;
using BattleShip.GameEngine.Arsenal.Flot.RectangleShips;
using BattleShip.GameEngine.Arsenal.Protection;
using BattleShip.GameEngine.Fields.Cells;
using BattleShip.GameEngine.Fields.Cells.StatusCell;

namespace BattleShip.DesktopUI.Field.DrawCells
{
    class DrawCell
    {
        private IDrawableCell _drawableCell;

        public void Draw(Cell cell, Graphics g, Point topLeft, byte sizeOneCell, byte borderWidth)
        {
            Type typeCell = cell.GetTypeOfCellObject();

            bool wasAttacked = cell.WasAttacked;

            if (typeCell == typeof (EmptyCell))
            {
                _drawableCell = new DrawEmptyCell(cell.IsProtected);
            }
            else if (typeCell == typeof(OneStoreyRectangleShip))
            {
                _drawableCell = new DrawShip(cell.IsProtected);
            }
            else if (typeCell == typeof(TwoStoreyRectangleShip))
            {
                _drawableCell = new DrawShip(cell.IsProtected);
            }
            else if (typeCell == typeof(ThreeStoreyRectangleShip))
            {
                _drawableCell = new DrawShip(cell.IsProtected);
            }
            else if (typeCell == typeof(FourStoreyRectangleShip))
            {
                _drawableCell = new DrawShip(cell.IsProtected);
            }
            else if (typeCell == typeof(Pvo))
            {
                _drawableCell = new DrawPVOProtect();
            }
            else if (typeCell == typeof (AroundShip))
            {
                _drawableCell = new DrawEmptyCell(cell.IsProtected);
            }

            // намалювати
            _drawableCell.Draw(wasAttacked, g, topLeft, sizeOneCell, borderWidth);
        }
    }
}
