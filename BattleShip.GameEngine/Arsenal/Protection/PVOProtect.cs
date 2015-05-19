using System;
using BattleShip.GameEngine.Arsenal.Gun.Destroyable;
using BattleShip.GameEngine.GameEvent;
using BattleShip.GameEngine.GameEventArgs;
using BattleShip.GameEngine.Location;


namespace BattleShip.GameEngine.Arsenal.Protection
{
    public class Pvo : ProtectBase
    {
        #region Constructor

        public Pvo(byte id, Position position, byte size)
            : base(id, position)
        {
            protectionList.Add(typeof (PlaneDestroy));

            // встановлення координат позицій, які будуть захищені
            currentProtectedPositions = new Position[size];
            for (byte i = 0; i < size; i++)
            {
                currentProtectedPositions[i] = new Position(position.Line, i);
            }
        }

        #endregion Constructors


        #region Events

        public override event Action<GameEvenArgs> DeadHandler;

        public override event Action<ProtectEventArgs> ProtectedHandler; 

        #endregion


        #region Public methods

        public override Position[] GetProtectedPositions()
        {
            var pos = new Position[base.currentProtectedPositions.Length];
            for (byte i = 0; i < pos.Length; i++)
            {
                pos[i] = new Position(base.currentProtectedPositions[i].Line, base.currentProtectedPositions[i].Column);
            }

            return pos;
        }

        #endregion Public methods


        #region OnEvents

        public override void KillMe(GameEvenArgs e)
        {
            if (ProtectedHandler != null)
            {
                ProtectedHandler(new ProtectEventArgs(this.GetType()));
            }

            if (DeadHandler != null)
            {
                DeadHandler(e);
            }
        }

        #endregion OnEvents
    }
}