using System.Drawing;
using System.Windows.Forms;

namespace BattleShip.DesktopUI.Field.DrawCells.DrawType
{
    class DrawShip : IDrawableCell
    {
        public DrawShip(bool sequre)
        {
            Sequre = sequre;
        }

        public bool Sequre { get; private set; }

        public void Draw(bool wasAttacked, Graphics g, Point topLeft, byte sizeOneCell, byte borderWidth)
        {
            Pen pen = new Pen(Color.Black, 2);

            // відносно всього поля, а не тільки відносно ігрового регіону
            Point newTopLeft = UcField.GetPositionForPlayRegion(topLeft, sizeOneCell, borderWidth);

            UcField.DrawRectangleForPlayRegion(pen, g, newTopLeft, sizeOneCell);

            if (Sequre)
            {
                g.FillRectangle(Brushes.Aquamarine, newTopLeft.X + sizeOneCell / 8, newTopLeft.Y + sizeOneCell / 8,
                    sizeOneCell - (2 * sizeOneCell / 8) + 1, sizeOneCell - (2 * sizeOneCell / 8) + 1);
            }

            if (wasAttacked)
            {
                pen = new Pen(Color.Red, 2);
                g.DrawLine(pen, newTopLeft, new Point(newTopLeft.X + sizeOneCell, newTopLeft.Y + sizeOneCell));
                g.DrawLine(pen, new Point(newTopLeft.X, newTopLeft.Y + sizeOneCell), new Point(newTopLeft.X + sizeOneCell, newTopLeft.Y));
            }
        }
    }
}
