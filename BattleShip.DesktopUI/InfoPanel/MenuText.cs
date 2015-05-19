using System;
using System.Drawing;
using System.Windows.Forms;



namespace BattleShip.DesktopUI.InfoPanel
{
    public sealed class ActiveMenuText : SimpleMenuText
    {
        public int WidthMsgPxls { get; private set; }
        public int HeightMsgPxls { get; private set; }

        public bool IsSetted { get; private set; }
        public Pen SettedPen { get; private set; }

        public ActiveMenuText(string msg, Color color, Font textFont, Point beginPointPxls, Pen pen, bool isSetted = false)
            : base(msg, color, textFont, beginPointPxls)
        {
            Size s = TextRenderer.MeasureText(msg, TextFont);

            WidthMsgPxls = s.Width;
            HeightMsgPxls = s.Height;

            IsSetted = isSetted;
            SettedPen = pen;
        }

        public static bool IsPointInThisRegion(Point beginPointPxls, int widthMsgPxls, int heightMsgPxls, Point point)
        {
            if ((beginPointPxls.X < point.X) &
                (beginPointPxls.X + widthMsgPxls > point.X) &
                (beginPointPxls.Y < point.Y) &
                (beginPointPxls.Y + heightMsgPxls > point.Y))
            {
                return true;
            }

            return false;
        }

        public void ChangeSettedStatus()
        {
            IsSetted = !IsSetted;

            if (ChangeCurrentActiveStatus != null)
            {
                ChangeCurrentActiveStatus();
            }
        }

        public event Action ChangeCurrentActiveStatus;

        public void OnChangeCurrentActiveStatus()
        {
            IsSetted = !IsSetted;
        }
    }

    public class SimpleMenuText
    {
        public string Msg { get; private set; }
        public Color TextColor { get; private set; }
        public Font TextFont { get; private set; }

        public Point BeginPointPxls { get; private set; }

        public SimpleMenuText(string msg, Color color, Font textFont, Point beginPointPxls)
        {
            Msg = msg;
            TextColor = color;
            TextFont = textFont;
            BeginPointPxls = beginPointPxls;
        }
    }
}
