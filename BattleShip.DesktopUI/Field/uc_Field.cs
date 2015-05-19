using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BattleShip.DesktopUI.Field.DrawCells;
using BattleShip.GameEngine.Arsenal.Flot.Corectible;
using BattleShip.GameEngine.Arsenal.Gun;
using BattleShip.GameEngine.Arsenal.Gun.Destroyable;
using BattleShip.GameEngine.Arsenal.Protection;
using BattleShip.GameEngine.Location;
using Color = System.Drawing.Color;
using Pen = System.Drawing.Pen;


namespace BattleShip.DesktopUI.Field
{

    public enum StepGameProcces { SettingShip, SettingProtect, Play }

    public partial class UcField : UserControl
    {
        #region Constructor

        public UcField()
        {
            InitializeComponent();
        }

        #endregion Constructor


        #region Private 

        // щоб знати як підсвічувати клітинки
        private StepGameProcces _stepGameProcces = StepGameProcces.SettingShip;
        private GameEngine.Fields.BaseField _field;
        private byte _sizeOneCellPxls;
        private const string literals = "abcdefghklmnoprstu";
        private string _playerName;

        private Bitmap _btBackgroundRegion;
        private Graphics _gBackGroundRegion;

        private Graphics _gPlayRegion;
        private Bitmap _btBitmapPlayRegion;

        private Position[] positions = new Position[2];

        private Pen _currentPen;

        private delegate void LightCell(Point point);
        private LightCell _currentLightCell;

        private readonly Pen _defaultPen = new Pen(Color.MediumAquamarine, 3);

        public Dictionary<byte, byte> AvailableShipsDictionary { get; set; }

        #endregion Private 


        #region Public 

        public Gun CurrentGun { get; set; }

        public bool StatusReadyToRead { get; private set; }

        public StepGameProcces GameProccesStep
        {
            get
            {
                return _stepGameProcces;
            }

            set
            {
                if (value == StepGameProcces.SettingShip)
                {
                    _currentLightCell = LightCellCursor;
                }
                else if (value == StepGameProcces.SettingProtect)
                {
                    _currentLightCell = LightCellForProtect;
                }
                else if (value == StepGameProcces.Play)
                {
                    _currentLightCell = LightCellForShot;
                    CurrentGun = new Gun();
                    CurrentGun.ChangeCurrentGun(new GunDestroy());
                }

                _stepGameProcces = value;
            }
        }

        #endregion Public 


        #region Private Methods

        // намалювати ігрове поле без ігрової частини
        private void DrawBackGroundRegion()
        {
            Pen pen = new Pen(Color.LightSkyBlue);
            Graphics g = _gBackGroundRegion;
            g.Clear(Color.White);
            // text parametrizies
            Font font = new Font("Segoe Print", 16);
            System.Drawing.Brush brush = new SolidBrush(Color.BlueViolet);
            StringFormat sf = new StringFormat(StringFormatFlags.LineLimit);

            // vertical and horizontal lines
            for (int i = 0; i < _field.Size + 1; i++)
            {
                // vertical line
                g.DrawLine(pen, new Point((i + 1) * _sizeOneCellPxls, 0), new Point((i + 1) * _sizeOneCellPxls, this.Height));

                // horizontal line
                g.DrawLine(pen, new Point(0, (i + 1) * _sizeOneCellPxls), new Point(this.Width, (i + 1) * _sizeOneCellPxls));

                if (i != _field.Size)
                {
                    // vertical (numbers)
                    g.DrawString((i + 1).ToString(), font, brush, new PointF(0, (i + 2) * _sizeOneCellPxls), sf);

                    // horizontal (literals)
                    g.DrawString(literals[i].ToString(), font, brush,
                        new PointF((i + 1) * _sizeOneCellPxls, _sizeOneCellPxls), sf);
                }
            }
            // horizontal line (end)
            g.DrawLine(pen, new Point(0, Height - _sizeOneCellPxls), new Point(this.Width, Height - _sizeOneCellPxls));

            // write info about persons field name
            brush = new SolidBrush(Color.Black);
            g.DrawString("Region of " + _playerName, font, brush, new PointF(_sizeOneCellPxls * 2, 0), sf);

            pen = new Pen(Color.BlueViolet, 3);

            // vertical lines board of field
            g.DrawLine(pen, new Point(_sizeOneCellPxls, _sizeOneCellPxls * 2),
                new Point(_sizeOneCellPxls, this.Height - _sizeOneCellPxls));
            g.DrawLine(pen, new Point(this.Width - _sizeOneCellPxls, _sizeOneCellPxls * 2),
                new Point(this.Width - _sizeOneCellPxls, this.Height - _sizeOneCellPxls));

            // horizontal lines boadrd of field
            g.DrawLine(pen, new Point(_sizeOneCellPxls, _sizeOneCellPxls * 2),
                new Point(this.Width - _sizeOneCellPxls, _sizeOneCellPxls * 2));
            g.DrawLine(pen, new Point(_sizeOneCellPxls, this.Height - _sizeOneCellPxls),
                new Point(this.Width - _sizeOneCellPxls, this.Height - _sizeOneCellPxls));

            //_gBackGroundRegion.
        }

        // намалювати ігрову частину
        private void DrawPlayRegionLines()
        {
            Pen pen = new Pen(Color.LightSkyBlue);

            for (byte i = 1; i < _field.Size; i++)
            {
                // horizontal lines
                _gPlayRegion.DrawLine(pen, new Point(0, i * _sizeOneCellPxls - 2),
                    new Point(pb_PlayRegion.Width - 1, i * _sizeOneCellPxls - 2));

                // vertical lines
                _gPlayRegion.DrawLine(pen, new Point(i * _sizeOneCellPxls - 2, 0),
                    new Point(i * _sizeOneCellPxls - 2, pb_PlayRegion.Height - 1));
            }
        }

        // намалювати ігрові обєкти на полі
        private void DrawGameElements()
        {
            DrawCell drawCell = new DrawCell();

            for (byte y = 0; y < _field.Size; y++)
            {
                for (byte x = 0; x < _field.Size; x++)
                {
                    drawCell.Draw(_field[x, y], _gPlayRegion, new Point(y, x), _sizeOneCellPxls, 3);
                }
            }
        }

        // очистити ігрову частину поля
        private void ClearPlayRegionInBitMap()
        {
            if (Enabled)
            {
                _gPlayRegion.Clear(Color.White);
            }
            else
            {
                _gPlayRegion.Clear(Color.Gainsboro);
            }
        }

        // оновити ігрову частину поля
        private void RefreshGameObjectsInBitMap()
        {
            ClearPlayRegionInBitMap();

            DrawPlayRegionLines();

            DrawGameElements();
        }

        // дати координату відносно ігрової частини поля
        private Point PointFieldForUser(Point point)
        {
            return new Point(point.X / _sizeOneCellPxls, point.Y / _sizeOneCellPxls);
        }


        #region LightCell

        private void LightCellForShipSet(Point point)
        {
            Position pos = new Position((byte)point.Y, (byte)point.X);

            if (positions[0] == pos)
            {
                RefreshGameObjectsInBitMap();
                DrawRectangleForPlayRegion(_currentPen, _gPlayRegion, GetPositionForPlayRegion(point, _sizeOneCellPxls, 3), _sizeOneCellPxls);
                pb_PlayRegion.Refresh();
            }

            Func<byte, bool> IsAvailableShip = (byte couStor) =>
            {
                if (!AvailableShipsDictionary.ContainsKey(couStor) ||
                    (AvailableShipsDictionary.ContainsKey(couStor) & AvailableShipsDictionary[couStor] == 0))
                {
                    return false;
                }

                return true;
            };

            Action<byte> SetColor = (byte couStor) =>
            {
                if (!IsAvailableShip(couStor))
                {
                    _currentPen = new Pen(Color.Red, 3);
                }
                else
                {
                    _currentPen = new Pen(Color.LightGreen, 3);
                }
            };

            Action<Point[]> RefreshField = (Point[] array) =>
            {
                RefreshGameObjectsInBitMap();

                    for (byte i = 0; i < array.Length; i++)
                    {
                        DrawRectangleForPlayRegion(_currentPen, _gPlayRegion, array[i], _sizeOneCellPxls);
                    }

                    pb_PlayRegion.Refresh();
            };

            Point[] arr = null;
            byte countStorey = 0;

            if (Ractangle.IsCorrectColumn(positions[0], pos))
            {
                countStorey = (byte)(Math.Abs(pos.Line - positions[0].Line) + 1);

                SetColor(countStorey);

                arr = new Point[countStorey];

                byte begin = (pos.Line < positions[0].Line) ? pos.Line : positions[0].Line;

                for (byte i = 0; i < countStorey; i++)
                {
                    arr[i] = GetPositionForPlayRegion(new Point(pos.Column, begin + i), _sizeOneCellPxls, 3);
                }

                RefreshField(arr);
            }
            else if (Ractangle.IsCorrectLine(positions[0], pos))
            {
                countStorey = (byte)(Math.Abs(pos.Column - positions[0].Column) + 1);

                SetColor(countStorey);

                arr = new Point[countStorey];

                byte begin = (pos.Column < positions[0].Column) ? pos.Column : positions[0].Column;

                for (byte i = 0; i < countStorey; i++)
                {
                    arr[i] = GetPositionForPlayRegion(new Point(begin + i, pos.Line), _sizeOneCellPxls, 3);
                }

                RefreshField(arr);
            }
        }

        private void LightCellForShot(Point point)
        {
            RefreshField();

            Position pos = new Position((byte)point.Y, (byte)point.X);
            Position[] positions = CurrentGun.Shot(pos, _field.Size);

            _currentPen = new Pen(Color.Brown, 3);

            for (byte i = 0; i < positions.Length; i++)
            {
                Point p = GetPositionForPlayRegion(new Point(positions[i].Column, positions[i].Line), _sizeOneCellPxls, 3);
                DrawRectangleForPlayRegion(_currentPen, _gPlayRegion, p, _sizeOneCellPxls);
            }

            pb_PlayRegion.Refresh();
        }

        private void LightCellCursor(Point point)
        {
            RefreshField();

            _currentPen = _defaultPen;

            Point pos = GetPositionForPlayRegion(point, _sizeOneCellPxls, 3);
            DrawRectangleForPlayRegion(_currentPen, _gPlayRegion, pos, _sizeOneCellPxls);

            pb_PlayRegion.Refresh();
        }

        private void LightCellNone(Point point)
        {

        }

        private void LightCellForProtect(Point point)
        {
            RefreshField();

            _currentPen = new Pen(Color.Blue, 3);

            Position pos = new Position((byte)point.Y, (byte)point.X);
            Pvo pvo = new Pvo(0, pos, _field.Size);
            Position[] positions = pvo.GetProtectedPositions();

            DrawRectangleForPlayRegion(_currentPen, _gPlayRegion, GetPositionForPlayRegion(new Point(pos.Column, pos.Line), _sizeOneCellPxls, 3), _sizeOneCellPxls);

            for (byte i = 0; i < positions.Length; i++)
            {
                Point p = GetPositionForPlayRegion(new Point(positions[i].Column, positions[i].Line), _sizeOneCellPxls, 3);
                _gPlayRegion.FillRectangle(Brushes.Aquamarine, p.X + _sizeOneCellPxls / 8, p.Y + _sizeOneCellPxls / 8, _sizeOneCellPxls - (2 * _sizeOneCellPxls / 8) + 1, _sizeOneCellPxls - (2 * _sizeOneCellPxls / 8) + 1);
            }

            pb_PlayRegion.Refresh();
        }

        #endregion LightCell


        #region OnEvents

        private bool _leftMouseButtonPressed = false;

        // коли курсор рухається по ігоровому полі
        private Point _lastPointCursor = new Point(-1, -1);

        private void pb_PlayRegion_MouseMove(object sender, MouseEventArgs e)
        {
            Point currentCursorPointOnPlayField = PointFieldForUser(e.Location);

            if (_lastPointCursor != currentCursorPointOnPlayField)
            {
                _currentLightCell(currentCursorPointOnPlayField);
            }

            _lastPointCursor = currentCursorPointOnPlayField;        
        }

        private void pb_PlayRegion_MouseLeave(object sender, EventArgs e)
        {
            RefreshGameObjectsInBitMap();
            pb_PlayRegion.Refresh();
        }

        private void pb_PlayRegion_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left & !_leftMouseButtonPressed & AvailableShipsDictionary != null)
            {
                _leftMouseButtonPressed = true;

                if (GameProccesStep == StepGameProcces.SettingShip)
                {
                    StatusReadyToRead = false;

                    Point currentCursorPointOnPlayField = PointFieldForUser(e.Location);
                    positions[0] = new Position((byte) currentCursorPointOnPlayField.Y,
                        (byte) currentCursorPointOnPlayField.X);

                    _currentLightCell = LightCellForShipSet;

                    _currentLightCell(currentCursorPointOnPlayField);
                }
            }

            if (e.Button == MouseButtons.Right & _leftMouseButtonPressed)
            {
                _leftMouseButtonPressed = false;

                _currentLightCell = LightCellCursor;

                Point currentCursorPointOnPlayField = PointFieldForUser(e.Location);

                _currentLightCell(currentCursorPointOnPlayField);
            }
        }

        private void pb_PlayRegion_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left & _leftMouseButtonPressed)
            {
                _leftMouseButtonPressed = false;

                if (GameProccesStep == StepGameProcces.SettingShip)
                {
                    Point currentCursorPointOnPlayField = PointFieldForUser(e.Location);
                    positions[1] = new Position((byte) currentCursorPointOnPlayField.Y,
                        (byte) currentCursorPointOnPlayField.X);

                    _currentPen = _defaultPen;

                    StatusReadyToRead = true;

                    _currentLightCell = LightCellCursor;

                    RefreshField();
                    pb_PlayRegion.Refresh();
                }

                Point currentCursorPoint = PointFieldForUser(e.Location);
                LightCellCursor(currentCursorPoint);
            }
        }

        private void pb_PlayRegion_MouseClick(object sender, MouseEventArgs e)
        {
            if (GameProccesStep == StepGameProcces.SettingProtect || GameProccesStep == StepGameProcces.Play)
            {
                Point currentCursorPointOnPlayField = PointFieldForUser(e.Location);
                positions[0] = new Position((byte) currentCursorPointOnPlayField.Y,
                    (byte) currentCursorPointOnPlayField.X);

                StatusReadyToRead = true;
            }
        }

        #endregion Onevents

        #endregion Private Methods


        #region Public Methods

        public void InitNewField(string playerName, GameEngine.Fields.BaseField field, byte sizeOneCellPxls = 32)
        {
            _field = field;
            _sizeOneCellPxls = sizeOneCellPxls;
            _playerName = playerName;

            pb_PlayRegion.Location = new Point(_sizeOneCellPxls + 2, 2*_sizeOneCellPxls + 2);
            pb_PlayRegion.Width = _sizeOneCellPxls*field.Size - 3;
            pb_PlayRegion.Height = pb_PlayRegion.Width;

            this.Width = (field.Size + 2)*_sizeOneCellPxls;
            this.Height = this.Width + _sizeOneCellPxls;

            // задній фон
            pb_BackGroundRegion.Width = Width;
            pb_BackGroundRegion.Height = Height;
            _btBackgroundRegion = new Bitmap(pb_BackGroundRegion.Width, pb_BackGroundRegion.Height);
            _gBackGroundRegion = Graphics.FromImage(_btBackgroundRegion);

            // привязатри до фону
            pb_BackGroundRegion.Image = _btBackgroundRegion;

            // для буферизації графіки в PlayRegion
            _btBitmapPlayRegion = new Bitmap(pb_PlayRegion.Width, pb_PlayRegion.Height);
            _gPlayRegion = Graphics.FromImage(_btBitmapPlayRegion);

            // привязатри до фону
            pb_PlayRegion.Image = _btBitmapPlayRegion;

            _currentPen = _defaultPen;

            ClearPlayRegionInBitMap();

            DrawBackGroundRegion();

            RefreshGameObjectsInBitMap();

            _currentLightCell = LightCellCursor;
        }

        public static Point GetPositionForPlayRegion(Point topLeft, byte sizeOneCell, byte borderWidth)
        {
            return new Point(topLeft.X * sizeOneCell - borderWidth + 1, topLeft.Y * sizeOneCell - borderWidth + 1);
        }

        public static void DrawRectangleForPlayRegion(Pen pen, Graphics g, Point topLeft, byte sizeOneCell)
        {
            g.DrawRectangle(pen, topLeft.X, topLeft.Y, sizeOneCell, sizeOneCell);
        }

        public Position[] GetPositions()
        {
            StatusReadyToRead = false;
            return positions;
        }
       
        public void RefreshField()
        {
            RefreshGameObjectsInBitMap();
            pb_PlayRegion.Refresh();
        }

        public void ResetStatusReadyToRead()
        {
            StatusReadyToRead = false;
        }

        public void ShipSettedSuccessfuly(byte countStorey)
        {
            byte count = AvailableShipsDictionary[countStorey];

            if (count == 0)
            {
                return;
            }

            AvailableShipsDictionary.Remove(countStorey);

            AvailableShipsDictionary.Add(countStorey, --count);
        }

        public void OffCursorOnField()
        {
            _currentLightCell = LightCellNone;
        }

        public void OnCursorOnField()
        {
            _currentLightCell = LightCellCursor;
        }

        #endregion Public Methods
    }
}
