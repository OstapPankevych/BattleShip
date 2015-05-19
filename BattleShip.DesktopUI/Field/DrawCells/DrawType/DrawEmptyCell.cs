using System.Drawing;

namespace BattleShip.DesktopUI.Field.DrawCells.DrawType
{
    class DrawEmptyCell : IDrawableCell
    {
        public DrawEmptyCell(bool sequre)
        {
            Sequre = sequre;
        }

        public bool Sequre { get; private set; }

        public void Draw(bool wasAttacked, Graphics g, Point topLeft, byte sizeOneCell, byte borderWidth)
        {
            // відносно всього поля, а не тільки відносно ігрового регіону
            Point newTopLeft = UcField.GetPositionForPlayRegion(topLeft, sizeOneCell, borderWidth);

            if (Sequre)
            {
                g.FillRectangle(Brushes.Aquamarine, newTopLeft.X + sizeOneCell/8, newTopLeft.Y + sizeOneCell/8,
                    sizeOneCell - (2*sizeOneCell/8) + 1, sizeOneCell - (2*sizeOneCell/8) + 1);
            }

            if (wasAttacked)
            {
                g.DrawEllipse(new Pen(Color.OrangeRed, 4), newTopLeft.X + sizeOneCell / 2 - 2, newTopLeft.Y + sizeOneCell / 2 - 2, 4, 4);
            }
        }
    }
}
