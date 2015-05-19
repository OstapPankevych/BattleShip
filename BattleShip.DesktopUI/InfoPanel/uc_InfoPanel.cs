using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BattleShip.DesktopUI.Field.DrawCells;
using BattleShip.DesktopUI.Field.DrawCells.DrawType;
using BattleShip.GameEngine.Arsenal.Flot.RectangleShips;
using BattleShip.GameEngine.Fields.Cells;
using BattleShip.GameEngine.Location;

namespace BattleShip.DesktopUI.InfoPanel
{
    public sealed partial class uc_InfoPanel : UserControl
    {
        #region Constructor

        public uc_InfoPanel()
        {
            InitializeComponent();

            DoubleBuffered = true;
        }

        #endregion Constructor


        #region Private

        private Graphics _gRegion;

        private Bitmap _btRegion;

        private byte _sizeOneCellPxls = 32;

        private readonly List<ActiveMenuText> _activeTextList = new List<ActiveMenuText>();
        private readonly List<SimpleMenuText> _simpleTextList = new List<SimpleMenuText>(); 

        #endregion Private


        #region Private methods

        private void DrawLines()
        {
            Pen pen = new Pen(Color.LightSkyBlue);

            for (byte i = 1; i < pb_Region.Height / _sizeOneCellPxls + 1; i++)
            {
                // horizontal lines
                _gRegion.DrawLine(pen, new Point(0, i * _sizeOneCellPxls),
                    new Point(pb_Region.Width, i * _sizeOneCellPxls));
            }

            for (byte i = 1; i < pb_Region.Width / _sizeOneCellPxls + 1; i++)
            {
                // vertical lines
                _gRegion.DrawLine(pen, new Point(i * _sizeOneCellPxls, 0),
                    new Point(i * _sizeOneCellPxls, pb_Region.Height));
            }
        }

        private void ChangeSize()
        {
            pb_Region.Width = Width;
            pb_Region.Height = Height;

            _btRegion = new Bitmap(Width, Height);
            pb_Region.Image = _btRegion;
            _gRegion = Graphics.FromImage(_btRegion);
        }

        private void Print(SimpleMenuText str)
        {
            System.Drawing.Brush brush = new SolidBrush(str.TextColor);
            StringFormat sf = new StringFormat(StringFormatFlags.LineLimit);

            _gRegion.DrawString(str.Msg, str.TextFont, brush, str.BeginPointPxls);

            if (str is ActiveMenuText)
            {
                ActiveMenuText local = (ActiveMenuText) str;
                if (local.IsSetted)
                {
                    DrawCurve(local.BeginPointPxls, local.SettedPen, local.WidthMsgPxls, local.HeightMsgPxls);
                }
            }
        }

        private void ReWriteAllText()
        {
            ClearAllText();

            for (int i = 0; i < _simpleTextList.Count; i++)
            {
                Print(_simpleTextList[i]);
            }

            for (int i = 0; i < _activeTextList.Count; i++)
            {
                Print(_activeTextList[i]);
            }

            pb_Region.Refresh();
        }

        private void DrawCurve(Point point, Pen pen, int widthText, int heightText)
        {
            // створити точки
            Point[] points = new Point[7];
            points[0] = new Point(point.X - widthText / 15, (int)(point.Y + heightText * 0.8));
            points[1] = new Point((int)(point.X + widthText * 0.7), (int)(point.Y + heightText * 1.01));
            points[2] = new Point((int)(point.X + widthText * 1.2), (int)(point.Y + heightText * 0.5));
            points[3] = new Point((int)(point.X + widthText * 0.8), (int)(point.Y));
            points[4] = new Point((int)(point.X), (int)(point.Y + heightText * 0.15));
            points[5] = new Point((int)(point.X - widthText / 8), (int)(point.Y + heightText / 2));
            points[6] = new Point(point.X + widthText / 3, (int)(point.Y + heightText * 0.9));

            _gRegion.DrawCurve(pen, points, 0.6F);
        }


        #endregion Private methods


        #region Public methods

        public void Init(byte sizeOneCellPxls)
        {
            _lastActiveStr = _strNoneLastActive;

            _sizeOneCellPxls = sizeOneCellPxls;

            _gRegion = pb_Region.CreateGraphics();

            ClearAllText();
        }

        public void WriteActiveText(ActiveMenuText str)
        {
            _activeTextList.Add(str);

            Print(str);
        }

        public void WriteText(SimpleMenuText str)
        {
            _simpleTextList.Add(str);

            Print(str);
        }

        public void ClearAllText()
        {
            ChangeSize();

            _gRegion.Clear(Color.White);

            DrawLines();
        }

        public void DeleteAllData()
        {
            _lastActiveStr = _strNoneLastActive;
            _simpleTextList.Clear();
            _activeTextList.Clear();   
        }

        public void DrawCellOfShip(byte line, byte column, Color color)
        {
            _gRegion.DrawRectangle(new Pen(color, 3), column * _sizeOneCellPxls, line * _sizeOneCellPxls, _sizeOneCellPxls, _sizeOneCellPxls);
        }

        public void DrawCellSequre(byte line, byte column)
        {
            Point newTopLeft = new Point(column * _sizeOneCellPxls, line * _sizeOneCellPxls);

            _gRegion.FillRectangle(Brushes.Aquamarine, newTopLeft.X + _sizeOneCellPxls / 8, newTopLeft.Y + _sizeOneCellPxls / 8,
                    _sizeOneCellPxls - (2 * _sizeOneCellPxls / 8) + 1, _sizeOneCellPxls - (2 * _sizeOneCellPxls / 8) + 1);
        }


        #endregion Public methods


        #region OnEvents

        private readonly ActiveMenuText _strNoneLastActive = new ActiveMenuText("", Color.Black,  DefaultFont, new Point(0, 0), new Pen(Color.FloralWhite));
        private ActiveMenuText _lastActiveStr;

        private void pb_Region_MouseMove(object sender, MouseEventArgs e)
        {
            // коли курсор миші знаходиться в межах виділеного тексту, тоді нічого не робити
            if (!_lastActiveStr.Equals(_strNoneLastActive))
            {
                if (ActiveMenuText.IsPointInThisRegion(_lastActiveStr.BeginPointPxls, _lastActiveStr.WidthMsgPxls,
                    _lastActiveStr.HeightMsgPxls, e.Location))
                {
                    return;
                }
                else
                {
                    _lastActiveStr = _strNoneLastActive;

                    ReWriteAllText();
                }
            }
            else
            {
                // перевіряємо чи курсор знаходиться на якомусь з активного тексту
                for (int i = 0; i < _activeTextList.Count; i++)
                {
                    if (ActiveMenuText.IsPointInThisRegion(_activeTextList[i].BeginPointPxls,
                        _activeTextList[i].WidthMsgPxls,
                        _activeTextList[i].HeightMsgPxls, e.Location))
                    {
                        ClearAllText();

                        for (int j = 0; j < _simpleTextList.Count; j++)
                        {
                            Print(_simpleTextList[j]);
                        }

                        for (int j = 0; j < _activeTextList.Count; j++)
                        {
                            if (i != j)
                            {
                                Print(_activeTextList[j]);
                            }
                            else
                            {
                                ActiveMenuText str = new ActiveMenuText(_activeTextList[j].Msg,
                                    _activeTextList[j].TextColor, 
                                    new Font(_activeTextList[i].TextFont.FontFamily,
                                        _activeTextList[i].TextFont.Size + 7),
                                    _activeTextList[j].BeginPointPxls, new Pen(Color.LimeGreen, 3), true);

                                Print(str);

                                _lastActiveStr = str;
                            }
                        }
                    }
                }
            }
        }

        private void pb_Region_MouseClick(object sender, MouseEventArgs e)
        {
            // коли курсор миші знаходиться в межах виділеного тексту, тоді виконувати дію
            if (!_lastActiveStr.Equals(_strNoneLastActive) &
                ActiveMenuText.IsPointInThisRegion(_lastActiveStr.BeginPointPxls, _lastActiveStr.WidthMsgPxls,
                    _lastActiveStr.HeightMsgPxls, e.Location))
            {
                //_lastActiveStr.ChangeSettedStatus();

                // шукаємо текст в листі
                for (int i = 0; i < _activeTextList.Count; i++)
                {
                    if (_activeTextList[i].BeginPointPxls == _lastActiveStr.BeginPointPxls)
                    {
                        _lastActiveStr.ChangeSettedStatus();

                        _activeTextList[i].ChangeSettedStatus();

                        ReWriteAllText();
                    }
                }
            }
        }

        #endregion OnEvents
    }
}
