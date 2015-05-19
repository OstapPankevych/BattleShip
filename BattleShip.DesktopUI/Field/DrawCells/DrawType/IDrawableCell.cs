using System.Drawing;

namespace BattleShip.DesktopUI.Field.DrawCells.DrawType
{
    interface IDrawableCell 
    {
        void Draw(bool wasAttacked, Graphics g,  Point topLeft, byte sizeOneCell, byte borderWidth);
    }
}
