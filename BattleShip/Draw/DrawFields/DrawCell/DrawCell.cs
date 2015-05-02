using System;
using BattleShip.ConsoleUI.Draw.DrawFields.DrawCell.DrawType;
using BattleShip.GameEngine.Arsenal.Flot.RectangleShips;
using BattleShip.GameEngine.Arsenal.Protection;
using BattleShip.GameEngine.Fields.Cells;
using BattleShip.GameEngine.Fields.Cells.StatusOfCells;

namespace BattleShip.ConsoleUI.Draw.DrawFields.DrawCell
{
    /*
     * Review GY: рекомендую перетворити даний клас на простенький параметризований фабричний метод, в цьому випадку клас потрібно переіменувати. 
     *  public IDrawableCell Draw(Type typeCell, bool drawAllElements = false);
     *  Тобто, залежно від переданого typeCell, ми отримаємо конкретний IDrawableCell, а вже ззовні викличемо для нього drawableCell.Draw(wasAttacked);
     */
    class DrawCellWithAllElements
    {
        private IDrawableCell _drawableCell;

        public void Draw(Cell cell, bool drawAllElements = false)
        {
            Type typeCell = cell.GetStatusCell();

            bool wasAttacked = cell.WasAttacked;

            if (typeCell == typeof (EmptyCell))
            {
                _drawableCell = new DrawEmptyCell();
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
            else if (typeCell == typeof(PVOProtect))
            {
                _drawableCell = new DrawPVOProtect();
            }
            else if (typeCell == typeof (AroundShip))
            {
                if (drawAllElements)
                    _drawableCell = new DrawAroundShip();
                else
                    _drawableCell = new DrawEmptyCell();
            }

            // намалювати
            _drawableCell.Draw(wasAttacked);
        }
    }
}
